using UnityEngine;
using UnityEngine.AI;

/* [0] 개요 : AgentController
		- 
		- 
		- 
		- 
		- 
*/

namespace MySample
{
    public class AgentController : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private NavMeshAgent agent;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 참조.
            agent = this.GetComponent <NavMeshAgent>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 마우스 좌클릭한 지점으로 Agent가 이동.
            if (Input. GetMouseButtonDown(0))
            {
                // [ ] - [ ] - [ ] - 1) . 클릭한 지점을 구하기.
                Vector3 worldPosition = RayToWorld();
                // [ ] - [ ] - [ ] - 2) . Agent의 이동 목표지점 설정.
                agent.SetDestination(worldPosition);
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) 월드 포지션값 가져오기 → 마우스의 위치에 쏘는 Ray를 이용.
        private Vector3 RayToWorld()
        {
            Vector3 worldPos = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point;
            }
            return worldPos;
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .

    }
}
