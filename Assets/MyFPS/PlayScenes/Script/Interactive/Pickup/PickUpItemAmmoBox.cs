using UnityEngine;

/* [0] 개요 : PickUpItemAmmoBox
		- 아이템 움직임 구현.
            - 아이템이 360도 회전.
            - 위 아래로 왔다갔다 흔들림 구현(Mathf : Sine 곡선 활용).
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{
    public class PickUpItemAmmoBox : PickUpItem
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 총알지급 → 아이템 먹는 효과.
        [SerializeField] private int giveAmmo = 7;
        #endregion Variable





        // [4] Custom Method.
        #region Custom Method
        // [ ] - ) OnPickup.
        protected override bool OnPickup()
        {
            // [ ] - [ ] - ) . Ammobox 아이템을 먹었을 때의 효과 구현.
            // ) PlayerDataManager.Instance.AddAmmo(giveAmmo);
            return true;
        }
        #endregion Custom Method
    }
}
