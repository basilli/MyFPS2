using UnityEngine;

/* [0] ���� : PlayCasting
		- �÷��̾�� ���鿡 �ִ� ������Ʈ(�ݶ��̴�)���� �Ÿ��� ���ϴ� Ŭ����.
		- RayCast�� �̿�.
*/

namespace MyFPS
{
    public class PlayCasting : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) Ÿ�ٱ����� �Ÿ�.
        public static float distanceFromTarget;
        // [ ] - 2) �ν�����â������ Ÿ�ٱ����� �Ÿ��� ������.
        public float toTarget;
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Start()
        {
            // [ ] - [ ] - 1) �ʱ�ȭ.
            distanceFromTarget = Mathf.Infinity;
            toTarget = distanceFromTarget;
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) Ray�� ���� �Ÿ��� ���ϱ�.
            RaycastHit hit;     // ) Ray�� Hit ������ �����ϴ� ����.
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
            {
                distanceFromTarget = hit.distance;
                toTarget = distanceFromTarget;
            }
        }

        // [ ] - 3) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            // Ray ����� �׸��� �� ���� 100
            RaycastHit hit;
            float maxDistance = 100f;
            bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit );
            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(transform.position, transform.forward*hit.distance);
            }
            else
            {
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }
        #endregion Unity Event Method
    }
}



// [3] 
// [ ] - ) 
// [ ] - [ ] - ) 