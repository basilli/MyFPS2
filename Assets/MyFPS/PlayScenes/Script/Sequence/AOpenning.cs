using UnityEngine;
using TMPro;
using System.Collections;       // ) 코루틴 함수 활성화.

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
        [SerializeField] private string sequence = "I need get out of here!";
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) SequencePlay 실행.
            StartCoroutine(SequencePlay());   
        }
        #endregion Unity Event Method





        // [3] Unity Event Method.
        #region Custom Method
        // [ ] - 1) SequencePlay → 오프닝 연출 코루틴 함수.
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) 플레이어 캐릭터 비활성화.
            thePlayer.SetActive(false);

            // [ ] - [ ] - 2) 페이드인 연출(1초 대기 후 페이드인 효과).
            fader.FadeStart(1f);
            // [ ] - [ ] - 3) 화면 하단에 시나리오 텍스트 화면 출력(3초).
            sequenceText.text = sequence;
            // [ ] - [ ] - 4) 3초 후에 시나리오 텍스트 사라짐.
            yield return new WaitForSeconds(3f);
            sequenceText.text = "";
            // [ ] - [ ] - 5) 플레이어 캐릭터 활성화.
            thePlayer.SetActive(true);

            yield return null;
        }
        #endregion Custom Method
    }
}


// [ ] - ) 
// [ ] - [ ] - ) 