using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DoubleTechniStyle
{
    public class MainMenuNav : MonoBehaviour
    {
       

        public GameObject mainMenuCanvas;
        public GameObject levelSelectCanvas;
        public GameObject instructionsCanvas;

        public GameObject welcomeButton;

        public GameObject selectButton;
        public GameObject instructionsButton;
        public GameObject exitButton;
        public GameObject newButton;

        public string sceneName;

        //private SaveController saveController;

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(newButton);
        }

        
        private void Update()
        {
            NavigatePages();
           // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            //  Debug.Log(EventSystem.currentS)
        }
        public void LoadOverworld()
        {
             SceneManager.LoadScene("OverworldScene");
            
            Debug.Log("Loading");
        }

        public void LoadLevelOne()
        {
            SceneManager.LoadScene("Scene 1 Isaac");
        }

        public void LoadLevelTwo()
        {
            SceneManager.LoadScene("Level 2 Omar");
        }


        public void LoadLevelThree()
        {
            SceneManager.LoadScene("Level 3 [someones name here]");
        }

        public void LoadLevelFour()
        {
            SceneManager.LoadScene("Level 4 [someone name here]");
        }

        public void LoadLevelFive()
        {
            SceneManager.LoadScene("Boss");
        }
        public void LoadLevelSelectCanvs()
        {
            mainMenuCanvas.SetActive(false);
            levelSelectCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(welcomeButton);
            TMP_Text welcomeText = welcomeButton.GetComponent<TMP_Text>();
            welcomeText.color = Color.red;
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }

        public GameObject[] levelNames;
        public void NavigatePages() //Grey out page text
        {
            for (int i = 0; i < levelNames.Length; i++)
            {
                TMP_Text levelText = levelNames[i].GetComponent<TMP_Text>();

                if (EventSystem.current.currentSelectedGameObject == levelNames[i])
                {

                   
                    levelText.color = Color.red;

                }

                else
                {
                    levelText.color = Color.white;
                    //pages[i].SetActive(false);


                }

            }

        }
        public GameObject backButton;
        public void LoadInstructionsScreen()
        {
            EventSystem.current.SetSelectedGameObject(null);
           // EventSystem.current.SetSelectedGameObject(firstSelectedButton);

            mainMenuCanvas.SetActive(false);
            instructionsCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(backButton);


        }

        public void LoadMainMenuFromSelect()
        {

            levelSelectCanvas.SetActive(false);
           
            mainMenuCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectButton);


        }

        public void LoadMainMenuFromInstructions()
        {

            instructionsCanvas.SetActive(false);
           
            mainMenuCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(instructionsButton);


        }

        public void ExitGame()
        {
            Debug.Log("Here");
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
