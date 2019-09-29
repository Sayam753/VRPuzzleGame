using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class start_with_button : MonoBehaviour
{
    public void start_game()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
