using UnityEngine;
using UnityEngine.InputSystem;

/* [0] ���� : PlayerController
		- ĳ������ �̵��� �����ϴ� Ŭ����.
*/

namespace MySimple
{
    public class PlayerController : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private CharacterController controller;
        // [ ] - 2) �Է� �� WASD�� �Է��� ���� ������.
        private Vector2 inputVector;
        // [ ] - 3) �̵�.
        [SerializeField] private float moveSpeed = 10f;
        // [ ] - [ ] - 1) �̵� �Է°�.
        private Vector2 inputMove;
        // [ ] - 4) �߷�.
        private float gravity = -9.81f;
        // [ ] - [ ] - 1) �߷±��� ����� �̵��ӵ�.
        private Vector3 velocity;
        // [ ] - 5) �׶��� üũ �� �߹ٴ� ��ġ.
        public Transform groundCkeck;
        // [ ] - [ ] - 1) üũ�ϴ� ���� �ݰ�.
        [SerializeField] private float checkRange = 0.2f;
        // [ ] - [ ] - 2) �׶��� ���̾� �Ǻ�
        [SerializeField] private LayerMask groundMask;
        // [ ] - ��) �������̡�
        [SerializeField] private float jumpHeight = 1f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) ����.
            controller = this.GetComponent<CharacterController>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ���� ���� ���.
            bool isGrounded = GroundCheck();
            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -5f;
            }
            // [ ] - [ ] - 2) ����.
            // [ ] - [ ] - [ ] - 1) Global �� �̵� �� �۷ι� ������� �̵���.
            // )        Vector3 moveDir = Vector3.right * inputMove.x + Vector3.forward * inputMove.y;
            // [ ] - [ ] - [ ] - 2) Local �� �̵�.
            Vector3 moveDir = transform.right * inputMove.x + transform.forward * inputMove.y;
            // [ ] - [ ] - 3) �̵�.
            controller.Move(moveDir * Time.deltaTime * moveSpeed);
            // [ ] - [ ] - 3) �߷¿� ���� �̵�.
            velocity.y += gravity * Time.deltaTime;      // ) ���� �ӵ� ����(V).
            controller.Move(velocity * Time.deltaTime);       // ) ���� ���� �Ÿ� ����(S).
        }
        #endregion Unity Event Method





        // [3] Custyom Method.
        #region Custyom Method
        // [ ] - 1) OnMove �� Move Input �ý����� ����� �Լ�.
        public void OnMove(InputAction.CallbackContext context)
        {
            // [ ] - [ ] - 1) �÷��̾ ĳ������ �̵� �Է°��� �޾ƿ��� ����.
            inputMove = context.ReadValue<Vector2>();
        }

        // [ ] - 2) OnJump
        public void OnJump(InputAction.CallbackContext context)
        {
            // [ ] - 1) ĳ���Ͱ� ���� ������� ���� ����.
            if (context.started && GroundCheck())
            {

            }
            // [ ] - 2) ���� ���̸�ŭ �ٱ����� �ӵ� ���ϱ�.
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        // [ ] - 3) GroundCheck �� �׶��� üũ.
        bool GroundCheck()
        {
            // [ ] - 1) ĳ���Ͱ� ���� ����ִ��� Ȯ��.
            return Physics.CheckSphere(groundCkeck.position, checkRange, groundMask);
        }
        #endregion Custyom Method
    }
}

// [ ] - ) 
// [ ] - [ ] - )