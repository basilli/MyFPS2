using UnityEngine;

/* [0] 개요 : DoorCellExit
		- 다음씬 넘어가기.
		- 문을 열면 문 여는 소리 → 배경음 종료 및 다음 씬으로 이동.
*/

namespace MyFPS
{
    public class DoorCellExit : Interactive
    {

        // [1] Variable.
        #region Variable
        // [ ] - 1) AudioSource.
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene02";         // ) MainScene02로 이동.
        // [ ] - 2) Animator.
        public Animator animator;       // ) 문 여는 애니메이션.
        [SerializeField] private string isOpen = "IsOpen";
        // [ ] - 3) AudioSource.
        public AudioSource doorBang;        // ) 문 여는 소리.
        public AudioSource bgm01;       // ) 배경음.
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
            // [ ] - 1) 문을 여는 애니메이션.
            animator.SetBool(isOpen, true);
            // [ ] - 2) 배경음 종료.
            bgm01.Stop();
            // [ ] - 3) 문을 여는 소리 재생.
            doorBang.Play();
            // [ ] - ) 씬 종료시 처리할 내용 구현 → 저장, 불러오기, 결과창 등.
            // [ ] - 4) 다음 씬으로 이동.
            fader.FadeTo(loadToScene);
            // [ ] - 5) 문 충돌체 제거.
            this.GetComponent<BoxCollider>().enabled = false;


        }
        #endregion Custom Method
    }

}
// [ ] - ) 
// [ ] - [ ] - ) 