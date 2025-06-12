using UnityEngine;

/* [0] 개요 : Destructable
		- 죽었을 때 Health를 가지고있는 오브젝트를 킬하는 클래스.
*/

namespace Unity.FPS.Game
{
    public class Destructable : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private Health health;
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 참조.
            health = this.GetComponent<Health>();
        }


        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 초기화 → Health의 UnityAction 함수를 등록. 
            health.OnDamaged += OnDamaged;
            health.OnDie += OnDie;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [3] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ OnDamaged → Health의 UnityAction 함수에 OnDamaged가 등록될 함수.
        private void OnDamaged(float damage, GameObject damageSource)
        { 
        // TODO : 데미지 구현.
        }


        // [◆] - ▶▶▶ OnDie.
        void OnDie()
        {
            // [◇] - [◆] - ) 죽음처리.
            ;
            // [◇] - [◆] - ) 오브젝트 킬.
            Destroy(gameObject);
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲

    }
}
