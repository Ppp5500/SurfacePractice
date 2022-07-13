using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleName
{
    [RequireComponent(typeof(Button))]
    public class QuestButtonForm : MonoBehaviour
    {
        private Button _button;

        void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ViewQuestDetale);
        }

        void ViewQuestDetale()
        {
            if (QuestManager.Instance != null)
                QuestManager.Instance.ShowQuestDetale(GetComponentInChildren<Text>().text);
            else
                Debug.Log("Can't ShowQuestDetale");
        }
    }
}

