using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleName
{
    public class DialogueManager : MonoBehaviour
    {
        
        public static DialogueManager Instance;

        [SerializeField] private TextAsset txt;
        [SerializeField] private string[,] Sentence;
        [SerializeField] private int lineSize, rowSize;
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Text speakerText;
        [SerializeField] private Text dialogueText;
        

        private void Start()
        {
            if(Instance == null)
                Instance = this;
        }

        public void StartDialogue(string _questName)
        {
            dialoguePanel.SetActive(true);
        }

        IEnumerator Typing(string text)
        {
            foreach(char letter in text.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

