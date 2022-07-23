using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// DialoguePanel을 통해 대화창 제어
// 대화 내용은 DatabaseManager를 통해 csv를 파싱 받음
namespace SimpleName
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Text speakerText;
        [SerializeField] private Text dialogueText;

        private DatabaseManager databaseManager;
        private Dialogue[] dialogues;

        private int currentDialogueIndex = 0;
        private int currentContextsIndex = 0;

        private void Start()
        {
            if(Instance == null)
                Instance = this;

            databaseManager = GameObject.FindObjectOfType<DatabaseManager>();
        }

        public void ShowDialogue(int _startNum, int _endNum)
        {
            dialoguePanel.SetActive(true);

            dialogues = databaseManager.GetDialogue(_startNum, _endNum);

            currentDialogueIndex = 0;
            currentContextsIndex = 0;

            NextDialogue();
        }

        public void NextDialogue()
        {
            if (currentDialogueIndex < dialogues.Length)
            {
                speakerText.text = dialogues[currentDialogueIndex].name;

                if (currentContextsIndex < dialogues[currentDialogueIndex].contexts.Length)
                {
                    //dialogueText.text = dialogues[currentDialogueIndex].contexts[currentContextsIndex];
                    StartCoroutine("Typing", dialogues[currentDialogueIndex].contexts[currentContextsIndex]);
                    currentContextsIndex++;
                }
                else
                {
                    currentContextsIndex = 0;
                    currentDialogueIndex++;
                    NextDialogue();
                }
            }
            else
                CloseDialogue();
        }

        private void CloseDialogue()
        {
            dialoguePanel.SetActive(false);
        }

        IEnumerator Typing(string text)
        {
            dialogueText.text = "";
            foreach(char letter in text.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

