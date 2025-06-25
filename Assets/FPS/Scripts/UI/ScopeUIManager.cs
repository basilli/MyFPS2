using UnityEngine;
using Unity.FPS.Gameplay;

/* [0] 개요 : ScopeUIManager
		- 스코프 UI ON / OFF.
*/

namespace Unity.FPS.UI
{
    public class ScopeUIManager : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private PlayerWeaponManager weaponManager;
        public GameObject scopeUI;

        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            weaponManager = FindFirstObjectByType<PlayerWeaponManager>();
        }


        // [◆] - ▶▶▶ OnEnable.
        private void OnEnable()
        {
            // [◇] - [◆] - ) 이벤트 함수 등록.
            weaponManager.OnScopedWeapon += OnScope;
            weaponManager.OffScopedWeapon += OffScope;
        }


        // [◆] - ▶▶▶ OnDisable.
        private void OnDisable()
        {
            // [◇] - [◆] - ) 이벤트 함수 해제.
            weaponManager.OnScopedWeapon -= OnScope;
            weaponManager.OffScopedWeapon -= OffScope;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ OnScope.
        private void OnScope()
        {
            scopeUI.SetActive(true);
        }


        // [◆] - ▶▶▶ OffScope.
        private void OffScope()
        {
            scopeUI.SetActive(false);
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}

// [◆] - ▶▶▶ 123.

// [◆] - ▶▶▶ 456.


// [◇] - [◆] - ) 789.
// [◇] - [◇] - [◆] ) 147.
// [◇] - [◇] - [◇] - [◆] ) 258.
// [◇] - [◇] - [◇] - [◇] - [◆] ) 369.