using UnityEngine;
using UnityEngine.Events;

/* [0] 개요 : RobotHealth
        - 로봇의 체력을 관리하는 클래스.
*/

namespace MyFPS
{
    public class RobotHealth : MonoBehaviour, IDamageable
    {

        // [1] Variable.
        #region Variable
        // [ ] - 1) 체력
        private float currentHealth;
        [SerializeField] private float maxHealth = 20f;
        // [ ] - 2) 킬 딜레이
        [SerializeField] private float destoryDelay = 6f;
        private bool isDeath = false;
        // [ ] - 3) 죽음시 호출되는 이벤트 함수 선언
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
            // [ ] - [ ] - 2) 데미지 연출 → SFX, VFX.
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
            // [ ] - [ ] - 2) 죽음시 등록되는 함수 호출, 보상처리...
            OnDie?.Invoke();
            // [ ] - [ ] - 3) 킬.
            Destroy(gameObject, destoryDelay);
        }
        #endregion Custom Method
    }
}

// [ ] - ) 
// [ ] - [ ] - ) 