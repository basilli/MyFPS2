using UnityEngine;
using UnityEngine.InputSystem;

/* [0] 개요 : PlayerController
		- 캐릭터의 이동을 관리하는 클래스.
*/

namespace MyFPS
{
    public class PlayerController : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private CharacterController controller;
        public PistolShoot pistolShoot;
        // [ ] - 2) 입력 → 이동.
        private Vector2 inputMove;
        // [ ] - 3) 이동.
        [SerializeField] private float moveSpeed = 10f;
        // [ ] - 4) 중력.
        private float gravity = -9.81f;
        // [ ] - [ ] - 1) 중력까지 계산한 이동속도.
        [SerializeField] private Vector3 velocity;
        // [ ] - 5) 그라운드 체크 → 발바닥 위치.
        public Transform groundCkeck;
        // [ ] - [ ] - 1) 체크하는 구의 반경.
        [SerializeField] private float checkRange = 0.2f;
        // [ ] - [ ] - 2) 그라운드 레이어 판별.
        [SerializeField] private LayerMask groundMask;
        // [ ] - 6) 점프높이.
        [SerializeField] private float jumpHeight = 1f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 참조.
            controller = this.GetComponent<CharacterController>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 땅에 있을 경우.
            bool isGrounded = GroundCheck();
            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -10f;
            }
            // [ ] - [ ] - 2) 방향.
            // [ ] - [ ] - [ ] - 1) Global 축 이동 → 글로벌 공간계로 이동함.
            // )        Vector3 moveDir = Vector3.right * inputMove.x + Vector3.forward * inputMove.y;
            // [ ] - [ ] - [ ] - 2) Local 축 이동.
            Vector3 moveDir = transform.right * inputMove.x + transform.forward * inputMove.y;
            // [ ] - [ ] - 3) 이동.
            controller.Move(moveDir * Time.deltaTime * moveSpeed);
            // [ ] - [ ] - 3) 중력에 따른 이동.
            velocity.y += gravity * Time.deltaTime;      // ) 낙하 속도 공식(V).
            controller.Move(velocity * Time.deltaTime);       // ) 자유 낙하 거리 공식(S).
        }
        #endregion Unity Event Method





        // [3] Custyom Method.
        #region Custyom Method









        // [ ] - 1) OnMove → Move Input 시스템이 등록할 함수.
        public void OnMove(InputAction.CallbackContext context)
        {
            // [ ] - [ ] - 1) 플레이어나 캐릭터의 이동 입력값을 받아오는 역할.
            inputMove = context.ReadValue<Vector2>();
        }

        // [ ] - 2) OnJump
        public void OnJump(InputAction.CallbackContext context)
        {
            // [ ] - 1) 캐릭터가 땅에 닿아있을 때만 실행.
            if (context.started && GroundCheck())
            {
                // [ ] - [ ] - 1) 점프 높이만큼 뛰기위한 속도 구하기.
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }
        }

        // [ ] - 3) OnFire.
        public void OnFire(InputAction.CallbackContext context)
        {
            // )        Debug.Log(PlayerDataManager.Instance.Weapon);
            // [ ] - [ ] - 1) 무장체크 → 플레이어가 총을 들었는지 확인.
            if (PlayerDataManager.Instance.Weapon == WeaponType.None)
            {
                Debug.Log("WeaponType : None");
                return;
            }
            // [ ] - [ ] - 2) 키다운 → 버튼 다운.
            if (context.started)
            {
                // [ ] - [ ] - [ ] - 1) StartCoroutine → Shoot 실행.
                pistolShoot.Fire();
            }
        }

        // [ ] - 4) GroundCheck → 그라운드 체크.
        bool GroundCheck()
        {
            // [ ] - 1) 캐릭터가 땅에 닿아있는지 확인.
            return Physics.CheckSphere(groundCkeck.position, checkRange, groundMask);
        }
        #endregion Custyom Method
    }
}