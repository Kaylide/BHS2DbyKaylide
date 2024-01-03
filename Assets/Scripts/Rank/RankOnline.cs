using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RankOnline : MonoBehaviour
{
    public static RankOnline instance;
    public string jsonUserNameID;
    public GameObject panelDisconnect;
    public GameObject rank;
    public UserRank userRank;
    public GameObject Rank_Type_1, Rank_Type_2;
    public Text txt_Time;
    public float _time;
    public int timeCurrent, hh, mm, ss;
    public bool tessafasdfasfdasdf;
    public string _HH, _MM, _SS;
    // Use this for initialization
    private void OnApplicationFocus(bool focus)
    {
        Application.LoadLevel("Rank");
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        _time = 0;
        StartCoroutine(getRank());
        if (PlayerPrefs.GetInt("loginFb", 0) == 0)
        {
            StartCoroutine(SendUpdateUser());
        }



    }

    // Update is called once per frame
    void Update()
    {
        //tessafasdfasfdasdf = GetInfoAndUpdate.isConnect;
        if (timeCurrent < 0)
        {
            //timeCurrent = 600;
            txt_Time.text = "00" + ":" + "00" + ":" + "00";
        }
        else
        {
            _time += Time.deltaTime;
            timeCurrent = (int)(userRank.time - _time);
            hh = (int)(timeCurrent / 3600);
            mm = (int)((timeCurrent - hh * 3600) / 60);
            ss = (int)(timeCurrent - hh * 3600 - mm * 60);


            if (hh < 10) _HH = "0" + hh;
            else _HH = hh.ToString();

            if (mm < 10) _MM = "0" + mm;
            else _MM = mm.ToString();

            if (ss < 10) _SS = "0" + ss;
            else _SS = ss.ToString();

            txt_Time.text = _HH + ":" + _MM + ":" + _SS;

        }
        if (GetInfoAndUpdate.isConnect == true)
        {

            panelDisconnect.SetActive(false);
        }
        else
        {
            panelDisconnect.SetActive(true);
            Debug.Log("afasdfasdfasdfsdfsadfasfasf");
        }
    }

    IEnumerator getRank()
    {
        if (PlayerPrefs.GetInt("loginFb", 0) == 0)
        {
            jsonUserNameID = PlayerPrefs.GetString("userID");
        }
        else
        {
            jsonUserNameID = PlayerPrefs.GetString("userNameFB");
        }
        WWW www = new WWW("http://35.198.197.119:8080/soccer/user_rank?userName=" + jsonUserNameID + "&from=0&limit=30");
        yield return www;

        Debug.Log(www.text);
        userRank = JsonUtility.FromJson<UserRank>(www.text);

        Debug.Log(userRank.Items.Length);
        if (www.error == null)
        {
            GetInfoAndUpdate.isConnect = true;
            timeCurrent = userRank.time;
            for (int i = 0; i < userRank.Items.Length; i++)
            {
                if (i % 2 == 0)
                {
                    GameObject _rank1 = Instantiate(Rank_Type_1) as GameObject;
                    _rank1.transform.parent = rank.transform;
                }
                else
                {
                    GameObject _rank2 = Instantiate(Rank_Type_2) as GameObject;
                    _rank2.transform.parent = rank.transform;
                }


            }

            for (int i = 0; i < userRank.Items.Length; i++)
            {
                GameObject[] _rank = GameObject.FindGameObjectsWithTag("Rank");

                Debug.Log(userRank.Items[1].rank + 1);
                _rank[i].GetComponent<GetInfoRank>().txt_rank.text = (userRank.Items[i].rank + 1) + ".";
                _rank[i].GetComponent<GetInfoRank>().txt_nameTeam.text = userRank.Items[i].nickName;
                _rank[i].GetComponent<GetInfoRank>().txt_win.text = userRank.Items[i].win.ToString();
                _rank[i].GetComponent<GetInfoRank>().txt_lose.text = userRank.Items[i].lose.ToString();
                _rank[i].GetComponent<GetInfoRank>().txt_draw.text = userRank.Items[i].draw.ToString();
                _rank[i].GetComponent<GetInfoRank>().txt_points.text = userRank.Items[i].point.ToString();
                _rank[i].GetComponent<GetInfoRank>().txt_prize.text = userRank.Items[i].reward.ToString();




                if (userRank.Items[i].avatarUrl == "ava1")
                {
                    _rank[i].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[0];
                }
                else if (userRank.Items[i].avatarUrl == "ava2")
                {
                    _rank[i].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[1];
                }
                else if (userRank.Items[i].avatarUrl == "ava3")
                {
                    _rank[i].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[2];
                }
                else
                {
                    WWW www1 = new WWW(userRank.Items[i].avatarUrl);
                    yield return www1;
                    _rank[i].GetComponent<GetInfoRank>().img_flag.sprite
                           = Sprite.Create(www1.texture, new Rect(0, 0, www1.texture.width, www1.texture.height), new Vector2(0, 0));

                }

            }
            Debug.Log(userRank.me.rank);
            if (userRank.me.rank < 0 || userRank.me.rank > userRank.Items.Length -1)
            {

                GameObject _rankMe = Instantiate(Rank_Type_1) as GameObject;
                _rankMe.transform.parent = rank.transform;
                GameObject[] _rank = GameObject.FindGameObjectsWithTag("Rank");
                if (userRank.me.rank < 999)
                {
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_rank.text = (userRank.me.rank + 1) + ".";
                }
                else
                {
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_rank.text = "...";
                }

                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_nameTeam.text = userRank.me.nickName;
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_win.text = userRank.me.win.ToString();
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_lose.text = userRank.me.lose.ToString();
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_draw.text = userRank.me.draw.ToString();
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_points.text = userRank.me.point.ToString();
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().txt_prize.text = userRank.me.reward.ToString();
                _rank[userRank.Items.Length].GetComponent<GetInfoRank>().img_rankColor.color = Color.green;


                if (userRank.me.avatarUrl == "ava1")
                {
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[0];
                }
                else if (userRank.me.avatarUrl == "ava2")
                {
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[1];
                }
                else if (userRank.me.avatarUrl == "ava3")
                {
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().img_flag.sprite = SelectTeam.instance.MyTeamFlagPlayer[2];
                }

                else
                {
                    WWW www1 = new WWW(userRank.me.avatarUrl);
                    yield return www1;
                    _rank[userRank.Items.Length].GetComponent<GetInfoRank>().img_flag.sprite
                           = Sprite.Create(www1.texture, new Rect(0, 0, www1.texture.width, www1.texture.height), new Vector2(0, 0));

                }


            }
            else
            {

                GameObject[] _rank123 = GameObject.FindGameObjectsWithTag("Rank");
                for (int i = 0; i < userRank.Items.Length; i++)
                {

                    if (userRank.me.userName == userRank.Items[i].userName)
                    {
                        _rank123[i].GetComponent<GetInfoRank>().img_rankColor.color = Color.green;
                    }
                    else
                    {
                        _rank123[i].GetComponent<GetInfoRank>().img_rankColor.color = Color.white;
                    }


                }

            }

        }

        else
        {
            GetInfoAndUpdate.isConnect = false;
        }


    }

    IEnumerator SendUpdateUser()
    {
        WWWForm form = new WWWForm();

        form.AddField("userName", PlayerPrefs.GetString("userID"));
        form.AddField("avatar", "ava" + (PlayerPrefs.GetInt("indexFlagMyTeam", 0) + 1));


        UnityWebRequest www = UnityWebRequest.Post("http://35.198.197.119:8080/soccer/update_user", form);
        yield return www.Send();
        if (www.responseCode == 200)
        {

            Debug.Log("22222222222222222222222222");

        }
        else
        {

            Debug.Log("11111111111111111111111");
        }
    }
    public void ButtonExit()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        Application.LoadLevel("Menu");
    }

    public void ExitDisconnect()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelDisconnect.SetActive(false);
        Application.LoadLevel("Rank");
        GetInfoAndUpdate.instance.GetInfoSever();
    }
}



