using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;
using Proyecto26;
using FullSerializer;

public class siging_up : MonoBehaviour
{
    //private string Authkey = "AIzaSyDh2hhP7ysuh4_VLYNmNKNEv8_crIfGvPI"; 
    private string databaseURL =  "https://treasure-hunt-51ca4.firebaseio.com/users/";
    public InputField Nametext;
    //public Text scoretext;
    public static string PlayerName;
    public static int PlayerScore=0;
    // Start is called before the first frame update
    public void create_new_user()
    {
        PostToDataBase();
    }
    private void PostToDataBase()
    {
        PlayerName = Nametext.text;
        User user = new User();
        
        RestClient.Put(databaseURL+PlayerName+".json",user);
        //RestClient.Post(databaseURL+".json",{});
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
