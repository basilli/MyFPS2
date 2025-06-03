using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/* [0] ���� : EJumpTrigger
		- ���󰡴� �� ���� Ʈ����.
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{
    public class EJumpTrigger : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) . �÷��̾� ����.
        public PlayerInput playerInput;
        // [ ] - 2) . ���� ������Ʈ.
        public GameObject activityObject;
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) �±׷� �÷��̾� üũ.
            if (other.tag == "Player")
            {
            }
            // [ ] - [ ] - 2) Ʈ���� ����.
            this.GetComponent<BoxCollider>().enabled = false;
            // [ ] - [ ] - 3) StartCoroutine �� SequercePlayer ����.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - ) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - [ ] - 1) �÷��̾� �̵� ������ ���� ��ǲ ����.
            playerInput.enabled = false;
            // [ ] - [ ] - 2) ���� ����.
            activityObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            activityObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 3) ������ �������� �÷��̾� �̵��� ���� ��ǲ ����.
            playerInput.enabled = true;
        }
        #endregion Custom Method
    }
}