using UnityEngine;

/* [0] ���� : SafeZoneInTrigger
		- Ʈ���ſ� ���� �÷��̾ ���������� �ִٰ� ������.
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
                // [ ] - [ ] - [ ] - 1) SafeZoneInTrigger Ȱ��ȭ.
                SafeZoneOutTrigger.SetActive(true);
                // [ ] - [ ] - [ ] - 2) SafeZoneInTrigger ��Ȱ��ȭ.
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