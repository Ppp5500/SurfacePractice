using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    class MyQuest : Quest
    {
        public MyQuest(string a, string b, string c, string d)
            :base(a, b, c, d)
        {
            Debug.Log("MyQuest Created!");
        }
    }

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

