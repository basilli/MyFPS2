using UnityEngine;

/* [0] ���� : FlyingObject
		- ���ư��ٰ� �׶��忡 �������� �ε����� ���� �÷���.
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
                // )        Debug.Log("�ٴڿ� �ε���.");
                AudioManager.Instance.Play("DoorBang2");
            }
        }
        #endregion Unity Event Method
    }

}
