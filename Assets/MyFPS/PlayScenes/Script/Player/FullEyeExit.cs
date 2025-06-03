using UnityEngine;
using TMPro;
using System.Collections;

/* [0] ���� : FullEyeExit
		- ���� ������ ��� ������ ����� ���� ����.
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{

    public class FullEyeExit : Interactive
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        public GameObject fakePicture;      // ) �� ����.
        public GameObject realPicture;      // ) �� �׸��� �ִ� ����.
        // [ ] - 2) ������ ��.
        public Animator animator;
        [SerializeField] private string openTrigger = "ExitOpen";
        // [ ] - 3) ���.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "You need more Eye Pictures";
        #endregion Variable





        // [2] Custom Method.
        #region Custom Method
        // [ ] - 1) DoAction.
        protected override void DoAction()
        {
            // [ ] - [ ] - 1) �������� 2���� ��Ҵ��� Ȯ��.
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.LEFTEYE_KEY) && PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.RIGHTEYE_KEY))
            {
                OpenHiddenWall();
            }
            // [ ] - [ ] - 2) ���������� ���ڸ� ���.
            else
            {
                StartCoroutine(LockHiddenWall());
            }
        }

        // [ ] - 2) OpenHiddenWall.
        private void OpenHiddenWall()
        {
            // [ ] - [ ] - 1) .����.
            fakePicture.SetActive(false);
            realPicture.SetActive(true);
            // [ ] - [ ] - 2) .������ ��.
            animator.SetTrigger(openTrigger);
        }

        // [ ] - 3) DoAction.
        IEnumerator LockHiddenWall()
        {
            // [ ] - [ ] - 1) .
            unInteractive = true;      // ) �����ͷ�Ƽ�� ��� ����.
            sequenceText.text = sequence;
            yield return new WaitForSeconds(2f);
            unInteractive = false;      // ) �����ͷ�Ƽ�� ��� �ѱ�.
            sequenceText.text = "";
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
    }

}
