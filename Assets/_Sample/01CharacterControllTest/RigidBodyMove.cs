using UnityEngine;
using UnityEngine.InputSystem;

/* [0] ���� : RigidBodyMove
		- ĳ������ �̵��� �����ϴ� Ŭ����.
*/

namespace MySample
{
    public class RigidBodyMove : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        // [ ] - 2) �̵�.
        private Vector2 inputMove;
        // [ ] - 3) �̵���Ű�� ��.
        [SerializeField] private float movePower = 10f;
        // [ ] - 4) �߷� üũ.
        private bool isGround = false;
        // [ ] - 5) ����.
        [SerializeField] private float jumpPower = 10f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) ����.
            rb = this.GetComponent<Rigidbody>();
            capsuleCollider = this.GetComponent<CapsuleCollider>();

        }

        // [ ] - 2) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - [ ] - 1) �׶��� üũ.
            isGround = GroundCheck();

            // [ ] - [ ] - 2) ����
            Vector3 moveDir = transform.right * inputMove.x + transform.forward * inputMove.y;
            // [ ] - [ ] - 3) �̵�
            rb.AddForce(moveDir * movePower, ForceMode.Force);
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

        // [ ] - 2) OnJump.
        public void OnJump(InputAction.CallbackContext context)
        {
            // [ ] - [ ] - 1).
            if (context.started && isGround)
            {
                // [ ] - [ ] - [ ] - 1) ���� �������� 1ȸ�� ���� ��.
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }

        // [ ] - 3) GroundCheck �� �׶��� üũ.
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

            // [ ] - [ ] - 2) ĳ��Ʈ ������ ��� ����.
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