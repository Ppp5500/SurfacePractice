using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    public enum ControlSubject
    {
        Player,
        UI,
    }

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControl : MonoBehaviour
    {
        public MyInputAction myInputAction;
        private CharacterController cc;
        private Vector3 moveInput;
        private Vector2 lookInput;
        private float shiftKey;
        private float spaceKey;
        private bool tabKey;
        private float _moveSpeed;
        private float _jumpTimer = 0;
        private Animator _anim;
        private AudioSource _audioSource;

        public ControlSubject csubject = ControlSubject.Player;

        [Tooltip("카메라가 추적할 트랜스폼")]
        [SerializeField] private GameObject followTransform;
        [Tooltip("카메라가 오브젝트")]
        [SerializeField] private Camera cam;
        [Tooltip("캐릭터의 걷기속도")]
        [SerializeField] private float _walkSpeed = 3;
        [Tooltip("캐릭터의 달리기속도")]
        [SerializeField] private float _runSpeed = 5;
        //[Tooltip("캐릭터의 점프강도")]
        //[SerializeField] private float _jumpPower = 8;
        [Tooltip("캐릭터의 점프 딜레이")]
        [SerializeField] private float _jumpDelay = 1.5f;
        [Tooltip("화면 회전 속도")]
        [SerializeField] private float _camRotationSpeed = 1f;
        [Tooltip("발소리")]
        public AudioClip[] audioClips;

        void Awake()
        {
            myInputAction = new MyInputAction();
            cc = GetComponent<CharacterController>();
            _anim = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            myInputAction.Player.Enable();
            myInputAction.UI.Enable();
            myInputAction.UIInt.Enable();
        }

        void Update()
        {
            GetInputPlayer();
            GetInputUIInt();
            Tab();
            Move();
            AnimControl();
        }

        void LateUpdate()
        {
            CameraRotate();
        }

        private void GetInputPlayer()
        {
            // keyboard ad
            moveInput.x = myInputAction.Player.Move.ReadValue<Vector2>().x;
            // keyboard ws
            moveInput.z = myInputAction.Player.Move.ReadValue<Vector2>().y;

            lookInput = myInputAction.Player.Look.ReadValue<Vector2>();
            shiftKey = myInputAction.Player.Run.ReadValue<float>();
            spaceKey = myInputAction.Player.Jump.ReadValue<float>();

            if (shiftKey > 0.0f)
                _moveSpeed = _runSpeed;
            else
                _moveSpeed = _walkSpeed;
        }

        private void GetInputUIInt()
        {
            tabKey = myInputAction.UIInt.Tab.triggered;
        }

        private void AnimControl()
        {
            if (moveInput != Vector3.zero)
                _anim.SetFloat("AnimWalkFloat", _moveSpeed);
            else
                _anim.SetFloat("AnimWalkFloat", 0);
        }

        private void Move()
        {
            // 카메라 시점
            Vector3 finalDirection = followTransform.transform.TransformDirection(moveInput);
            finalDirection.y = 0;

            // 방향 입력이 있을 때만 카메라 시점으로 캐릭터 회전
            if (moveInput != Vector3.zero)
            {
                cc.transform.forward = finalDirection.normalized;
            }

            if (cc.isGrounded)
            {
                if (_jumpTimer < _jumpDelay)
                {
                    _jumpTimer += Time.deltaTime;
                }

                if ((_jumpTimer >= _jumpDelay) && (spaceKey > 0.5))
                {
                    _anim.SetTrigger("AnimJumpTrigger");
                    _jumpTimer = 0;
                }
            }

            cc.Move(finalDirection * _moveSpeed * Time.deltaTime);
        }

        private void CameraRotate()
        {
            // followTransform을 마우스 입력대로 회전
            followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.y * _camRotationSpeed, Vector3.left);
            followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.x * _camRotationSpeed, Vector3.up);

            // 추적 대상의 각도
            var angles = followTransform.transform.localEulerAngles;
            // 회전하지 않도록 고정
            angles.z = 0;

            // 내려다 보는 각도
            var angle = followTransform.transform.localEulerAngles.x;

            // 내려다 보는 각도 제한
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }
            followTransform.transform.localEulerAngles = angles;
        }

        private void Tab()
        {
            if (tabKey)
            {
                if (QuestManager.Instance.IsQuestPanelOpen())
                {
                    csubject = ControlSubject.UI;
                    SwitchControl(csubject);
                }
                else
                {
                    csubject = ControlSubject.Player;
                    SwitchControl(csubject);
                }
                    
            }
        }

        public void SwitchControl(ControlSubject _subject)
        {
            if (_subject == ControlSubject.Player)
            {
                myInputAction.Player.Enable();
                //myInputAction.UI.Disable();
            }
            else if (_subject == ControlSubject.UI)
            {
                myInputAction.Player.Disable();
                //myInputAction.UI.Enable();
            }
        }

        public void FootstepSound()
        {
            var rand = Random.Range(0, 4);
            _audioSource.PlayOneShot(audioClips[rand]);
        }
    }
}