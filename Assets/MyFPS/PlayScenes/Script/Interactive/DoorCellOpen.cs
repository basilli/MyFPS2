using UnityEngine;
using TMPro;

/* [0] ���� : DoorCellOpen
		- ������ ���ͷ�Ƽ�� �׼� ����.
*/

namespace MyFPS
{
	public class DoorCellOpen : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) ���� �÷��̾�� �Ÿ�.
        private float theDistance;
        // [ ] - 2) �׼� UI.
        public GameObject actionUI;
        public TextMeshProUGUI actionText;
        // [ ] - [ ] - 1) ũ�ν����.
        public GameObject extraCross;
        // [ ] - [ ] - 2) ����.
        [SerializeField] private string action = "Open The Door";       // ) ����Ƽ�� Inspector���� ActionText�� ������ ���� �� ����.
        // [ ] - 4) �ִϸ��̼�.
        public Animator animator;
        // [ ] - [ ] - 1) �ִϸ��̼� �Ķ���� ��Ʈ��.
        private string paramIsOpen = "IsOpen";
        #endregion Variables





        // [2] Unity Event Method
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ���� �÷��̾�� �Ÿ� ��������.
            theDistance = PlayCasting.distanceFromTarget;
        }

        // [ ] - 2) OnMouseOver.
        private void OnMouseOver()
        {
            // [ ] - [ ] - 1) ũ�ν���� Ű��.
            extraCross.SetActive(true);

            if (theDistance <= 2f)
            {
                ShowActionUI();

                // )        ToDo : New Input System ����.

                // [ ] - [ ] - 2) Ű �Է� üũ.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // [ ] - [ ] - [ ] - 1) UI ����� + ������ + Box Collider ����.
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
            // [ ] - [ ] - 1) ũ�ν���� ����.
            extraCross.SetActive(false);

            HideActionUI();
        }
        #endregion Unity Event Method





        // [3] Custom Method
        #region Custom Method
        // [ ] - 1) ShowActionUI �� Action UI �����ֱ�.
        private void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
        }
        
        // [ ] - 2) HideActionUI �� Action UI �����.
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