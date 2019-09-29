using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;
using System;
using FullSerializer;
using System.Linq;
public class leaderboard : MonoBehaviour
{
    public Text top_five;
    public static fsSerializer serializer = new fsSerializer();
    void Start()
    {
        top_five.text = "";
        RestClient.Get("https://treasure-hunt-51ca4.firebaseio.com/"+"users/"+".json").Then(response => 
        {
            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string,User> users = null;
            serializer.TryDeserialize(userData,ref users);
            Dictionary<string,int> s = new Dictionary<string,int>();
            foreach(var user in users.Values)
            {
                s.Add(user.userName,user.userScore);
            }
            var sortedDict = (from entry in s orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value).Take(5);
            foreach(var x in sortedDict)
            {
                top_five.text = top_five.text + x + "\n";
            }
        });
        Invoke("Start",5f);
    }

    
    public void move_to_ar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
