using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using SimpleJSON;
using Unity.VisualScripting;


public class AdsController : MonoBehaviour
{
    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private float deltaTime;
    private bool isShowingAppOpenAd;
    public bool Isstart;



    [Header("--------------------->Android<---------------------------")]

    public string DeviceID, BanneradUnitId, RewardadUnitId, InterstitialadUnitId,OpenUnitId;


    [Header("---------------------><---------------------------")]
    public bool showFpsMeter = true;

    public static AdsController instance;

    private void Awake()
    {
        
        instance = this;
        Isstart = true;
        //OnSendRegistion();

        DontDestroyOnLoad(this);    
        //StartCoroutine(OnSendRegistion());
        //PlayerPrefs.DeleteAll();
        AdsSetup();


    }


    //public IEnumerator OnSendRegistion()
    //{


    //    using (var www = UnityWebRequest.Get("https://merge-puzzle-game-2048-default-rtdb.firebaseio.com/puzzlebox.json"))
    //    {
    //        yield return www.SendWebRequest();
    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.Log("Error::::::::::" + www.error);
    //            Isstart = true;
    //            AdsSetup();
    //        }
    //        else
    //        {
    //            JSONNode json = JSONNode.Parse(www.downloadHandler.text);
    //            print(www.downloadHandler.text);
    //            string AppId = json["AppId"];
    //            string RewardAdId = json["RewardAdId"];
    //            string BannerAdId = json["BannerAdId"];
    //            string InstialAdId = json["InstialAdId"];
    //            Isstart = json["IsStart"];
    //            print("Finished Uploading Screenshot" + AppId);
    //            print("Finished Uploading Screenshot" + RewardAdId);
    //            print("Finished Uploading Screenshot" + BannerAdId);
    //            print("Finished Uploading Screenshot" + InstialAdId);
    //            print("Finished Uploading Screenshot" + Isstart);

    //            if (Isstart)
    //            {


    //                BanneradUnitId = BannerAdId;
    //                RewardadUnitId = RewardAdId;
    //                InterstitialadUnitId = InstialAdId;




    //                MobileAds.Initialize(InitializationStatus =>
    //                {
    //                    print("Ads Initilized !!");
    //                });
    //                AdsSetup();
    //            }



    //        }
    //    }
    //}



    #region UNITY MONOBEHAVIOR METHODS

    public void AdsSetup()
    {

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        //deviceIds.Add(AdID+"75EF8D155528C04DACBBA6F36F433035");
        deviceIds.Add(DeviceID);
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);

