using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/* [0] 개요 : PickupLeftEye
		- Left Eye이라는 퍼즐 아이템 획득.
*/

namespace MyFPS
{

	public class PickupLeftEye : Interactive
	{
        // [1] Variable.
        #region Variable
        // [ ] - 1) 퍼즐 아이템.
        [SerializeField] private PuzzleKey puzzleKey = PuzzleKey.LEFTEYE_KEY;
        // [ ] - 2) 퍼즐 UI.
        public GameObject puzzleUI;
        public Image puzzleImage;
        public Sprite puzzleSprite;
        // [ ] - 3) 퍼즐 대사.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "You have obtained a puzzle item.";
        // [ ] - 4) 숨겨진 벽.
        public GameObject fakeWall;
        public GameObject hiddenWall;
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) DoAction.
        protected override void DoAction()
        {
            StartCoroutine(ShowPuzzleUI());
        }

        // [ ] - 2) ShowPuzzleUI.
        IEnumerator ShowPuzzleUI()
        {
            // )        Debug.Log("Left Eye이라는 퍼즐 아이템 획득.");
            // [ ] - [ ] - 1) 퍼즐 아이템 획득.
            PlayerDataManager.Instance.GainPuzzleKey(puzzleKey);
            // [ ] - [ ] - 2) 숨겨진 벽 체크.
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.LEFTEYE_KEY) && PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.RIGHTEYE_KEY))
            {
                fakeWall.SetActive(false);
                hiddenWall.SetActive(true);
            }
            // [ ] - [ ] - 3) 인터랙티브 기능 제거.
            // )        unInteractive = true;
            // [ ] - [ ] - 4) 연출.
            sequenceText.text = "";
            puzzleUI.SetActive(true);
            puzzleImage.sprite = puzzleSprite;
            yield return new WaitForSeconds(0.3f);
            sequenceText.text = sequence;
            yield return new WaitForSeconds(1.7f);
            // [ ] - [ ] - 5) 값 초기화.
            sequenceText.text = "";
            puzzleUI.SetActive(false);
            // [ ] - [ ] - 6) 트리거 오브젝트 킬.
            Destroy(gameObject);
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .

    }
}
