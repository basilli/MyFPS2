using UnityEngine;

/* [0] ���� : PlayerAniTest
		- WASD �Է¹޾Ƽ� �÷��̾� �յ��¿� �̵� �� Old Input���� ����.
		- �յ��¿� �̵��� �Ƶ��¿� �ִϸ��̼� �÷��̸� ��.
		- �̵��� ���� ���� ��� �ִϸ��̼��� �÷�����.
		- �̵��ӵ� 5.
*/

namespace MySample
{
    public class PlayerAniTest : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) ����.
        private Animator animator;
        // [ ] - 2) �̵�.
        [SerializeField] private float moveSpeed = 5f;
        // [ ] - [ ] - 1) ��ǲ.
        private float moveX;
        private float moveZ;
        // [ ] - 4) �ִϸ��̼� �Ķ����.
        [SerializeField] private string moveMode = "MoveMode";
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 
            animator = this.GetComponent<Animator>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ��ǲ �� A,D or �¿� ȭ��ǥ�� ������ �¿� �̵� & W,S or ���� ȭ��ǥ ������ ���� �̵�.
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            // [ ] - [ ] - 2) ����
            Vector3 dir = new Vector3(moveX, 0f, moveZ);
            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
            /*
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))       // ) WŰ�� ������ ������ �̵�.
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))       // ) SŰ�� ������ �ڷ� �̵�.
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))       // ) AŰ�� ������ �������� �̵�.
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))       // ) DŰ�� ������ ���������� �̵�.
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            }
            */
            // [ ] - [ ] - 3) �ִϸ��̼� ȣ��.
            // )        AnimationStateTest();
            AnimationBlendTest();
        }

        // [ ] - 3) AnimationStateTest.
        private void AnimationBlendTest()
        {
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveZ", moveZ);
        }

        // [ ] - 4) AnimationStateTest.
        private void AnimationStateTest()
        {
            // [ ] - [ ] - 1) ����϶� �ִϸ��̼� ����.
            if (moveX == 0f && moveZ == 0f)
            {
                animator.SetInteger(moveMode, 0);
            }
            // [ ] - [ ] - 2) �յ��¿� �̵����� ���� �ִϸ��̼� ����.
            else
            {
                if (moveZ > 0)
                {
                    animator.SetInteger(moveMode, 1);
                }
                else if (moveZ < 0)
                {
                    animator.SetInteger(moveMode, 2);
                }
                if (moveX < 0)
                {
                    animator.SetInteger(moveMode, 3);
                }
                else if (moveX > 0)
                {
                    animator.SetInteger(moveMode, 4);
                }
            }
        }
    }
    #endregion Unity Event Method
}



// [3] 
// [ ] - ) 
// [ ] - [ ] - ) 
