using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NOPAUSE : MonoBehaviour
{
    public GameObject text;
   
    void Update()
    {
        //if (PlayerInputManager.instance.MenuOpenCloseInput)
        //{

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            StartCoroutine(ShowText());
        }


    }

    IEnumerator ShowText()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(1f);
        text.SetActive(false);
    }
}
