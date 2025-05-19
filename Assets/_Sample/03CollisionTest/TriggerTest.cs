using UnityEngine;

/* [0] ���� : TriggerTest
*/

namespace MySample
{

    public class TriggerTest : MonoBehaviour
    {
        // [1] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"On Trigger Enter : {other.tag}");
            // [ ] - [ ] - 1) MoveObject �� �÷��̾�� ���������� 3��ŭ ���� ���ϸ�, ���������� �ٲ�.
            MoveObject player = other.GetComponent<MoveObject>();
            if (player)
            {
                player.MoveRight();
                player.ChangeColorRed();
            }
        }

        // [ ] - 2) OnCollisionStay.
        private void OnTriggerStay(Collider other)
        {
            Debug.Log($"On Trigger Stay : {other.tag}");
        }


        // [ ] - 3) OnTriggerExit.
        private void OnTriggerExit(Collider other)
        {
            MoveObject player = other.GetComponent<MoveObject>();
            if (player)
            {
                player.MoveRight();
                player.ChangeColorOrigin();
            }
        }

        #endregion Unity Event Method

    }
}

// [1] 
// [2] 
// [3] 
// [ ] - ) 
// [ ] - [ ] - ) 