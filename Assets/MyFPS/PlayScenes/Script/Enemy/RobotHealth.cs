using UnityEngine;
using UnityEngine.Events;

/* [0] ���� : RobotHealth
        - �κ��� ü���� �����ϴ� Ŭ����.
*/

namespace MyFPS
{
    public class RobotHealth : MonoBehaviour, IDamageable
    {

        // [1] Variable.
        #region Variable
        // [ ] - 1) ü��
        private float currentHealth;
        [SerializeField] private float maxHealth = 20f;
        // [ ] - 2) ų ������
        [SerializeField] private float destoryDelay = 6f;
        private bool isDeath = false;
        // [ ] - 3) ������ ȣ��Ǵ� �̺�Ʈ �Լ� ����
        public UnityAction OnDie;
        #endregion Variable





        // [2] Property.
        #region Property
        public bool IsDeath => isDeath;
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            currentHealth = maxHealth;
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) TakeDamage.
        public void TakeDamage(float damage)
        {
            // [ ] - [ ] - 1) .
            currentHealth -= damage;
            Debug.Log($"Robot CurrentHealth : {currentHealth}");
            // [ ] - [ ] - 2) ������ ���� �� SFX, VFX.
            if (currentHealth <= 0f && isDeath == false)
            {
                Die();
            }
        }

        // [ ] - 2) Die.
        private void Die()
        {
            // [ ] - [ ] - 1) .
            isDeath = true;
            // [ ] - [ ] - 2) ������ ��ϵǴ� �Լ� ȣ��, ����ó��...
            OnDie?.Invoke();
            // [ ] - [ ] - 3) ų.
            Destroy(gameObject, destoryDelay);
        }
        #endregion Custom Method
    }
}

// [ ] - ) 
// [ ] - [ ] - ) 