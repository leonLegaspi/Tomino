using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public PlayerCam playerCam;

    private bool isPlayerRange;
    private bool didDialogueStart;

    private int lineIndex;

    private float typingTime = 0.05f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private void Update()
    {
        if(isPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }

            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerRange = false;
        }
    }

    private void StartDialogue()
    {
        playerCam.active = false;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            playerCam.active = true;    
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }
}
