using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInfoRank : MonoBehaviour
{

    public static GetInfoRank instance;
    public Text txt_rank, txt_win, txt_lose, txt_draw, txt_points, txt_prize, txt_nameTeam;
    public Image img_flag, img_rankColor;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
