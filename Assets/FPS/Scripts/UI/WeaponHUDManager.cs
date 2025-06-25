using UnityEngine;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;
using Unity.VisualScripting;

/* [0] 개요 : WeaponHUDManager
		- Weapon ammo UI 관리 클래스.
*/

namespace Unity.FPS.UI
{
	public class WeaponHUDManager : MonoBehaviour
	{
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private PlayerWeaponManager playerWeaponManager;


        // [◆] - ▶▶▶ ETC.
        public RectTransform ammoPanel;                                                        // ) ammoCounter 프리팹 오브젝트의 부모 오브젝트.
        public AmmoCounter ammoCounterPrefab;                                             // ) ammoCounter 프리팹.
        private List<AmmoCounter> ammoCounters = new List<AmmoCounter>();       // ) ammoCount UI 리스트.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 참조.
            playerWeaponManager = FindFirstObjectByType<PlayerWeaponManager>();
        }


        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) .
            WeaponController activeWeapon = playerWeaponManager.GetActiveWeapon();
            // [◇] - [◆] - ) .
            if (activeWeapon)
            {
                AddWeapon(activeWeapon, playerWeaponManager.ActiveWeaponIndex);
                SwitchWeapon(activeWeapon);
            }
            // [◇] - [◆] - ) 초기화.
            playerWeaponManager.OnAddedWeapon += AddWeapon;
            playerWeaponManager.OnRemovedWeapon += RemoveWeapon;
            playerWeaponManager.OnSwitchToWeapon += SwitchWeapon;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ AddWeapon → 무기 추가시 UI 프리팹 추가.
        private void AddWeapon(WeaponController newWeapon, int weaponIndex)
        {
            // [◇] - [◆] - ) AmmoCounter 오브젝트 생성.
            AmmoCounter ammoCounter = Instantiate(ammoCounterPrefab, ammoPanel);
            // [◇] - [◆] - ) UI 초기화.
            ammoCounter.Initialized(newWeapon, weaponIndex);
            // [◇] - [◆] - ) UI리스트에 추가.
            ammoCounters.Add(ammoCounter);
        }


        // [◆] - ▶▶▶ RemoveWeapon → 무기 제거시 UI 프리팹 제거.
        private void RemoveWeapon(WeaponController oldWeapon, int weaponIndex)
        {
            // [◇] - [◆] - ) 삭제될 AmmoCounter UI 찾기.
            int findCounterIndex = -1;
            for (int i = 0; i < ammoCounters.Count; i++)
            {
                if (ammoCounters[i].WeaponCounterIndex == weaponIndex)
                {
                    findCounterIndex = i;
                    // [◇] - [◇] - [◇] - [◆] ) UI 오브젝트 제거.
                    Destroy(ammoCounters[i].gameObject);
                    break;
                }
            }
            // [◇] - [◆] - ) UI를 찾았다면 리스트에서 제거.
            if (findCounterIndex >= 0)
            {
                ammoCounters.RemoveAt(findCounterIndex);
            }
        }


        // [◆] - SwitchWeapon ▶▶▶ → 무기 교체시 UI(ammoPanel) 리빌딩(갱신) 적용.
        private void SwitchWeapon(WeaponController weapon)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(ammoPanel);
        }

        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}
// [◆] - ▶▶▶ 123.


// [◆] - ▶▶▶ 456.


// [◇] - [◆] - ) 789.
// [◇] - [◇] - [◆] ) 147.
// [◇] - [◇] - [◇] - [◆] ) 258.
// [◇] - [◇] - [◇] - [◇] - [◆] ) 369.