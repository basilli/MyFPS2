using UnityEngine;

/* [0] 개요 : RigidBodyTest
		- ForceMode.Force
            : 연속적, 질량을 고려.
            : 바람, 자기력등 연속적으로 주어지는 힘.
        - ForceMode.Acceleration
            : 연속적, 질량은 무시.
            : 중력, 질량과 상관없이 일정한 가속을 구현.
		- ForceMode.Impulse
            : 불연속적, 질량을 고려.
            : 타격, 폭발등 순간적인 힘이 적용.
		- ForceMode.VelocityChange
            : 불연속적, 질량은 무시.
            : 순간적으로 지정한 속도의 변화를 줄 때.
		- 
*/

namespace MySample
{
	public class RigidBodyTest : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) 강체.
        private Rigidbody rb;
        // [ ] - 2) 힘.
        public float power = 100f;
        #endregion Variables





        // [2] Start.
        private void Start()
        {
            // [ ] - 1) 참조.
            rb = this.GetComponent<Rigidbody>();
            // [ ] - 2) 일회성, 월드 앞방향, 100의 힘으로 오브젝트를 이동시킴.
            // )        rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
            // [ ] - 3) 일회성, 로컬 앞방향, 100의 힘으로 오브젝트를 이동시킴.
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