using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyTeam : MonoBehaviour
{

    public static MyTeam instance;
    public int id,id2, amountMoney;

    public Image img_speed, img_jumb, img_shoot;
    public float[] Shoot_x, Shoot_y, Jump, Speed, addForceHead_x;
    public float shootMax_x, shootMax_y, jumpMax, speedMax;
    public int[] UpGradeSpeed, UpGradeShoot, UpGradeJump;
    public int[] PriceUpGradeSpeedPlayer, PriceUpGradeShootPlayer, PriceUpGradeJumpPlayer;
    public int[] PriceUpGradeSpeed, PriceUpGradeShoot, PriceUpGradeJump;

    public Text textPriceUpGradeSpeed, textPriceUpGradeShoot, textPriceUpGradeJump, textSpeedCurrent, textJumpcurrent, textShootCurrent;

    public float[] shootLoading_x, shootLoading_y, jumpLoading, speedLoading;
    public GameObject btnUpgradeSpeed, btnUpgradeShoot, btnUpgradeJump;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        
        id = PlayerPrefs.GetInt("IDPlayer", 0);

        for (int i = 6; i < PriceUpGradeSpeed.Length - 1; i++)
        {
            PriceUpGradeSpeed[0] = 100;
            PriceUpGradeSpeed[1] = 200;
            PriceUpGradeSpeed[2] = 300;
            PriceUpGradeSpeed[3] = 500;
            PriceUpGradeSpeed[4] = 800;
            PriceUpGradeSpeed[5] = 1300;
            PriceUpGradeSpeed[6] = 2000;

            PriceUpGradeSpeed[i + 1] = PriceUpGradeSpeed[i] + 3000;

            PriceUpGradeShoot[0] = 100;
            PriceUpGradeShoot[1] = 200;
            PriceUpGradeShoot[2] = 300;
            PriceUpGradeShoot[3] = 500;
            PriceUpGradeShoot[4] = 800;
            PriceUpGradeShoot[5] = 1300;
            PriceUpGradeShoot[6] = 2000;
            PriceUpGradeShoot[i + 1] = PriceUpGradeShoot[i] + 3000;

            PriceUpGradeJump[0] = 100;
            PriceUpGradeJump[1] = 200;
            PriceUpGradeJump[2] = 300;
            PriceUpGradeJump[3] = 500;
            PriceUpGradeJump[4] = 800;
            PriceUpGradeJump[5] = 1300;
            PriceUpGradeJump[6] = 2000;
            PriceUpGradeJump[i + 1] = PriceUpGradeJump[i] + 3000;
        }
        UpGradeSpeed[id] = PlayerPrefs.GetInt("UpGradeSpeed" + id, 0);
        UpGradeShoot[id] = PlayerPrefs.GetInt("UpGradeShoot" + id, 0);
        UpGradeJump[id] = PlayerPrefs.GetInt("UpGradeJump" + id, 0);

        PriceUpGradeSpeedPlayer[id] = PriceUpGradeSpeed[UpGradeSpeed[id]];
        PriceUpGradeJumpPlayer[id] = PriceUpGradeJump[UpGradeJump[id]];
        PriceUpGradeShootPlayer[id] = PriceUpGradeShoot[UpGradeShoot[id]];

        textPriceUpGradeSpeed.text = PriceUpGradeSpeed[UpGradeSpeed[id]].ToString();
        textPriceUpGradeShoot.text = PriceUpGradeShoot[UpGradeShoot[id]].ToString();
        textPriceUpGradeJump.text = PriceUpGradeJump[UpGradeJump[id]].ToString();

        PlayerPrefs.SetInt("money", 99999);
        amountMoney = PlayerPrefs.GetInt("money");


        Shoot_x[0] = PlayerPrefs.GetFloat("Shoot_x0", 41);
        Shoot_y[0] = PlayerPrefs.GetFloat("Shoot_y0", 360);
        Jump[0] = PlayerPrefs.GetFloat("Jump0", 7f);
        Speed[0] = PlayerPrefs.GetFloat("Speed0", 350);
        addForceHead_x[0] = PlayerPrefs.GetFloat("addForceHead_x0", 130);

        Shoot_x[1] = PlayerPrefs.GetFloat("Shoot_x1", 42f);
        Shoot_y[1] = PlayerPrefs.GetFloat("Shoot_y1", 365f);
        Jump[1] = PlayerPrefs.GetFloat("Jump1", 6.75f);
        Speed[1] = PlayerPrefs.GetFloat("Speed1", 357);
        addForceHead_x[1] = PlayerPrefs.GetFloat("addForceHead_x1", 125);

        Shoot_x[2] = PlayerPrefs.GetFloat("Shoot_x2", 41.2f);
        Shoot_y[2] = PlayerPrefs.GetFloat("Shoot_y2", 361f);
        Jump[2] = PlayerPrefs.GetFloat("Jump2", 7.3f);
        Speed[2] = PlayerPrefs.GetFloat("Speed2", 350);
        addForceHead_x[2] = PlayerPrefs.GetFloat("addForceHead_x2", 142);

        Shoot_x[3] = PlayerPrefs.GetFloat("Shoot_x3", 42.4f);
        Shoot_y[3] = PlayerPrefs.GetFloat("Shoot_y3", 367f);
        Jump[3] = PlayerPrefs.GetFloat("Jump3", 7.25f);
        Speed[3] = PlayerPrefs.GetFloat("Speed3", 360);
        addForceHead_x[3] = PlayerPrefs.GetFloat("addForceHead_x3", 135);

        Shoot_x[4] = PlayerPrefs.GetFloat("Shoot_x4", 42f);
        Shoot_y[4] = PlayerPrefs.GetFloat("Shoot_y4", 365f);
        Jump[4] = PlayerPrefs.GetFloat("Jump4", 7.4f);
        Speed[4] = PlayerPrefs.GetFloat("Speed4", 364);
        addForceHead_x[4] = PlayerPrefs.GetFloat("addForceHead_x4", 145);

        Shoot_x[5] = PlayerPrefs.GetFloat("Shoot_x5", 43f);
        Shoot_y[5] = PlayerPrefs.GetFloat("Shoot_y5", 370f);
        Jump[5] = PlayerPrefs.GetFloat("Jump5", 7.3f);
        Speed[5] = PlayerPrefs.GetFloat("Speed5", 365);
        addForceHead_x[5] = PlayerPrefs.GetFloat("addForceHead_x5", 150);

        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetFloat("Shoot_x" + i, Shoot_x[i]);
            PlayerPrefs.SetFloat("Shoot_y" + i, Shoot_y[i]);
            PlayerPrefs.SetFloat("Jump" + i, Jump[i]);
            PlayerPrefs.SetFloat("Speed" + i, Speed[i]);
            PlayerPrefs.SetFloat("addForceHead_x" + i, addForceHead_x[i]);
        }

        shootLoading_x[0] = PlayerPrefs.GetFloat("shootLoading_x0", 11);
        shootLoading_y[0] = PlayerPrefs.GetFloat("shootLoading_y0", 210);
        jumpLoading[0] = PlayerPrefs.GetFloat("jumpLoading0", 4.75f);
        speedLoading[0] = PlayerPrefs.GetFloat("speedLoading0", 200);

        shootLoading_x[1] = PlayerPrefs.GetFloat("shootLoading_x1", 18f);
        shootLoading_y[1] = PlayerPrefs.GetFloat("shootLoading_y1", 245);
        jumpLoading[1] = PlayerPrefs.GetFloat("jumpLoading1", 4.5f);
        speedLoading[1] = PlayerPrefs.GetFloat("speedLoading1", 210);

        shootLoading_x[2] = PlayerPrefs.GetFloat("shootLoading_x2", 12.4f);
        shootLoading_y[2] = PlayerPrefs.GetFloat("shootLoading_y2", 217f);
        jumpLoading[2] = PlayerPrefs.GetFloat("jumpLoading2", 5.05f);
        speedLoading[2] = PlayerPrefs.GetFloat("speedLoading2", 205);

        shootLoading_x[3] = PlayerPrefs.GetFloat("shootLoading_x3", 20.8f);
        shootLoading_y[3] = PlayerPrefs.GetFloat("shootLoading_y3", 259);
        jumpLoading[3] = PlayerPrefs.GetFloat("jumpLoading3", 5.375f);
        speedLoading[3] = PlayerPrefs.GetFloat("speedLoading3", 215);

        shootLoading_x[4] = PlayerPrefs.GetFloat("shootLoading_x4", 18f);
        shootLoading_y[4] = PlayerPrefs.GetFloat("shootLoading_y4", 245);
        jumpLoading[4] = PlayerPrefs.GetFloat("jumpLoading4", 5.5f);
        speedLoading[4] = PlayerPrefs.GetFloat("speedLoading4", 220);

        shootLoading_x[5] = PlayerPrefs.GetFloat("shootLoading_x5", 25f);
        shootLoading_y[5] = PlayerPrefs.GetFloat("shootLoading_y5", 280f);
        jumpLoading[5] = PlayerPrefs.GetFloat("jumpLoading5", 5.4f);
        speedLoading[5] = PlayerPrefs.GetFloat("speedLoading5", 225);

        speedMax = 375f;
        jumpMax = 8.25f;
        shootMax_x = 46f;
        shootMax_y = 385f;

        img_jumb.fillAmount = jumpLoading[id] / jumpMax;
        img_speed.fillAmount = speedLoading[id] / speedMax;
        img_shoot.fillAmount = Mathf.Sqrt(shootLoading_x[id] * shootLoading_x[id] + shootLoading_y[id] * shootLoading_y[id])
            / Mathf.Sqrt(shootMax_x * shootMax_x + shootMax_y * shootMax_y);

        if (PlayerPrefs.GetInt("money") >= PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            btnUpgradeSpeed.GetComponent<Button>().interactable = true;
            textPriceUpGradeSpeed.color = Color.white;
        }
        else
        {
            btnUpgradeSpeed.GetComponent<Button>().interactable = false;
            textPriceUpGradeSpeed.color = Color.grey;
        }
        if (PlayerPrefs.GetInt("money") >= PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            btnUpgradeShoot.GetComponent<Button>().interactable = true;
            textPriceUpGradeShoot.color = Color.white;
        }
        else
        {
            btnUpgradeShoot.GetComponent<Button>().interactable = false;
            textPriceUpGradeShoot.color = Color.grey;

        }
        if (PlayerPrefs.GetInt("money") >= PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            btnUpgradeJump.GetComponent<Button>().interactable = true;
            textPriceUpGradeJump.color = Color.white;
        }
        else
        {
            btnUpgradeJump.GetComponent<Button>().interactable = false;
            textPriceUpGradeJump.color = Color.grey;
        }

    }

    // Update is called once per frame
    void Update()
    {
        int _id = PlayerPrefs.GetInt("IDPlayer");
        Scene _sceneCurrent = SceneManager.GetActiveScene();

        if(_sceneCurrent.name == "MyTeam")
        {
            if (id2 == _id)
            {
                if (PlayerPrefs.GetInt("money") >= PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
                {
                    btnUpgradeSpeed.GetComponent<Button>().interactable = true;
                    textPriceUpGradeSpeed.color = Color.white;
                }
                else
                {
                    btnUpgradeSpeed.GetComponent<Button>().interactable = false;
                    textPriceUpGradeSpeed.color = Color.grey;
                }
                if (PlayerPrefs.GetInt("money") >= PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
                {
                    btnUpgradeShoot.GetComponent<Button>().interactable = true;
                    textPriceUpGradeShoot.color = Color.white;
                }
                else
                {
                    btnUpgradeShoot.GetComponent<Button>().interactable = false;
                    textPriceUpGradeShoot.color = Color.grey;

                }
                if (PlayerPrefs.GetInt("money") >= PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
                {
                    btnUpgradeJump.GetComponent<Button>().interactable = true;
                    textPriceUpGradeJump.color = Color.white;
                }
                else
                {
                    btnUpgradeJump.GetComponent<Button>().interactable = false;
                    textPriceUpGradeJump.color = Color.grey;
                }
            }
            else
            {
                btnUpgradeSpeed.GetComponent<Button>().interactable = false;
                textPriceUpGradeSpeed.color = Color.grey;
                btnUpgradeShoot.GetComponent<Button>().interactable = false;
                textPriceUpGradeShoot.color = Color.grey;
                btnUpgradeJump.GetComponent<Button>().interactable = false;
                textPriceUpGradeJump.color = Color.grey;
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("money") >= PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                btnUpgradeSpeed.GetComponent<Button>().interactable = true;
                textPriceUpGradeSpeed.color = Color.white;
            }
            else
            {
                btnUpgradeSpeed.GetComponent<Button>().interactable = false;
                textPriceUpGradeSpeed.color = Color.grey;
            }
            if (PlayerPrefs.GetInt("money") >= PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                btnUpgradeShoot.GetComponent<Button>().interactable = true;
                textPriceUpGradeShoot.color = Color.white;
            }
            else
            {
                btnUpgradeShoot.GetComponent<Button>().interactable = false;
                textPriceUpGradeShoot.color = Color.grey;

            }
            if (PlayerPrefs.GetInt("money") >= PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
            {
                btnUpgradeJump.GetComponent<Button>().interactable = true;
                textPriceUpGradeJump.color = Color.white;
            }
            else
            {
                btnUpgradeJump.GetComponent<Button>().interactable = false;
                textPriceUpGradeJump.color = Color.grey;
            }
        }

        PriceUpGradeSpeedPlayer[_id] = PriceUpGradeSpeed[UpGradeSpeed[_id]];
        PriceUpGradeJumpPlayer[_id] = PriceUpGradeJump[UpGradeJump[_id]];
        PriceUpGradeShootPlayer[_id] = PriceUpGradeShoot[UpGradeShoot[_id]];

        textSpeedCurrent.text = (int)(img_speed.fillAmount * 100) + "/" + "100";
        textShootCurrent.text = (int)(img_shoot.fillAmount * 100) + "/" + "100";
        textJumpcurrent.text = (int)(img_jumb.fillAmount * 100) + "/" + "100";

        if (img_speed.fillAmount < 1)
        {
            btnUpgradeSpeed.SetActive(true);
        }
        else
        {
            btnUpgradeSpeed.SetActive(false);
        }

        if (img_jumb.fillAmount < 1)
        {
            btnUpgradeJump.SetActive(true);
        }
        else
        {
            btnUpgradeJump.SetActive(false);
        }

        if (img_shoot.fillAmount < 1)
        {
            btnUpgradeShoot.SetActive(true);
        }
        else
        {
            btnUpgradeShoot.SetActive(false);
        }

        if (PlayerPrefs.GetInt("money") < PriceUpGradeSpeedPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {

            btnUpgradeSpeed.GetComponent<Button>().interactable = false;
            textPriceUpGradeSpeed.color = Color.grey;
        }
        if (PlayerPrefs.GetInt("money") < PriceUpGradeShootPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {

            btnUpgradeShoot.GetComponent<Button>().interactable = false;
            textPriceUpGradeShoot.color = Color.grey;

        }
        if (PlayerPrefs.GetInt("money") < PriceUpGradeJumpPlayer[PlayerPrefs.GetInt("IDPlayer")])
        {
            btnUpgradeJump.GetComponent<Button>().interactable = false;
            textPriceUpGradeJump.color = Color.grey;
        }
    }


    public void ButtonHome()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        Application.LoadLevel("Menu");
    }

    public void ButtonUpGradeSpeed()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.upgradeButton.Play();
        }
        int _iD = PlayerPrefs.GetInt("IDPlayer");

        if (PlayerPrefs.GetInt("money") >= PriceUpGradeSpeedPlayer[_iD] && img_speed.fillAmount < 1)
        {
            StartCoroutine(SendUpdateSpeed());
        }
       
    }

    IEnumerator SendUpdateSpeed()
    {
        int _iD = PlayerPrefs.GetInt("IDPlayer");
            

        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money - PriceUpGradeSpeedPlayer[_iD]);

        GetInfoAndUpdate.isConnect = true;

        UpGradeSpeed[_iD]++;
        PlayerPrefs.SetInt("UpGradeSpeed" + _iD, UpGradeSpeed[_iD]);
        Speed[_iD] += 1f;
        if (Speed[_iD] >= 375)
        {
            Speed[_iD] = 375;
        }
        speedLoading[_iD] += 7f;
        PlayerPrefs.SetFloat("Speed" + _iD, Speed[_iD]);
        PlayerPrefs.SetFloat("speedLoading" + _iD, speedLoading[_iD]);
        img_speed.fillAmount = speedLoading[_iD] / speedMax;

        textPriceUpGradeSpeed.text = PriceUpGradeSpeed[UpGradeSpeed[_iD]].ToString();
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }
    public void ButtonUpGradeShoot()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.upgradeButton.Play();
        }
        int _iD = PlayerPrefs.GetInt("IDPlayer");
        if (PlayerPrefs.GetInt("money") >= PriceUpGradeShootPlayer[_iD] && img_shoot.fillAmount < 1)
        {
            StartCoroutine(SendUpdateShoot());

        }


    }

    IEnumerator SendUpdateShoot()
    {
        int _iD = PlayerPrefs.GetInt("IDPlayer");

            int _money = PlayerPrefs.GetInt("money");
            PlayerPrefs.SetInt("money", _money - PriceUpGradeShootPlayer[_iD]);

            GetInfoAndUpdate.isConnect = true;

            UpGradeShoot[_iD]++;
            PlayerPrefs.SetInt("UpGradeShoot" + _iD, UpGradeShoot[_iD]);
            Shoot_y[_iD] += 1f;
            Shoot_x[_iD] += 0.2f;

            if (Shoot_x[_iD] >= 46)
            {
                Shoot_x[_iD] = 46f;
            }
            if (Shoot_y[_iD] >= 385)
            {
                Shoot_y[_iD] = 385f;
            }
            shootLoading_x[_iD] += 1.4f;
            shootLoading_y[_iD] += 7f;
            PlayerPrefs.SetFloat("Shoot_x" + _iD, Shoot_x[_iD]);
            PlayerPrefs.SetFloat("Shoot_y" + _iD, Shoot_y[_iD]);
            PlayerPrefs.SetFloat("shootLoading_x" + _iD, shootLoading_x[_iD]);
            PlayerPrefs.SetFloat("shootLoading_y" + _iD, shootLoading_y[_iD]);
            img_shoot.fillAmount = Mathf.Sqrt(shootLoading_x[_iD] * shootLoading_x[_iD] + shootLoading_y[_iD] * shootLoading_y[_iD])
                / Mathf.Sqrt(shootMax_x * shootMax_x + shootMax_y * shootMax_y);
            textPriceUpGradeShoot.text = PriceUpGradeShoot[UpGradeShoot[_iD]].ToString();

        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

    public void ButtonUpGradeJump()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.upgradeButton.Play();
        }
        int _iD = PlayerPrefs.GetInt("IDPlayer");
        if (PlayerPrefs.GetInt("money") >= PriceUpGradeJumpPlayer[_iD] && img_jumb.fillAmount < 1)
        {
            StartCoroutine(SendUpdateJump());

        }
    }

    IEnumerator SendUpdateJump()
    {
        int _iD = PlayerPrefs.GetInt("IDPlayer");
        
            int _money = PlayerPrefs.GetInt("money");
            PlayerPrefs.SetInt("money", _money - PriceUpGradeJumpPlayer[_iD]);

            GetInfoAndUpdate.isConnect = true;

            UpGradeJump[_iD]++;
            PlayerPrefs.SetInt("UpGradeJump" + _iD, UpGradeJump[_iD]);
            Jump[_iD] += 0.05f;
            if (Jump[_iD] >= 8.25f)
            {
                Jump[_iD] = 8.25f;
            }
            jumpLoading[_iD] += 0.125f;
            PlayerPrefs.SetFloat("Jump" + _iD, Jump[_iD]);
            PlayerPrefs.SetFloat("jumpLoading" + _iD, jumpLoading[_iD]);
            img_jumb.fillAmount = jumpLoading[_iD] / jumpMax;
            textPriceUpGradeJump.text = PriceUpGradeJump[UpGradeJump[_iD]].ToString();
            addForceHead_x[_iD] += 3f;
            if (addForceHead_x[_iD] >= 220f)
            {
                addForceHead_x[_iD] = 220f;
            }
            PlayerPrefs.SetFloat("addForceHead_x" + _iD, addForceHead_x[_iD]);
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }
}
