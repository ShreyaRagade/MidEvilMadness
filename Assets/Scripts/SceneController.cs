using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    // Load a scene by its name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load the next scene in the Build Settings index
    public void LoadNextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIndex);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")) // Ensure your player object is tagged "Player"
        {
            LoadNextScene();
        }
    }


}
