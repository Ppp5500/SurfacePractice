using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    public class DialogueObject : MonoBehaviour
    {
        private DialogueManager dialogueManager;
        private MeshRenderer meshRenderer;
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField] private bool isHaveOutline;
        public string questname;


        private void Start()
        {
            GameObject.FindGameObjectWithTag("DialogueManager").TryGetComponent<DialogueManager>(out dialogueManager);

            //if(isHaveOutline)
            //    meshRenderer = GetComponentInChildren<MeshRenderer>();

            if (isHaveOutline)
                skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void CallDialogueManager()
        {
            dialogueManager.StartDialogue(questname);

            if (skinnedMeshRenderer != null)
            {
                Debug.Log("didIcallyou?");
                skinnedMeshRenderer.material.SetFloat("_OutlineWidth", 1.02f);
            }
                
        }
    }
}

