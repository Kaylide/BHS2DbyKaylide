using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
//using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AdmodController : MonoBehaviour
{

    public string ANDROID_BANNER_ID = "";
    public string ANDROID_INTERTITIAL_ID = "";
    public string ANDROID_REWARD_ID = "";

    public string IOS_BANNER_ID = "";
    public string IOS_INTERTITIAL_ID = "";
    public string IOS_REWARD_ID = "";

    public bool PositionTop = false;
    public bool isTesting = false;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    public RewardBasedVideoAd rewardBasedVideo;
    public static int numberReward;


    private static AdmodController _instance;

    public static AdmodController Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        //RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideo();
    }

    //void On

    public void hideBanner()
    {
        bannerView.Hide();
    }
    public void showBanner()
    {
        if (bannerView != null)
        {
            bannerView.Show();

        }
    }


    IEnumerator ShowPanelSetting()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.panelSetting.SetActive(true);
        Time.timeScale = 0.0f;

    }

    public void showInterstitial()
    {
        Scene _sceneCurrent = SceneManager.GetActiveScene();
        if (interstitial != null)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                if (_sceneCurrent.name == "Game")
                {
                    StartCoroutine(ShowPanelSetting());
                }

            }
            else
                RequestInterstitial();
        }
        else
        {
            RequestInterstitial();
            print("Interstitial is not ready yet.");
        }

    }
    void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "ca-app-pub-5849952604847046/7645634190";
#elif UNITY_ANDROID
			string adUnitId = ANDROID_BANNER_ID;
#elif UNITY_IOS
			string adUnitId = IOS_BANNER_ID;
#else
			string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        if (PositionTop)
        {
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        }
        else
        {
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        }

        bannerView.LoadAd(createAdRequest());
    }

    public void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
			string adUnitId = ANDROID_INTERTITIAL_ID;
#elif UNITY_IOS
			string adUnitId = IOS_INTERTITIAL_ID;
#else
			string adUnitId = "unexpected_platform";
#endif

        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdClosed += Interstitial_OnAdClosed;
        interstitial.LoadAd(createAdRequest());


    }

    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        RequestInterstitial();
    }

    // Returns an ad request with custom ad targeting.
    public AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .Build();

    }

    public void RequestRewardBasedVideo()
    {
        //int ran = Random.Range(0, 3);
        //if (ran != 1)
        //{
        //    return;
        //}

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = ANDROID_REWARD_ID;
#elif UNITY_IOS
        string adUnitId = IOS_REWARD_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardBasedVideo = RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void RewardBasedVideo_OnAdRewarded(object sender, Reward e)
    {
        if (numberReward == 0)
        {
            GameObject.FindGameObjectWithTag("okRewardGiftTime").GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (numberReward == 1)
        {
            GameObject.FindGameObjectWithTag("btnReward").SetActive(false);
            StartCoroutine(SendUpdateUser());
        }
        else if (numberReward == 2)
        {
            GameObject.FindGameObjectWithTag("okReward").GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (numberReward == 3)
        {
            Loadding.relive--;
            GameController.Instance.panelRelive.SetActive(false);
            SceneManager.LoadScene("Game");
        }
        RequestRewardBasedVideo();
    }

    public void showRewardAdsVideo()
    {
        if (rewardBasedVideo != null && rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    public bool isHaveReward()
    {
        return (rewardBasedVideo != null && rewardBasedVideo.IsLoaded());
    }

    IEnumerator SendUpdateUser()
    {
        WWWForm form = new WWWForm();
        if (PlayerPrefs.GetInt("loginFb", 0) == 0)
        {
            form.AddField("userName", PlayerPrefs.GetString("userID"));
        }
        else
        {
            form.AddField("userName", PlayerPrefs.GetString("userNameFB"));
        }

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            FullTime.instance.text_MoneyTotal.text = (((GameController.goals * 50) + FullTime.instance.amountMoneyMatch) * 2).ToString();
            form.AddField("coin", (GameController.goals * 50) + FullTime.instance.amountMoneyMatch);
        }
        else if (Menu.mode == (int)Menu.MODE.EXHIBITION)
        {
            form.AddField("coin", FullTime.instance.rewardExhibition);
            FullTime.instance.txt_rewardExhibition.text = (FullTime.instance.rewardExhibition * 2).ToString();
        }


        UnityWebRequest www = UnityWebRequest.Post("http://35.198.197.119:8080/soccer/update_user", form);
        yield return www.Send();
        if (www.responseCode == 200)
        {
            GetInfoAndUpdate.isConnect = true;
            Debug.Log("22222222222222222222222222");
            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                int _money = PlayerPrefs.GetInt("money");
                _money += (GameController.goals * 50) + FullTime.instance.amountMoneyMatch;
                PlayerPrefs.SetInt("money", _money);
            }
            else if (Menu.mode == (int)Menu.MODE.EXHIBITION)
            {
                int _money = PlayerPrefs.GetInt("money");
                _money += FullTime.instance.rewardExhibition;
                PlayerPrefs.SetInt("money", _money);
            }
        }
        else
        {
            GetInfoAndUpdate.isConnect = false;
            Debug.Log("11111111111111111111111");
        }
    }
}
