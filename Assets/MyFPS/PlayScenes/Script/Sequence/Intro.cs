using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

/* [0] ���� : Intro
*/

namespace MyFPS
{

    public class Intro : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        public SceneFader fader;        // ����Ƽ���� SceneFader�� �����ͼ� ����.
        [SerializeField] private string loadToScene = "MainScene01";        // MainScene01�� �����ͼ� ����.
        private SplineAutoDolly.FixedSpeed dolly;

        // [ ] - 2) �̵�.
        public CinemachineSplineCart cart;      // ) ����Ƽ���� Dolly Cart�� �����ͼ� ����.
        private bool[] isArrive;        // ) �̵�����Ʈ ������ �����Ͽ����� ���� Ȯ��.
        [SerializeField] private int wayPointIndex;      // ) ���� �̵� ��ǥ ����.

        // [ ] - 3) ����.
        public Animator animator;
        public GameObject introUI;      // ) ����Ƽ���� introUI�� �����ͼ� ����.
        public GameObject theShedLight;      // ) ����Ƽ���� theShedLight�� �����ͼ� ����.
        [SerializeField] private string aroundTrigger = "Arround";
        #endregion Variable






        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) �ʱ�ȭ.
            isArrive = new bool[5];
            wayPointIndex = 0;
            dolly = cart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
            dolly.Speed = 0f;
            // [ ] - [ ] - 2) ������.
            StartCoroutine(PlayStartSequence());
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ��������.
            if (cart.SplinePosition >= wayPointIndex && isArrive[wayPointIndex] == false)
            {
                Arrive();
            }
            // [ ] - [ ] - 2) ��ŵ.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GotoPlayScene();
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) Arrive.
        private void Arrive()
        {
            // [ ] - [ ] - 1) ������ ��������Ʈ üũ.
            if (wayPointIndex == isArrive.Length - 1)
            {
                StartCoroutine(PlayEndSequence());
            }
            else
            {
                StartCoroutine(PlayStaySequence());
            }
        }

        // [ ] - 2) �÷��̽����� ����.
        private void GotoPlayScene()
        {
            // [ ] - [ ] - 1) �ڷ�ƾ ����.
            StopAllCoroutines();
            // [ ] - [ ] - 2) BGM ����.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 3) .
            fader.FadeTo(loadToScene);
        }

        // [ ] - 3) ���� ������.
        IEnumerator PlayStartSequence()
        {
            isArrive[0] = true;
            // [ ] - [ ] - 1) FadeStart �� ���̵��� ȿ��.
            fader.FadeStart();
            // [ ] - [ ] - 2) BGM.
            AudioManager.Instance.PlayBgm("IntroBGM");
            yield return new WaitForSeconds(1f);
            // [ ] - [ ] - 3) �ֺ��� �θ��� �Ÿ���.
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);
            // [ ] - [ ] - 4) �̵�����.
            wayPointIndex = 1;      // ) ���� ��ǥ�������� ����.
            dolly.Speed = 0.05f;
        }

        // [ ] - 3) �̵�����Ʈ ���� ���� ������.
        IEnumerator PlayStaySequence()
        {
            // [ ] - [ ] - 1) ���� üũ�ϱ�.
            isArrive[wayPointIndex] = true;
            dolly.Speed = 0f;       // ) �̵� ���߱�.
            // [ ] - [ ] - 2) �ֺ��� �θ��� �Ÿ���.
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);

            switch (wayPointIndex)
            {
                case 1:
                    introUI.SetActive(true);
                    dolly.Speed = 0.1f;
                    break;
                case 2:
                    introUI.SetActive(false);
                    dolly.Speed = 0.1f;
                    break;
                case 3:
                    theShedLight.SetActive(true);
                    dolly.Speed = 0.2f;
                    break;
            }
            introUI.SetActive(true);
            // [ ] - [ ] - 4) �̵�����.
            wayPointIndex++;      // ) ���� ��ǥ�������� ����.
        }

        // [ ] - 4) �������� ���� ������.
        IEnumerator PlayEndSequence()
        {
            // [ ] - [ ] - 1) ���θ� ����Ʈ ���� ���� ���ɷ� �̵� �� ����� ����.

            // [ ] - [ ] - 2) ���� üũ.
            isArrive[wayPointIndex] = true;

            // [ ] - [ ] - 3) �̵� ����
            dolly.Speed = 0f;

            // [ ] - [ ] - 4) ���θ��� �Һ��� ����.
            theShedLight.SetActive(false);
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 5) BGM ����.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 6) ���������� ����.
            fader.FadeTo(loadToScene);
        }
        #endregion Custom Method
    }
}
