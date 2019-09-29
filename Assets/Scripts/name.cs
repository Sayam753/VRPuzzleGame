using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class name : MonoBehaviour
{
    public Text username;
    void Update()
    {
        username.text = "Username : "+ siging_up.PlayerName;
    }
}
