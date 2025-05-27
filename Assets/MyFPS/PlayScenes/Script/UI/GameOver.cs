using UnityEngine;


/* [0] 개요 : GameOver
		- 게임오버 처리 → 메뉴가기, 다시하기.
*/

namespace MyFPS
{

    public class GameOver : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        public SceneFader fader;
        // [ ] - 2) .
        [SerializeField] private string loadToScene = "PlayScene";
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 마우스 커서의 활성화 및 보이기.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // [ ] - [ ] - 2) FadeIn 효과.
            fader.FadeStart(0f);
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) Retry.
        public void Retry()
        {
            fader.FadeTo(loadToScene);
        }
        // [ ] - 2) Menu.
        public void Menu()
        {
            Debug.Log("Go To Menu!");
        }
        #endregion Custom Method
    }
}
// [ ] - ) 
// [ ] - [ ] - ) 
