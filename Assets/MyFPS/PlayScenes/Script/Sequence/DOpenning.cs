using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;
using UnityEngine.SceneManagement;

/* [0] ���� : DOpenning
		- MainScene02 ������ Ŭ����.
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{
    public class DOpenning : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) �÷��̾� ������Ʈ.
        public GameObject thePlayer;
        // [ ] - 2) ���̴� ��ü.
        public SceneFader fader;
        // [ ] - 3) �ó����� ��� ó��.
        public TextMeshProUGUI sequenceText;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) ���ӵ�����(�� ��ȣ) ����.
            /* PlayerPrefs ���
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SceneNumber", sceneNumber);
            Debug.Log($"Save Scene Number : {sceneNumber}");
            */
            // [ ] - [ ] - 2) File System ���.
            PlayerDataManager.Instance.SceneNumber = SceneManager.GetActiveScene().buildIndex;
            SaveLoad.SaveData();
            // [ ] - [ ] - 2) Ŀ�� ����.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // [ ] - [ ] - 3) ������ �÷���. 
            StartCoroutine(SequencePlay());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) 
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) �÷��̾� ĳ���� ��Ȱ��ȭ.
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) ���̵��� ����(1�� ��� �� ���̵��� ȿ��).
            fader.FadeStart();
            // [ ] - [ ] - 3) ȭ�� �ϴܿ� �ó����� �ؽ�Ʈ ȭ�� ���(3��).
            sequenceText.text = "";

            yield return new WaitForSeconds(1f);

            // TODO : Cheating

            // [ ] - [ ] - 4) ���� 2���� ����
            PlayerDataManager.Instance.Weapon = WeaponType.Pistol;
            PlayerDataManager.Instance.AddAmmo(5);

            // [ ] - [ ] - 5) ����� �÷���.
            AudioManager.Instance.PlayBgm("Bgm02");
            // [ ] - [ ] - 6) �÷��̾� ĳ���� Ȱ��ȭ.
            input.enabled = true;


        }
        #endregion Custom Method

        // [ ] - ) 
        // [ ] - [ ] - ) 
    }
}
