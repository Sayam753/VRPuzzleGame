using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;
using Proyecto26;
public class score : MonoBehaviour, ITrackableEventHandler
{
    private string databaseURL =  "https://treasure-hunt-51ca4.firebaseio.com/";
    User user = new User();
    public Text UserName;

    public TrackableBehaviour mTrackableBehaviour;
    public TrackableBehaviour.Status m_PreviousStatus;
    public TrackableBehaviour.Status m_NewStatus;

    public HashSet<string> TrackersList = new HashSet<string>();
    public void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
           SceneManager.LoadScene(0);
        }
        //userScore.text = "UserScore : "+siging_up.PlayerScore.ToString();

    }

    public void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            if(mTrackableBehaviour.TrackableName == "q" || mTrackableBehaviour.TrackableName == "apple" || mTrackableBehaviour.TrackableName == "bill")
            {
                UpdatingDatabase(mTrackableBehaviour.TrackableName);
            }
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            OnTrackingLost();
        }
    }
    protected void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        
        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }
    public void Posting_To_Database(string userscore)
    {
        RestClient.Put(databaseURL+"users/"+user.userName+".json",user);
    }
    protected void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
    public Text userScore;

   public void UpdatingDatabase(string ItemName)
   {
        // TrackersList.Add("apple");
        // TrackersList.Add("bill");
        // TrackersList.Add("q");
        // TrackersList.Add("apple1");
        // TrackersList.Add("apple2");
        // TrackersList.Add("apple3");
        // foreach (string item in TrackersList)
        // {
        //     if(PlayerPrefs.HasKey("found" + item) == false){
        //         Debug.Log("Added " + "found" + item);
        //         PlayerPrefs.SetInt("found" + item, 0);
        //     }
        // }
        int newpoint = 1;
        // if(TrackersList.Contains(ItemName))
        // {
        //     newpoint = 1;
        // }
        UserName.text = siging_up.PlayerName;
        RestClient.Get<User>("https://treasure-hunt-51ca4.firebaseio.com/users/"+siging_up.PlayerName+".json").Then(response =>
        {
            user = response;
            user.userScore += newpoint;
            // Debug.Log("marker found" + ItemName);
            // if(PlayerPrefs.GetInt("found" + ItemName) == 0){
            //     user.userScore += newpoint;
            //     Debug.Log("marker found" + ItemName);
            //     PlayerPrefs.SetInt("found" + ItemName, 1);
            // }
            userScore.text = "UserScore : "+user.userScore.ToString();
            Debug.Log(user.userName);
            Debug.Log(user.userScore);
            Posting_To_Database(user.userScore.ToString());
       });
   }

}


