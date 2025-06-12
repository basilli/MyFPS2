using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;       // ) �ڷ�ƾ �Լ� Ȱ��ȭ.
using UnityEngine.SceneManagement;

/* [0] ���� : AOpenning
		- �÷��� �� ������ ����.
*/

namespace MyFPS
{
    public class AOpenning : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) �÷��̾� ������Ʈ.
        public GameObject thePlayer;
        // [ ] - 2) ���̴� ��ü.
        public SceneFader fader;
        // [ ] - 3) �ó����� ��� ó��.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence01 = "... Where am I?";
        [SerializeField] private string sequence02 = "I need get out of here!";
        // [ ] - 4) �����.
        public AudioSource bgm01;
        // [ ] - 5) ��� �� ����.
        public AudioSource line01;
        public AudioSource line02;
        #endregion Variables





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
            // [ ] - [ ] - 3) Ŀ�� ����.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // [ ] - [ ] - 4) SequencePlay ���� �� ������ ����.
            StartCoroutine(SequencePlay());   
        }
        #endregion Unity Event Method





        // [3] Custom Method.
            #region Custom Method
        // [ ] - 1) SequencePlay �� ������ ���� �ڷ�ƾ �Լ�.
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) �÷��̾� ĳ���� ��Ȱ��ȭ.
            // )        thePlayer.SetActive(false);
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) ���̵��� ����(1�� ��� �� ���̵��� ȿ��).
            fader.FadeStart(4f);
            // [ ] - [ ] - 3) ȭ�� �ϴܿ� �ó����� �ؽ�Ʈ ȭ�� ���(3��).
            sequenceText.text = sequence01;
            line01.Play();
            yield return new WaitForSeconds(3f);

            sequenceText.text = sequence02;
            line02.Play();
            // [ ] - [ ] - 4) 3�� �Ŀ� �ó����� �ؽ�Ʈ �����.
            yield return new WaitForSeconds(3f);
            sequenceText.text = "";
            // [ ] - [ ] - 5) ����� �÷���.
            bgm01.Play();
            // [ ] - [ ] - 6) �÷��̾� ĳ���� Ȱ��ȭ.
            // )        thePlayer.SetActive(true);
            input.enabled = true;

            yield return null;
        }
        #endregion Custom Method
    }
}


// [ ] - ) 
// [ ] - [ ] - ) 