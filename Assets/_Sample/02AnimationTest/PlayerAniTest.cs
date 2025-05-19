using UnityEngine;

/* [0] 개요 : PlayerAniTest
		- WASD 입력받아서 플레이어 앞뒤좌우 이동 → Old Input으로 설정.
		- 앞뒤좌우 이동시 왚뒤좌우 애니메이션 플레이를 함.
		- 이동이 없을 때는 대기 애니메이션을 플레이함.
		- 이동속도 5.
*/

namespace MySample
{
    public class PlayerAniTest : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 참조.
        private Animator animator;
        // [ ] - 2) 이동.
        [SerializeField] private float moveSpeed = 5f;
        // [ ] - [ ] - 1) 인풋.
        private float moveX;
        private float moveZ;
        // [ ] - 4) 애니메이션 파라미터.
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
            // [ ] - [ ] - 1) 인풋 → A,D or 좌우 화살표를 눌러서 좌우 이동 & W,S or 전후 화살표 눌러서 전후 이동.
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            // [ ] - [ ] - 2) 방향
            Vector3 dir = new Vector3(moveX, 0f, moveZ);
            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
            /*
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))       // ) W키를 눌러서 앞으로 이동.
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))       // ) S키를 눌러서 뒤로 이동.
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))       // ) A키를 눌러서 왼쪽으로 이동.
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))       // ) D키를 눌러서 오른쪽으로 이동.
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            }
            */
            // [ ] - [ ] - 3) 애니메이션 호출.
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
            // [ ] - [ ] - 1) 대기일때 애니메이션 실행.
            if (moveX == 0f && moveZ == 0f)
            {
                animator.SetInteger(moveMode, 0);
            }
            // [ ] - [ ] - 2) 앞뒤좌우 이동으로 인한 애니메이션 실행.
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
