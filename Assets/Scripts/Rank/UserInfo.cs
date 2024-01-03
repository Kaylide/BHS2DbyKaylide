using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UserInfo
{

    public string userName;
    public string nickName;
    public string fbToken;
    public string avatarUrl;
    public int coin;
    public int win;
    public int lose;
    public int draw;
    public int point;
    public int rank;
    public int reward;
    public string access_token;
}

[Serializable]
public class UserRank
{
    public UserInfo[] Items;
    public UserInfo me;
    public int time;
}
