using Unity.Mathematics;
using UnityEngine;

/* [0] ���� : PlayerMove
		- �̵��� Rigidbody Force�� �̿��Ͽ� �̵� �� ������ �ڵ� �̵���.
		- �¿�Ű�� �Է¹޾� �¿�� �̵���.
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
        // [ ] - 1) ����.
        private Rigidbody rb;
        // [ ] - 2) ������ �̵�.
        [SerializeField] private float forwardForce = 10f;
        // [ ] - 3) �¿�� �̵�.
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
            // [ ] - [ ] - 1) ����.
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
            // [ ] - [ ] - 2) �¿��̵�.
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