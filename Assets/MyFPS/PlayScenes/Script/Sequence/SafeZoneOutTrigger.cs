using UnityEngine;

/* [0] 개요 : SafeZoneOutTrigger
		- 트리거에 들어오면 플레이어가 안전지역에 없다고 저장함.
*/

namespace MyFPS
{
    public class SafeZoneOutTrigger : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - ) .
        public GameObject SafeZoneInTrigger;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - [ ] - 1) .
            if (other.tag == "Player")
            {
                PlayerController.safeZoneIn = false;
            }
        }

        // [ ] - 2) OnTriggerExit.
        private void OnTriggerExit(Collider other)
        {
            // [ ] - [ ] - 1) .
            if (other.tag == "Player")
            {
                // [ ] - [ ] - [ ] - 1) SafeZoneOutTrigger 활성화.
                SafeZoneInTrigger.SetActive(true);
                // [ ] - [ ] - [ ] - 2) SafeZoneOutTrigger 비활성화.
                this.gameObject.SetActive(false);
            }
        }
            #endregion Unity Event Method
    }
}
