using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Menu : MonoBehaviour
{
    public static Menu instance;
    public static int mode;
    public Text textMoney;
    public int disMatch, isMatchStage;
    public int[,] ValueTeam = new int[4, 8];
    public int[] ScoreTeam = new int[32];

    public int[,] ValueTeamPlayOff = new int[2, 8];
    public int[,] GoalsPlayOff = new int[2, 8];

    public int[,] GoalsFirtOff = new int[2, 4];
    public int[,] ValueTeamFirtOff = new int[2, 4];

    public int[,] ValueTeamQuarterFinals = new int[2, 2];
    public int[,] GoalsQuarterFinals = new int[2, 2];
    public Image img_Loadding;
    public GameObject panelLoadding, btnMenu, panelLoadScene, panelSetting;
    public static bool isLoadding;
    public Image soundImg, musicImg;
    public Sprite muteSoundSprite, muteMusicSprite;
    public Sprite soundSprite, musicSprite;
    public int rdMusic;
    public Image headPlayer, shoePlayer;
    public Text nameMyPlayer;

    public Sprite[] sp_BGMenu;
    public SpriteRenderer img_bg;

    public static string exitShop;
    public GameObject panelDisconnect;
    // Use this for initialization
    private void Awake()
    {

    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("getInfo", 0) > 0)
        {
            GetInfoAndUpdate.instance.GetInfoSever();

        }

        CheckMusicAndSound();
        exitShop = "Menu";
        int id = PlayerPrefs.GetInt("IDPlayer", 0);
        headPlayer.sprite = SelectTeam.instance.MyTeamHeadPlayer[id];
        nameMyPlayer.text = SelectTeam.instance.MyTeamHeadPlayer[id].name;
        int rd = UnityEngine.Random.Range(0, 3);
        shoePlayer.sprite = SelectTeam.instance.sp_shoe[rd];

        if (isLoadding == false)
        {
            GetInfoAndUpdate.isConnect = true;
            StartCoroutine(waitLoadScene());
            img_bg.sprite = sp_BGMenu[0];

        }
        else
        {
            panelLoadScene.SetActive(false);
            btnMenu.SetActive(true);
            panelLoadding.SetActive(false);
            img_bg.sprite = sp_BGMenu[1];
        }
        disMatch = PlayerPrefs.GetInt("disMatch", 0);
        isMatchStage = PlayerPrefs.GetInt("isStage1", 1);



        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (PlayerPrefs.GetInt("disMatch") == 1)
        {
            SetupDisMatchWC();
            disMatch = 0;
            PlayerPrefs.SetInt("disMatch", disMatch);
            if (isMatchStage >= 5)
            {
                PlayerPrefs.SetInt("WinOrLose", 1);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ValueTeam[i, j] = PlayerPrefs.GetInt("valueTeam" + "[" + i + "," + j + "]");

            }
        }
        for (int i = 0; i < ScoreTeam.Length; i++)
        {

            ScoreTeam[i] = PlayerPrefs.GetInt("scoreTeam" + "[" + i + "]", 0);

        }
        Debug.Log("wcPlayerwcPlayerwcPlayerwcPlayerwcPlayerwcPlayerAIIIIIIIIIII    " + PlayerPrefs.GetInt("wcAI"));

    }
    IEnumerator waitLoadScene()
    {
        string NameMyTeam = PlayerPrefs.GetString("NameMyTeam", "");
        img_bg.sprite = sp_BGMenu[0];
        yield return new WaitForSeconds(4.5f);
        panelLoadScene.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        img_bg.sprite = sp_BGMenu[1];

        btnMenu.SetActive(true);
        panelLoadding.SetActive(false);
        if (NameMyTeam.Equals("") || PlayerPrefs.GetInt("setCreatAcc", 0) == 0)
        {
            if (PlayerPrefs.GetInt("loginFb", 0) == 0)
            {
                EditTeam.instance.panelEditTeam.SetActive(true);
            }
        }
        else
        {
            EditTeam.instance.panelEditTeam.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        panelLoadScene.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        textMoney.text = PlayerPrefs.GetInt("money").ToString();

        if (GetInfoAndUpdate.isConnect == true)
        {
            
            panelDisconnect.SetActive(false);
        }
        else
        {
            panelDisconnect.SetActive(true);
            Debug.Log("afasdfasdfasdfsdfsadfasfasf");
        }

        if (isMatchStage <= 4 && isMatchStage > 1)
        {
            ListSortScore();
            if (isMatchStage == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (ValueTeam[i, j] == PlayerPrefs.GetInt("wcPlayer") && i > 1)
                        {
                            PlayerPrefs.SetInt("WinOrLose", 1);
                        }

                    }
                }
            }
        }
        if (isLoadding == false)
        {
            img_Loadding.fillAmount += 0.01f;
        }

        if (img_Loadding.fillAmount >= 1)
        {

            isLoadding = true;
        }

    }



    public void SetupDisMatchWC()
    {
        if (isMatchStage <= 4)
        {
            ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] += 3;
            PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);

        }
        else if (isMatchStage == 5)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ValueTeamPlayOff[i, j] = PlayerPrefs.GetInt("ValueTeamPlayOff" + "[" + i + "," + j + "]", 0);
                    GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcPlayer")
                       || ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[0, j] = 0;
                            GoalsPlayOff[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                        else if (ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[1, j] = 0;
                            GoalsPlayOff[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 6)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ValueTeamFirtOff[i, j] = PlayerPrefs.GetInt("ValueTeamFirtOff" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamFirtOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[0, j] = 0;
                            GoalsFirtOff[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                        else if (ValueTeamFirtOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[1, j] = 0;
                            GoalsFirtOff[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                    }
                }
            }
        }
        if (isMatchStage == 7)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    ValueTeamQuarterFinals[i, j] = PlayerPrefs.GetInt("ValueTeamQuarterFinals" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamQuarterFinals[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[0, j] = 0;
                            GoalsQuarterFinals[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                        else if (ValueTeamQuarterFinals[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[1, j] = 0;
                            GoalsQuarterFinals[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                    }
                }
            }
        }
    }

    public void ListSortScore()
    {

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = j + 1; k < 4; k++)
                {

                    if (ScoreTeam[ValueTeam[j, i] - 1] < ScoreTeam[ValueTeam[k, i] - 1])
                    {
                        int temp2 = ValueTeam[j, i];
                        ValueTeam[j, i] = ValueTeam[k, i];
                        ValueTeam[k, i] = temp2;
                    }
                }
            }

        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PlayerPrefs.SetInt("valueTeam" + "[" + i + "," + j + "]", ValueTeam[i, j]);
            }

        }
    }

    public void ButtonExhibition()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("SelectTeam");
        mode = (int)MODE.EXHIBITION;
    }

    public void ButtonRank()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Rank");
    }

    public void ButtonWorldCup()
    {
        SceneManager.LoadScene("WorldCup");
        mode = (int)MODE.WORLDCUP;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void ButtonMyteam()
    {
        SceneManager.LoadScene("MyTeam");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void ButtonExit()
    {
        panelSetting.SetActive(false);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void ButtonAddMoney()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void ButtonShop()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void ButtonSetting()
    {
        panelSetting.SetActive(true);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void ButtonSound()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if (soundImg.sprite == soundSprite)
        {

            soundImg.sprite = muteSoundSprite;
            PlayerPrefs.SetInt(GameConstants.SOUND, 0);


        }
        else if (soundImg.sprite == muteSoundSprite)
        {
            soundImg.sprite = soundSprite;
            PlayerPrefs.SetInt(GameConstants.SOUND, 1);

        }

    }

    public void ButtonMusic()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        if (musicImg.sprite == musicSprite)
        {
            SoundManager.Instance.musicBG.mute = true;
            musicImg.sprite = muteMusicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 0);
        }
        else if (musicImg.sprite == muteMusicSprite)
        {
            SoundManager.Instance.musicBG.mute = false;
            musicImg.sprite = musicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 1);
        }
    }

    public void ExitDisconnect()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelDisconnect.SetActive(false);
        Application.LoadLevel("Menu");
        GetInfoAndUpdate.instance.GetInfoSever();
    }
    private void CheckMusicAndSound()
    {
        SoundManager.Instance.matchLost.Stop();
        SoundManager.Instance.matchWon.Stop();
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            soundImg.sprite = soundSprite;


        }
        else
        {
            soundImg.sprite = muteSoundSprite;
            SoundManager.Instance.musicBG.mute = true;
        }
        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            musicImg.sprite = musicSprite;
            SoundManager.Instance.musicBG.mute = false;
        }
        else
        {
            musicImg.sprite = muteMusicSprite;
            SoundManager.Instance.musicBG.mute = true;
        }
    }

    public void ButtonMoreGame()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

