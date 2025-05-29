using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;

/* [0] 개요 : DOpenning
		- MainScene02 오프닝 클래스.
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
        // [ ] - 1) 플레이어 오브젝트.
        public GameObject thePlayer;
        // [ ] - 2) 페이더 객체.
        public SceneFader fader;
        // [ ] - 3) 시나리오 대사 처리.
        public TextMeshProUGUI sequenceText;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 커서 제어.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // [ ] - [ ] - 2) 시퀀스 플레이. 
            StartCoroutine(SequencePlay());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) 
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) 플레이어 캐릭터 비활성화.
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) 페이드인 연출(1초 대기 후 페이드인 효과).
            fader.FadeStart();
            // [ ] - [ ] - 3) 화면 하단에 시나리오 텍스트 화면 출력(3초).
            sequenceText.text = "";

            yield return new WaitForSeconds(1f);

            // TODO : Cheating

            // [ ] - [ ] - 4) 메인 2번씬 설정
            PlayerDataManager.Instance.Weapon = WeaponType.Pistol;
            // )        PlayerDataManager.Instance.AddAmmo(5);

            // [ ] - [ ] - 5) 배경음 플레이.
            AudioManager.Instance.PlayBgm("Bgm02");
            // [ ] - [ ] - 6) 플레이어 캐릭터 활성화.
            input.enabled = true;


        }
        #endregion Custom Method

        // [ ] - ) 
        // [ ] - [ ] - ) 
    }
}
