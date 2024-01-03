using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectTeam : MonoBehaviour
{
    public static SelectTeam instance;

    public string[] nameTeam;
    public Sprite[] flagTeam;
    public Sprite[] star;
    public int[] Top_Star;
    public Sprite[]
    Player_1, Player_2, Player_3, Player_4, Player_5, Player_6, Player_7, Player_8, Player_9, Player_10,
    Player_11, Player_12, Player_13, Player_14, Player_15, Player_16, Player_17, Player_18, Player_19, Player_20,
    Player_21, Player_22, Player_23, Player_24, Player_25, Player_26, Player_27, Player_28, Player_29, Player_30, Player_31, Player_32;
    public string[]
    Name_Player_1, Name_Player_2, Name_Player_3, Name_Player_4, Name_Player_5, Name_Player_6, Name_Player_7, Name_Player_8, Name_Player_9, Name_Player_10,
    Name_Player_11, Name_Player_12, Name_Player_13, Name_Player_14, Name_Player_15, Name_Player_16, Name_Player_17, Name_Player_18,
    Name_Player_19, Name_Player_20, Name_Player_21, Name_Player_22, Name_Player_23, Name_Player_24,
    Name_Player_25, Name_Player_26, Name_Player_27, Name_Player_28, Name_Player_29, Name_Player_30, Name_Player_31, Name_Player_32;
    //public static int topPlayer, topAI;
    public Sprite[] sp_Stadiums;
    public Sprite[] sp_shoe, sp_Body;
    public Sprite[] MyTeamHeadPlayer, MyTeamBodyPlayer, MyTeamFlagPlayer;

    public float[] Shoot_x, Shoot_y, Jump, Speed, addForceHead_x;
    public Sprite[] sp_ball;
    // Use this for initialization

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else DestroyImmediate(gameObject);
    }

    private void Start()
    {



    }




}
