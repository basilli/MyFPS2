using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/* [0] 개요 : EJumpTrigger
		- 날라가는 컵 연출 트리거.
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
        // [ ] - 1) . 플레이어 제어.
        public PlayerInput playerInput;
        // [ ] - 2) . 연출 오브젝트.
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
            // [ ] - [ ] - 1) 태그로 플레이어 체크.
            if (other.tag == "Player")
            {
            }
            // [ ] - [ ] - 2) 트리거 해제.
            this.GetComponent<BoxCollider>().enabled = false;
            // [ ] - [ ] - 3) StartCoroutine → SequercePlayer 실행.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - ) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - [ ] - 1) 플레이어 이동 방지를 위한 인풋 제어.
            playerInput.enabled = false;
            // [ ] - [ ] - 2) 연출 시작.
            activityObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            activityObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 3) 연출이 끝났으니 플레이어 이동을 위한 인풋 제어.
            playerInput.enabled = true;
        }
        #endregion Custom Method
    }
}