using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;       // ) IEnumerator Ȱ��ȭ.
using TMPro;       // )  TextMeshProUGUI Ȱ��ȭ.

/* [0] ���� : BFirstTrigger
		- ù��° Ʈ���� ����.
*/

namespace MyFPS
{
	public class BFirstTrigger : MonoBehaviour
	{
        // [1] Variable.
        #region Variable
        // [ ] - 1) �÷��̾� ������Ʈ.
        public GameObject thePlayer;
        // [ ] - 2) ȭ��ǥ ������Ʈ.
        public GameObject theArrow;
        // [ ] - 3) �ó����� ��� ó��.
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "Looks like a weapon on that table.";
        // [ ] - 4) ��� �� ����.
        public AudioSource line03;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) OnTriggerEnter.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) �ױ׷� �÷��̾� üũ.
            if (other.tag == "Player")
            {
            }
            // [ ] - [ ] - 2) Ʈ���� ����.
            this.GetComponent<BoxCollider>().enabled = false;
            // )        Debug.Log($"other : {other.name}");
            // [ ] - [ ] - 2) StartCoroutine �� SequercePlayer ����.
            StartCoroutine(SequercePlayer());
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) SequercePlayer.
        IEnumerator SequercePlayer()
        {
            // [ ] - [ ] - 1) �÷��̾� ĳ���� ��Ȱ��ȭ.
            // )        thePlayer.SetActive(false);
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;
            // [ ] - [ ] - 2) ȭ�� �ϴܿ� �ó����� �ؽ�Ʈ ȭ�� ���(3��).
            sequenceText.text = sequence;
            line03.Play();
            // [ ] - [ ] - 3) 1�� ������ �߻�.
            yield return new WaitForSeconds(1f);
            // [ ] - [ ] - 4) ȭ��ǥ Ȱ��ȭ.
            theArrow.SetActive(true);
            // [ ] - [ ] - 5) 2�� ������ �߻�.
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 6) �ó����� �ؽ�Ʈ �����.
            sequenceText.text = "";
            // [ ] - [ ] - 7) �÷��̾� ĳ���� Ȱ��ȭ.
            // )        thePlayer.SetActive(true);
            input.enabled = false;

            yield return null;
        }
        #endregion Custom Metho
    }
}




// [ ] - ) 
// [ ] - [ ] - ) 