using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public float timeKeepBall;
    public int isMatchStage;
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
    public int disMatch;
    public Text text_GoalsLeft, text_GoalsRight;
    public static int goals, goalsConceded;

    public static GameController Instance { get; private set; }
    //public Button btnSetting;
    public GameObject
        targetLeft,
        targetRight,
        ball,
        playerComputer,
        player;

    [Space]
    public Character2DController playerController;
    public AIPlayer aiController;

    [Space]
    [Header("Starting position of the ball")]
    [Space]
    public Vector3 posCenter;

    public bool Scored { get; private set; }
    public bool EndMatch { get; private set; }

    public float matchTime;
    public GameObject panelGoldGoal, textGoldGoal;
    public bool isShowPanelGoldGoal;
    public GameObject img_timeKeepBall;
    public Sprite[] time123;
    public GameObject img_whistle;
    public GameObject panelRelive;

    private void Update()
    {
        timeKeepBall += Time.deltaTime;
        if (timeKeepBall > 4)
        {
            timeKeepBall = 3;
            img_timeKeepBall.SetActive(false);

        }

        if (timeKeepBall >= 0)
        {
            img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[0];
        }
        if (timeKeepBall >= 1)
        {
            img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[1];
        }
        if (timeKeepBall >= 2)
        {
            img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[2];
        }
        if (timeKeepBall >= 3)
        {
            img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[3];

        }


    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        img_whistle.SetActive(false);
        isShowPanelGoldGoal = false;
        panelGoldGoal.SetActive(false);
        textGoldGoal.SetActive(false);
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
            isMatchStage++;
            PlayerPrefs.SetInt("isStage1", isMatchStage);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
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
            if (Loadding.relive == 1)
            {
                SetupWinOrLoseWC();
            }

        }
        disMatch = PlayerPrefs.GetInt("disMatch", 0);
        ball.SetActive(false);
        player.SetActive(false);
        playerComputer.SetActive(false);
        //btnSetting.interactable = false;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }


        goals = 0;
        goalsConceded = 0;
        StartGame();
        SetupPositionPlayer();
        StartCoroutine(SetPlayerTrue());

    }


    public void SetupPositionPlayer()
    {
        if (Loadding.leftOrRight == 0)
        {

            player.transform.position = new Vector2(5f, -2.5f);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            playerComputer.transform.position = new Vector2(-5f, -2.5f);
            playerComputer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        else
        {

            player.transform.position = new Vector2(-5f, -2.5f);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            playerComputer.transform.position = new Vector2(5f, -2.5f);
            playerComputer.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void StartGame()
    {
        Reset();
    }

    private void Reset()
    {

        SetupPositionPlayer();
        EndMatch = Scored = false;
        timeKeepBall = 0f;
        goals = goalsConceded = 0;
        text_GoalsRight.text = goals.ToString();
        text_GoalsLeft.text = goalsConceded.ToString();

        ball.transform.position = posCenter;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);


    }
    IEnumerator waitLoadFullTime()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.refereeEndGame.Play();
        }
        img_whistle.SetActive(true);
        yield return new WaitForSeconds(2f);
        img_whistle.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FullTime");

    }
    IEnumerator SetPlayerTrue()
    {
        yield return new WaitForSeconds(3f);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.refereeStartGame.Play();
        }
        yield return new WaitForSeconds(1f);
        ball.SetActive(true);
        player.SetActive(true);
        playerComputer.SetActive(true);
        //btnSetting.interactable = true;

        StartCoroutine("RunMatchTime");
    }
    public void ButtonWatchVideo()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdmodController.Instance.showRewardAdsVideo();
        AdmodController.numberReward = 3;
    }
    public void ButtonExitWatchVideo()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelRelive.SetActive(false);
        Loadding.relive--;
        Time.timeScale = 1;
        ball.SetActive(true);
        player.SetActive(true);
        playerComputer.SetActive(true);
    }
    public void ButtonYesGoldGoal()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        timeKeepBall = 0;
        img_timeKeepBall.SetActive(true);
        panelGoldGoal.SetActive(false);
        textGoldGoal.SetActive(true);
        isShowPanelGoldGoal = true;
        Time.timeScale = 1;
        StartCoroutine(WaitGoalGold());
    }
    IEnumerator WaitGoalGold()
    {
        yield return new WaitForSeconds(3f);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.refereeStartGame.Play();
        }
        yield return new WaitForSeconds(1.0f);
        img_timeKeepBall.SetActive(false);
        panelRelive.SetActive(false);
        ball.SetActive(true);
        player.SetActive(true);
        playerComputer.SetActive(true);
        ball.transform.position = posCenter;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        SetupPositionPlayer();
    }
    public void GoldGoal()
    {
        ball.SetActive(false);
        player.SetActive(false);
        playerComputer.SetActive(false);
        Time.timeScale = 0;
    }
    private IEnumerator RunMatchTime()
    {

        var time = matchTime;
        UIManager.Instance.timeText.text = time + "";
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (time > 0)
            {
                time--;

            }
            UIManager.Instance.timeText.text = time + "";

            if (time == 0)
            {
                if (Menu.mode == (int)Menu.MODE.EXHIBITION)
                {

                    StartCoroutine(waitLoadFullTime());
                    break;
                }
                else
                {
                    if (goals < goalsConceded)
                    {
                        if (isMatchStage <= 4)
                        {
                            StartCoroutine(waitLoadFullTime());
                            break;
                        }
                        else
                        {
                            if (Loadding.relive == 1)
                            {
                                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                                {
                                    SoundManager.Instance.refereeStartGame.Play();
                                }
                                panelRelive.SetActive(true);
                                GoldGoal();
                            }
                            else
                            {
                                StartCoroutine(waitLoadFullTime());
                                break;
                            }
                        }

                    }
                    else if (goals > goalsConceded)
                    {
                        StartCoroutine(waitLoadFullTime());
                        break;
                    }
                    else
                    {
                        if (isMatchStage <= 4)
                        {
                            StartCoroutine(waitLoadFullTime());
                            break;
                        }
                        else
                        {
                            if (isShowPanelGoldGoal == false)
                            {
                                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                                {
                                    SoundManager.Instance.refereeStartGame.Play();
                                }
                                panelGoldGoal.SetActive(true);
                                GoldGoal();
                            }
                            if (goals != goalsConceded)
                            {
                                StartCoroutine(waitLoadFullTime());
                                break;
                            }
                        }
                    }
                }




            }


        }

        if (goals < goalsConceded)
        {
            aiController.anim.SetBool(aiController.CelebrateHash, true);
            AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);

        }
        else if (goals == goalsConceded)
        {

            playerController.Anim.SetBool(playerController.CelebrateHash, true);
            aiController.anim.SetBool(aiController.CelebrateHash, true);
            AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);

        }
        else
        {

            playerController.Anim.SetBool(playerController.CelebrateHash, true);

        }

        EndMatch = true;

    }



    public void ScoredAgainst(bool netOfAIPlayer)
    {

        if (!EndMatch)
        {
            Scored = true;
            if (netOfAIPlayer)
            {
                goals++;
                playerController.Anim.SetBool(playerController.CelebrateHash, true);
            }
            else
            {
                goalsConceded++;
                aiController.anim.SetBool(aiController.CelebrateHash, true);

            }

            if (Loadding.leftOrRight == 0)
            {
                text_GoalsRight.text = goals.ToString();
                text_GoalsLeft.text = goalsConceded.ToString();
            }
            else
            {
                text_GoalsRight.text = goalsConceded.ToString();
                text_GoalsLeft.text = goals.ToString();
            }

            StartCoroutine(ContinueMatch(netOfAIPlayer));
        }
    }

    private IEnumerator ContinueMatch(bool netOfAIPlayer)
    {
        yield return new WaitForSeconds(3f);
        //Ball.instance.ischeckTriggerRight = false;
        //Ball.instance.ischeckTriggerLeft = false;
        playerController.Anim.SetBool(playerController.CelebrateHash, false);
        aiController.anim.SetBool(aiController.CelebrateHash, false);

        if (!EndMatch)
        {
            SetupPositionPlayer();
            timeKeepBall = 0;

            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            if (netOfAIPlayer)
            {
                if (Loadding.leftOrRight == 0)
                {
                    ball.transform.position = posCenter;
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50 - 1.5f * matchTime, 150));
                }
                else
                {
                    ball.transform.position = posCenter;
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(50 + 1.5f * matchTime, 150));
                }
            }
            else
            {
                if (Loadding.leftOrRight == 0)
                {
                    ball.transform.position = posCenter;
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(50 + 1.5f * matchTime, 150));

                }
                else
                {
                    ball.transform.position = posCenter;
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50 - 1.5f * matchTime, 150));
                }
            }


            Scored = false;

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.refereeStartGame.Play();
            }
        }
    }

    public void RestartMatch()
    {
        Reset();
        playerController.Anim.SetBool(playerController.CelebrateHash, false);
        aiController.anim.SetBool(aiController.CelebrateHash, false);
        StartCoroutine("RunMatchTime");
    }

    public void Surrender()
    {
        StopCoroutine("RunMatchTime");
        goals = 3;
        goalsConceded = 0;
        if (Loadding.leftOrRight == 0)
        {
            text_GoalsRight.text = goals.ToString();
            text_GoalsLeft.text = goalsConceded.ToString();
        }
        else
        {
            text_GoalsRight.text = goalsConceded.ToString();
            text_GoalsLeft.text = goals.ToString();
        }

        aiController.anim.SetBool(aiController.CelebrateHash, true);
        AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);


        Scored = false;
        EndMatch = true;
        img_whistle.SetActive(true);
        StartCoroutine(waitLoadFullTime());
    }

    private void OnApplicationPause(bool pause)
    {
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            disMatch = 1;
            PlayerPrefs.SetInt("disMatch", disMatch);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            disMatch = 0;
            PlayerPrefs.SetInt("disMatch", disMatch);
        }
    }
    public void SetupWinOrLoseWC()
    {
        if (isMatchStage <= 4)
        {
            for (int i = 0; i < 15; i++)
            {
                random[i] = Random.Range(0, 10);

                if (random[i] == 1 || random[i] == 0)
                {

                    ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1] += 1;
                    ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1] += 1;
                    PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer1[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1]);
                    PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer2[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1]);

                }

                else
                {
                    if (SetupMatchWC.instance.TopPlayer1[i] <= SetupMatchWC.instance.TopPlayer2[i])
                    {
                        ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1] += 3;
                        PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer1[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1]);
                    }
                    else
                    {
                        ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1] += 3;
                        PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer2[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1]);
                    }
                }
                Debug.Log("asfasfasdfsfasdfRandommmmmm  " + SetupMatchWC.instance.ValuePlayer2[i]);
            }
        }

        else if (isMatchStage == 5)
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

                    if (ValueTeamPlayOff[i, j] != PlayerPrefs.GetInt("wcPlayer")
                       && ValueTeamPlayOff[i, j] != PlayerPrefs.GetInt("wcAI"))
                    {
                        if (SelectTeam.instance.Top_Star[ValueTeamPlayOff[0, j] - 1] <= SelectTeam.instance.Top_Star[ValueTeamPlayOff[1, j] - 1])
                        {
                            GoalsPlayOff[0, j] = Random.Range(4, 9);
                            GoalsPlayOff[1, j] = Random.Range(0, 5);
                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                        else
                        {
                            GoalsPlayOff[0, j] = Random.Range(0, 5);
                            GoalsPlayOff[1, j] = Random.Range(4, 9);
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

                    if (ValueTeamFirtOff[i, j] != PlayerPrefs.GetInt("wcPlayer") && ValueTeamFirtOff[i, j] != PlayerPrefs.GetInt("wcAI"))
                    {
                        if (SelectTeam.instance.Top_Star[ValueTeamFirtOff[0, j] - 1] <= SelectTeam.instance.Top_Star[ValueTeamFirtOff[1, j] - 1])
                        {
                            GoalsFirtOff[0, j] = Random.Range(4, 9);
                            GoalsFirtOff[1, j] = Random.Range(0, 5);
                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                        else
                        {
                            GoalsFirtOff[0, j] = Random.Range(0, 5);
                            GoalsFirtOff[1, j] = Random.Range(4, 9);
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

                    if (ValueTeamQuarterFinals[i, j] != PlayerPrefs.GetInt("wcPlayer") && ValueTeamQuarterFinals[i, j] != PlayerPrefs.GetInt("wcAI"))
                    {
                        if (SelectTeam.instance.Top_Star[ValueTeamQuarterFinals[0, j] - 1] <= SelectTeam.instance.Top_Star[ValueTeamQuarterFinals[1, j] - 1])
                        {
                            GoalsQuarterFinals[0, j] = Random.Range(4, 9);
                            GoalsQuarterFinals[1, j] = Random.Range(0, 5);
                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                        else
                        {
                            GoalsQuarterFinals[0, j] = Random.Range(0, 5);
                            GoalsQuarterFinals[1, j] = Random.Range(4, 9);
                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                    }
                }
            }
        }

    }
}
