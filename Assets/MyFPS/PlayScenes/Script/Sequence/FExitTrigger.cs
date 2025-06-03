using UnityEngine;

/* [0] ���� : FExitTrigger
		- Ʈ���Ű� �۵��ϸ� ���� �޴� ������.
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
            // [ ] - [ ] - 1) �÷��̾� üũ.
            if (other.tag == "Player");
            {
                // [ ] - [ ] - [ ] - 1) ���θ޴��� ����.
                SceneClear();
            }
        }

        // [ ] - 2) SceneClear.
        private void SceneClear()
        {
            // [ ] - [ ] - 1) �� Ŭ���� ó��.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 2) Ŀ������.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // [ ] - [ ] - 3) ���θ޴� ����.
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
