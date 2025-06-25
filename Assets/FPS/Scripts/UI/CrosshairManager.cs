using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;


/* [0] 개요 : CrosshairManager
*/

namespace Unity.FPS.UI
{

    public class CrosshairManager : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ .
        public Image crosshairImage;
        public Sprite nullCrosshairSprite;      // 액티브한 무기가 없을 때 보여지는 크로스헤어.

        // [◆] - ▶▶▶ 참조.
        private PlayerWeaponManager weaponManager;
        private RectTransform crosshairRectTransform;        // ) 크로스헤어 이미지 트랜스폼(사이즈 조정).
        private CrosshairData crosshairDefault;                  // ) 액티브한 무기의 기본 크로스헤어 데이터.
        private CrosshairData crosshairTarget;                   // ) 액티브한 무기의 타겟팅 크로스헤어 데이터.
        private CrosshairData currentCrosshair;                  // ) 현재 보여지는 크로스헤어 데이터.

        // [◆] - ▶▶▶ 연출.
        private float crosshairUpdateSharpness = 5f;        // ) 연출 변경 속도.

        // [◆] - ▶▶▶ 연출 변경 시점 체크.
        private bool wasPointingAtEnemy;
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 참조.
            weaponManager = FindFirstObjectByType<PlayerWeaponManager>();
            OnWeaponChanged(weaponManager.GetActiveWeapon());
            // [◇] - [◆] - ) 무기교체시 호출되는 이벤트 함수에 등록.
            weaponManager.OnSwitchToWeapon += OnWeaponChanged;
        }

        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) .
            UpdateCrosshairPointingAtEnemy(false);
            // [◇] - [◆] - ) 상태변경 체크.
            wasPointingAtEnemy = weaponManager.IsPointingAtEnemy;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [3] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ UpdateCrosshairPointingAtEnemy → 크로스헤어 정보에 따라 크로스헤어 그리기.
        private void UpdateCrosshairPointingAtEnemy(bool force)
        {
            // [◇] - [◆] - ) 크로스헤어 데이터가 없을 경우.
            if (crosshairDefault.crosshairSprite == null)
                return;
            // [◇] - [◆] - ) 기본(디폴트)상태에서 적을 포착하는 순간.
            if ((force || (wasPointingAtEnemy == false)) && weaponManager.IsPointingAtEnemy == true)
            {
                currentCrosshair = crosshairTarget;
                crosshairImage.sprite = currentCrosshair.crosshairSprite;
                crosshairRectTransform.sizeDelta = currentCrosshair.crosshairSize * Vector2.one;
            }
            // [◇] - [◆] - ) 적을 포착하고 있다가 적을 놓치는 순간.
            else if ((force || (wasPointingAtEnemy == true)) && weaponManager.IsPointingAtEnemy == false)
            {
                currentCrosshair = crosshairDefault;
                crosshairImage.sprite = currentCrosshair.crosshairSprite;
                crosshairRectTransform.sizeDelta = currentCrosshair.crosshairSize * Vector2.one;
            }
            // [◇] - [◆] - ) 연출.
            crosshairImage.color = Color.Lerp(crosshairImage.color, currentCrosshair.crosshairColor, crosshairUpdateSharpness * Time.deltaTime);
            crosshairRectTransform.sizeDelta = Mathf.Lerp(crosshairRectTransform.sizeDelta.x, currentCrosshair.crosshairSize, crosshairUpdateSharpness*Time.deltaTime) * Vector2.one;
        }

        // [◆] - ▶▶▶ OnWeaponChanged → 무기 교체시 호출되는 함수(크로스헤어 정보 업데이트).
        private void OnWeaponChanged(WeaponController newWeapon)
        {
            // [◇] - [◆] - ) .
            if (newWeapon)
            {
                crosshairImage.enabled = true;
                crosshairRectTransform = crosshairImage.GetComponent<RectTransform>();
                // [◇] - [◇] - [◆] ) 새로 액티브한 무기의 크로스헤어 데이터를 가져와 저장.
                crosshairDefault = newWeapon.defaultCrosshair;
                crosshairTarget = newWeapon.targetInSightCrosshair;
            }
            else        // ) 새로운 무기가 없을 경우, 
            {
                if (nullCrosshairSprite)
                {
                    crosshairImage.sprite = nullCrosshairSprite;
                }
                else
                {
                    crosshairImage.enabled = false;
                }
                UpdateCrosshairPointingAtEnemy(true);
            }
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
