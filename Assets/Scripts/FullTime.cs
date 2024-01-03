using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FullTime : MonoBehaviour
{
    public static FullTime instance;
    public GameObject buttonExhibition, buttonWC;
    public Text text_resultGoal, text_YouWin, text_amoutGoal, text_AmoutMoneyMatch, text_AmoutMoneyGoal,
    text_MoneyTotal, text_TeamNameLeft, text_TeamNameRight, money;
    public Image flagLeft, flagRight;
    public int amountMoneyMatch, amountMoney;
    public int dir_top;
    public int[,] ValueTeam = new int[4, 8];
    public int[] ScoreTeam = new int[32];
    public int[] random = new int[16];
    public int[,] randomPlayOff = new int[2, 8];
    public int[,] GoalsPlayOff = new int[2, 8];
    public int[,] ValueTeamPlayOff = new int[2, 8];

    public int[,] GoalsFirtOff = new int[2, 4];
    public int[,] ValueTeamFirtOff = new int[2, 4];

    public int[,] ValueTeamQuarterFinals = new int[2, 2];
    public int[,] GoalsQuarterFinals = new int[2, 2];
    public List<int> listWinR4 = new List<int>();
    public int isMatchStage;
    public GameObject GoalsLeft, GoalsRight, btnx2, btnx2_exhibition;
    public SpriteRenderer stadium;
    public int[] indexPlayer = new int[32];
    public int teamPlayer;

    public Text txt_rewardExhibition;
    public int rewardExhibition, randomReward;

    public GameObject panelNotEnoughMoney;

    public GameObject panelDisconnect;
    // Use this for initialization
    public bool isConnect;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        isConnect = GetInfoAndUpdate.isConnect;

        StartCoroutine(SendUpdateUser());

        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            SoundManager.Instance.musicMatch.Stop();
            SoundManager.Instance.musicBG.mute = true;
        }

        Menu.exitShop = "FullTime";
        if (GameController.goals < GameController.goalsConceded)
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchLost.Play();
            }
        }
        else if (GameController.goals == GameController.goalsConceded)
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
        }
        else
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
        }


        for (int i = 0; i < 32; i++)
        {
            indexPlayer[i] = PlayerPrefs.GetInt("index" + i, 0);
        }
        int index_Stadium = PlayerPrefs.GetInt("index_Stadium", 0);
        stadium.sprite = SelectTeam.instance.sp_Stadiums[index_Stadium];



        isMatchStage = PlayerPrefs.GetInt("isStage1", 1);



        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            if (randomReward == 0)
            {
                btnx2.SetActive(true);
            }
            else
            {
                btnx2.SetActive(false);
            }
            teamPlayer = PlayerPrefs.GetInt("wcPlayer");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
                }

            }

            buttonWC.SetActive(true);
            buttonExhibition.SetActive(false);
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
            SetupWinOrLoseWC();

        }
        else
        {
            buttonWC.SetActive(false);
            buttonExhibition.SetActive(true);
        }


        dir_top = Loadding.topPlayer - Loadding.topAI;
        text_AmoutMoneyGoal.text = (GameController.goals * 50).ToString();
        text_amoutGoal.text = "+ " + GameController.goals + " Goals:";

        if (Loadding.leftOrRight == 0)
        {
            text_resultGoal.text = GameController.goalsConceded + " - " + GameController.goals;
            if (PlayerPrefs.GetInt("loginFb", 0) == 0)
            {
                flagRight.sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                text_TeamNameRight.text = PlayerPrefs.GetString("NameMyTeam");
            }
            else
            {
                flagRight.sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                text_TeamNameRight.text = PlayerPrefs.GetString("nameFB");
            }

            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                
                flagLeft.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                text_TeamNameLeft.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];
            }
            else
            {
                
                flagLeft.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                text_TeamNameLeft.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];
            }

        }
        else
        {
            text_resultGoal.text = GameController.goals + " - " + GameController.goalsConceded;

            if (PlayerPrefs.GetInt("loginFb", 0) == 0)
            {
                flagLeft.sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                text_TeamNameLeft.text = PlayerPrefs.GetString("NameMyTeam");
            }
            else
            {
                flagLeft.sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                text_TeamNameLeft.text = PlayerPrefs.GetString("nameFB");
            }


            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                flagRight.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                text_TeamNameRight.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];

            }
            else
            {
                flagRight.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                text_TeamNameRight.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];

            }

        }
        SetupScoreStage();


    }

    public void ButtonAddMoney()
    {
        SceneManager.LoadScene("Shop");
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void SetupWinOrLoseWC()
    {
        if (isMatchStage == 5)
        {
            ValueTeamPlayOff[0, 0] = ValueTeam[0, 0];
            ValueTeamPlayOff[1, 0] = ValueTeam[1, 1];

            ValueTeamPlayOff[0, 1] = ValueTeam[0, 2];
            ValueTeamPlayOff[1, 1] = ValueTeam[1, 3];

            ValueTeamPlayOff[0, 2] = ValueTeam[1, 0];
            ValueTeamPlayOff[1, 2] = ValueTeam[0, 1];

            ValueTeamPlayOff[0, 3] = ValueTeam[1, 2];
            ValueTeamPlayOff[1, 3] = ValueTeam[0, 3];

            ValueTeamPlayOff[0, 4] = ValueTeam[0, 4];
            ValueTeamPlayOff[1, 4] = ValueTeam[1, 5];

            ValueTeamPlayOff[0, 5] = ValueTeam[0, 6];
            ValueTeamPlayOff[1, 5] = ValueTeam[1, 7];

            ValueTeamPlayOff[0, 6] = ValueTeam[1, 4];
            ValueTeamPlayOff[1, 6] = ValueTeam[0, 5];

            ValueTeamPlayOff[0, 7] = ValueTeam[1, 6];
            ValueTeamPlayOff[1, 7] = ValueTeam[0, 7];


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcPlayer")
                       || ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[0, j] = GameController.goals;
                            GoalsPlayOff[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                        else if (ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[1, j] = GameController.goals;
                            GoalsPlayOff[0, j] = GameController.goalsConceded;

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
                            GoalsFirtOff[0, j] = GameController.goals;
                            GoalsFirtOff[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                        else if (ValueTeamFirtOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[1, j] = GameController.goals;
                            GoalsFirtOff[0, j] = GameController.goalsConceded;

                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 7)
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
                            GoalsQuarterFinals[0, j] = GameController.goals;
                            GoalsQuarterFinals[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                        else if (ValueTeamQuarterFinals[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[1, j] = GameController.goals;
                            GoalsQuarterFinals[0, j] = GameController.goalsConceded;

                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 8)
        {

            listWinR4.Add(PlayerPrefs.GetInt("listWinR40"));
            listWinR4.Add(PlayerPrefs.GetInt("listWinR41"));
            if (listWinR4[0] == PlayerPrefs.GetInt("wcPlayer"))
            {

                PlayerPrefs.SetInt("Number_Goals_CK0", GameController.goals);
                PlayerPrefs.SetInt("Number_Goals_CK1", GameController.goalsConceded);
            }
            else if (listWinR4[1] == PlayerPrefs.GetInt("wcPlayer"))
            {

                PlayerPrefs.SetInt("Number_Goals_CK0", GameController.goalsConceded);
                PlayerPrefs.SetInt("Number_Goals_CK1", GameController.goals);
            }
        }
    }
    public void SetupScoreStage()
    {

        if (GameController.goals > GameController.goalsConceded)
        {
            if (Menu.mode == (int)Menu.MODE.EXHIBITION)
            {
                int _unlock = PlayerPrefs.GetInt("unlock", 1);
                rewardExhibition = UISelectTeam.instance.priceEntryFee[PlayerPrefs.GetInt("teamAI") - 1] * 2;
                txt_rewardExhibition.text = rewardExhibition.ToString();
                StartCoroutine(SendUpdateCoin2(rewardExhibition));

                if (randomReward == 0)
                {
                    btnx2_exhibition.SetActive(true);
                }
                else
                {
                    btnx2_exhibition.SetActive(true);
                }
                if (UISelectTeam.id == _unlock)
                {
                    _unlock++;
                    PlayerPrefs.SetInt("unlock", _unlock);
                }
            }
            else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                switch (dir_top)
                {
                    case 1:
                        amountMoneyMatch = 200;
                        break;
                    case 2:
                        amountMoneyMatch = 300;
                        break;
                    case 3:
                        amountMoneyMatch = 400;
                        break;
                    case 4:
                        amountMoneyMatch = 500;
                        break;
                    case 5:
                        amountMoneyMatch = 600;
                        break;
                    case 6:
                        amountMoneyMatch = 700;
                        break;
                    case 7:
                        amountMoneyMatch = 800;
                        break;
                    default:
                        amountMoneyMatch = 100;
                        break;

                }
                text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
                text_MoneyTotal.text = ((GameController.goals * 50) + amountMoneyMatch).ToString();

                StartCoroutine(SendUpdateCoin2(((GameController.goals * 50) + amountMoneyMatch)));
                //GetInfoAndUpdate.instance.UpdateCoinSever(((GameController.goals * 50) + amountMoneyMatch));
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage <= 4)
            {
                ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1] += 3;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcPlayer") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1]);
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage == 8)
            {
                //FBManager.instance.logEventChampion();
                PlayerPrefs.SetInt("champion", 1);
            }

            text_YouWin.text = "YOU WIN !";

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
        }
        else if (GameController.goals == GameController.goalsConceded)
        {
            if (Menu.mode == (int)Menu.MODE.EXHIBITION)
            {
                if (randomReward == 0)
                {
                    btnx2_exhibition.SetActive(true);
                }
                else
                {
                    btnx2_exhibition.SetActive(true);
                }
                rewardExhibition = UISelectTeam.instance.priceEntryFee[PlayerPrefs.GetInt("teamAI") - 1];
                txt_rewardExhibition.text = rewardExhibition.ToString();
                //GetInfoAndUpdate.instance.UpdateCoinSever(rewardExhibition);
                StartCoroutine(SendUpdateCoin2(rewardExhibition));

            }
            else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                amountMoneyMatch = 100;
                text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
                text_MoneyTotal.text = ((GameController.goals * 50) + amountMoneyMatch).ToString();

                StartCoroutine(SendUpdateCoin2(((GameController.goals * 50) + amountMoneyMatch)));
            }

            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage <= 4)
            {
                ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] += 1;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);

                ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1] += 1;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcPlayer") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1]);
            }



            text_YouWin.text = "DRAW !";
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }

        }
        else
        {
            if (Menu.mode == (int)Menu.MODE.EXHIBITION)
            {
                rewardExhibition = 0;
                txt_rewardExhibition.text = rewardExhibition.ToString();
                btnx2_exhibition.SetActive(false);
                StartCoroutine(SendUpdateCoin2(rewardExhibition));

            }
            else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                amountMoneyMatch = 0;
                btnx2.SetActive(false);
                text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
                text_MoneyTotal.text = ((GameController.goals * 50) + amountMoneyMatch).ToString();

                StartCoroutine(SendUpdateCoin2(((GameController.goals * 50) + amountMoneyMatch)));
            }

            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage <= 4)
            {
                ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] += 3;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage >= 5 && isMatchStage <= 8)
            {
                PlayerPrefs.SetInt("WinOrLose", 1);
            }
            text_YouWin.text = "YOU LOSE !";


            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchLost.Play();
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

    // Update is called once per frame
    void Update()
    {
        money.text = PlayerPrefs.GetInt("money").ToString();

        if (GetInfoAndUpdate.isConnect == true)
        {
            panelDisconnect.SetActive(false);
        }
        else
        {
            panelDisconnect.SetActive(true);
        }
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            if (PlayerPrefs.GetInt("isStage1") <= 4)
            {
                ListSortScore();
                if (PlayerPrefs.GetInt("isStage1") == 4)
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
        //GetInfoAndUpdate.instance.GetInfoSever();
    }

    public void ButtonHome()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }
    public void ButtonNewGame()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("SelectTeam");
    }
    public void ButtonReMatch()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        int _money = PlayerPrefs.GetInt("money");
        if (_money >= UISelectTeam.instance.priceEntryFee[PlayerPrefs.GetInt("teamAI") - 1])
        {
            
            StartCoroutine(SendUpdateCoin(-UISelectTeam.instance.priceEntryFee[PlayerPrefs.GetInt("teamAI") - 1]));
           
        }
        else
        {
            panelNotEnoughMoney.SetActive(true);
        }

    }
    public void ButtonExit()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        panelNotEnoughMoney.SetActive(false);
    }
    public void ButtonContinious()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("WorldCup");
    }

    public void ButtonX2()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdmodController.Instance.showRewardAdsVideo();

    }

    IEnumerator SendUpdateCoin(int coin)
    {

        GetInfoAndUpdate.isConnect = true;
        Debug.Log("22222222222222222222222222");
        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money + coin);
        SceneManager.LoadScene("Game");
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161

    }

    IEnumerator SendUpdateCoin2(int coin)
    {
        isConnect = true;
        Debug.Log("22222222222222222222222222");
        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money + coin);
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

    IEnumerator SendUpdateUser()
    {
        GetInfoAndUpdate.isConnect = true;
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161

    }
}

