using UnityEngine;

/* [0] ���� : Damageable
		- �������� �Դ� �浹ü���� �������Ѽ� �������� ����ϴ� Ŭ����.
*/

namespace Unity.FPS.Game
{
    public class Damageable : MonoBehaviour
    {
        // [1] Variable.
        #region ������ Variable ������
        // [��] - ������ ����.
        private Health health;
        [SerializeField] private float DamageMutiplier = 1.0f;      // ) ������ ���.
        [SerializeField] private float sensibilityToSelfDamage = 0.5f;        // ) ���� ������ ���.
        #endregion ������ Variable ������





        // [2] Unity Event Method.
        #region ������ Unity Event Method ������
        // [��] - ������ Awake.
        private void Awake()
        {
            // [��] - [��] - ) ����.
            health = this.GetComponent<Health>();
            if (health == null)
            {
                health = this.GetComponentInParent<Health>();       // ) Health ������Ʈ�� ĳ���� ������Ʈ�� �� �� �θ� ������Ʈ�� �پ��־�� ��.
            }
        }
        #endregion ������ Unity Event Method ������





        // [3] Custom Method.
        #region ������ Custom Method ������
        // [��] - ������ InflictDamage �� ���� ���ݿ� ���� ������ ���� üũ.
        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            // [��] - [��] - ) ���� ������ ������ ����� �ϱ����� �غ�.
            if (health == null)
                return;
            var totalDamage = damage;
            // [��] - [��] - ) ���� ������ �ƴ� ��쿡�� ������ ��� ����.
            if (isExplosionDamage == false)
            {
                // [��] - [��] - [��] - ) ������ ��� ����.
                totalDamage *= DamageMutiplier;
            }
            // [��] - [��] - ) ���� ������ üũ.
            if (health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfDamage;
            }
            // [��] - [��] - ) ������ ���� ��� �� ����.
            health.TakeDamage(totalDamage, damageSource);
            #endregion ������ Custom Method ������
        }
    }
}
