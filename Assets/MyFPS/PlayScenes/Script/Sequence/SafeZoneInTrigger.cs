using UnityEngine;

/* [0] 개요 : SafeZoneInTrigger
		- 트리거에 들어가면 플레이어가 안전지역에 있다고 저장함.
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{

    public class SafeZoneInTrigger : MonoBehaviour
    {
        // [1] Variable.
        #region Variable

        // [ ] - [ ] - ) .
        public GameObject SafeZoneOutTrigger;
        #endregion Variable






        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - [ ] - 1) .
            if (other.tag == "Player")
            {
                PlayerController.safeZoneIn = true;
            }
        }

        // [ ] - 2) OnTriggerExit.
        private void OnTriggerExit(Collider other)
        {
            // [ ] - [ ] - 1) .
            if (other.tag == "Player")
            {
                // [ ] - [ ] - [ ] - 1) SafeZoneInTrigger 활성화.
                SafeZoneOutTrigger.SetActive(true);
                // [ ] - [ ] - [ ] - 2) SafeZoneInTrigger 비활성화.
                this.gameObject.SetActive(false);
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - ) .


        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
        #endregion Custom Method
    }
}