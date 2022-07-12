using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleName
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControl : MonoBehaviour
    {
        private MyInputAction myInputAction;
        private CharacterController cc;
        private Vector3 _moveInput;
        private Vector2 _lookInput;
        private float _shiftKey;
        private float _spaceKey;
        private bool _tabKey;
        private bool _isPlayerFocus = true;
        private float _moveSpeed;
        private float _jumpTimer = 0;
        private Animator _anim;

        //public GameObject characterModel;

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

        void Awake()
        {
            myInputAction = new MyInputAction();
            cc = GetComponent<CharacterController>();
            _anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            InputChange();
        }

        void Start()
        {

        }

        void Update()
        {
            GetInput();
            Tab();
            AnimControl();
            if (_isPlayerFocus)
            {
                Move();
            }
            
        }

        void LateUpdate()
        {
            if (_isPlayerFocus)
            {
                CameraRotate();
            }
        }

        private void GetInput()
        {
            // keyboard ad
            _moveInput.x = myInputAction.Player.Move.ReadValue<Vector2>().x;
            // keyboard ws
            _moveInput.z = myInputAction.Player.Move.ReadValue<Vector2>().y;

            _lookInput = myInputAction.Player.Look.ReadValue<Vector2>();
            _shiftKey = myInputAction.Player.Run.ReadValue<float>();
            _spaceKey = myInputAction.Player.Jump.ReadValue<float>();
            _tabKey = myInputAction.Player.Tab.triggered;

            if (_shiftKey > 0.0f)
                _moveSpeed = _runSpeed;
            else
                _moveSpeed = _walkSpeed;
        }

        private void AnimControl()
        {
            if (_moveInput != Vector3.zero)
                _anim.SetFloat("AnimWalkFloat", _moveSpeed);
            else
                _anim.SetFloat("AnimWalkFloat", 0);
        }

        private void Move()
        {
            // 카메라 시점
            Vector3 finalDirection = followTransform.transform.TransformDirection(_moveInput);
            finalDirection.y = 0;

            // 방향 입력이 있을 때만 카메라 시점으로 캐릭터 회전
            if (_moveInput != Vector3.zero)
            {
                cc.transform.forward = finalDirection.normalized;
            }

            if (cc.isGrounded)
            {
                if (_jumpTimer < _jumpDelay)
                {
                    _jumpTimer += Time.deltaTime;
                }

                if ((_jumpTimer >= _jumpDelay) && (_spaceKey > 0.5))
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
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInput.y * _camRotationSpeed, Vector3.left);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInput.x * _camRotationSpeed, Vector3.up);

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
        public void InputChange()
        {
            if (myInputAction.Player.enabled)
                myInputAction.Player.Disable();
            else
                myInputAction.Player.Enable();
        }

        private void Tab()
        {
            if (_tabKey)
            {
                if (QuestManager.Instance.IsQuestPanelOpen())
                    _isPlayerFocus = false;
                else
                    _isPlayerFocus = true;
            }
        }
    }
}