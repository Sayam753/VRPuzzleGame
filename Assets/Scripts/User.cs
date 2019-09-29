using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class User
{
    public string userName ;
    public int userScore;
    public User()
    {
        userName = siging_up.PlayerName;
        userScore= siging_up.PlayerScore;
    }
}