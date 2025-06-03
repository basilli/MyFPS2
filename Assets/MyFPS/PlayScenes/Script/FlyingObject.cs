using UnityEngine;

/* [0] 개요 : FlyingObject
		- 날아가다가 그라운드에 떨어지면 부딪히는 사운드 플레이.
*/

namespace MyFPS
{
    public class FlyingObject : MonoBehaviour
    {
        // [1] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnCollisionEnter.
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground" && collision.relativeVelocity.magnitude > 1.0f)
            {
                // )        Debug.Log("바닥에 부딪힘.");
                AudioManager.Instance.Play("DoorBang2");
            }
        }
        #endregion Unity Event Method
    }

}
