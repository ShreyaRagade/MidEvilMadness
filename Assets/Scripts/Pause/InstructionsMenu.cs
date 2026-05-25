using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour, ISubmitHandler
{
    public GameObject backButton;
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject firstSelectedButton;
    public GameObject optionsButton;

    public void OnSubmit(BaseEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

}
