using UnityEngine;
using UnityEngine.InputSystem;

/* [0] 개요 : RigidBodyMove
		- 캐릭터의 이동을 관리하는 클래스.
*/

namespace MySample
{
    public class RigidBodyMove : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        // [ ] - 2) 이동.
        private Vector2 inputMove;
        // [ ] - 3) 이동시키는 힘.
        [SerializeField] private float movePower = 10f;
        // [ ] - 4) 중력 체크.
        private bool isGround = false;
        // [ ] - 5) 점프.
        [SerializeField] private float jumpPower = 10f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 참조.
            rb = this.GetComponent<Rigidbody>();
            capsuleCollider = this.GetComponent<CapsuleCollider>();

        }

        // [ ] - 2) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - [ ] - 1) 그라운드 체크.
            isGround = GroundCheck();

            // [ ] - [ ] - 2) 방향
            Vector3 moveDir = transform.right * inputMove.x + transform.forward * inputMove.y;
            // [ ] - [ ] - 3) 이동
            rb.AddForce(moveDir * movePower, ForceMode.Force);
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

        // [ ] - 2) OnJump.
        public void OnJump(InputAction.CallbackContext context)
        {
            // [ ] - [ ] - 1).
            if (context.started && isGround)
            {
                // [ ] - [ ] - [ ] - 1) 위쪽 방향으로 1회성 힘을 줌.
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }

        // [ ] - 3) GroundCheck → 그라운드 체크.
        private bool GroundCheck()
        {
            // [ ] - [ ] - 1) .
            if (isGround)
            {
                if (inputMove == Vector2.zero)
                {
                    rb.linearDamping = 20f;
                }
                else
                {
                    rb.linearDamping = 5f;
                }
            }
            else
            { 
            
            }

            // [ ] - [ ] - 2) 캐스트 정보를 담는 변수.
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, capsuleCollider.radius, Vector3.down, out hit, (capsuleCollider.height/2 - capsuleCollider.radius) + 0.01f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                return true;
            }
            return false;
        }
        #endregion Custyom Method
    }
}

// [ ] - ) 
// [ ] - [ ] - )