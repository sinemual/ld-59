using UnityEngine;

public class InternetAccessStateService : MonoBehaviour
{
    public bool isDebugInternet;

    public bool isHaveInternet;
    private TimeManagerService _timeManagerService;
    private bool isHaveInternetSavedState;

    private void Update()
    {
        isHaveInternet = Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                         Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        if (isHaveInternetSavedState != !isHaveInternet)
        {
            if (isHaveInternet)
                InternetAppeared();
            else
                InternetDropped();
        }

        isHaveInternetSavedState = !isHaveInternet;
    }

    private void OnApplicationPause(bool _isPause)
    {
        if (!_isPause)
            isHaveInternet = Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                             Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
    }

    public bool IsHaveNetTimeAndInternet()
    {
        return  isHaveInternet;
    }

    public void InternetAppeared()
    {
        //InterstitialAdTimer.Instance.InterstitialAdShowed();
       // _timeManagerService.ManualGetNetTime();
    }

    public void InternetDropped()
    {
    }
}