using MySimple;
using UnityEngine;

/* [0] 개요 : Robot
		- Enemy(로봇)을 제어하는 클래스.
*/

namespace MyFPS
{
    // [0] Robotstate.
    public enum Robotstate
    {
        R_Idle = 0, 
        R_Walk = 1,
        R_Attack = 2, 
        R_Death = 3,
        R_Patrol,
        R_Chase
    }
    public class Robot : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private Animator animator;
        public Transform thePlayer;
        private RobotHealth robotHealth;
        // [ ] - 2) 로봇의 현재 상태.
        private Robotstate robotState;
        // [ ] - 3) 로봇의 이전 상태.
        private Robotstate beforeState;
        // [ ] - 4) 이동속도.
        [SerializeField] public float moveSpeed = 5f;
        // [ ] - 6) 공격범위.
        [SerializeField] public float attackRange = 1.5f;
        // [ ] - 7) 공격력.
        [SerializeField] private float attackDamage = 5f;
        // [ ] - 8) 공격 타이머.
        [SerializeField] private float attackTime = 2f;
        private float countdown;
        // [ ] - 9) 애니메이션 파라미터.
        private string enemyState = "EnemyState";
        //
        public AudioSource jumpScare;
        public AudioSource bgm01;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {
            // [ ] - [ ] - 1) 참조.
            animator = this.GetComponent<Animator>();
            robotHealth = this.GetComponent<RobotHealth>();
        }

        // [ ] - 2) OnEnable.
        private void OnEnable()
        {
            // [ ] - [ ] - 1) 이벤트 함수 등록.
            robotHealth.OnDie += OnDie;

            // [ ] - [ ] - 2) 초기화.
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 3) OnDisable.
        private void OnDisable()
        {
            // [ ] - [ ] - 1) 이벤트 함수 해제.
            robotHealth.OnDie -= OnDie;
        }

        // [ ] - 3) Update.
        void Update()
        {
            // [ ] - [ ] - 1) .
            if (robotHealth.IsDeath)
                return;
            // [ ] - [ ] - 2) 이동구현.
            Vector3 target = new Vector3(thePlayer.position.x, 0f, thePlayer.position.z);
            Vector3 dir = target - this.transform.position;
            float distance = Vector3.Distance(transform.position, target);
            // [ ] - [ ] - [ ] - 1) 공격범위 체크.
            if (distance <= attackRange)
            {
                ChangeState(Robotstate.R_Attack);
            }
            // [ ] - [ ] - 3) 상태구현.
            switch (robotState)
            {
                // [ ] - [ ] - [ ] - 1) 대기.
                case Robotstate.R_Idle:
                    break;
                // [ ] - [ ] - [ ] - 2) 이동.
                case Robotstate.R_Walk:
                    transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
                    transform.LookAt(target);        // ) 타겟 바라보기.
                    break;
                // [ ] - [ ] - [ ] - 3) 공격.
                case Robotstate.R_Attack:
                    // [ ] - [ ] - [ ] - [ ] - 1) 2초마다 5의 데미지를 줌.
                    // )        OnAttackTimer();
                    // [ ] - [ ] - [ ] - [ ] - 2) 공격범위 바깥으로 나가면 추적함.
                    if (distance > attackDamage)
                    {
                        ChangeState(Robotstate.R_Walk);
                    }
                    break;
                // [ ] - [ ] - [ ] - 4) 사망.
                case Robotstate.R_Death:
                    break;
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) 새로운 상태를 상태를 매개변수로 받아 새로운 상태를 셋팅
        public void ChangeState(Robotstate newState)
        {
            // [ ] - [ ] - 1) 현재 상태 체크.
            if (robotState == newState)
            {
                return;
            }
            // [ ] - [ ] - 2) 현재 상태를 이전 상태로 저장.
            beforeState = robotState;
            // [ ] - [ ] - 3) 새로운 상태를 현재 상태로 저장.
            robotState = newState;
            // [ ] - [ ] - 4) 상태 변경에 따른 구현 내용.
            animator.SetInteger(enemyState, (int)robotState);
        }

        // [ ] - 2) OnAttackTimer → 2초마다 5의 데미지를 줌.
        private void OnAttackTimer()
        {
            countdown += Time.deltaTime;
            if (countdown >= attackTime)
            {
                // [ ] - [ ] - 1) 타이머 내용.
                Attack();
                // [ ] - [ ] - 2) 타이머 초기화.
                countdown = 0;
            }
        }

        // [ ] - 3) Attack.
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

        // [ ] - 4) Die.
        private void OnDie()
        {
            // [ ] - [ ] - 1) 죽음 처리.
            ChangeState(Robotstate.R_Death);
            // [ ] - [ ] - 2) 배경음 변경.
            jumpScare.Stop();
            bgm01.Play();
            // [ ] - [ ] - 3) .
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion Custom Method
    }
}

