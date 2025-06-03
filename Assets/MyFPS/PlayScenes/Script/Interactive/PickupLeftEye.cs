using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/* [0] ���� : PickupLeftEye
		- Left Eye�̶�� ���� ������ ȹ��.
*/

namespace MyFPS
{

	public class PickupLeftEye : Interactive
	{
        // [1] Variable.
        #region Variable
        // [ ] - 1) ���� ������.
        [SerializeField] private PuzzleKey puzzleKey = PuzzleKey.LEFTEYE_KEY;
        // [ ] - 2) ���� UI.
        public GameObject puzzleUI;
        public Image puzzleImage;
        public Sprite puzzleSprite;
        // [ ] - 3) ���� ���.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "You have obtained a puzzle item.";
        // [ ] - 4) ������ ��.
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
            // )        Debug.Log("Left Eye�̶�� ���� ������ ȹ��.");
            // [ ] - [ ] - 1) ���� ������ ȹ��.
            PlayerDataManager.Instance.GainPuzzleKey(puzzleKey);
            // [ ] - [ ] - 2) ������ �� üũ.
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.LEFTEYE_KEY) && PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.RIGHTEYE_KEY))
            {
                fakeWall.SetActive(false);
                hiddenWall.SetActive(true);
            }
            // [ ] - [ ] - 3) ���ͷ�Ƽ�� ��� ����.
            // )        unInteractive = true;
            // [ ] - [ ] - 4) ����.
            sequenceText.text = "";
            puzzleUI.SetActive(true);
            puzzleImage.sprite = puzzleSprite;
            yield return new WaitForSeconds(0.3f);
            sequenceText.text = sequence;
            yield return new WaitForSeconds(1.7f);
            // [ ] - [ ] - 5) �� �ʱ�ȭ.
            sequenceText.text = "";
            puzzleUI.SetActive(false);
            // [ ] - [ ] - 6) Ʈ���� ������Ʈ ų.
            Destroy(gameObject);
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .

    }
}
