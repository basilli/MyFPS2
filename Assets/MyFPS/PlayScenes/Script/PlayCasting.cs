using UnityEngine;

/* [0] 개요 : PlayCasting
		- 플레이어와 정면에 있는 오브젝트(콜라이더)와의 거리를 구하는 클래스.
		- RayCast를 이용.
*/

namespace MyFPS
{
    public class PlayCasting : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 타겟까지의 거리.
        public static float distanceFromTarget;
        // [ ] - 2) 인스펙터창에서의 타겟까지의 거리의 디버깅용.
        public float toTarget;
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Start()
        {
            // [ ] - [ ] - 1) 초기화.
            distanceFromTarget = Mathf.Infinity;
            toTarget = distanceFromTarget;
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) Ray를 쏴서 거리를 구하기.
            RaycastHit hit;     // ) Ray의 Hit 정보를 저장하는 변수.
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
            {
                distanceFromTarget = hit.distance;
                toTarget = distanceFromTarget;
            }
        }

        // [ ] - 3) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            // Ray 기즈모 그리기 및 길이 100
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