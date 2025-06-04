using UnityEngine;
using UnityEngine.AI;

/* [0] ���� : AgentController
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
        // [ ] - 1) ����.
        private NavMeshAgent agent;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) ����.
            agent = this.GetComponent <NavMeshAgent>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ���콺 ��Ŭ���� �������� Agent�� �̵�.
            if (Input. GetMouseButtonDown(0))
            {
                // [ ] - [ ] - [ ] - 1) . Ŭ���� ������ ���ϱ�.
                Vector3 worldPosition = RayToWorld();
                // [ ] - [ ] - [ ] - 2) . Agent�� �̵� ��ǥ���� ����.
                agent.SetDestination(worldPosition);
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) ���� �����ǰ� �������� �� ���콺�� ��ġ�� ��� Ray�� �̿�.
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
