using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnityId = "Rewarded_Android";
    [SerializeField] private string _iosAdUnityId = "Rewarded_iOS";
    private string _adUnityId;
    public static RewardedAdsButton S;
    private bool _isPlaying = false;
    public void Awake()
    {
        S = this;
        _adUnityId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosAdUnityId : _androidAdUnityId;
    }
    public void Start()
    {
        LoadAd();
    }
    public void LoadAd()
    {
        Debug.Log("Loading Ad:" + _adUnityId);
        Advertisement.Load(_adUnityId,this);
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
        Debug.Log($"ERROR ad :{_adUnityId} - {error.ToString()} - {message}");
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
    public void OnUnityAdsShowComplete(string adUnityId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnityId.Equals(_adUnityId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(7))
        {
            int coin = PlayerPrefs.GetInt("Money");
            coin += PlayerPrefs.GetInt("CoinAds");
            if (PlayerPrefs.GetInt("CoinAds")!=15)
            {
                int o = PlayerPrefs.GetInt("CoinAds");
                o -= 5;
                PlayerPrefs.SetInt("CoinAds", o);
            }
            CoinForAds.instance.adsButton.text = "" + PlayerPrefs.GetInt("CoinAds");
            PlayerPrefs.SetInt("Money", coin);
            PlayerPrefs.SetInt("MaxMoney", coin);
            MenuScore.Instance.ScoreTXT(PlayerPrefs.GetInt("BestScore"), PlayerPrefs.GetInt("Money"));
        }
        if (adUnityId.Equals(_adUnityId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if (!_isPlaying)
            {
                Health.instance.AddHeart();
                GameManager.instance.loseWindow.gameObject.SetActive(false);
                PlayerFly.playerInstance.AdLeave();
                GameManager.instance.Continue();
                _isPlaying = true;
            }
            
        }
        if (adUnityId.Equals(_adUnityId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(5))
        {
            //GameManager.instance.MamyMoney();
            PlayerPrefs.SetInt("DayNow" + $"{0}", -1);
            PlayerPrefs.SetInt("DayNow" + $"{1}", -1);
            PlayerPrefs.SetInt("DayNow" + $"{2}", -1);
            GameManager.instance.LoadScene(5);
        }
        LoadAd();

    }
}
