using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

/* Script that holds the generic dialogue UI and 
 shows and destroys it*/

public class NPCDialogueController : MonoBehaviour
{
    public static NPCDialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public GameObject portraitPanel;
    public GameObject namePanel;

    public GameObject[] pages;

    public bool isChoiceOpen = true;

    public Image portraitImage;
    //UNCHECK THE ABOVE ONCE YOU GET YOUR PORTRAIT IMAGES (this is the placeholder for the default or whatever)

    //ADD YOUR 'OPTIONS' UI HERE
 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); //Only one instance
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
        namePanel.SetActive(show);



    }

    public void ShowPortraitUI(bool show)
    {
        portraitPanel.SetActive(show);
    }



    public void SetNPCInfo(string NPCName, Sprite NPCPortrait)
    {
        nameText.text = NPCName;
        portraitImage.sprite = NPCPortrait;
    }

    public void SetDialogueText(string text)
    {

        dialogueText.text = text;

    }

    //I think you should add the input action here. Handle the actual logic elsewhere
    //Put input action on empty object that holds the text, or .. ? how to navigate from option to option?
   
}
