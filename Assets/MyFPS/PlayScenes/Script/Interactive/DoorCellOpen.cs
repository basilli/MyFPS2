using UnityEngine;
using TMPro;

/* [0] ���� : DoorCellOpen
		- ������ ���ͷ�Ƽ�� �׼� ����.
*/

namespace MyFPS
{
	public class DoorCellOpen : Interactive
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) �ִϸ��̼�.
        public Animator animator;
        // [ ] - [ ] - 1) �ִϸ��̼� �Ķ���� ��Ʈ��.
        protected private string paramIsOpen = "IsOpen";
        // [ ] - 2) �� ���� �Ҹ�
        public AudioSource audioSource;
        #endregion Variables





        // [2] Unity Event Method
        #region Unity Event Method
        #endregion Unity Event Method





        // [3] Custom Method
        #region Custom Method
        // [ ] - 1) DoAction.
        protected override void DoAction()
        {
            // [ ] - 1) �� ���� + �� ���� �Ҹ� + Box Collider ����.
            animator.SetBool(paramIsOpen, true);
            audioSource.Play();
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion Custom Method
    }
}