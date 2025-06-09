using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

/* [0] 개요 : Intro
*/

namespace MyFPS
{

    public class Intro : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        public SceneFader fader;        // 유니티에서 SceneFader를 가져와서 연결.
        [SerializeField] private string loadToScene = "MainScene01";        // MainScene01를 가져와서 연결.
        private SplineAutoDolly.FixedSpeed dolly;

        // [ ] - 2) 이동.
        public CinemachineSplineCart cart;      // ) 유니티에서 Dolly Cart를 가져와서 연결.
        private bool[] isArrive;        // ) 이동포인트 지점에 도착하였는지 여부 확인.
        [SerializeField] private int wayPointIndex;      // ) 다음 이동 목표 지점.

        // [ ] - 3) 연출.
        public Animator animator;
        public GameObject introUI;      // ) 유니티에서 introUI를 가져와서 연결.
        public GameObject theShedLight;      // ) 유니티에서 theShedLight를 가져와서 연결.
        [SerializeField] private string aroundTrigger = "Arround";
        #endregion Variable






        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 초기화.
            isArrive = new bool[5];
            wayPointIndex = 0;
            dolly = cart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
            dolly.Speed = 0f;
            // [ ] - [ ] - 2) 시퀀스.
            StartCoroutine(PlayStartSequence());
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 도착판정.
            if (cart.SplinePosition >= wayPointIndex && isArrive[wayPointIndex] == false)
            {
                Arrive();
            }
            // [ ] - [ ] - 2) 스킵.
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
            // [ ] - [ ] - 1) 마지막 엔드포인트 체크.
            if (wayPointIndex == isArrive.Length - 1)
            {
                StartCoroutine(PlayEndSequence());
            }
            else
            {
                StartCoroutine(PlayStaySequence());
            }
        }

        // [ ] - 2) 플레이신으로 가기.
        private void GotoPlayScene()
        {
            // [ ] - [ ] - 1) 코루틴 정지.
            StopAllCoroutines();
            // [ ] - [ ] - 2) BGM 정지.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 3) .
            fader.FadeTo(loadToScene);
        }

        // [ ] - 3) 시작 시퀀스.
        IEnumerator PlayStartSequence()
        {
            isArrive[0] = true;
            // [ ] - [ ] - 1) FadeStart → 페이드인 효과.
            fader.FadeStart();
            // [ ] - [ ] - 2) BGM.
            AudioManager.Instance.PlayBgm("IntroBGM");
            yield return new WaitForSeconds(1f);
            // [ ] - [ ] - 3) 주변을 두리번 거리기.
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);
            // [ ] - [ ] - 4) 이동시작.
            wayPointIndex = 1;      // ) 다음 목표지점으로 설정.
            dolly.Speed = 0.05f;
        }

        // [ ] - 3) 이동포인트 지점 도착 시퀀스.
        IEnumerator PlayStaySequence()
        {
            // [ ] - [ ] - 1) 도착 체크하기.
            isArrive[wayPointIndex] = true;
            dolly.Speed = 0f;       // ) 이동 멈추기.
            // [ ] - [ ] - 2) 주변을 두리번 거리기.
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
            // [ ] - [ ] - 4) 이동시작.
            wayPointIndex++;      // ) 다음 목표지점으로 설정.
        }

        // [ ] - 4) 최종지점 도착 시퀀스.
        IEnumerator PlayEndSequence()
        {
            // [ ] - [ ] - 1) 오두막 라이트 끄고 다음 씨능로 이동 및 배경음 종료.

            // [ ] - [ ] - 2) 도착 체크.
            isArrive[wayPointIndex] = true;

            // [ ] - [ ] - 3) 이동 멈춤
            dolly.Speed = 0f;

            // [ ] - [ ] - 4) 오두막의 불빛이 꺼짐.
            theShedLight.SetActive(false);
            yield return new WaitForSeconds(2f);
            // [ ] - [ ] - 5) BGM 종료.
            AudioManager.Instance.StopBgm();
            // [ ] - [ ] - 6) 다음씬으로 가기.
            fader.FadeTo(loadToScene);
        }
        #endregion Custom Method
    }
}
