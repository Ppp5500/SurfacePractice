using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
