using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loadding : MonoBehaviour
{
    public static int leftOrRight, topPlayer, topAI;
    public Image flagLeft, flagRight, loadding;
    public Text textMode, teamLeft, teamRight, text_Loadding;
    public int teamPlayer, teamAI;
    public static int relive;
    // Use this for initialization
    void Start()
    {
        relive = 1;
        leftOrRight = Random.Range(0, 2);
        loadding.fillAmount = 0;
        int isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
        if (Menu.mode == (int)Menu.MODE.EXHIBITION)
        {
            textMode.text = "Friendly";
        }
        else
        {
            switch (isMatchStage)
            {
                case 1:
                case 2:
                case 3:
                    textMode.text = "Group Stage";
                    break;
                case 4:
                    textMode.text = "Round Of 16";
                    break;

                case 5:
                    textMode.text = "Quarter Finals";
                    break;
                case 6:
                    textMode.text = "Semifinals";
                    break;
                case 7:
                    textMode.text = "Final";
                    break;

            }
        }
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt("wcAI");
            teamPlayer = PlayerPrefs.GetInt("wcPlayer");
        }
        else
        {
            teamAI = PlayerPrefs.GetInt("teamAI");
            teamPlayer = PlayerPrefs.GetInt("teamPlayer");
        }

        if (leftOrRight == 0)
        {
            if (PlayerPrefs.GetInt("loginFb", 0) == 0)
            {
                flagRight.sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                teamRight.text = PlayerPrefs.GetString("NameMyTeam");
            }
            else
            {
                flagRight.sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                teamRight.text = PlayerPrefs.GetString("nameFB");
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                

                flagLeft.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                teamLeft.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];
            }
            else
            {
                
                flagLeft.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                teamLeft.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("loginFb", 0) == 0)
            {
                flagLeft.sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                teamLeft.text = PlayerPrefs.GetString("NameMyTeam");
            }
            else
            {
                flagLeft.sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                teamLeft.text = PlayerPrefs.GetString("nameFB");
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                flagRight.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                teamRight.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];

            }
            else
            {
                flagRight.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                teamRight.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];

            }

        }
        StartCoroutine(LoadSceneGame());

    }

    // Update is called once per frame
    void Update()
    {

        text_Loadding.text = ((int)(loadding.fillAmount * 100)) + " %";
        SetTopAI();
        SetTopPlayer();


    }
    IEnumerator LoadSceneGame()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadAsync());
    }
    IEnumerator LoadAsync()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(7);
        while (!operation.isDone)
        {
           
            float progres = Mathf.Clamp01(operation.progress);
            loadding.fillAmount = progres + 0.1f;
            if (loadding.fillAmount >= 1)
            {
                loadding.fillAmount = 1;
            }
            text_Loadding.text = ((int)(loadding.fillAmount * 100)) + " %";

            yield return null;

        }

    }

    public void SetTopPlayer()
    {
        topPlayer = 8;
    }
    public void SetTopAI()
    {

        switch (teamAI)
        {
            case 3:
            case 11:
            case 12:
                topAI = 1;
                break;
            case 1:
            case 4:
            case 25:
                topAI = 2;
                break;
            case 6:
            case 9:
            case 27:
            case 31:
                topAI = 3;
                break;
            case 5:
            case 24:
            case 28:
            case 14:
            case 26:
            case 29:
            case 18:
            case 21:
            case 15:
                topAI = 4;
                break;
            case 16:
            case 10:
            case 23:
            case 22:
            case 8:
            case 7:
            case 20:
                topAI = 5;
                break;
            case 32:
            case 13:
                topAI = 6;
                break;
            case 30:
            case 2:
                topAI = 7;
                break;
            case 17:
            case 19:
                topAI = 8;
                break;
        }
    }
}
