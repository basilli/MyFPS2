using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;       // ) 코루틴 함수 활성화.
using UnityEngine.SceneManagement;

/* [0] 개요 : AOpenning
		- 플레이 씬 오프닝 연출.
*/

namespace MyFPS
{
    public class AOpenning : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 플레이어 오브젝트.
        public GameObject thePlayer;
        // [ ] - 2) 페이더 객체.
        public SceneFader fader;
        // [ ] - 3) 시나리오 대사 처리.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence01 = "... Where am I?";
        [SerializeField] private string sequence02 = "I need get out of here!";
        // [ ] - 4) 배경음.
        public AudioSource bgm01;
        // [ ] - 5) 대사 및 음성.
        public AudioSource line01;
        public AudioSource line02;
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 게임데이터(씬 번호) 저장.
            /* PlayerPrefs 모드
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SceneNumber", sceneNumber);
            Debug.Log($"Save Scene Number : {sceneNumber}");
            */
            // [ ] - [ ] - 2) File System 모드.
            PlayerDataManager.Instance.SceneNumber = SceneManager.GetActiveScene().buildIndex;
            SaveLoad.SaveData();
            // [ ] - [ ] - 3) 커서 제어.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // [ ] - [ ] - 4) SequencePlay 실행 → 오프닝 연출.
            StartCoroutine(SequencePlay());   
        }
        #endregion Unity Event Method





        // [3] Custom Method.
            #region Custom Method
        // [ ] - 1) SequencePlay → 오프닝 연출 코루틴 함수.
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) 플레이어 캐릭터 비활성화.
            // )        thePlayer.SetActive(false);
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) 페이드인 연출(1초 대기 후 페이드인 효과).
            fader.FadeStart(4f);
            // [ ] - [ ] - 3) 화면 하단에 시나리오 텍스트 화면 출력(3초).
            sequenceText.text = sequence01;
            line01.Play();
            yield return new WaitForSeconds(3f);

            sequenceText.text = sequence02;
            line02.Play();
            // [ ] - [ ] - 4) 3초 후에 시나리오 텍스트 사라짐.
            yield return new WaitForSeconds(3f);
            sequenceText.text = "";
            // [ ] - [ ] - 5) 배경음 플레이.
            bgm01.Play();
            // [ ] - [ ] - 6) 플레이어 캐릭터 활성화.
            // )        thePlayer.SetActive(true);
            input.enabled = true;

            yield return null;
        }
        #endregion Custom Method
    }
}


// [ ] - ) 
// [ ] - [ ] - ) 