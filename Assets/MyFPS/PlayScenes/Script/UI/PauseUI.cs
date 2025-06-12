using UnityEngine;
using UnityEngine.InputSystem;

/* [0] 개요 : PauseUI
		- 게임 중 메뉴 관리 클래스.
*/

namespace MyFPS
{
    public class PauseUI : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        public SceneFader fader;
        // [ ] - 2) .
        [SerializeField] private string loadToScene = "PlayScene";
        // [ ] - 1) GameObject.
        public GameObject pauseUI;
        // [ ] - 2) PlayerInput.
        public PlayerInput playerInput;
        #endregion Variable





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) OnPause. 
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Toggle();
            }
        }
        // [ ] - 2) Toggle →ESC키 누르면 UI 활성화, ESC키를 다시 누르면 UI 비활성화(토글). 
        public void Toggle()
        {
            // [ ] - [ ] - 1) 
            pauseUI.SetActive(!pauseUI.activeSelf);
            // [ ] - [ ] - 2) 창이 열린 상태.
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                // )        playerInput.enabled = false;        // ) 조작불능 상태.
                Cursor.lockState = CursorLockMode.None;     // ) 커서 제어.
                Cursor.visible = true;
            }
            // [ ] - [ ] - 3) 창이 닫힌 상태.
            else
            {
                Time.timeScale = 1f;
                // )        playerInput.enabled = true;        // ) 조작가능 상태.
                Cursor.lockState = CursorLockMode.Locked;     // ) 커서 제어.
                Cursor.visible = false;
            }
        }
        // [ ] - 2) 메뉴가기 버튼 호출.
        public void Menu()
        {
            fader.FadeTo(loadToScene);
        }
        #endregion Custom Method
    }

}
// [ ] - ) 
// [ ] - [ ] - ) 