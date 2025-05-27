using UnityEngine;

/* [0] ���� : DoorCellExit
		- ������ �Ѿ��.
		- ���� ���� �� ���� �Ҹ� �� ����� ���� �� ���� ������ �̵�.
*/

namespace MyFPS
{
    public class DoorCellExit : Interactive
    {

        // [1] Variable.
        #region Variable
        // [ ] - 1) AudioSource.
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene02";         // ) MainScene02�� �̵�.
        // [ ] - 2) Animator.
        public Animator animator;       // ) �� ���� �ִϸ��̼�.
        [SerializeField] private string isOpen = "IsOpen";
        // [ ] - 3) AudioSource.
        public AudioSource doorBang;        // ) �� ���� �Ҹ�.
        public AudioSource bgm01;       // ) �����.
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
            // [ ] - 1) ���� ���� �ִϸ��̼�.
            animator.SetBool(isOpen, true);
            // [ ] - 2) ����� ����.
            bgm01.Stop();
            // [ ] - 3) ���� ���� �Ҹ� ���.
            doorBang.Play();
            // [ ] - ) �� ����� ó���� ���� ���� �� ����, �ҷ�����, ���â ��.
            // [ ] - 4) ���� ������ �̵�.
            fader.FadeTo(loadToScene);
            // [ ] - 5) �� �浹ü ����.
            this.GetComponent<BoxCollider>().enabled = false;


        }
        #endregion Custom Method
    }

}
// [ ] - ) 
// [ ] - [ ] - ) 