#if UNITY_ANDROID
        Application.OpenURL("market://search?q=pub:MGH2+GAME");
#elif UNITY_IOS
          Application.OpenURL("https://itunes.apple.com/developer/mui-cao/id1054920595");
#endif
    }

    public void ButtonShareFB()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        Application.OpenURL("https://www.facebook.com/minigameshouse/");
    }

    public void Facebook()
    {

#if UNITY_EDITOR
        Application.OpenURL("https://www.facebook.com/kayl1de/");
#elif UNITY_IPHONE
         Debug.Log("Unity iPhone");
         
#else
         if(checkPackageAppIsPresent("com.facebook.katana")) {
        Application.OpenURL("fb://page/355069494984740"); //there is Facebook app installed so let's use it
         }
         else {
         Application.OpenURL("https://www.facebook.com/kayl1de/"); // no Facebook app - use built-in web browser
         }
         
#endif
    }

    public bool checkPackageAppIsPresent(string package)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        //take the list of all packages on the device
        AndroidJavaObject appList = packageManager.Call<AndroidJavaObject>("getInstalledPackages", 0);
        int num = appList.Call<int>("size");
        for (int i = 0; i < num; i++)
        {
            AndroidJavaObject appInfo = appList.Call<AndroidJavaObject>("get", i);
            string packageNew = appInfo.Get<string>("packageName");
            if (packageNew.CompareTo(package) == 0)
            {
                return true;
            }
        }
        return false;
    }



    public enum MODE
    {
        EXHIBITION, WORLDCUP
    };
}

