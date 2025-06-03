using UnityEngine;

/* [0] 개요 : FExitTrigger
		- 트리거가 작동하면 메인 메뉴 보내기.
*/

namespace MyFPS
{
	public class FExitTrigger : MonoBehaviour
	{
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainMenu";
        #endregion Variable





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) 플레이어 체크.
            if (other.tag == "Player");
            {
                // [ ] - [ ] - [ ] - 1) 메인메뉴로 가기.
                SceneClear();
            }
        }

        // [ ] - 2) SceneClear.
        private void SceneClear()
        {
            // [ ] - [ ] - 1) 씬 클리어 처리.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 2) 커서제어.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // [ ] - [ ] - 3) 메인메뉴 가기.
            fader.FadeTo(loadToScene);
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
    }
}
