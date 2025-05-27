using UnityEngine;

/* [0] 개요 : PlayerDataManager
		- 플레이어 데이터 관리 클래스 → 싱글톤(다음씬에서 데이터 보존)으로 구현.
*/

namespace MyFPS
{
    // [1] 장착 무기 타입 → enum.
    public enum WeaponType
    { 
        None, 
        Pistol,
    }


    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        // [2] Variable.
        #region Variable
        #endregion Variable





        // [2] Property.
        #region Property
        // [ ] - 1) 무기 타입.
        public WeaponType Weapon { get; set; }
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 플레이 데이터 초기화.
            Weapon = WeaponType.None;
        }
        #endregion Unity Event Method





        // [] Custom Method.
        #region Custom Method
        #endregion Custom Method
    }
}
// [ ] - ) 
// [ ] - [ ] - ) 