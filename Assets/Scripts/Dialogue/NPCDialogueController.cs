using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

/* NPC Dialogue Controller Script
 * Description: Script that holds the generic dialogue UI and shows and hides it
 * April 10th SHREYA (sr3745): Adjusted Portrait Panel and Portrait Image, so that the NPC's Portrait Image shows correctly when interacting with NPC. There were problems with the Sprite Renderer vs. Image
 * But they have now been fixed. 

 */

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

   // public Sprite portraitImage;
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
        portraitPanel.SetActive(show);


    }

    public void ShowPortraitUI(bool show)
    {
        portraitPanel.SetActive(show);
    }



    public void SetNPCInfo(string NPCName, Sprite NPCPortrait)
    {
        nameText.text = NPCName;
        portraitImage.sprite = NPCPortrait;

        // portraitPanel.image. = NPCPortrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;

    }
   
}
