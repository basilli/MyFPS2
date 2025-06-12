using UnityEngine;
using UnityEngine.Events;

/* [0] ���� : Health
		- ü���� �����ϴ� Ŭ����.
*/

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        // [1] Variable.
        #region ������ Variable ������
        // [��] - ������ ����.
        [SerializeField] private float maxHealth = 100f;
        private bool isDeath = false;
        [SerializeField] private float criticalHealthRatio = 0.2f;      // ) ü���� 20% ������ ��� ���� ��� ����.
        public UnityAction<float> OnHeal;      // ) ���� �ϸ� ��ϵ� �Լ��� ȣ����.
        public UnityAction<float, GameObject> OnDamaged;        // ) �������� ������ ��ϵ� �Լ��� ȣ����.
        public UnityAction OnDie;       // ) �׾��� �� ��ϵ� �Լ��� ȣ����.
        #endregion ������ Variable ������





        // [2] Property.
        #region ������ Property ������
        // [��] - ������ �ܺο����� ������� ���� �ٲ��� ���ϰ� ��ȣ�ϰ�, ���ο����� ����.
        public float CurrentHealth { get; private set; }


        // [��] - ������ ���� üũ.
        public bool Invincible { get; set; }
        #endregion ������ Property ������





        // [3] Unity Event Method.
        #region ������ Unity Event Method ������
        // [��] - ������ Start.
        private void Start()
        {
            // [��] - [��] - ) �ʱ�ȭ.
            CurrentHealth = maxHealth;
            Invincible = false;
        }
        #endregion ������ Unity Event Method ������





        // [4] Custom Method.
        #region ������ Custom Method ������
        // [��] - ������ CanPickUp �� �� �������� ���� �� �ִ��� üũ.
        public bool CanPickUp() => CurrentHealth < maxHealth;


        // [��] - ������ GetRatio �� UI�� HP �� ��������.
        public float GetRatio() => CurrentHealth / maxHealth;


        // [��] - ������  �� ����üũ.
        public bool IsCritical() => GetRatio() <= criticalHealthRatio;


        // [��] - ������ Heal �� �����.
        public void Heal(float healthAmount)
        {
            // [��] - [��] - ) �� �ϱ� ���� ü��.
            float beforeHealth = CurrentHealth;
            // [��] - [��] - ) .
            CurrentHealth += healthAmount;
            // [��] - [��] - ) ü���� �ִ� �ּҰ� Ŭ����.
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
            // [��] - [��] - ) ���� ��.
            float realHeal = CurrentHealth - beforeHealth;
            if (realHeal > 0f)
            {
                // [��] - [��] - [��] - ) ������ �� ��ϵ� �Լ��� ȣ����.
                OnHeal?.Invoke(realHeal);
                // )        Debug.Log("Health Heal");

            }

            /* ----------------------------------------------------------------------------------------------------------------------------------------
            // [��] - [��] - ) �׾����� ȸ�� �Ұ�.
            if (isDeath)
                return;
            // [��] - [��] - ) ���� ü�� ���.
            float missingHealth = maxHealth - CurrentHealth;
            // [��] - [��] - ) ���� ȸ������ ������ ��ŭ�� �ϱ�.
            float actualHeal = Mathf.Min(healAmount, missingHealth);
            // [��] - [��] - ) .
            if (actualHeal > 0f)
            {
                // [��] - [��] - [��] - ) ȸ�� ����.
                CurrentHealth += actualHeal;
                Debug.Log($"�� ����! �� ȸ����: {actualHeal}, ���� ü��: {CurrentHealth}");
                // TODO: �� ����Ʈ�� ���� ��� ����
            }
            else
            {
                Debug.Log("�� ���ʿ� �� ü���� ���� á���ϴ�.");
            }
            ---------------------------------------------------------------------------------------------------------------------------------------- */
        }


        // [��] - ������ TakeDamage �� �Ű������� ������ �� �������� �� ������Ʈ.
        public void TakeDamage(float damage, GameObject damageSource)
        {
            // [��] - [��] - ) ����üũ.
            if (Invincible == true)
                return;
            // [��] - [��] - ) ������ ���� ������ ���.
            float beforeHealth = CurrentHealth;         // ) ������ �Ա� ���� ü��.
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);      // ü���� �ִ�,�ּڰ��� Ŭ����.
            // [��] - [��] - ) ���� ������.
            float realDamage = beforeHealth - CurrentHealth;
            if (realDamage > 0f)
            {
                // [��] - [��] - [��] ) ������ ȿ�� ����.
                OnDamaged?.Invoke(realDamage, damageSource);
            }
            // [��] - [��] - ) ����ó��.
            HandleDeath();
        }


        // [��] - ������ HandleDeath.
        private void HandleDeath()
        {
            // [��] - [��] - ) ĳ������ ������� Ȯ�� �� ����� �ڵ� ���� ����.
            if (isDeath == true)
                return;
            // [��] - [��] - ) ü���� 0���� ������� ���.
            if (CurrentHealth <= 0)
            {
                isDeath = true;
                // [��] - [��] - [��] - ) ��������.
                OnDie?.Invoke();
            }
        }
        #endregion ������ Custom Method ������
    }
}