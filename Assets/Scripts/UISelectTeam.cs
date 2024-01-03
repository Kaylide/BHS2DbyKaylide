using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISelectTeam : MonoBehaviour
{
    public static UISelectTeam instance;
    public Text textMoney;
    public GameObject[] paneLock;
    public int unlock;
    public int[] priceEntryFee;
    public GameObject panelNotPlay, scrollRect;
    public static int id;
    public int _teamAI;
    public GameObject panelDisconnect;

    // Use this for initialization

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        unlock = PlayerPrefs.GetInt("unlock", 1);

        PlayerPrefs.SetInt("unlock", unlock);
        if (unlock > 2)
        {
            scrollRect.GetComponent<RectTransform>().localPosition = new Vector3(3664 - (250 * (unlock - 2)), 0, 0);
        }

        for (int i = 1; i < unlock; i++)
        {
            if (unlock > 1)
            {
                paneLock[i - 1].SetActive(false);
            }
            else
            {
                paneLock[i].SetActive(true);
            }

        }



        Menu.exitShop = "SelectTeam";
        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            SoundManager.Instance.musicBG.mute = false;
            SoundManager.Instance.matchLost.Stop();
            SoundManager.Instance.matchWon.Stop();
        }

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
    }

    public void ExitDisconnect()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelDisconnect.SetActive(false);
        Application.LoadLevel("SelectTeam");
        GetInfoAndUpdate.instance.GetInfoSever();
    }

    public void ButtonAddMoney()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void ButtonBack()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }
    public void ButtonPlay(int teamAI)
    {
        _teamAI = teamAI;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        if (priceEntryFee[teamAI - 1] <= PlayerPrefs.GetInt("money"))
        {
            StartCoroutine(SendUpdateCoin());

        }
        else
        {
            panelNotPlay.SetActive(true);
        }

    }


    IEnumerator SendUpdateCoin()
    {
        GetInfoAndUpdate.isConnect = true;
        Debug.Log("22222222222222222222222222");
        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money - priceEntryFee[_teamAI - 1]);
        PlayerPrefs.SetInt("teamAI", _teamAI);
        SceneManager.LoadScene("SetupStadium");
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

    public void ButtonPlay2(int _id)
    {
        id = _id;
    }

    public void ButtonExit()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelNotPlay.SetActive(false);
    }

}