        // Listen to application foreground / background events.


    }
    public void Start()
    {

    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");

        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // the main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            RequestAndLoadAppOpenAd();
            RequestAndLoadRewardedAd();
            RequestAndLoadInterstitialAd();
            LoadAd();
        });
    }

    private void Update()
    {
        if (showFpsMeter)
        {

            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;

        }

    }

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion


    public void CreateBannerView()
    {        // These ad units are configured to always serve test ads.

        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.



        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(BanneradUnitId, AdSize.Banner, AdPosition.Bottom);

    }
    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (bannerView == null && Isstart)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }






    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        PrintStatus("Requesting Interstitial ad.");


        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(InterstitialadUnitId);

        // Add Event Handlers
        interstitialAd.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Interstitial ad loaded.");

        };
        interstitialAd.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Interstitial ad failed to load with error: " + args.LoadAdError.GetMessage());

        };
        interstitialAd.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Interstitial ad opening.");

        };
        interstitialAd.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Interstitial ad closed.");
            RequestAndLoadInterstitialAd();

        };
        interstitialAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("Interstitial ad recorded an impression.");
        };
        interstitialAd.OnAdFailedToShow += (sender, args) =>
        {
            PrintStatus("Interstitial ad failed to show.");
        };
        interstitialAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Interstitial ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Load an interstitial ad
        interstitialAd.LoadAd(CreateAdRequest());
    }

    public void OnShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.IsLoaded() && Isstart)
        {
            interstitialAd.Show();
        }
        else
        {
            PrintStatus("Interstitial ad is not ready yet.");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        PrintStatus("Requesting Rewarded ad.");

        // create new rewarded ad instance
        rewardedAd = new RewardedAd(RewardadUnitId);

        // Add Event Handlers
        rewardedAd.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Reward ad loaded.");
          

        };
        rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Reward ad failed to load.");

        };
        rewardedAd.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Reward ad opening.");
            Time.timeScale = 0;

        };
        rewardedAd.OnAdFailedToShow += (sender, args) =>
        {
            PrintStatus("Reward ad failed to show with error: " + args.AdError.GetMessage());

        };
        rewardedAd.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Reward ad closed.");
            RequestAndLoadRewardedAd();
            Time.timeScale = 1;
            //GM.GetInstance().AddDiamond(240, true);
            //if(Shop.Instance.gameObject.active)
            //{
            //Shop.Instance.Ongetcoin();

            //}
            //else if(Confirm.Instance.gameObject.active)
            //{
            //    Confirm.Instance.Ongetcoin();
            //}
           

        };
        rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            PrintStatus("User earned Reward ad reward: " + args.Amount);

        };
        rewardedAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("Reward ad recorded an impression.");
        };
        rewardedAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Rewarded ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && Isstart)
        {
            rewardedAd.Show();
        }
        else
        {
            Time.timeScale = 1;
            RequestAndLoadRewardedAd();
            PrintStatus("Rewarded ad is not ready yet.");
        }
    }



    public void OpenAdInspector()
    {
        PrintStatus("Open ad Inspector.");
          Time.timeScale = 0;
        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                PrintStatus("ad Inspector failed to open with error: " + error);
            }
            else
            {
                PrintStatus("Ad Inspector opened successfully.");
               
            }
        });
    }

    #endregion

    #region Utility

    ///<summary>
    /// Log the message and update the status text on the main thread.
    ///<summary>
    private void PrintStatus(string message)
    {
        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

        });
    }

    #endregion

    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (!isShowingAppOpenAd
                    && appOpenAd != null
                    && DateTime.Now < appOpenExpireTime);
        }
    }



    public void RequestAndLoadAppOpenAd()
    {
        PrintStatus("Requesting App Open ad.");

        AppOpenAd.LoadAd(OpenUnitId,
                         ScreenOrientation.Portrait,
                         CreateAdRequest(),
                         OnAppOpenAdLoad);
    }

    private void OnAppOpenAdLoad(AppOpenAd ad, AdFailedToLoadEventArgs error)
    {
        if (error != null)
        {
            PrintStatus("App Open ad failed to load with error: " + error);
            OnAdFailedToLoad(ad,error);
            return;
        }

        PrintStatus("App Open ad loaded. Please background the app and return.");
        this.appOpenAd = ad;
        this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;
        if(Isstart)
        {
            ShowAppOpenAd();

        }
        else
        {
            //MainScene.Instance.Offsplsh();
        }
    }

    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAdAvailable)
        {
            return;
        }

        // Register for ad events.
        this.appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad dismissed.");
            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad failed to present with error: " + args.AdError.GetMessage());

            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad opened.");
            //MainScene.Instance.Offsplsh();
        };
        this.appOpenAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("App Open ad recorded an impression.");
           
        };
        this.appOpenAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "App Open ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        isShowingAppOpenAd = true;
        appOpenAd.Show();
    }

    public void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        LoadAdError loadAdError = args.LoadAdError;

        // Gets the domain from which the error came.
        string domain = loadAdError.GetDomain();

        // Gets the error code. See
        // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
        // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
        // for a list of possible codes.
        int code = loadAdError.GetCode();

        // Gets an error message.
        // For example "Account not approved yet". See
        // https://support.google.com/admob/answer/9905175 for explanations of
        // common errors.
        string message = loadAdError.GetMessage();

        // Gets the cause of the error, if available.
        AdError underlyingError = loadAdError.GetCause();

        // All of this information is available via the error's toString() method.
        Debug.Log("Load error string: " + loadAdError.ToString());

        // Get response information, which may include results of mediation requests.
        ResponseInfo responseInfo = loadAdError.GetResponseInfo();
        Debug.Log("Response info: " + responseInfo.ToString());
    }



}
