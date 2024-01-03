using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHead_Shoe : MonoBehaviour
{
    public static GetHead_Shoe instance;
    public GameObject shoePlayer, flagPlayer;
    public GameObject shoeAI, flagAI;
    public Text nameTeamPlayer, nameTeamAI;
    public SpriteRenderer headPlayer, headAI, bodyPlayer, bodyAI;
    public int[] indexPlayer = new int[32];
    public int teamPlayer, teamAI,randomShoePlayer;

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
        int id = PlayerPrefs.GetInt("IDPlayer", 0);
        for (int i = 0; i < 32; i++)
        {
            indexPlayer[i] = PlayerPrefs.GetInt("index" + i, 0);
        }
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamPlayer = PlayerPrefs.GetInt("wcPlayer");
            teamAI = PlayerPrefs.GetInt("wcAI");
        }
        else
        {
            teamPlayer = PlayerPrefs.GetInt("teamPlayer");
            teamAI = PlayerPrefs.GetInt("teamAI");
        }
        Debug.Log("1234565675754asdfasdfasfasdfasfasfsafsdfsdfasdfasdfasdf12312234234 " + PlayerPrefs.GetInt("wcAI") + PlayerPrefs.GetInt("wcPlayer"));

        randomShoePlayer = Random.Range(0, 3);
        int randomShoeAI = Random.Range(0, 3);
        shoePlayer.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoePlayer];
        if (randomShoeAI == randomShoePlayer)
        {
            if (randomShoeAI == 0)
            {
                shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI + 1];
            }
            else
            {
                shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI - 1];
            }
        }
        else
        {
            shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI];
        }

        GetUIPlayer();
        GetUIAI();

        headPlayer.sprite = SelectTeam.instance.MyTeamHeadPlayer[id];
        bodyPlayer.sprite = SelectTeam.instance.MyTeamBodyPlayer[0];
        int rd = Random.Range(0, SetupStatium.instance.listAI.Count);
        headAI.sprite = SetupStatium.instance.listAI[rd];
        bodyAI.sprite = SelectTeam.instance.sp_Body[teamAI - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetUIPlayer()
    {
        //if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        //{

            if (Loadding.leftOrRight == 0)
            {
                if (PlayerPrefs.GetInt("loginFb", 0) == 0)
                {
                    flagPlayer.GetComponent<Image>().sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                    nameTeamPlayer.text = PlayerPrefs.GetString("NameMyTeam");
                }
                else
                {
                    flagPlayer.GetComponent<Image>().sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                    nameTeamPlayer.text = PlayerPrefs.GetString("nameFB");
                }

            }
            else
            {
                if (PlayerPrefs.GetInt("loginFb", 0) == 0)
                {
                    flagAI.GetComponent<Image>().sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
                    nameTeamAI.text = PlayerPrefs.GetString("NameMyTeam");
                }
                else
                {
                    flagAI.GetComponent<Image>().sprite = EditTeam.instance.img_FlagMyTeam.sprite;
                    nameTeamAI.text = PlayerPrefs.GetString("nameFB");
                }

            }
        //}
        //else
        //{
        //    if (Loadding.leftOrRight == 0)
        //    {
        //        flagPlayer.GetComponent<Image>().sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
        //        nameTeamPlayer.text = PlayerPrefs.GetString("NameMyTeam");
        //    }
        //    else
        //    {
        //        flagAI.GetComponent<Image>().sprite = SelectTeam.instance.MyTeamFlagPlayer[PlayerPrefs.GetInt("indexFlagMyTeam")];
        //        nameTeamAI.text = PlayerPrefs.GetString("NameMyTeam");
        //    }
        //}


    }
    public void GetUIAI()
    {
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {

            if (Loadding.leftOrRight == 0)
            {
                
                flagAI.GetComponent<Image>().sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                nameTeamAI.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];
            }
            else
            {
                flagPlayer.GetComponent<Image>().sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("wcAI") - 1];
                nameTeamPlayer.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("wcAI") - 1];
            }
        }
        else
        {
            if (Loadding.leftOrRight == 0)
            {
                flagAI.GetComponent<Image>().sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                nameTeamAI.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];
            }
            else
            {
                flagPlayer.GetComponent<Image>().sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt("teamAI") - 1];
                nameTeamPlayer.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt("teamAI") - 1];
            }
        }


    }

}
