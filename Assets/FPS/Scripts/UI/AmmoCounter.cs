using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine.UI;
using TMPro;                    // ) TextMeshProUGUI 활성화.

/* [0] 개요 : AmmoCounter
		- 무기의 Ammo Counter UI를 관리하는 클래스.
*/

namespace Unity.FPS.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private PlayerWeaponManager weaponManager;
        private WeaponController weaponController;
        private int weaponCounterIndex;                             // ) Ammo Counter UI 인덱스 번호.


        // [◆] - ▶▶▶ UI.
        public TextMeshProUGUI weaponIndexText;
        public Image ammoFillImage;                                                  // ) FillImageAmmo 가져오기.
        public CanvasGroup canvasGroup;                                            // ) UI 투명도.
        [SerializeField] [Range(0,1)] private float unSelectedOpacity = 0.5f;       // ) 선택되지않는 UI의 투명값.
        private Vector3 unSelectedScale = Vector3.one*0.8f;                        // ) 선택되지않는 UI의 크기(80%).
        [SerializeField] private float ammoFillSharpness = 10f;                      // ) ammo UI 게이지 바 충전속도(Lerp 계수).
        [SerializeField] private float weaponSwitchSharpness = 10f;               // ) 무기 변경 시, UI 투명도 및 크기 변경 속도.


        // [◆] - ▶▶▶ 게이지 바 컬러 효과.
        public ForBackColorChange forBackColorChange;
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        public int WeaponCounterIndex => weaponCounterIndex;
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲




        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) .
            float currentFillRate = weaponController.CurrentAmmoRate;
            // [◇] - [◆] - ) 게이지바.
            ammoFillImage.fillAmount = Mathf.Lerp(ammoFillImage.fillAmount, currentFillRate,Time.deltaTime*ammoFillSharpness);
            // [◇] - [◆] - ) 액티브 무기와 아닌 무기 구분하기.
            bool isActiveWeapon = (weaponController == weaponManager.GetActiveWeapon());
            // [◇] - [◆] - ) UI 투명도.
            float currentOparcity = isActiveWeapon ? 1f : unSelectedOpacity;
            // [◇] - [◆] - ) 무기를 바꿀 때 자연스럽게 투명도가 바뀜.
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, currentOparcity, Time.deltaTime*weaponSwitchSharpness);
            // [◇] - [◆] - ) UI크기 → 무기를 바꿀 때 연출 구현.
            Vector3 currentScale = isActiveWeapon ? Vector3.one : unSelectedScale;
            transform.localScale = Vector3.Lerp(transform.localScale, currentScale, Time.deltaTime * weaponSwitchSharpness);
            // [◇] - [◆] - ) 게이지바 컬러 효과.
            forBackColorChange.UpdateVisual(currentFillRate);
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Initialized → Ammo Counter UI 초기화.
        public void Initialized(WeaponController weapon, int weaponIndex)
        {
            weaponController = weapon;
            weaponCounterIndex = weaponIndex;
            // [◇] - [◆] - ) weaponManager 가져오기.
            weaponManager = FindFirstObjectByType<PlayerWeaponManager>();
            // [◇] - [◆] - ) UI 초기화.
            weaponIndexText.text = (weaponIndex+1).ToString();
            // [◇] - [◆] - ) 컬러효과 초기화.
            forBackColorChange.Initialized(1f, 0.1f);
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲

    }
}