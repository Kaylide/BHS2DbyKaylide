using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SetupStatium : MonoBehaviour
{
    public static SetupStatium instance;
    public GameObject buttonBall, buttonStadium;
    public GameObject panelStadium, panelBall;
    public List<Sprite> listAI;
    public int  teamAI, indexBall;
    public Text textMoney;
    public Image img_Stadium;
    public int index_Stadium;
    public Button btnLeftStadium, btnRightStadium;

    public GameObject used, btnSelect;
    public GameObject[] ball;

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
        Menu.exitShop = "SetupStadium";
        index_Stadium = PlayerPrefs.GetInt("index_Stadium", 0);
        img_Stadium.sprite = SelectTeam.instance.sp_Stadiums[index_Stadium];


        buttonStadium.GetComponent<Image>().color = Color.green;
        panelStadium.SetActive(true);
        panelBall.SetActive(false);
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt("wcAI");
        }
        else
        {
            teamAI = PlayerPrefs.GetInt("teamAI");
        }
        GetAI();

        used.transform.position = ball[PlayerPrefs.GetInt("indexBall")].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        textMoney.text = PlayerPrefs.GetInt("money").ToString();
        PlayerPrefs.SetInt("index_Stadium", index_Stadium);

        img_Stadium.sprite =SelectTeam.instance.sp_Stadiums[index_Stadium];


        if(index_Stadium == 0)
        {
            btnLeftStadium.interactable = false;
        }
        else 
        {
            btnLeftStadium.interactable = true;
        }
        if (index_Stadium == SelectTeam.instance.sp_Stadiums.Length -1)
        {
            btnRightStadium.interactable = false;
        }
        else
        {
            btnRightStadium.interactable = true;
        }
        if(index_Stadium <= 0)
        {
            index_Stadium = 0;
        }
        if (index_Stadium >= SelectTeam.instance.sp_Stadiums.Length - 1)
        {
            index_Stadium = SelectTeam.instance.sp_Stadiums.Length - 1;
        }
    }
   
    public void ButtonBall(int index)
    {
        
        if(index != PlayerPrefs.GetInt("indexBall"))
        {
            indexBall = index;
            btnSelect.SetActive(true);
            btnSelect.transform.position = ball[index].transform.position;
        }
        else
        {
            btnSelect.SetActive(false);
        }

    }

    public void SelectBall()
    {
        btnSelect.SetActive(false);
        used.transform.position = ball[indexBall].transform.position;
        PlayerPrefs.SetInt("indexBall", indexBall);
    }

    public void ButtonAddMoney()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void BtnLeftStadium()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        index_Stadium--;

    }
    public void BtnRightStadium()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        index_Stadium++;
    }

    public void ButtonSetupBall()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelStadium.SetActive(false);
        panelBall.SetActive(true);
        buttonBall.GetComponent<Image>().color = Color.green;
        buttonStadium.GetComponent<Image>().color = Color.white;

    }
    public void ButtonStadium()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelStadium.SetActive(true);
        panelBall.SetActive(false);
        buttonStadium.GetComponent<Image>().color = Color.green;
        buttonBall.GetComponent<Image>().color = Color.white;

       
    }
    public void ButtonStart()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Loading");
        buttonBall.GetComponent<Image>().color = Color.white;
        buttonStadium.GetComponent<Image>().color = Color.white;
    }
    public void ButtonHome()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }

    public void GetAI()
    {
        switch (teamAI)
        {
            case 1:
                for (int i = 0; i < SelectTeam.instance.Player_1.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_1[i]);
                }
                break;
            case 2:
                for (int i = 0; i < SelectTeam.instance.Player_2.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_2[i]);
                }
                break;
            case 3:
                for (int i = 0; i < SelectTeam.instance.Player_3.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_3[i]);
                }
                break;
            case 4:
                for (int i = 0; i < SelectTeam.instance.Player_4.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_4[i]);
                }
                break;
            case 5:
                for (int i = 0; i < SelectTeam.instance.Player_5.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_5[i]);
                }
                break;
            case 6:
                for (int i = 0; i < SelectTeam.instance.Player_6.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_6[i]);
                }
                break;
            case 7:
                for (int i = 0; i < SelectTeam.instance.Player_7.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_7[i]);
                }
                break;
            case 8:
                for (int i = 0; i < SelectTeam.instance.Player_8.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_8[i]);
                }
                break;
            case 9:
                for (int i = 0; i < SelectTeam.instance.Player_9.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_9[i]);
                }
                break;
            case 10:
                for (int i = 0; i < SelectTeam.instance.Player_10.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_10[i]);
                }
                break;
            case 11:
                for (int i = 0; i < SelectTeam.instance.Player_11.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_11[i]);
                }
                break;
            case 12:
                for (int i = 0; i < SelectTeam.instance.Player_12.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_12[i]);
                }
                break;
            case 13:
                for (int i = 0; i < SelectTeam.instance.Player_13.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_13[i]);
                }
                break;
            case 14:
                for (int i = 0; i < SelectTeam.instance.Player_14.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_14[i]);
                }
                break;
            case 15:
                for (int i = 0; i < SelectTeam.instance.Player_15.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_15[i]);
                }
                break;
            case 16:
                for (int i = 0; i < SelectTeam.instance.Player_16.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_16[i]);
                }
                break;
            case 17:
                for (int i = 0; i < SelectTeam.instance.Player_17.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_17[i]);
                }
                break;
            case 18:
                for (int i = 0; i < SelectTeam.instance.Player_18.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_18[i]);
                }
                break;
            case 19:
                for (int i = 0; i < SelectTeam.instance.Player_19.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_19[i]);
                }
                break;
            case 20:
                for (int i = 0; i < SelectTeam.instance.Player_20.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_20[i]);
                }
                break;
            case 21:
                for (int i = 0; i < SelectTeam.instance.Player_21.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_21[i]);
                }
                break;
            case 22:
                for (int i = 0; i < SelectTeam.instance.Player_22.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_22[i]);
                }
                break;
            case 23:
                for (int i = 0; i < SelectTeam.instance.Player_23.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_23[i]);
                }
                break;
            case 24:
                for (int i = 0; i < SelectTeam.instance.Player_24.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_24[i]);
                }
                break;
            case 25:
                for (int i = 0; i < SelectTeam.instance.Player_25.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_25[i]);
                }
                break;
            case 26:
                for (int i = 0; i < SelectTeam.instance.Player_26.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_26[i]);
                }
                break;
            case 27:
                for (int i = 0; i < SelectTeam.instance.Player_27.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_27[i]);
                }
                break;
            case 28:
                for (int i = 0; i < SelectTeam.instance.Player_28.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_28[i]);
                }
                break;
            case 29:
                for (int i = 0; i < SelectTeam.instance.Player_29.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_29[i]);
                }
                break;
            case 30:
                for (int i = 0; i < SelectTeam.instance.Player_30.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_30[i]);
                }
                break;
            case 31:
                for (int i = 0; i < SelectTeam.instance.Player_31.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_31[i]);
                }
                break;
            case 32:
                for (int i = 0; i < SelectTeam.instance.Player_32.Length; i++)
                {
                    listAI.Add(SelectTeam.instance.Player_32[i]);
                }
                break;

        }
    }
}
