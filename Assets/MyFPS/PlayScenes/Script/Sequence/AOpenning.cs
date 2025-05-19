using UnityEngine;
using TMPro;
using System.Collections;       // ) �ڷ�ƾ �Լ� Ȱ��ȭ.

/* [0] ���� : AOpenning
		- �÷��� �� ������ ����.
*/

namespace MyFPS
{
    public class AOpenning : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) �÷��̾� ������Ʈ.
        public GameObject thePlayer;
        // [ ] - 2) ���̴� ��ü.
        public SceneFader fader;
        // [ ] - 3) �ó����� ��� ó��.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "I need get out of here!";
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) SequencePlay ����.
            StartCoroutine(SequencePlay());   
        }
        #endregion Unity Event Method





        // [3] Unity Event Method.
        #region Custom Method
        // [ ] - 1) SequencePlay �� ������ ���� �ڷ�ƾ �Լ�.
        IEnumerator SequencePlay()
        {
            // [ ] - [ ] - 1) �÷��̾� ĳ���� ��Ȱ��ȭ.
            thePlayer.SetActive(false);

            // [ ] - [ ] - 2) ���̵��� ����(1�� ��� �� ���̵��� ȿ��).
            fader.FadeStart(1f);
            // [ ] - [ ] - 3) ȭ�� �ϴܿ� �ó����� �ؽ�Ʈ ȭ�� ���(3��).
            sequenceText.text = sequence;
            // [ ] - [ ] - 4) 3�� �Ŀ� �ó����� �ؽ�Ʈ �����.
            yield return new WaitForSeconds(3f);
            sequenceText.text = "";
            // [ ] - [ ] - 5) �÷��̾� ĳ���� Ȱ��ȭ.
            thePlayer.SetActive(true);

            yield return null;
        }
        #endregion Custom Method
    }
}


// [ ] - ) 
// [ ] - [ ] - ) 