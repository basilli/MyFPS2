using UnityEngine;
using UnityEngine.Events;

/* [0] 개요 : ProjectileBase
		- Projectile 클래스들의 부모 클래스(추상 클래스)
*/

namespace Unity.FPS.Game
{
    public class ProjectileBase : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 발사 시 등록된 함수를 호출되는 이벤트 함수.
        public UnityAction OnShoot;


        // [◆] - ▶▶▶ 456.


        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        public GameObject Owner { get; private set; }                       // ) 발사한 무기의 주인.
        public Vector3 InitialPosition { get; private set; }                    // ) 발사된 초기 위치.
        public Vector3 InitialDirection { get; private set; }                   // ) 발사할 때의 초기 앞 방향.
        public Vector3 InheritedMuzzleVelocity { get; private set; }        // ) 발사할 때 총구의 이동속도.
        public float InitalCharge { get; private set; }                         // ) 슛 타임의 무기의 충전량.
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Shoot → 매개변수로 발사체를 발사하는 무기를 받아옴.
        public void Shoot(WeaponController controller)
        {
            Owner = controller.Owner;
            InitialPosition = this.transform.position;
            InitialDirection = this.transform.forward;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity;
            InitalCharge = controller.CurrentCharge;
            // [◇] - [◆] - ) 발사시 등록된 함수들을 호출.
            OnShoot?.Invoke();
        }

        // [◆] - ▶▶▶ 456.


        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}
