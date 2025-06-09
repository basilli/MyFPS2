using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

/* [0] 개요 : CinemachineCameraShake
		- 시네머신 카메라의 흔들림 효과 구현 → 싱글톤.
*/

namespace MyFPS
{
    public class CinemachineCameraShake : Singleton<CinemachineCameraShake>
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private CinemachineBasicMultiChannelPerlin channelPrelin;
        // [ ] - 2) 흔들림 체크.
        private bool isShake = false;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - ) 참조.
            channelPrelin = this.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        // [ ] - 2) Update.
        private void Update()
        {
            /*
            // TODO : CHEATING TEST.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Shake(1f, 1f, 1f);
            }
            */
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) Shake → 카메라 흔들기.
        public void Shake(float amplitudeGain, float FrequencyGain, float shakeTime)        // ) amplitudeGain : 흔들림의 크기, FrequencyGain : 흔들림의 속도, shakeTime : 흔들림의 시간.
        {
            // [ ] - [ ] - ) 흔들릴 경우 흔들리지 않게 하기.
            if (isShake)
                return;
            // [ ] - [ ] - ) .
            StartCoroutine(StartShake(amplitudeGain, FrequencyGain, shakeTime));
        }

        // [ ] - 2) StartShake.
        IEnumerator StartShake(float amplitudeGain, float FrequencyGain, float shakeTime)
        {
            // [ ] - [ ] - ) 흔들림의 시작.
            isShake = true;
            // [ ] - [ ] - ) 흔들림의 셋팅.
            channelPrelin.AmplitudeGain = amplitudeGain;
            channelPrelin.FrequencyGain = FrequencyGain;
            // [ ] - [ ] - ) 흔들림의 지속시간.
            yield return new WaitForSeconds(shakeTime);
            // [ ] - [ ] - ) 흔들림의 초기화(고정).
            channelPrelin.AmplitudeGain = 0;
            channelPrelin.FrequencyGain = 0;
            // [ ] - [ ] - ) 흔들림의 끝.
            isShake = false;
        }
        #endregion Custom Method
    }
}
