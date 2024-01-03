using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInfoAndUpdate : MonoBehaviour
{
    public static GetInfoAndUpdate instance;
    public UserInfo userInfo, userInfoFB;
    public static bool isConnect = true;
    public string jsonUserNameID;
    // Use this for initialization
    private void Awake()
    {
        //isConnect = true;
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SendUpdateCoin(int coin)
    {
        isConnect = true;
        Debug.Log("22222222222222222222222222");
        int _money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", _money + coin);
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

    IEnumerator getInfoUser()
    {
        //if (PlayerPrefs.GetInt("loginFb", 0) == 0)
        //{
        //     jsonUserNameID = PlayerPrefs.GetString("userID");
        //}
        //else
        //{
        //     jsonUserNameID = PlayerPrefs.GetString("userNameFB");
        //}
        //WWW www = new WWW("http://35.198.197.119:8080/soccer/userinfo?userName=" + jsonUserNameID);
        //yield return www;

        //Debug.Log(www.error);
        //userInfo = JsonUtility.FromJson<UserInfo>(www.text);
        //Debug.Log(userInfo.rank);
        //if (www.error == null)
        //{
            isConnect = true;
            PlayerPrefs.SetInt("money", userInfo.coin);
        //}
        //else
        //{
        //    isConnect = false;
        //    Debug.Log("abccadfasdfasdf");
        //}
        yield return null; // Thêm dòng này để giải quyết lỗi CS0161
    }

   
    public void UpdateCoinSever(int coin)
    {
        StartCoroutine(SendUpdateCoin(coin));
    }
    public void GetInfoSever()
    {
        StartCoroutine(getInfoUser());
    }
}
