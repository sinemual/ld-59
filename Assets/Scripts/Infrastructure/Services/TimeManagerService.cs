using System;
using System.Collections;
using System.Globalization;
using Client;
using Client.Data.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class TimeManagerService
{
    private SharedData _data;
    private ICoroutineRunner _coroutineRunner;

    private DateTimeOffset _netTime;
    public DateTimeOffset NetTime => _netTime;

    public bool IsNetTimeGot;
    public event Action SecondUpdate;

    public TimeManagerService(SharedData data, ICoroutineRunner coroutineRunner)
    {
        _data = data;
        _coroutineRunner = coroutineRunner;
        IsNetTimeGot = false;
        _data.RuntimeData.IsWeHaveInternetTime = false;
        _coroutineRunner.StartCoroutine(GetNetTime());
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(1.0f);
        SecondUpdate?.Invoke();
        if (_netTime.ToString() != "")
            _netTime = NetTime.AddSeconds(Time.deltaTime);

        SaveWithNetTime(ref _data.SaveData.IdleRewardTimeKey, TimeSpan.Zero);
        _coroutineRunner.StartCoroutine(UpdateTime());
    }

    private IEnumerator GetNetTime()
    {
        var myHttpWebRequest = UnityWebRequest.Get("https://www.google.com");
        yield return myHttpWebRequest.SendWebRequest();

        if (myHttpWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("NETWORK ERROR");
            //_netTime = null;
            yield break;
        }

        var netTimeString = myHttpWebRequest.GetResponseHeader("date");
        if (netTimeString != "")
            _netTime = DateTimeOffset.ParseExact(netTimeString,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
        IsNetTimeGot = true;
        _coroutineRunner.StartCoroutine(UpdateTime());

        //Debug.Log("Global UTC time: " + netTime);
    }

    public void SaveWithNetTime(ref string key, TimeSpan add) => key = ToStrCurrentTime(add);

    public void AddTimeToExistTimer(ref string key, TimeSpan add) => SaveWithSpecifiedValue(ref key, LoadDateTimeOffset(ref key).Add(add));

    public bool IsNetTimeMoreThanSavedValue(ref string key)
    {
        var saved = LoadDateTimeOffset(ref key);
        return (NetTime - saved).TotalSeconds >= 0.0f;
    }

    public TimeSpan GetTime(ref string key)
    {
        var diff = GetTimeDifference(ref key);
        return diff.TotalSeconds < 0f ? TimeSpan.Zero : diff;
    }

    public DateTimeOffset LoadDateTimeOffset(ref string key)
    {
        if (!string.IsNullOrEmpty(key))
            return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(key));
        return DateTimeOffset.Now;
    }

    #region Dont use - it's internal (Low level api)

    private TimeSpan GetTimeDifference(ref string key)
    {
        var savedTime = LoadDateTimeOffset(ref key);
        var currentTime = ToDTONetTimeAsUnixTime();

        //Debug.Log($"Saved: {savedTime}, Current: {currentTime}");

        var diff = currentTime - savedTime;

        if (diff.TotalSeconds < 0)
            return TimeSpan.Zero;

        return diff;
    }

    private void SaveWithSpecifiedValue(ref string key, DateTimeOffset dto)
    {
        key = dto.ToUnixTimeMilliseconds().ToString();
    }

    private string ToStrCurrentTime(TimeSpan add)
    {
        return NetTime.Add(add).ToUnixTimeMilliseconds().ToString();
    }

    private DateTimeOffset ToDTOCurrentTimeAsUnixTime()
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(
            long.Parse(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()));
    }

    private DateTimeOffset ToDTONetTimeAsUnixTime()
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(NetTime.ToUnixTimeMilliseconds().ToString()));
        //return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()));
    }

    #endregion
}