using UnityEngine;
using UnityEngine.UI;

/* [0] 개요 : ForBackColorChange
		- UI 게이지 Bar 이미지 컬러 변경 효과.
*/

namespace Unity.FPS.UI
{

    public class ForBackColorChange : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ .
        public Image forgroundImage;                                      // ) .
        public Color defaultForgroundColor;                                // ) 게이지바 기본 컬러.
        public Color fullFlashForgroundColor;                              // ) 게이지바 100% 충전시 플래시 효과 컬러.
        public Image backgroundImage;                                   // ) .
        public Color defaultBackgroundColor;                             // ) 게이지바 백그라운드 이미지 기본 컬러.
        public Color emptyFlashBackgroundColor;                        // ) 게이지바 0%일때 플래시 효과 컬러.
        [SerializeField] private float fullValue = 1f;                         // ) 게이지바 Rate Max Value.
        [SerializeField] private float emptyValue = 0f;                     // ) 게이지바 Rate Min Value.
        [SerializeField] private float colorChangeSharpness = 5f;       // ) 컬러 변경 속도(Lerp 계수).
        private float m_PreviousValue;                                      // ) 연출을 위한 was 변수.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ OnServerInitialized → UI 게이지 Bar 초기화.
        public void Initialized(float fullValueRatio, float emptyValueRatio)
        {
            fullValue = fullValueRatio;
            emptyValue = emptyValueRatio;
            m_PreviousValue = fullValue;
        }


        // [◆] - ▶▶▶ 456 → 게이지바 효과 업데이트.
        public void UpdateVisual(float currentRatio)
        {
            // [◇] - [◆] - ) 100% 충전되는 순간을 체크.
            if (currentRatio == fullValue && currentRatio != m_PreviousValue)
            {
                forgroundImage.color = fullFlashForgroundColor;
            }
            else if (currentRatio < emptyValue)
            {
                backgroundImage.color = emptyFlashBackgroundColor;
            }
            else
            {
                forgroundImage.color = Color.Lerp(forgroundImage.color, defaultForgroundColor, Time.deltaTime * colorChangeSharpness);
                backgroundImage.color = Color.Lerp(backgroundImage.color, defaultBackgroundColor, Time.deltaTime * colorChangeSharpness);
            }
            // [◇] - [◆] - ) .
            m_PreviousValue = currentRatio;
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}