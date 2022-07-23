using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퀘스트를 다루기 위한 가상 클래스
// 이 클래스를 상속 받으면 생성 시 자동으로 QuestManager에 등록됨
namespace SimpleName
{
    public abstract class Quest
    {
        public string questName;
        public string target;
        public string toolTip;
        public string flavorText;

        public Quest( string _questName, string _target, string _toolTip, string _flavorText)
        {
            this.questName = _questName;
            this.target = _target;
            this.toolTip = _toolTip;
            this.flavorText = _flavorText;

            RegisterToManager();
        }

        // 퀘스트매니저에 퀘스트를 등록
        private void RegisterToManager()
        {
            if (QuestManager.Instance != null)
                QuestManager.Instance.AddQuestToQuestList(this);
            else
                Debug.Log("Can't Register");
        }
    }

    class MyQuest : Quest
    {
        public MyQuest(string name, string target, string toolTip, string flaverText)
            : base(name, target, toolTip, flaverText)
        {
            Debug.Log("MyQuest Created!");
        }
    }
}
