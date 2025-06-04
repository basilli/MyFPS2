using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/* [0] 개요 : GunMan
*/

namespace MyFPS
{
    public class GunMan : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private Animator animator;
        private NavMeshAgent agent;
        public Transform thePlayer;         // ) 타켓.
        private RobotHealth gunmanHealth;
        // [ ] - 2) 로봇의 현재 상태.
        private Robotstate gunmanState;
        // [ ] - 3) 로봇의 이전 상태.
        private Robotstate beforeState;
        // [ ] - 4) 패트롤.
        public Transform[] wayPoints;
        private int nowWayPointIndex = 0;
        // [ ] - 5) 대기 타이머.
        [SerializeField] private float idleTime = 2f;
        private float idleCountdown = 0f;
        // [ ] - 6) 디텍팅 거리.
        [SerializeField] private float detectingRange = 15f;
        // [ ] - 7) 플레이어 위치
        private Vector3 target;
        // [ ] - 8) 공격(총 발사) → 멈추고 Fire 애니메이션.
        [SerializeField] private float attackRange = 5f;
        // [ ] - 9) 애니메이션 파라미터.
        private string enemyState = "EnemyState";
        private string fire = "Fire";
        // [ ] - 10) 어택 타이머.
        [SerializeField] private float attackTime = 2f;
        private float attackCountdown = 0f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {
            // [ ] - [ ] - 1) 참조.
            animator = this.GetComponent<Animator>();
            agent = this.GetComponent<NavMeshAgent>();
            gunmanHealth = this.GetComponent<RobotHealth>();
        }

        // [ ] - 2) OnEnable.
        private void OnEnable()
        {
            // [ ] - [ ] - 1) 이벤트 함수 등록.
            gunmanHealth.OnDie += OnDie;
            // [ ] - [ ] - 2) 초기화.
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 3) OnDisable.
        private void OnDisable()
        {
            // [ ] - [ ] - 1) 이벤트 함수 해제.
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
            // [ ] - [ ] - 1) 죽음체크.
            if (gunmanHealth.IsDeath)
                return;

            // [ ] - [ ] - 2) 적 디텍팅.
            target = new Vector3(thePlayer.position.x, 0f, thePlayer.position.z);
            float distance = Vector3.Distance(transform.position, target);
            if (distance <= attackRange)        // ) attackRange = 5.
            {
                // [ ] - [ ] - [ ] - 1) 공격상태로 변경.
                ChangeState(Robotstate.R_Attack);
            }
            if (distance <= detectingRange)         // ) detectingRange = 15.
            {
                ChangeState(Robotstate.R_Chase);        // ) 추격상태로 변경.
            }

            // [ ] - [ ] - 3) 상태구현.
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
                            // [ ] - [ ] - [ ] - [ ] - [ ] - 1) 패트롤 상태 변환.
                            ChangeState(Robotstate.R_Patrol);
                            // [ ] - [ ] - [ ] - [ ] - [ ] - 1) 타이머 초기화
                            idleCountdown = 0f;
                        }
                    }
                    break;
                // [ ] - [ ] - [ ] - 2) R_Walk.
                case Robotstate.R_Walk:
                    break;
                // [ ] - [ ] - [ ] - 3) R_Attack.
                case Robotstate.R_Attack:
                    attackCountdown += Time.deltaTime;      // 공격 타이머.
                    if (attackCountdown >= attackTime)
                    {
                        animator.SetTrigger(fire);      // 발사.
                        attackCountdown = 0f;       // 타이머 초기화.
                    }
                    break;
                // [ ] - [ ] - [ ] - 4) R_Death.
                case Robotstate.R_Death:
                    break;
                // [ ] - [ ] - [ ] - 5) R_Patrol.
                case Robotstate.R_Patrol:
                    if (agent.remainingDistance <= 0.2f)        // ) 웨이포인트 도착 판정.
                    {
                        ChangeState(Robotstate.R_Idle);
                    }
                    break;
                // [ ] - [ ] - [ ] - 6) R_Chase.
                case Robotstate.R_Chase:
                    agent.SetDestination(target);       // ) 타겟을 향해 이동(실시간으로 목표지점을 변경).
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
        // [ ] - 1) 새로운 상태를 상태를 매개변수로 받아 새로운 상태를 셋팅
        public void ChangeState(Robotstate newState)
        {
            // [ ] - [ ] - 1) 현재 상태 체크.
            if (gunmanState == newState)
            {
                return;
            }
            // [ ] - [ ] - 2) 현재 상태를 이전 상태로 저장.
            beforeState = gunmanState;
            // [ ] - [ ] - 3) 새로운 상태를 현재 상태로 저장.
            gunmanState = newState;
            // [ ] - [ ] - 4) 상태 변경에 따른 구현 내용.
            if (gunmanState == Robotstate.R_Patrol)
            {
                // [ ] - [ ] - [ ] - 1) .
                animator.SetInteger(enemyState, (int)Robotstate.R_Walk);
                // [ ] - [ ] - [ ] - 2) 다음 웨이포인트로 이동.
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
                agent.SetDestination(target);       // ) 타겟을 향해 이동.
                animator.SetLayerWeight(1, 1);      // ) 조준 애니 적용.
            }
            else if (gunmanState == Robotstate.R_Attack)
            {
                animator.SetInteger(enemyState, (int)Robotstate.R_Idle);
                animator.SetLayerWeight(1, 1);      // 조준 애니 적용.
                animator.SetTrigger(fire);
                agent.ResetPath();      // 타겟을 향한 이동 멈춤.

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
            // [ ] - [ ] - 1) 죽음 처리.
            ChangeState(Robotstate.R_Death);
            // [ ] - [ ] - 2) 추가구현 내용.
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
