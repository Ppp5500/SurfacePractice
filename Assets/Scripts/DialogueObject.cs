using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    public class DialogueObject : MonoBehaviour
    {
        private DialogueManager dialogueManager;
        private SkinnedMeshRenderer skinnedMeshRenderer;
        private MyInputAction myInputAction;
        private bool EKey;

        [SerializeField] private DialogueEvent dialogue;
        [SerializeField] private bool isHaveOutline;
        [SerializeField] private GameObject InterButton;    // 실제로 상호작용할 오브젝트
        [SerializeField] private ParticleSystem talkParticle;
        public string questname;


        private void Start()
        {
            GameObject.FindGameObjectWithTag("DialogueManager").TryGetComponent<DialogueManager>(out dialogueManager);

            if (isHaveOutline)
                skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            InterButton.SetActive(false);

            myInputAction = new MyInputAction();
            myInputAction.UIInt.Enable();
        }

        public void StartDIalogue()
        {
            DialogueManager.Instance.ShowDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        }

        public void OnInteractButton()
        {
            InterButton.SetActive(true);
            talkParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            // 외곽선 표시
            if (skinnedMeshRenderer != null)
                skinnedMeshRenderer.material.SetFloat("_OutlineWidth", 1.02f);
        }

        public void StayInteractButton()
        {
            if (InterButton.activeSelf)
            {
                EKey = myInputAction.UIInt.Interact.triggered;

                if (EKey)
                {
                    StartDIalogue();
                    InterButton.SetActive(false);
                }
            }
        }

        public void OffInteractButton()
        {
            InterButton.SetActive(false);
            talkParticle.Play();

            // 외곽선 끄기
            if (skinnedMeshRenderer != null)
                skinnedMeshRenderer.material.SetFloat("_OutlineWidth", 1.0f);
        }
    }
}

