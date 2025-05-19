using UnityEngine;
using TMPro;

/* [0] 개요 : DoorCellOpen
		- 문열기 인터렉티브 액션 구현.
*/

namespace MyFPS
{
	public class DoorCellOpen : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) 문과 플레이어간의 거리.
        private float theDistance;
        // [ ] - 2) 액션 UI.
        public GameObject actionUI;
        public TextMeshProUGUI actionText;
        // [ ] - [ ] - 1) 크로스헤어.
        public GameObject extraCross;
        // [ ] - [ ] - 2) 문구.
        [SerializeField] private string action = "Open The Door";       // ) 유니티의 Inspector에서 ActionText의 내용을 적을 수 있음.
        // [ ] - 4) 애니메이션.
        public Animator animator;
        // [ ] - [ ] - 1) 애니메이션 파라미터 스트링.
        private string paramIsOpen = "IsOpen";
        #endregion Variables





        // [2] Unity Event Method
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 문과 플레이어간의 거리 가져오기.
            theDistance = PlayCasting.distanceFromTarget;
        }

        // [ ] - 2) OnMouseOver.
        private void OnMouseOver()
        {
            // [ ] - [ ] - 1) 크로스헤어 키기.
            extraCross.SetActive(true);

            if (theDistance <= 2f)
            {
                ShowActionUI();

                // )        ToDo : New Input System 구현.

                // [ ] - [ ] - 2) 키 입력 체크.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // [ ] - [ ] - [ ] - 1) UI 숨기기 + 문열기 + Box Collider 제거.
                    HideActionUI();
                    animator.SetBool(paramIsOpen, true);
                    this.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                HideActionUI();
            }
        }

        // [ ] - 3) OnMouseExit.
        private void OnMouseExit()
        {
            // [ ] - [ ] - 1) 크로스헤어 끄기.
            extraCross.SetActive(false);

            HideActionUI();
        }
        #endregion Unity Event Method





        // [3] Custom Method
        #region Custom Method
        // [ ] - 1) ShowActionUI → Action UI 보여주기.
        private void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
        }
        
        // [ ] - 2) HideActionUI → Action UI 숨기기.
        private void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
        }
        #endregion Custom Method
    }
}



// [ ] - ) 
// [ ] - [ ] - ) 