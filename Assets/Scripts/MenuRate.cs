using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuRate : MonoBehaviour
{

    public static MenuRate instance;
    public int startGame = 0;
    private GameObject panelRate;




    // Use this for initialization
    void Start()
    {
        panelRate = GameObject.Find("PanelRate");
        startGame = PlayerPrefs.GetInt("startGame");

        Debug.Log("============================================" + startGame);
        if (startGame >= 0)
        {
            if (startGame == 0 || startGame % 4 != 0)
            {
                panelRate.SetActive(false);
            }
            else
            {
                if(Menu.isLoadding == true)
                {
                    panelRate.SetActive(false);
                }
                else
                {
                    StartCoroutine(loadRate());
                }
            }
            startGame++;
            Debug.Log("============================================222" + startGame);
            PlayerPrefs.SetInt("startGame", startGame);
        }

        else
        {
            panelRate.SetActive(false);
        }

    }

    IEnumerator loadRate()
    {
        panelRate.SetActive(false);
        yield return new WaitForSeconds(6f);
        panelRate.SetActive(true);
    }

    public void Rate()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.applecat.worldchampion2018");
#elif UNITY_IOS
        Application.OpenURL("itms-apps:itunes.apple.com/us/app/apple-store/id1402912709?mt=8&action=write-review");
#endif

        startGame = -1;
        PlayerPrefs.SetInt("startGame", startGame);
        panelRate.SetActive(false);


    }

    public void Later()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        panelRate.SetActive(false);
    }
    public void Never()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        panelRate.SetActive(false);
        startGame = -1;
        PlayerPrefs.SetInt("startGame", startGame);
    }

}

