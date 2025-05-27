using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;       // ) IEnumerator 활성화.
using TMPro;       // )  TextMeshProUGUI 활성화.

/* [0] 개요 : BFirstTrigger
		- 첫번째 트리거 연출.
*/

namespace MyFPS
{
	public class BFirstTrigger : MonoBehaviour
	{
        // [1] Variable.
        #region Variable
        // [ ] - 1) 플레이어 오브젝트.
        public GameObject thePlayer;
        // [ ] - 2) 화살표 오브젝트.
        public GameObject theArrow;
        // [ ] - 3) 시나리오 대사 처리.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "Looks like a weapon on that table.";
        // [ ] - 4) 대사 및 음성.
        public AudioSource line03;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) 테그로 플레이어 체크.
            if (other.tag == "Player")
            {
            }
            // [ ] - [ ] - 2) 트리거 해제.
            this.GetComponent<BoxCollider>().enabled = false;
            // )        Debug.Log($"other : {other.name}");
            // [ ] - [ ] - 2) StartCoroutine → SequercePlayer 실행.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - [ ] - 1) 플레이어 캐릭터 비활성화.
            // )        thePlayer.SetActive(false);
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) 화면 하단에 시나리오 텍스트 화면 출력(3초).
            sequenceText.text = sequence;
            line03.Play();
            // [ ] - [ ] - 3) 1초 딜레이 발생.
            yield return new WaitForSeconds(1f);
            // [ ] - [ ] - 4) 화살표 활성화.
            theArrow.SetActive(true);
            // [ ] - [ ] - 5) 2초 딜레이 발생.
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 6) 시나리오 텍스트 사라짐.
            sequenceText.text = "";
            // [ ] - [ ] - 7) 플레이어 캐릭터 활성화.
            // )        thePlayer.SetActive(true);
            input.enabled = false;

            yield return null;
        }
        #endregion Custom Metho
    }
}




// [ ] - ) 
// [ ] - [ ] - ) 