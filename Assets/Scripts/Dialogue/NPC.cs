using System;
using System.Collections;
using System.Collections.Generic;
using DoubleTechniStyle;
using TMPro;
using UnityEditor;
using UnityEngine;

/* Script that holds all the talking logic for NPCs
 * like Choices, One-letter-at-atime, etc. 
 */

//If you call dialogueUI.ClearChoices() elsewhere, set choicesActive = false

namespace DoubleTechniStyle
{

    public class NPC : MonoBehaviour, IInteractable
    {
        public NPCDialogue dialogueData;
        private NPCDialogueController dialogueUI;

        public bool internalIsDialogueComplete = false;


        public int dialogueIndex; //Maybe move this to another class to handle logic? As it is pretty complicated
        private bool isTyping, isDialogueActive;

        private bool choicesActive = false;

        // public static NPC NPCInstance { get; private set; }

        private enum QuestState { NotStarted, InProgress, Completed }

        //try and see if you can use something like this to track when to say what?


       

        //public static NPC NPCInstance { get; private set; }

        // Sub-line handling
        private string[] currentSubLines = Array.Empty<string>();
        private int subLineIndex = 0; // index of the last enqueued subline

        // Visible-lines buffer (up to maxVisible). No upward scrolling.
        private readonly List<string> visibleLines = new List<string>();
        private int maxVisible = 3;

        public void Awake()
        {
           // namePanel = GetComponent<ChangeDialogueBox>(); //just one - fix
        }

        private void Start()
        {
            dialogueUI = NPCDialogueController.Instance;
            // tracker = Instance.dayState;

            if (dialogueUI == null)
            {
                Debug.LogError("NPCDialogueController instance is not found!");
                return;
            }

           
        }


        //private void Start()
        //{
        //    dialogueUI = NPCDialogueController.Instance;
        //    tracker = Instance.dayState;



        //    //HERE: fix this

        //    Debug.Log("FOUND PANEL: " + namePanel, this);

        //    dialogueUI.ClearChoices();
        //}
        public bool CanInteract()
        {
            return !isDialogueActive;
        }

        void Update()
        {
            if (isDialogueActive && !choicesActive && Input.GetKeyDown(KeyCode.X))
                RevealAllVisibleLines();
        }

        public void Interact()
        {
            if (dialogueData == null) //|| (PauseController.IsGamePaused && !isDialogueActive)) /* ADD THIS PAUSE CONTROLLER!! */
                return;

            if (isDialogueActive) NextLine(playerPressed: true);
            else StartDialogue();
        }
        public void StartDialogue()
        {
           
           
            isDialogueActive = true;


            //1: dialogueUI.SetNPCInfo: 
            dialogueUI.SetNPCInfo(dialogueData.NPCName, dialogueData.NPCPortrait); //Fix this if necessary

            //Should show all 2 Panels here
            dialogueUI.ShowDialogueUI(true);

            //This is the changing-size Name Panel
            // namePanel.ShowDialoguePanel(true); --> Changed this 12/11/25

           // PauseController.SetPause(true); /*PAUSE CONTROLLER*/
            
            PrepareCurrentSubLines();
            visibleLines.Clear();
            subLineIndex = 0;

            TryEnqueueAndTypeNextSubLine();
        }

       
        // playerPressed indicates this NextLine call came from a user button press
        void NextLine(bool playerPressed = false)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                if (visibleLines.Count > 0)
                {
                    visibleLines[visibleLines.Count - 1] = currentSubLines[subLineIndex];
                    RebuildTextFromVisibleLines();
                }
                isTyping = false;
                return;
            }

            //clear choices
            


           
            if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
            {

                EndDialogue();
                return;
            }

            

            bool hasMoreSublines = subLineIndex + 1 < currentSubLines.Length;
            bool bufferHasSpace = visibleLines.Count < maxVisible;

            if (hasMoreSublines && bufferHasSpace)
            {
                subLineIndex++;
                TryEnqueueAndTypeNextSubLine();
                return;
            }

            // If there are no more sublines for the current element and the player pressed the button,
            // advance to the next dialogue element immediately.
            bool currentElementExhausted = !hasMoreSublines && (visibleLines.Count > 0) && subLineIndex >= currentSubLines.Length - 1;
            if (currentElementExhausted && playerPressed)
            {
                if (dialogueIndex + 1 < dialogueData.dialogueLines.Length)
                {
                    dialogueIndex++;
                    PrepareCurrentSubLines();
                    visibleLines.Clear();
                    subLineIndex = 0;
                    TryEnqueueAndTypeNextSubLine();
                    return;
                }
                else
                {
                    EndDialogue();
                    return;
                }
            }

