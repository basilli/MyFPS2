using UnityEngine;

/* [0] ���� : RigidBodyTest
		- ForceMode.Force
            : ������, ������ ���.
            : �ٶ�, �ڱ�µ� ���������� �־����� ��.
        - ForceMode.Acceleration
            : ������, ������ ����.
            : �߷�, ������ ������� ������ ������ ����.
		- ForceMode.Impulse
            : �ҿ�����, ������ ���.
            : Ÿ��, ���ߵ� �������� ���� ����.
		- ForceMode.VelocityChange
            : �ҿ�����, ������ ����.
            : ���������� ������ �ӵ��� ��ȭ�� �� ��.
		- 
*/

namespace MySample
{
	public class RigidBodyTest : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) ��ü.
        private Rigidbody rb;
        // [ ] - 2) ��.
        public float power = 100f;
        #endregion Variables





        // [2] Start.
        private void Start()
        {
            // [ ] - 1) ����.
            rb = this.GetComponent<Rigidbody>();
            // [ ] - 2) ��ȸ��, ���� �չ���, 100�� ������ ������Ʈ�� �̵���Ŵ.
            // )        rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
            // [ ] - 3) ��ȸ��, ���� �չ���, 100�� ������ ������Ʈ�� �̵���Ŵ.
            rb.AddForce(transform.forward * power, ForceMode.Impulse);
            // )        rb.AddRelativeForce(Vector3.forward * power, ForceMode.Impulse);
        }





        // [3] FixedUpdate.
        private void FixedUpdate()
        {
            // rb.AddForce(Vector3.forward * power, ForceMode.Force);
        }
    }
}



// [ ] - ) 
// [ ] - [ ] - ) 