using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuyProductIDShop : MonoBehaviour
{
    public static BuyProductIDShop instance;
    public GameObject btnReward;
    public Text textmoney;
    public GameObject panelDisconnect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GameObject.FindGameObjectWithTag("okReward").GetComponent<RectTransform>().transform.localScale = new Vector3(0, 0, 0);


        AdmodController.numberReward = 2;
        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            SoundManager.Instance.musicBG.mute = false;
            SoundManager.Instance.matchLost.Stop();
            SoundManager.Instance.matchWon.Stop();
        }
    }
    public void Buy(GameObject pack)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        if (pack.name == "Pack1")
        {
            //Thêm 10000 coin
            GetInfoAndUpdate.instance.UpdateCoinSever(10000);
        }

        if (pack.name == "Pack2")
        {
            //Thêm 25000 coin
            GetInfoAndUpdate.instance.UpdateCoinSever(25000);
        }

        if (pack.name == "Pack3")
        {
            //Thêm 40000 coin
            GetInfoAndUpdate.instance.UpdateCoinSever(40000);
        }

        if (pack.name == "Pack4")
        {
            //Thêm 60000 coin
            GetInfoAndUpdate.instance.UpdateCoinSever(60000);
        }
        if (pack.name == "Pack5")
        {
            //Thêm 80000 coin
            GetInfoAndUpdate.instance.UpdateCoinSever(80000);
        }
    }

    private void Update()
    {
        textmoney.text = PlayerPrefs.GetInt("money").ToString();
        if (GetInfoAndUpdate.isConnect == true)
        {

            panelDisconnect.SetActive(false);
        }
        else
        {
            panelDisconnect.SetActive(true);
            Debug.Log("afasdfasdfasdfsdfsadfasfasf");
        }
    }

    public void ExitDisconnect()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelDisconnect.SetActive(false);
        Application.LoadLevel("Shop");
        GetInfoAndUpdate.instance.GetInfoSever();
    }

    public void ButtonExit()
    {
        SceneManager.LoadScene(Menu.exitShop);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void ButtonReward()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdmodController.Instance.showRewardAdsVideo();
    }
    public void ButtonGetReward()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.coinEffect.Play();
        }
        GameObject.FindGameObjectWithTag("okReward").GetComponent<RectTransform>().transform.localScale = new Vector3(0, 0, 0);
        btnReward.SetActive(false);
        GetInfoAndUpdate.instance.UpdateCoinSever(500);
    }


}
