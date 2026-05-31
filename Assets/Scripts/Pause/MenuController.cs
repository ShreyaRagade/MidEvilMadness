using DoubleTechniStyle;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    
    public static MenuController Instance { get; private set; }

    private void Awake()
    {

        

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }


    }




    void Update()
    {
      
        //if (PlayerInputManager.instance.MenuOpenCloseInput)
        //{

        if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
            if (!menuCanvas.activeSelf && PauseController.IsGamePaused || EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
        }

           
            ////what the menu canvas currently isn't
            //EventSystem.current.SetSelectedGameObject(arrow);


            //PauseController.SetPause(menuCanvas.activeSelf);
        }
    //}
}
