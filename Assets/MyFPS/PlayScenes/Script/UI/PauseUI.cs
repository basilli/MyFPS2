using UnityEngine;
using UnityEngine.InputSystem;

/* [0] ���� : PauseUI
		- ���� �� �޴� ���� Ŭ����.
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{
    public class PauseUI : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) GameObject.
        public GameObject pauseUI;
        // [ ] - 2) PlayerInput.
        public PlayerInput playerInput;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        #endregion Unity Event Method





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
        // [ ] - 2) Toggle ��ESCŰ ������ UI Ȱ��ȭ, ESCŰ�� �ٽ� ������ UI ��Ȱ��ȭ(���). 
        public void Toggle()
        {
            // [ ] - [ ] - 1) 
            pauseUI.SetActive(!pauseUI.activeSelf);
            // [ ] - [ ] - 2) â�� ���� ����.
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                // )        playerInput.enabled = false;        // ) ���ۺҴ� ����.
                Cursor.lockState = CursorLockMode.None;     // ) Ŀ�� ����.
                Cursor.visible = true;
            }
            // [ ] - [ ] - 3) â�� ���� ����.
            else
            {
                Time.timeScale = 1f;
                // )        playerInput.enabled = true;        // ) ���۰��� ����.
                Cursor.lockState = CursorLockMode.Locked;     // ) Ŀ�� ����.
                Cursor.visible = false;
            }
        }
        // [ ] - 2) �޴����� ��ư ȣ��.
        public void Menu()
        {
            Debug.Log("Go to Menu!");
        }
        #endregion Custom Method
    }

}
// [ ] - ) 
// [ ] - [ ] - ) 