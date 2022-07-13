using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace SimpleName
{
    public class QuestManager : MonoBehaviour
    {
        //private List<Quest> _quests = new List<Quest>();
        private Dictionary<string, Quest> dQuests = new Dictionary<string, Quest>();

        public static QuestManager Instance;
        public GameObject questPanel;
        public VerticalLayoutGroup verticalLayoutGroup;
        public Button buttonForm;
        public Text currentQuestName;
        public Text currentQuestFlavorText;
        public Text currentQuestTarget;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;

            questPanel.SetActive(false);
        }

        public void AddQuestToQuestList(Quest _quest)
        {
            dQuests.Add(_quest.questName, _quest);
            AddToScrollView(_quest.questName);

        }

        private void AddToScrollView(string _questName)
        {
            Button test = Instantiate(buttonForm, verticalLayoutGroup.transform.position, Quaternion.identity);
            test.transform.SetParent(verticalLayoutGroup.transform, false);

            test.GetComponentInChildren<Text>().text = _questName;
        }

        public void ShowQuestDetale(string _questName)
        {
            dQuests.TryGetValue(_questName, out var currentQuest);
            currentQuestName.text = currentQuest.questName;
            currentQuestFlavorText.text = currentQuest.flavorText;
            currentQuestTarget.text = currentQuest.target;
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

