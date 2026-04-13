using UnityEngine;
using TMPro;
using System.ComponentModel;
using UnityEngine.UI;

/* NPCDialogue Script - Scriptable Object that holds dialogue Information 
* Description: This script is a ScriptableObject, meaning that you can create data objects with it. Once an NPCDialogue object is created, it should be placed inside the NPC script in order for the dialogue to trigger.  
* (When script was created)
*/

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string NPCName;
    //public GameObject NPCPanel;
    //public Sprite NPCPortrait;

    [TextArea(1, 5)]
    public string[] dialogueLines;
    [HideInInspector] public TextAsset dialogueText;
    public float typingSpeed = 0.05f;

    public Sprite NPCPortrait;

    public bool[] endDialogueLines; //mark where dialogue ends

    public AudioClip[] Sound;
    public float voicePitch = 1f;

    public bool[] InterruptLine;

    public bool[] autoProgressLines;
    public float autoProgressDelay = 0f;


    public DialogueChoice[] choices;

    [HideInInspector] public float shortPauseDuration = 0.5f;
    [HideInInspector] public float longPauseDuration = 0.2f; //I think this is good for now, 12/10/25, you need to make a new scriptableobject with its info tho


}

[System.Serializable]

public class DialogueChoice
{
    public int dialogueIndex; //Dialogue line where choices appear
    public string[] choices; //player response options
    public int[] nextDialogueIndexes; //where choice leads
    public bool[] givesQuest;


}