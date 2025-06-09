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
        // [ ] - 7) .
        private float distance;
        // [ ] - 8) .
        private Vector3 originPosition;
        // [ ] - 9) 플레이어 위치
        private Vector3 target;
        // [ ] - 10) 공격(총 발사) → 멈추고 Fire 애니메이션.
        [SerializeField] private float attackRange = 5f;
        // [ ] - 11) 공격 데미지.
        [SerializeField] private float attackDamage = 5f;
        // [ ] - 12) 애니메이션 파라미터.
        private string enemyState = "EnemyState";
        private string fire = "Fire";
        // [ ] - 13) 어택 타이머.
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
            // [ ] - [ ] - 1) 초기화.
            originPosition = transform.position;
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 5) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 죽음체크.
            if (gunmanHealth.IsDeath)
                return;

            // [ ] - [ ] - 2) 플레이어 위치 확인 → 안전지역에 있을 경우.
            if (PlayerController.safeZoneIn)
            {
                // [ ] - [ ] - [ ] - 1) .
                if (gunmanState == Robotstate.R_Attack || gunmanState == Robotstate.R_Chase)
                {
                    // [ ] - [ ] - [ ] - [ ] - 1) 제자리로 돌아가기.
                    BackHome();
                    return;
                }
            }
            else        // ) EnemyZone에 있을 경우.
            {
                // [ ] - [ ] - 2) 적 디텍팅.
                target = new Vector3(thePlayer.position.x, 0f, thePlayer.position.z);
                distance = Vector3.Distance(transform.position, target);
                if (distance <= attackRange)        // ) attackRange = 5.
                {
                    // [ ] - [ ] - [ ] - 1) 공격상태로 변경.
                    ChangeState(Robotstate.R_Attack);
                }
                else if (distance <= detectingRange)         // ) detectingRange = 15.
                {
                    ChangeState(Robotstate.R_Chase);        // ) 추격상태로 변경.
                }
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
                    if (agent.remainingDistance <= 0.1f)        // ) 웨이포인트 도착 판정.
                    {
                        ChangeState(Robotstate.R_Idle);
                    }
                    break;
                // [ ] - [ ] - [ ] - 3) R_Attack.
                case Robotstate.R_Attack:
                    attackCountdown += Time.deltaTime;      // 공격 타이머.
                    if (attackCountdown >= attackTime)
                    {
                        animator.SetTrigger(fire);      // 발사.
                        attackCountdown = 0f;       // 타이머 초기화.
                    }
                    transform.LookAt(target);       // 타겟을 바라본다.
                    break;
                // [ ] - [ ] - [ ] - 4) R_Death.
                case Robotstate.R_Death:
                    break;
                // [ ] - [ ] - [ ] - 5) R_Patrol.
                case Robotstate.R_Patrol:

                    break;
                // [ ] - [ ] - [ ] - 6) R_Chase.
                case Robotstate.R_Chase:
                    agent.SetDestination(target);       // ) 타겟을 향해 이동(실시간으로 목표지점을 변경).
                    if (distance > detectingRange)      // ) 플레이어가 디텍팅 거리에서 벗어남.
                    {
                        BackHome(); // ) 제자리로 되돌아가기.
                    }
                    break;
            }
        }

        // [ ] - 6) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            // [ ] - [ ] - 1) 탐지범위.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectingRange);
            // [ ] - [ ] - 2) 공격범위.
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
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
                agent.ResetPath();      // ) 네비게이션 패스 초기화.
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
            agent.enabled = false;
            // [ ] - [ ] - 3) .
            this.GetComponent<BoxCollider>().enabled = false;
        }

        // [ ] - 4) BackHome → 제자리로 되돌아가기..
        public void BackHome()
        {
            // [ ] - [ ] - 1) 패트롤 여부 체크.
            if (wayPoints.Length > 1)
            {
                // [ ] - [ ] - [ ] - 1) 패트롤 상태 변환.
                ChangeState(Robotstate.R_Patrol);
            }
            else
            {
                // [ ] - [ ] - [ ] - 2) 걷기.
                ChangeState(Robotstate.R_Walk);         // ) 지정한 위치로 이동.
                agent.SetDestination(originPosition);
            }
            // [ ] - [ ] - 2) 조준 애니 해제.
            animator.SetLayerWeight(1, 0);
        }



        // [ ] - 5) Attack.
        public void Attack()
        {
            // [ ] - [ ] - 1) 타이머 내용.
            Debug.Log($"플레이어에게 {attackDamage}를 준다.");
            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
    }

}
