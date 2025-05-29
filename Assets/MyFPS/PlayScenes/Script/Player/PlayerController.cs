using UnityEngine;
using UnityEngine.InputSystem;

/* [0] ���� : PlayerController
		- ĳ������ �̵��� �����ϴ� Ŭ����.
*/

namespace MyFPS
{
    public class PlayerController : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private CharacterController controller;
        public PistolShoot pistolShoot;
        // [ ] - 2) �Է� �� �̵�.
        private Vector2 inputMove;
        // [ ] - 3) �̵�.
        [SerializeField] private float moveSpeed = 10f;
        // [ ] - 4) �߷�.
        private float gravity = -9.81f;
        // [ ] - [ ] - 1) �߷±��� ����� �̵��ӵ�.
        [SerializeField] private Vector3 velocity;
        // [ ] - 5) �׶��� üũ �� �߹ٴ� ��ġ.
        public Transform groundCkeck;
        // [ ] - [ ] - 1) üũ�ϴ� ���� �ݰ�.
        [SerializeField] private float checkRange = 0.2f;
        // [ ] - [ ] - 2) �׶��� ���̾� �Ǻ�.
        [SerializeField] private LayerMask groundMask;
        // [ ] - 6) ��������.
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
                velocity.y = -10f;
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
                // [ ] - [ ] - 1) ���� ���̸�ŭ �ٱ����� �ӵ� ���ϱ�.
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }
        }

        // [ ] - 3) OnFire.
        public void OnFire(InputAction.CallbackContext context)
        {
            // )        Debug.Log(PlayerDataManager.Instance.Weapon);
            // [ ] - [ ] - 1) ����üũ �� �÷��̾ ���� ������� Ȯ��.
            if (PlayerDataManager.Instance.Weapon == WeaponType.None)
            {
                Debug.Log("WeaponType : None");
                return;
            }
            // [ ] - [ ] - 2) Ű�ٿ� �� ��ư �ٿ�.
            if (context.started)
            {
                // [ ] - [ ] - [ ] - 1) StartCoroutine �� Shoot ����.
                pistolShoot.Fire();
            }
        }

        // [ ] - 4) GroundCheck �� �׶��� üũ.
        bool GroundCheck()
        {
            // [ ] - 1) ĳ���Ͱ� ���� ����ִ��� Ȯ��.
            return Physics.CheckSphere(groundCkeck.position, checkRange, groundMask);
        }
        #endregion Custyom Method
    }
}