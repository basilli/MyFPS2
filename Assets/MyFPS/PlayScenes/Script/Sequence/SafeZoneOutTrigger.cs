using UnityEngine;

/* [0] ���� : SafeZoneOutTrigger
		- Ʈ���ſ� ������ �÷��̾ ���������� ���ٰ� ������.
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
                // [ ] - [ ] - [ ] - 1) SafeZoneOutTrigger Ȱ��ȭ.
                SafeZoneInTrigger.SetActive(true);
                // [ ] - [ ] - [ ] - 2) SafeZoneOutTrigger ��Ȱ��ȭ.
                this.gameObject.SetActive(false);
            }
        }
            #endregion Unity Event Method
    }
}
