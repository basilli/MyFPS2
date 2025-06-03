using UnityEngine;
using TMPro;
using System.Collections;

/* [0] 개요 : FullEyeExit
		- 퍼즐 조각을 모두 모으면 비밀의 문이 열림.
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
        public GameObject fakePicture;      // ) 빈 액자.
        public GameObject realPicture;      // ) 눈 그림이 있는 액자.
        // [ ] - 2) 숨겨진 벽.
        public Animator animator;
        [SerializeField] private string openTrigger = "ExitOpen";
        // [ ] - 3) 대사.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "You need more Eye Pictures";
        #endregion Variable





        // [2] Custom Method.
        #region Custom Method
        // [ ] - 1) DoAction.
        protected override void DoAction()
        {
            // [ ] - [ ] - 1) 퍼즐조각 2개를 모았는지 확인.
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.LEFTEYE_KEY) && PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.RIGHTEYE_KEY))
            {
                OpenHiddenWall();
            }
            // [ ] - [ ] - 2) 퍼즐조각이 모자를 경우.
            else
            {
                StartCoroutine(LockHiddenWall());
            }
        }

        // [ ] - 2) OpenHiddenWall.
        private void OpenHiddenWall()
        {
            // [ ] - [ ] - 1) .액자.
            fakePicture.SetActive(false);
            realPicture.SetActive(true);
            // [ ] - [ ] - 2) .숨겨진 벽.
            animator.SetTrigger(openTrigger);
        }

        // [ ] - 3) DoAction.
        IEnumerator LockHiddenWall()
        {
            // [ ] - [ ] - 1) .
            unInteractive = true;      // ) 언인터랙티브 기능 끄기.
            sequenceText.text = sequence;
            yield return new WaitForSeconds(2f);
            unInteractive = false;      // ) 언인터랙티브 기능 켜기.
            sequenceText.text = "";
        }
        #endregion Custom Method

        // [ ] - ) .
        // [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - ) .
        // [ ] - [ ] - [ ] - [ ] - ) .
    }

}
