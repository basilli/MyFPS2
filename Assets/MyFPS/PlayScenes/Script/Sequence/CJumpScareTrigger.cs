using UnityEngine;
using System.Collections;       // ) IEnumerator Ȱ��ȭ.

/* [0] ���� : CJumpScareTrigger
		- �ι�° Ʈ���� ����.
        - 
*/

namespace MyFPS
{
    public class CJumpScareTrigger : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ������Ʈ.
        // [ ] - [ ] - 1) �� ������Ʈ.
        public GameObject Enemy;

        // [ ] - 2) ���� ������ �ִϸ��̼�
        public Animator animator;
        // [ ] - [ ] - 1) �ִϸ��̼� �Ķ���� ��Ʈ�� �� ���� ���� ���� ����.
        protected private string isOpen = "IsOpen";

        // [ ] - 3) �Ҹ�.
        // [ ] - [ ] - 1) ���� ������ �Ҹ�.
        public AudioSource doorBang;
        // [ ] - [ ] - 2) ���� �����ϴ� �Ҹ�.
        public AudioSource JumpScare;
        // [ ] - [ ] - 3) BGM.
        public AudioSource bgm01;
        #endregion Variable 





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) �ױ׷� �÷��̾� üũ.
            if (other.tag == "Player")
            { 
            }
            // [ ] - [ ] - 2) Ʈ���� ����.
            this.GetComponent<BoxCollider>().enabled = false;
            // [ ] - [ ] - 3) StartCoroutine �� SequercePlayer ����.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - 1) BGM ����.
            bgm01.Stop();
            // [ ] - 1) ���� ������ �ִϸ�����.
            animator.SetBool(isOpen, true);
            // [ ] - 2) �� ���� �Ҹ�.
            doorBang.Play();
            // [ ] - 4) �� ����
            Enemy.SetActive(true);
            // [ ] - 3) 1�� ������ �߻�.
            yield return new WaitForSeconds(1f);
            // [ ] - 5) ���� �����ϴ� �Ҹ�.
            JumpScare.Play();
            // [ ] - 6) ��
            Robot robot = Enemy.GetComponent<Robot>();
            if (robot)
            {
                robot.ChangeState(Robotstate.R_Walk);
            }
        }
        #endregion Custom Method
    }
}
