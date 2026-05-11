using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DoubleTechniStyle
{
    public class MainMenuNav : MonoBehaviour
    {
       

        public GameObject mainMenuCanvas;
        public GameObject optionsCanvas;
        public GameObject saveCanvas;
        public GameObject firstSelectedButton;
        public GameObject optionsButton;

        public string sceneName;

        //private SaveController saveController;



        public void LoadOverworld()
        {
             SceneManager.LoadScene("OverworldScene");
            
            Debug.Log("Loading");
        }

        public void LoadOptionsScreen()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);

            mainMenuCanvas.SetActive(false);
            optionsCanvas.SetActive(true);

        }

        public async void LoadSave()
        {
            await FadeTransition();
            // SaveController.SaveControllerInstance.LoadGame(); Fix this to have a save screen 12/20/25

        }

        public void LoadMainMenu()
        {

            optionsCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsButton);


        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }


        public async Task FadeTransition()
        {
            await ScreenFader.Instance.FadeOut();
            await ScreenFader.Instance.FadeIn();
        }



    }

}
