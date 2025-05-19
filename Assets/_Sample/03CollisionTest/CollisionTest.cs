using UnityEngine;

/* [0] 개요 : CollisionTest
*/

namespace MySample
{
	public class CollisionTest : MonoBehaviour
	{
        // [1] Unity Event Method
        #region Unity Event Method
        // [ ] - 1) OnCollisionEnter.
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"On Collision Enter : {collision.transform.tag}");
            // [ ] - [ ] - 1) MoveObject → 플레이어에게 왼쪽으로 3만큼 힘을 가함.
            MoveObject player = collision.transform.GetComponent<MoveObject>();
            if (player)
            {
                player.MoveLeft();
            }
        }

        // [ ] - 2) OnCollisionStay.
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log($"On Collision Stay : {collision.transform.tag}");
        }

        // [ ] - 3) OnCollisionExit.
        private void OnCollisionExit(Collision collision)
        {

            Debug.Log($"On Collision Exit : {collision.transform.tag}");
        }

        #endregion Unity Event Method


    }
}

// [3] 
// [ ] - ) 
// [ ] - [ ] - ) 