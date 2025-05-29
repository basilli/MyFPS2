using Unity.Mathematics;
using UnityEngine;

/* [0] 개요 : PlayerMove
		- 이동은 Rigidbody Force를 이용하여 이동 → 앞으로 자동 이동함.
		- 좌우키를 입력받아 좌우로 이동함.
		- 
		- 
		- 
*/

namespace MySample
{
    public class PlayerMove : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private Rigidbody rb;
        // [ ] - 2) 앞으로 이동.
        [SerializeField] private float forwardForce = 10f;
        // [ ] - 3) 좌우로 이동.
        [SerializeField] private float sideForce = 5f;
        // [ ] - 4) .
        private float dx = 0f;
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 참조.
            rb = this.GetComponent<Rigidbody>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) -1~1.
            dx = Input.GetAxis("Horizontal");
        }

        // [ ] - 3) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - [ ] - 1) .
            rb.AddForce(0f, 0f, forwardForce, ForceMode.Acceleration);
            // [ ] - [ ] - 2) 좌우이동.
            if (dx < 0)
            {
                rb.AddForce(-sideForce, 0f, 0f, ForceMode.Acceleration);
            }
            if (dx > 0)
            {
                rb.AddForce(sideForce, 0f, 0f, ForceMode.Acceleration);
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        #endregion Custom Method
    }

}
// [ ] - ) 
// [ ] - [ ] - ) 