            // If we've shown up to maxVisible OR ran out of sublines, move to next dialogue entry only when not waiting for a player press
            if (dialogueIndex + 1 < dialogueData.dialogueLines.Length)
            {
                dialogueIndex++;
                PrepareCurrentSubLines();
                visibleLines.Clear();
                subLineIndex = 0;
                TryEnqueueAndTypeNextSubLine();
                return;
            }

            EndDialogue();
        }

        void TryEnqueueAndTypeNextSubLine()
        {
            if (currentSubLines.Length == 0)
            {
                if (dialogueData.InterruptLine.Length > dialogueIndex && dialogueData.InterruptLine[dialogueIndex])
                    StartCoroutine(AutoAdvanceAfterDelay());
                return;
            }

            if (visibleLines.Count >= maxVisible)
                return;

            visibleLines.Add(string.Empty);
            RebuildTextFromVisibleLines();
            DisplayCurrentLine();

            //COMMENTED THIS OUT
            //StartCoroutine(TypeNewestVisibleLine(currentSubLines[subLineIndex]));
        }

        //CHANGING THIS
        IEnumerator TypeNewestVisibleLine(string lineToType)
        {
            isTyping = true;

            int lastIndex = visibleLines.Count - 1;
            string built = string.Empty;

            //change here --> from lineToType.Length to 
            for (int i = 0; i < lineToType.Length; i++)
            {
                built += lineToType[i];

                //adding sfx here - does it need to be 'voiceSound'? Mine is just 'Sound'
                SoundEffectManager.PlayVoice(dialogueData.Sound[dialogueIndex], dialogueData.voicePitch);
                visibleLines[lastIndex] = built;
                RebuildTextFromVisibleLines();
                yield return new WaitForSeconds(dialogueData.typingSpeed);
            }

            visibleLines[lastIndex] = lineToType;
            RebuildTextFromVisibleLines();

            isTyping = false;

            if (dialogueData.InterruptLine.Length > dialogueIndex && dialogueData.InterruptLine[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData.autoProgressDelay);
                NextLine();
                yield break;
            }

            // Do not auto-advance here. Wait for player press to call NextLine(playerPressed:true).
        }

        IEnumerator AutoAdvanceAfterDelay()
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }

        void PrepareCurrentSubLines()
        {
            subLineIndex = 0;

            if (dialogueData == null || dialogueData.dialogueLines == null || dialogueIndex >= dialogueData.dialogueLines.Length)
            {
                currentSubLines = Array.Empty<string>();
                return;
            }

            currentSubLines = dialogueData.dialogueLines[dialogueIndex]
                .Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        }

        void RebuildTextFromVisibleLines()
        {
            //here: COME BACK HERE (3:56)
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text = string.Join("\n", visibleLines));
        }

        void RevealAllVisibleLines()
        {
            if (currentSubLines.Length == 0)
                return;

            if (isTyping)
            {
                StopAllCoroutines();
                isTyping = false;
            }

            int firstVisibleIndex;
            if (visibleLines.Count > 0)
            {
                firstVisibleIndex = subLineIndex - (visibleLines.Count - 1);
                if (firstVisibleIndex < 0) firstVisibleIndex = 0;
            }
            else
            {
                firstVisibleIndex = 0;
                subLineIndex = 0;
            }

            visibleLines.Clear();
            int added = 0;
            int idx = firstVisibleIndex;
            while (added < maxVisible && idx < currentSubLines.Length)
            {
                visibleLines.Add(currentSubLines[idx]);
                added++;
                idx++;
            }

            subLineIndex = Math.Max(0, firstVisibleIndex + visibleLines.Count - 1);
            RebuildTextFromVisibleLines();
        }

     
        void DisplayCurrentLine()
        {

            StopAllCoroutines();
            StartCoroutine(TypeNewestVisibleLine(currentSubLines[subLineIndex]));


        }

        public void EndDialogue()
        {
            StopAllCoroutines();
            isDialogueActive = false;
            choicesActive = false;
            //here
            dialogueUI.SetDialogueText("");
            dialogueUI.ShowDialogueUI(false);
            // namePanel.ShowDialoguePanel(false); --> Changed this 12/11/25


         //   PauseController.SetPause(false); /* ADD PAUSE CONTROLLER */

            internalIsDialogueComplete = true;






        }



      
    }
}
