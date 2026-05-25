using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour, ISubmitHandler
{
    public GameObject[] levels;
    public TMP_Text sceneLoadText;

    public void OnSubmit(BaseEventData eventData)
    {
       
        if (EventSystem.current.currentSelectedGameObject == levels[0])
        {
            sceneLoadText.text = "";
            SceneManager.LoadScene("LevelOne"); //Make sure this is the correct name
            sceneLoadText.text = "Load Level One?";
        }
        else if (EventSystem.current.currentSelectedGameObject == levels[1])
        {
            sceneLoadText.text = "";
            SceneManager.LoadScene("LevelTwo");
            sceneLoadText.text = "Load Level Two?";
        }
        else if (EventSystem.current.currentSelectedGameObject == levels[2])
        {
            sceneLoadText.text = "";
            SceneManager.LoadScene("LevelThree");
            sceneLoadText.text = "Load Level Three?";
        }
        else if (EventSystem.current.currentSelectedGameObject == levels[3])
        {
            sceneLoadText.text = "";
            SceneManager.LoadScene("LevelFour");
            sceneLoadText.text = "Load Level Four?";
        }
        else if (EventSystem.current.currentSelectedGameObject == levels[4])
        {
            sceneLoadText.text = "";
            SceneManager.LoadScene("LevelFive");
            sceneLoadText.text = "Load Level Five?";
        }
;
    }

    


}
