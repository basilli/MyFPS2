using MySimple;
using UnityEngine;

/* [0] ���� : Robot
		- Enemy(�κ�)�� �����ϴ� Ŭ����.
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
        // [ ] - 1) ����.
        private Animator animator;
        public Transform thePlayer;
        private RobotHealth robotHealth;
        // [ ] - 2) �κ��� ���� ����.
        private Robotstate robotState;
        // [ ] - 3) �κ��� ���� ����.
        private Robotstate beforeState;
        // [ ] - 4) �̵��ӵ�.
        [SerializeField] public float moveSpeed = 5f;
        // [ ] - 6) ���ݹ���.
        [SerializeField] public float attackRange = 1.5f;
        // [ ] - 7) ���ݷ�.
        [SerializeField] private float attackDamage = 5f;
        // [ ] - 8) ���� Ÿ�̸�.
        [SerializeField] private float attackTime = 2f;
        private float countdown;
        // [ ] - 9) �ִϸ��̼� �Ķ����.
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
            // [ ] - [ ] - 1) ����.
            animator = this.GetComponent<Animator>();
            robotHealth = this.GetComponent<RobotHealth>();
        }

        // [ ] - 2) OnEnable.
        private void OnEnable()
        {
            // [ ] - [ ] - 1) �̺�Ʈ �Լ� ���.
            robotHealth.OnDie += OnDie;

            // [ ] - [ ] - 2) �ʱ�ȭ.
            ChangeState(Robotstate.R_Idle);
        }

        // [ ] - 3) OnDisable.
        private void OnDisable()
        {
            // [ ] - [ ] - 1) �̺�Ʈ �Լ� ����.
            robotHealth.OnDie -= OnDie;
        }

        // [ ] - 3) Update.
        void Update()
        {
            // [ ] - [ ] - 1) .
            if (robotHealth.IsDeath)
                return;
            // [ ] - [ ] - 2) �̵�����.
            Vector3 target = new Vector3(thePlayer.position.x, 0f, thePlayer.position.z);
            Vector3 dir = target - this.transform.position;
            float distance = Vector3.Distance(transform.position, target);
            // [ ] - [ ] - [ ] - 1) ���ݹ��� üũ.
            if (distance <= attackRange)
            {
                ChangeState(Robotstate.R_Attack);
            }
            // [ ] - [ ] - 3) ���±���.
            switch (robotState)
            {
                // [ ] - [ ] - [ ] - 1) ���.
                case Robotstate.R_Idle:
                    break;
                // [ ] - [ ] - [ ] - 2) �̵�.
                case Robotstate.R_Walk:
                    transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
                    transform.LookAt(target);        // ) Ÿ�� �ٶ󺸱�.
                    break;
                // [ ] - [ ] - [ ] - 3) ����.
                case Robotstate.R_Attack:
                    // [ ] - [ ] - [ ] - [ ] - 1) 2�ʸ��� 5�� �������� ��.
                    // )        OnAttackTimer();
                    // [ ] - [ ] - [ ] - [ ] - 2) ���ݹ��� �ٱ����� ������ ������.
                    if (distance > attackDamage)
                    {
                        ChangeState(Robotstate.R_Walk);
                    }
                    break;
                // [ ] - [ ] - [ ] - 4) ���.
                case Robotstate.R_Death:
                    break;
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) ���ο� ���¸� ���¸� �Ű������� �޾� ���ο� ���¸� ����
        public void ChangeState(Robotstate newState)
        {
            // [ ] - [ ] - 1) ���� ���� üũ.
            if (robotState == newState)
            {
                return;
            }
            // [ ] - [ ] - 2) ���� ���¸� ���� ���·� ����.
            beforeState = robotState;
            // [ ] - [ ] - 3) ���ο� ���¸� ���� ���·� ����.
            robotState = newState;
            // [ ] - [ ] - 4) ���� ���濡 ���� ���� ����.
            animator.SetInteger(enemyState, (int)robotState);
        }

        // [ ] - 2) OnAttackTimer �� 2�ʸ��� 5�� �������� ��.
        private void OnAttackTimer()
        {
            countdown += Time.deltaTime;
            if (countdown >= attackTime)
            {
                // [ ] - [ ] - 1) Ÿ�̸� ����.
                Attack();
                // [ ] - [ ] - 2) Ÿ�̸� �ʱ�ȭ.
                countdown = 0;
            }
        }

        // [ ] - 3) Attack.
        public void Attack()
        {
            // [ ] - [ ] - 1) Ÿ�̸� ����.
            Debug.Log($"�÷��̾�� {attackDamage}�� �ش�.");
            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }

        // [ ] - 4) Die.
        private void OnDie()
        {
            // [ ] - [ ] - 1) ���� ó��.
            ChangeState(Robotstate.R_Death);
            // [ ] - [ ] - 2) ����� ����.
            jumpScare.Stop();
            bgm01.Play();
            // [ ] - [ ] - 3) .
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion Custom Method
    }
}

