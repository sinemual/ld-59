using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class AudioService
{
    private AudioData _audioData;
    private SharedData _data;
    private GameObject _audioSourcesContainer;
    private readonly List<Sound> _allSounds;

    private float minCutoff = 0f;
    private float maxCutoff = 22000f;
    
    public AudioService(SharedData data)
    {
        _data = data;
        _audioData = data.AudioData;
        _audioSourcesContainer = Object.Instantiate(_data.StaticData.PrefabData.EmptyPrefab);
        _audioSourcesContainer.gameObject.name = "AudioContainer";
        
        _allSounds = new List<Sound>();
        _allSounds.AddRange(_audioData.sounds);
        _allSounds.AddRange(_audioData.soundtrackSounds);
        _allSounds.AddRange(_audioData.uiSounds);
        _allSounds.AddRange(_audioData.shootingLevelSounds);
        _allSounds.AddRange(_audioData.battleSounds);
        Init();
    }

    private void Init()
    {
        foreach (var s in _allSounds)
        {
            s.source = _audioSourcesContainer.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    public void Play(string soundName)
    {
        var sound = _allSounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        sound.source.volume = sound.volume * (1f + Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
        sound.source.pitch = sound.pitch * (1f + Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

        sound.source.Play();
    }

    public void Stop(string soundName)
    {
        var sound = _allSounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        sound.source.Stop();
    }
    
    public void StopAllSoundtracks()
    {
        foreach (var sound in _audioData.soundtrackSounds)
            if (sound.source != null)
                sound.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (var sound in _allSounds)
            if (sound.source != null)
                sound.source.Stop();
    }

    public void ToggleMusic(bool on)
    {
        _audioData.musicMixer.SetFloat("AudioVolume", on ? -5f : -80f);
    }
    
    public void MusicSetLowpassCutoff(float value)
    {
        value = Mathf.Clamp01(value);
        float cutoff = Mathf.Lerp(minCutoff, maxCutoff, value);
        _audioData.musicMixer.SetFloat("LowpassCutoff", cutoff * _data.BalanceData.CutOffCurve.Evaluate(value));
    }
    
    public void EffectSetLowpassCutoff(float value)
    {
        value = Mathf.Clamp01(value);
        float cutoff = Mathf.Lerp(minCutoff, maxCutoff, value);
        _audioData.effectsMixer.SetFloat("LowpassCutoff", cutoff * _data.BalanceData.CutOffCurve.Evaluate(value));
    }

    public void ToggleEffects(bool on)
    {
        _audioData.effectsMixer.SetFloat("AudioVolume", on ? -5f : -80f);
    }

    public void ChangeMusicVolume(float value)
    {
        if (value < 0.1f)
            value = -80;
        else
            value = Mathf.Log10(value) * 20;

        _audioData.musicMixer.SetFloat("AudioVolume", value);
    }

    public void ChangeEffectsVolume(float value)
    {
        if (value < 0.1f)
            value = -80;
        else
            value = Mathf.Log10(value) * 20;
        
        _audioData.effectsMixer.SetFloat("AudioVolume", value);
    }
}