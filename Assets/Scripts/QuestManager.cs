using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleName
{
    public class QuestManager : MonoBehaviour
    {
        private List<QuestStruct> _quests = new List<QuestStruct>();

        public static QuestManager Instance;
        public GameObject questPanel;

        private void Start()
        {
            if(Instance == null)
                Instance = this;
        }

        public void OpenQuestPanel()
        {
            if (questPanel.activeSelf)
                questPanel.SetActive(false);
            else
                questPanel.SetActive(true);
        }

        // 퀘스트 패널이 열려있으면 닫고 false 반환
        // 퀘스트 패널이 닫혀있으면 열고 true 반환
        public bool IsQuestPanelOpen()
        {
            if (questPanel.activeSelf)
            {
                questPanel.SetActive(false);
                return false;
            }
            else
            {
                questPanel.SetActive(true);
                return true;
            }
                
        }
    }
}

