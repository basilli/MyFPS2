using TMPro;
using UnityEngine;

/* [0] 개요 : PickupPistol
		- 아이템(권총) 획득 인터랙티브 구현.
*/

namespace MyFPS
{
    public class PickupPistol : Interactive
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 인터랙티브 액션.
        public GameObject realPistol;
        public GameObject theArrow;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) 총줍기 + 화살표 제거 + 무기데이터 저장 + Box Collider 제거.
        protected override void DoAction()
        {
            realPistol.SetActive(true);
            theArrow.SetActive(false);
            PlayerDataManager.Instance.Weapon = WeaponType.Pistol;
            this.gameObject.SetActive(false);
        }
        #endregion Custom Method
    }
}