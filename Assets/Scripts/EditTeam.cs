using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTeam : MonoBehaviour
{
    public static EditTeam instance;

    public InputField inputNameTeam;
    public GameObject panelEditTeam, used, close;
    public string NameMyTeam;
    public Text nameMyTeam;
    public Button apply;
    public int indexFlagMyTeam;
    public int[] HashFlagMyTeam, PriceFlagMyTeam;
    public GameObject[] btnBuy, flagMyTeam;
    public Image img_FlagMyTeam;
    public GameObject ErrorCreatAcc;
    public Text error;
    public GameObject buttonPlayasGuest;
    public Sprite sp_login1, sp_login2;

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
            buttonPlayasGuest.GetComponent<Button>().interactable = true;
            NameMyTeam = PlayerPrefs.GetString("NameMyTeam", "");
            PlayerPrefs.SetString("NameMyTeam", NameMyTeam);
            if (NameMyTeam.Equals("") || PlayerPrefs.GetInt("setCreatAcc", 0) == 0)
            {
                close.SetActive(false);
                inputNameTeam.interactable = true;
            }
            else
            {
                inputNameTeam.interactable = false;
                close.SetActive(true);
                panelEditTeam.SetActive(false);
            }

            nameMyTeam.text = NameMyTeam;
            inputNameTeam.text = PlayerPrefs.GetString("NameMyTeam");
            indexFlagMyTeam = PlayerPrefs.GetInt("indexFlagMyTeam", 0);
            PlayerPrefs.SetInt("indexFlagMyTeam", indexFlagMyTeam);
            img_FlagMyTeam.sprite = SelectTeam.instance.MyTeamFlagPlayer[indexFlagMyTeam];
            //panelGrey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            used.SetActive(true);
            flagMyTeam[0].GetComponent<Image>().color = Color.white;
            flagMyTeam[1].GetComponent<Image>().color = Color.white;
            flagMyTeam[2].GetComponent<Image>().color = Color.white;
            used.transform.position = flagMyTeam[PlayerPrefs.GetInt("indexFlagMyTeam")].transform.position;
            if (NameMyTeam.Equals("") || PlayerPrefs.GetInt("setCreatAcc", 0) == 0)
            {
                close.SetActive(false);
            }
            else
            {
                close.SetActive(true);
            }
    }
    public void EditMyTeam()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelEditTeam.SetActive(true);
        inputNameTeam.text = PlayerPrefs.GetString("NameMyTeam");


    }
    public void CloseEditMyTeam()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelEditTeam.SetActive(false);
    }
    public void ApplyEditMyTeam()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        int index = PlayerPrefs.GetInt("indexFlagMyTeam");
        img_FlagMyTeam.sprite = SelectTeam.instance.MyTeamFlagPlayer[index];

        if (PlayerPrefs.GetString("NameMyTeam").Equals("") || PlayerPrefs.GetInt("setCreatAcc", 0) == 0)
        {
            if (inputNameTeam.text.Length >= 6 && inputNameTeam.text.Length <= 18)
            {

                StartCoroutine(Register());
            }
            else
            {
                error.text = "Name must be more than 6 characters and shorter  18 characters.";
                ErrorCreatAcc.SetActive(true);
            }
        }
        else
        {
            panelEditTeam.SetActive(false);
        }

    }


    public void SelectFlag(int indexFlag)
    {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.buttonClick.Play();
            }
            PlayerPrefs.SetInt("indexFlagMyTeam", indexFlag);
    }

    IEnumerator Register()
    {
            panelEditTeam.SetActive(false);
            PlayerPrefs.SetString("NameMyTeam", inputNameTeam.text);
            nameMyTeam.text = PlayerPrefs.GetString("NameMyTeam");
            inputNameTeam.interactable = false;

            if (PlayerPrefs.GetInt("setCreatAcc") == 0)
            {
                PlayerPrefs.SetInt("setCreatAcc", 1);
            }
            PlayerPrefs.SetInt("getInfo", 1);
            GetInfoAndUpdate.instance.GetInfoSever();
            yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

    public void ButtonCloseError()
    {
        ErrorCreatAcc.SetActive(false);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
}
