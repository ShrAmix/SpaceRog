using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class ADSmanager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMod;
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;

    private string gameId;
    void Awake()
    {
        InitializeAds();
    }
    public void InitializeAds()
    {
        gameId=(Application.platform==RuntimePlatform.IPhonePlayer)
            ? iosGameId 
            : androidGameId;
        Advertisement.Initialize(gameId, testMod,this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error,string massage)
    {
        Debug.Log("OnInitializationFailed");
    }


}
