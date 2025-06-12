using UnityEngine;

/* [0] 개요 : Damageable
		- 데미지를 입는 충돌체마다 부착시켜서 데미지를 계산하는 클래스.
*/

namespace Unity.FPS.Game
{
    public class Damageable : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private Health health;
        [SerializeField] private float DamageMutiplier = 1.0f;      // ) 데미지 계수.
        [SerializeField] private float sensibilityToSelfDamage = 0.5f;        // ) 셀프 데미지 계수.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 참조.
            health = this.GetComponent<Health>();
            if (health == null)
            {
                health = this.GetComponentInParent<Health>();       // ) Health 컴포넌트는 캐릭터 오브젝트의 맨 위 부모 오브젝트에 붙어있어야 함.
            }
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [3] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ InflictDamage → 범위 공격에 의한 데미지 여부 체크.
        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            // [◇] - [◆] - ) 실제 적용할 데미지 계산을 하기위한 준비.
            if (health == null)
                return;
            var totalDamage = damage;
            // [◇] - [◆] - ) 범위 공격이 아닌 경우에만 데미지 계수 적용.
            if (isExplosionDamage == false)
            {
                // [◇] - [◇] - [◆] - ) 데미지 계수 연산.
                totalDamage *= DamageMutiplier;
            }
            // [◇] - [◆] - ) 셀프 데미지 체크.
            if (health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfDamage;
            }
            // [◇] - [◆] - ) 데미지 최종 계산 후 적용.
            health.TakeDamage(totalDamage, damageSource);
            #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
        }
    }
}
