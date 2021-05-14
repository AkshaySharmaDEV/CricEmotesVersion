using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4120671";
    private string bannerID = "banner";
    private string interstitialID = "interstitial";
    private string rewardedVideoID = "rewardedVideo";
    public bool TestMode;
    
    public Button watchRewardAd;

    public GameObject Watermark;
    

    void Start()
    {
        Advertisement.Initialize(gameID, TestMode);
        
        watchRewardAd.interactable = Advertisement.IsReady(rewardedVideoID);
        Advertisement.AddListener(this);
    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedVideoID);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementID)
    {
        if (placementID == rewardedVideoID)
        {
            watchRewardAd.interactable = true;
        }

        // if (placementID == interstitialID)
        // {
        //     showInterstitial.interactable = true;
        // }

        if (placementID == bannerID)
        {
            Advertisement.Banner.SetPosition (BannerPosition.TOP_CENTER);

            Advertisement.Banner.Show(bannerID);
        }
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (placementID == rewardedVideoID)
        {
            if (showResult == ShowResult.Finished)
            {
                GetReward();
            }
            else if (showResult == ShowResult.Skipped)
            {
                //Do nothing
            }
            else if (showResult == ShowResult.Failed)
            {
                //tell player ads failed
            }
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        //Show or log the error here
    }

    public void OnUnityAdsDidStart(string placementID)
    {
        //Do this if ads starts
    }

    public void GetReward()
    {
        Destroy(Watermark);

    }

    public void StopBanner()
    {
        Advertisement.Banner.Hide();
    }
}