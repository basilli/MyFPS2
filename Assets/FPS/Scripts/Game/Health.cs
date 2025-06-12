using UnityEngine;
using UnityEngine.Events;

/* [0] 개요 : Health
		- 체력을 관리하는 클래스.
*/

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        [SerializeField] private float maxHealth = 100f;
        private bool isDeath = false;
        [SerializeField] private float criticalHealthRatio = 0.2f;      // ) 체력이 20% 이하일 경우 위험 경계 비율.
        public UnityAction<float> OnHeal;      // ) 힐을 하면 등록된 함수를 호출함.
        public UnityAction<float, GameObject> OnDamaged;        // ) 데미지를 입으면 등록된 함수를 호출함.
        public UnityAction OnDie;       // ) 죽었을 때 등록된 함수를 호출함.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ 외부에서는 마음대로 값을 바꾸지 못하게 보호하고, 내부에서만 제어.
        public float CurrentHealth { get; private set; }


        // [◆] - ▶▶▶ 무적 체크.
        public bool Invincible { get; set; }
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 초기화.
            CurrentHealth = maxHealth;
            Invincible = false;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ CanPickUp → 힐 아이템을 먹을 수 있는지 체크.
        public bool CanPickUp() => CurrentHealth < maxHealth;


        // [◆] - ▶▶▶ GetRatio → UI에 HP 바 게이지량.
        public float GetRatio() => CurrentHealth / maxHealth;


        // [◆] - ▶▶▶  → 위험체크.
        public bool IsCritical() => GetRatio() <= criticalHealthRatio;


        // [◆] - ▶▶▶ Heal → 힐계산.
        public void Heal(float healthAmount)
        {
            // [◇] - [◆] - ) 힐 하기 전의 체력.
            float beforeHealth = CurrentHealth;
            // [◇] - [◆] - ) .
            CurrentHealth += healthAmount;
            // [◇] - [◆] - ) 체력의 최대 최소값 클램프.
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
            // [◇] - [◆] - ) 리얼 힐.
            float realHeal = CurrentHealth - beforeHealth;
            if (realHeal > 0f)
            {
                // [◇] - [◆] - [◆] - ) 힐구현 → 등록된 함수를 호출함.
                OnHeal?.Invoke(realHeal);
                // )        Debug.Log("Health Heal");

            }

            /* ----------------------------------------------------------------------------------------------------------------------------------------
            // [◇] - [◆] - ) 죽었으면 회복 불가.
            if (isDeath)
                return;
            // [◇] - [◆] - ) 잃은 체력 계산.
            float missingHealth = maxHealth - CurrentHealth;
            // [◇] - [◆] - ) 실제 회복량은 부족한 만큼만 하기.
            float actualHeal = Mathf.Min(healAmount, missingHealth);
            // [◇] - [◆] - ) .
            if (actualHeal > 0f)
            {
                // [◇] - [◆] - [◆] - ) 회복 적용.
                CurrentHealth += actualHeal;
                Debug.Log($"힐 성공! ▶ 회복량: {actualHeal}, 현재 체력: {CurrentHealth}");
                // TODO: 힐 이펙트나 사운드 재생 가능
            }
            else
            {
                Debug.Log("힐 불필요 ▶ 체력이 가득 찼습니다.");
            }
            ---------------------------------------------------------------------------------------------------------------------------------------- */
        }


        // [◆] - ▶▶▶ TakeDamage → 매개변수로 데미지 및 데미지를 준 오브젝트.
        public void TakeDamage(float damage, GameObject damageSource)
        {
            // [◇] - [◆] - ) 무적체크.
            if (Invincible == true)
                return;
            // [◇] - [◆] - ) 실제로 입은 데미지 계산.
            float beforeHealth = CurrentHealth;         // ) 데미지 입기 전의 체력.
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);      // 체력의 최대,최솟값의 클램프.
            // [◇] - [◆] - ) 리얼 데미지.
            float realDamage = beforeHealth - CurrentHealth;
            if (realDamage > 0f)
            {
                // [◇] - [◇] - [◆] ) 데미지 효과 구현.
                OnDamaged?.Invoke(realDamage, damageSource);
            }
            // [◇] - [◆] - ) 죽음처리.
            HandleDeath();
        }


        // [◆] - ▶▶▶ HandleDeath.
        private void HandleDeath()
        {
            // [◇] - [◆] - ) 캐릭터의 사망여부 확인 및 사망시 코드 실행 정지.
            if (isDeath == true)
                return;
            // [◇] - [◆] - ) 체력이 0보다 작을경우 사망.
            if (CurrentHealth <= 0)
            {
                isDeath = true;
                // [◇] - [◇] - [◆] - ) 죽음구현.
                OnDie?.Invoke();
            }
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}