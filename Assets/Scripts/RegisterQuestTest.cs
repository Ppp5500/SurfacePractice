using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    public class RegisterQuestTest : MonoBehaviour
    {
        public string myName;
        public string target;
        public string toolTip;
        public string flavorText;

        void Start()
        {
            MyQuest myQuest = new MyQuest(myName, target, toolTip, flavorText);
        }
    }
}

