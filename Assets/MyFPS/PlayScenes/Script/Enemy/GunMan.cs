using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/* [0] ���� : GunMan
*/

namespace MyFPS
{
    public class GunMan : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private Animator animator;
        private NavMeshAgent agent;
        public Transform thePlayer;         // ) Ÿ��.
        private RobotHealth gunmanHealth;
        // [ ] - 2) �κ��� ���� ����.
        private Robotstate gunmanState;
        // [ ] - 3) �κ��� ���� ����.
        private Robotstate beforeState;
        // [ ] - 4) ��Ʈ��.
        public Transform[] wayPoints;
        private int nowWayPointIndex = 0;
        // [ ] - 5) ��� Ÿ�̸�.
        [SerializeField] private float idleTime = 2f;
        private float idleCountdown = 0f;
        // [ ] - 6) ������ �Ÿ�.
        [SerializeField] private float detectingRange = 15f;
        // [ ] - 7) �÷��̾� ��ġ
        private Vector3 target;
        // [ ] - 8) ����(�� �߻�) �� ���߰� Fire �ִϸ��̼�.
        [SerializeField] private float attackRange = 5f;
        // [ ] - 9) �ִϸ��̼� �Ķ����.
        private string enemyState = "EnemyState";
        private string fire = "Fire";
        // [ ] - 10) ���� Ÿ�̸�.
        [SerializeField] private float attackTime = 2f;
        private float attackCountdown = 0f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {
            // [ ] - [ ] - 1) ����.
            animator = this.GetComponent<Animator>();
            agent = this.GetComponent<NavMeshAgent>();
            gunmanHealth = this.GetComponent<RobotHealth>();
        }

        // [ ] - 2) OnEnable.
        private void OnEnable()
        {
            // [ ] - [ ] - 1) �̺�Ʈ �Լ� ���.
            gunmanHealth.OnDie += OnDie;
            // [ ] - [ ] - 2) �ʱ�ȭ.
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 3) OnDisable.
        private void OnDisable()
        {
            // [ ] - [ ] - 1) �̺�Ʈ �Լ� ����.
            gunmanHealth.OnDie -= OnDie;
        }

        // [ ] - 4) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) .
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 5) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ����üũ.
            if (gunmanHealth.IsDeath)
                return;

            // [ ] - [ ] - 2) �� ������.
            target = new Vector3(thePlayer.position.x, 0f, thePlayer.position.z);
            float distance = Vector3.Distance(transform.position, target);
            if (distance <= attackRange)        // ) attackRange = 5.
            {
                // [ ] - [ ] - [ ] - 1) ���ݻ��·� ����.
                ChangeState(Robotstate.R_Attack);
            }
            if (distance <= detectingRange)         // ) detectingRange = 15.
            {
                ChangeState(Robotstate.R_Chase);        // ) �߰ݻ��·� ����.
            }

            // [ ] - [ ] - 3) ���±���.
            switch (gunmanState)
            {
                // [ ] - [ ] - [ ] - 1) R_Idle.
                case Robotstate.R_Idle:
                    if (wayPoints.Length > 1)
                    {
                        // [ ] - [ ] - [ ] - [ ] - 1) 
                        idleCountdown += Time.deltaTime;
                        if (idleCountdown >= idleTime)
                        {
                            // [ ] - [ ] - [ ] - [ ] - [ ] - 1) ��Ʈ�� ���� ��ȯ.
                            ChangeState(Robotstate.R_Patrol);
                            // [ ] - [ ] - [ ] - [ ] - [ ] - 1) Ÿ�̸� �ʱ�ȭ
                            idleCountdown = 0f;
                        }
                    }
                    break;
                // [ ] - [ ] - [ ] - 2) R_Walk.
                case Robotstate.R_Walk:
                    break;
                // [ ] - [ ] - [ ] - 3) R_Attack.
                case Robotstate.R_Attack:
                    attackCountdown += Time.deltaTime;      // ���� Ÿ�̸�.
                    if (attackCountdown >= attackTime)
                    {
                        animator.SetTrigger(fire);      // �߻�.
                        attackCountdown = 0f;       // Ÿ�̸� �ʱ�ȭ.
                    }
                    break;
                // [ ] - [ ] - [ ] - 4) R_Death.
                case Robotstate.R_Death:
                    break;
                // [ ] - [ ] - [ ] - 5) R_Patrol.
                case Robotstate.R_Patrol:
                    if (agent.remainingDistance <= 0.2f)        // ) ��������Ʈ ���� ����.
                    {
                        ChangeState(Robotstate.R_Idle);
                    }
                    break;
                // [ ] - [ ] - [ ] - 6) R_Chase.
                case Robotstate.R_Chase:
                    agent.SetDestination(target);       // ) Ÿ���� ���� �̵�(�ǽð����� ��ǥ������ ����).
                    break;
            }
        }

        // [ ] - 6) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectingRange);
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) ���ο� ���¸� ���¸� �Ű������� �޾� ���ο� ���¸� ����
        public void ChangeState(Robotstate newState)
        {
            // [ ] - [ ] - 1) ���� ���� üũ.
            if (gunmanState == newState)
            {
                return;
            }
            // [ ] - [ ] - 2) ���� ���¸� ���� ���·� ����.
            beforeState = gunmanState;
            // [ ] - [ ] - 3) ���ο� ���¸� ���� ���·� ����.
            gunmanState = newState;
            // [ ] - [ ] - 4) ���� ���濡 ���� ���� ����.
            if (gunmanState == Robotstate.R_Patrol)
            {
                // [ ] - [ ] - [ ] - 1) .
                animator.SetInteger(enemyState, (int)Robotstate.R_Walk);
                // [ ] - [ ] - [ ] - 2) ���� ��������Ʈ�� �̵�.
                GoNextWayPoint();
            }
            else if (gunmanState == Robotstate.R_Idle)
            {
                animator.SetInteger(enemyState, (int)gunmanState);
                idleTime = Random.Range(2f, 5f);
            }
            else if (gunmanState == Robotstate.R_Chase)
            {
                animator.SetInteger(enemyState, (int)Robotstate.R_Walk);
                agent.SetDestination(target);       // ) Ÿ���� ���� �̵�.
                animator.SetLayerWeight(1, 1);      // ) ���� �ִ� ����.
            }
            else if (gunmanState == Robotstate.R_Attack)
            {
                animator.SetInteger(enemyState, (int)Robotstate.R_Idle);
                animator.SetLayerWeight(1, 1);      // ���� �ִ� ����.
                animator.SetTrigger(fire);
                agent.ResetPath();      // Ÿ���� ���� �̵� ����.

            }
            else
            {
                animator.SetInteger(enemyState, (int)gunmanState);
            }
        }

        // [ ] - 2) GoNextWayPoint.
        private void GoNextWayPoint()
        {
            nowWayPointIndex++;
            if (nowWayPointIndex >= wayPoints.Length)
            {
                nowWayPointIndex = 0;
            }
            agent.SetDestination(wayPoints[nowWayPointIndex].position);
        }


        // [ ] - 3) Die.
        private void OnDie()
        {
            // [ ] - [ ] - 1) ���� ó��.
            ChangeState(Robotstate.R_Death);
            // [ ] - [ ] - 2) �߰����� ����.
            // [ ] - [ ] - 3) .
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
    }

}
