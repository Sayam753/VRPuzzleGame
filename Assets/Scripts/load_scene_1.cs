using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class load_scene_1 : MonoBehaviour
{
    public void loadscene()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
