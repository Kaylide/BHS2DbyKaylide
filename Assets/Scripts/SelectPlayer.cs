using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    public Text textMoney;
    public GameObject cursor;
    public GameObject usedEffect;
    public GameObject[] player, buttonBuy;
    public int id, amountMoney;
    public GameObject buttonSelect;
    public int[] buyHash;
    public int[] price;
    public Image[] shoePlayer, headPlayer, bodyPlayer, stage;
    public Text[] namePlayer;
    public int _IDPlayer;
    public GameObject panelDisconnect;
    // Use this for initialization
    void Start()
    {
        Menu.exitShop = "MyTeam";
        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            SoundManager.Instance.musicBG.mute = false;
        }
        id = PlayerPrefs.GetInt("IDPlayer", 0);
        amountMoney = PlayerPrefs.GetInt("money");
        textMoney.text = amountMoney.ToString();
        for (int i = 1; i < player.Length; i++)
        {
            buyHash[i] = PlayerPrefs.GetInt("buyHash" + i, 0);
            if (buyHash[i] == 1)
            {
                buttonBuy[i - 1].SetActive(false);
            }
        }
        buyHash[0] = 1;

        GetCursor();
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

        for (int i = 0; i < player.Length; i++)
        { 
            if (buyHash[i] == 0)
            {
                shoePlayer[i].color = Color.grey;
                headPlayer[i].color = Color.grey;
                bodyPlayer[i].color = Color.grey;
                stage[i].color = Color.grey;
                namePlayer[i].color = Color.grey;
            }
            else
            {
                shoePlayer[i].color = Color.white;
                headPlayer[i].color = Color.white;
                bodyPlayer[i].color = Color.white;
                stage[i].color = Color.white;
                namePlayer[i].color = Color.white;
            }
        }

    }

    public void ExitDisconnect()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelDisconnect.SetActive(false);
        Application.LoadLevel("MyTeam");
        GetInfoAndUpdate.instance.GetInfoSever();
    }
    public void ButtonBuyPlayer(int ID)
    {
        _IDPlayer = ID;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if (amountMoney >= price[ID - 1] && buyHash[ID] == 0)
        {
            StartCoroutine(SendUpdateCoinBuyPlayer());
        }

    }

    IEnumerator SendUpdateCoinBuyPlayer()
    {
        Debug.Log(-price[_IDPlayer - 1]);
        GetInfoAndUpdate.isConnect = true;
        Debug.Log("22222222222222222222222222");
        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money -price[_IDPlayer - 1]);
        buttonBuy[_IDPlayer - 1].SetActive(false);
        buyHash[_IDPlayer] = 1;
        PlayerPrefs.SetInt("buyHash" + _IDPlayer, 1);
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161 
    }

    public void ButtonAddMoney()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void UsedPlayer()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        buttonSelect.SetActive(false);
        PlayerPrefs.SetInt("IDPlayer", id);
        MyTeam.instance.textSpeedCurrent.color = Color.white;
        MyTeam.instance.textJumpcurrent.color = Color.white;
        MyTeam.instance.textShootCurrent.color = Color.white;

        if (PlayerPrefs.GetInt("money") >= MyTeam.instance.PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            MyTeam.instance.btnUpgradeSpeed.GetComponent<Button>().interactable = true;
            MyTeam.instance.textPriceUpGradeSpeed.color = Color.white;
        }
        else
        {
            MyTeam.instance.btnUpgradeSpeed.GetComponent<Button>().interactable = false;
            MyTeam.instance.textPriceUpGradeSpeed.color = Color.grey;
        }
        if (PlayerPrefs.GetInt("money") >= MyTeam.instance.PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            MyTeam.instance.btnUpgradeShoot.GetComponent<Button>().interactable = true;
            MyTeam.instance.textPriceUpGradeShoot.color = Color.white;
        }
        else
        {
            MyTeam.instance. btnUpgradeShoot.GetComponent<Button>().interactable = false;
            MyTeam.instance.textPriceUpGradeShoot.color = Color.grey;

        }
        if (PlayerPrefs.GetInt("money") >=MyTeam.instance. PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            MyTeam.instance. btnUpgradeJump.GetComponent<Button>().interactable = true;
            MyTeam.instance.textPriceUpGradeJump.color = Color.white;
        }
        else
        {
            MyTeam.instance. btnUpgradeJump.GetComponent<Button>().interactable = false;
            MyTeam.instance. textPriceUpGradeJump.color = Color.grey;
        }

        MyTeam.instance.img_jumb.color = Color.white;
        MyTeam.instance.img_speed.color = Color.white;
        MyTeam.instance.img_shoot.color = Color.white;


        GameObject _cursor = GameObject.FindGameObjectWithTag("Cursor");
        Destroy(_cursor);
        GetCursor();
    }
    public void GetCursor()
    {
        switch (id)
        {
            case 0:
                cursor.transform.position = player[0].transform.position;
                GameObject _select = Instantiate(usedEffect, player[0].transform.position, Quaternion.identity) as GameObject;
                _select.transform.parent = player[0].transform;
                break;
            case 1:
                cursor.transform.position = player[1].transform.position;
                GameObject _select1 = Instantiate(usedEffect, player[1].transform.position, Quaternion.identity) as GameObject;
                _select1.transform.parent = player[1].transform;
                break;
            case 2:
                cursor.transform.position = player[2].transform.position;
                GameObject _select2 = Instantiate(usedEffect, player[2].transform.position, Quaternion.identity) as GameObject;
                _select2.transform.parent = player[2].transform;
                break;
            case 3:
                cursor.transform.position = player[3].transform.position;
                GameObject _select3 = Instantiate(usedEffect, player[3].transform.position, Quaternion.identity) as GameObject;
                _select3.transform.parent = player[3].transform;
                break;
            case 4:
                cursor.transform.position = player[4].transform.position;
                GameObject _select4 = Instantiate(usedEffect, player[4].transform.position, Quaternion.identity) as GameObject;
                _select4.transform.parent = player[4].transform;
                break;
            case 5:
                cursor.transform.position = player[5].transform.position;
                GameObject _select5 = Instantiate(usedEffect, player[5].transform.position, Quaternion.identity) as GameObject;
                _select5.transform.parent = player[5].transform;
                break;
        }
    }

    public void Player(int ID)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        cursor.transform.position = player[ID].transform.position;

        MyTeam.instance.UpGradeSpeed[ID] = PlayerPrefs.GetInt("UpGradeSpeed" + ID, 0);
        MyTeam.instance.UpGradeShoot[ID] = PlayerPrefs.GetInt("UpGradeShoot" + ID, 0);
        MyTeam.instance.UpGradeJump[ID] = PlayerPrefs.GetInt("UpGradeJump" + ID, 0);

        MyTeam.instance.textPriceUpGradeSpeed.text = MyTeam.instance.PriceUpGradeSpeed[MyTeam.instance.UpGradeSpeed[ID]].ToString();
        MyTeam.instance.textPriceUpGradeShoot.text = MyTeam.instance.PriceUpGradeShoot[MyTeam.instance.UpGradeShoot[ID]].ToString();
        MyTeam.instance.textPriceUpGradeJump.text = MyTeam.instance.PriceUpGradeJump[MyTeam.instance.UpGradeJump[ID]].ToString();

        MyTeam.instance.img_jumb.fillAmount = MyTeam.instance.jumpLoading[ID] / MyTeam.instance.jumpMax;
        MyTeam.instance.img_speed.fillAmount = MyTeam.instance.speedLoading[ID] / MyTeam.instance.speedMax;
        MyTeam.instance.img_shoot.fillAmount =
            Mathf.Sqrt(MyTeam.instance.shootLoading_x[ID] * MyTeam.instance.shootLoading_x[ID] + MyTeam.instance.shootLoading_y[ID] * MyTeam.instance.shootLoading_y[ID])
            / Mathf.Sqrt(MyTeam.instance.shootMax_x * MyTeam.instance.shootMax_x + MyTeam.instance.shootMax_y * MyTeam.instance.shootMax_y);
        id = ID;
        MyTeam.instance.id2 = ID;


        if (buyHash[id] == 1 && ID != PlayerPrefs.GetInt("IDPlayer"))
        {
            buttonSelect.SetActive(true);
            buttonSelect.transform.position = player[id].transform.position;
        }
        else
        {
            buttonSelect.SetActive(false);
        }
        if (ID == PlayerPrefs.GetInt("IDPlayer"))
        {

            MyTeam.instance.img_jumb.color = Color.white;
            MyTeam.instance.img_speed.color = Color.white;
            MyTeam.instance.img_shoot.color = Color.white;

            if (PlayerPrefs.GetInt("money") >= MyTeam.instance.PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                MyTeam.instance.btnUpgradeSpeed.GetComponent<Button>().interactable = true;
                MyTeam.instance.textPriceUpGradeSpeed.color = Color.white;
            }
            else
            {
                MyTeam.instance.btnUpgradeSpeed.GetComponent<Button>().interactable = false;
                MyTeam.instance.textPriceUpGradeSpeed.color = Color.grey;
            }
            if (PlayerPrefs.GetInt("money") >= MyTeam.instance.PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                MyTeam.instance.btnUpgradeShoot.GetComponent<Button>().interactable = true;
                MyTeam.instance.textPriceUpGradeShoot.color = Color.white;
            }
            else
            {
                MyTeam.instance.btnUpgradeShoot.GetComponent<Button>().interactable = false;
                MyTeam.instance.textPriceUpGradeShoot.color = Color.grey;

            }
            if (PlayerPrefs.GetInt("money") >= MyTeam.instance.PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                MyTeam.instance.btnUpgradeJump.GetComponent<Button>().interactable = true;
                MyTeam.instance.textPriceUpGradeJump.color = Color.white;
            }
            else
            {
                MyTeam.instance.btnUpgradeJump.GetComponent<Button>().interactable = false;
                MyTeam.instance.textPriceUpGradeJump.color = Color.grey;
            }
            MyTeam.instance.textSpeedCurrent.color = Color.white;
            MyTeam.instance.textJumpcurrent.color = Color.white;
            MyTeam.instance.textShootCurrent.color = Color.white;

        }
        else
        {
            MyTeam.instance.img_jumb.color = Color.grey;
            MyTeam.instance.img_speed.color = Color.grey;
            MyTeam.instance.img_shoot.color = Color.grey;

            MyTeam.instance.btnUpgradeJump.GetComponent<Button>().interactable = false;
            MyTeam.instance.btnUpgradeSpeed.GetComponent<Button>().interactable = false;
            MyTeam.instance.btnUpgradeShoot.GetComponent<Button>().interactable = false;

            MyTeam.instance.textSpeedCurrent.color = Color.grey;
            MyTeam.instance.textJumpcurrent.color = Color.grey;
            MyTeam.instance.textShootCurrent.color = Color.grey;

            MyTeam.instance.textPriceUpGradeSpeed.color = Color.grey;
            MyTeam.instance.textPriceUpGradeShoot.color = Color.grey;
            MyTeam.instance.textPriceUpGradeJump.color = Color.grey;
        }
    }

}
