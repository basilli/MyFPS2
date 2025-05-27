using UnityEngine;
using System.Collections;       // ) IEnumerator 활성화.

/* [0] 개요 : CJumpScareTrigger
		- 두번째 트리거 연출.
        - 
*/

namespace MyFPS
{
    public class CJumpScareTrigger : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 오브젝트.
        // [ ] - [ ] - 1) 적 오브젝트.
        public GameObject Enemy;

        // [ ] - 2) 문이 열리는 애니메이션
        public Animator animator;
        // [ ] - [ ] - 1) 애니메이션 파라미터 스트링 → 문을 열기 위한 조건.
        protected private string isOpen = "IsOpen";

        // [ ] - 3) 소리.
        // [ ] - [ ] - 1) 문이 열리는 소리.
        public AudioSource doorBang;
        // [ ] - [ ] - 2) 적이 등장하는 소리.
        public AudioSource JumpScare;
        // [ ] - [ ] - 3) BGM.
        public AudioSource bgm01;
        #endregion Variable 





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) 테그로 플레이어 체크.
            if (other.tag == "Player")
            { 
            }
            // [ ] - [ ] - 2) 트리거 해제.
            this.GetComponent<BoxCollider>().enabled = false;
            // [ ] - [ ] - 3) StartCoroutine → SequercePlayer 실행.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - 1) BGM 정지.
            bgm01.Stop();
            // [ ] - 1) 문이 열리는 애니메이터.
            animator.SetBool(isOpen, true);
            // [ ] - 2) 문 여는 소리.
            doorBang.Play();
            // [ ] - 4) 적 등장
            Enemy.SetActive(true);
            // [ ] - 3) 1초 딜레이 발생.
            yield return new WaitForSeconds(1f);
            // [ ] - 5) 적이 등장하는 소리.
            JumpScare.Play();
            // [ ] - 6) 적
            Robot robot = Enemy.GetComponent<Robot>();
            if (robot)
            {
                robot.ChangeState(Robotstate.R_Walk);
            }
        }
        #endregion Custom Method
    }
}
