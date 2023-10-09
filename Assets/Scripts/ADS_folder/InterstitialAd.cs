using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnityId = "Interstitial_Android";
    [SerializeField] private string _iosAdUnityId = "Interstitial_iOS";
    private string _adUnityId;
    public static InterstitialAd S;
    public void Awake()
    {
        S = this; 
        _adUnityId=(Application.platform==RuntimePlatform.IPhonePlayer)
            ? _iosAdUnityId 
            : _androidAdUnityId;
        LoadAd();
    }
    public void LoadAd()
    {
        Debug.Log("Loading Ad:"+_adUnityId);
        Advertisement.Load(_adUnityId, this);
    }
    public void ShowAd()
    {
        Debug.Log("Showing Ad:" + _adUnityId);
        Advertisement.Show(_adUnityId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"ERROR ad :{ _adUnityId} - { error.ToString()} - { message}");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"ERROR ad :{_adUnityId} - {error.ToString()} - {message}");
    }
    public void OnUnityAdsShowStart(string placementId)
    {
    }
    public void OnUnityAdsShowClick(string placementId)
    {
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }
}
