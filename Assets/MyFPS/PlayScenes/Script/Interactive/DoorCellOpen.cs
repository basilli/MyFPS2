using UnityEngine;
using TMPro;

/* [0] 개요 : DoorCellOpen
		- 문열기 인터렉티브 액션 구현.
*/

namespace MyFPS
{
	public class DoorCellOpen : Interactive
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 애니메이션.
        public Animator animator;
        // [ ] - [ ] - 1) 애니메이션 파라미터 스트링.
        protected private string paramIsOpen = "IsOpen";
        // [ ] - 2) 문 여는 소리
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
            // [ ] - 1) 문 열기 + 문 여는 소리 + Box Collider 제거.
            animator.SetBool(paramIsOpen, true);
            audioSource.Play();
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion Custom Method
    }
}