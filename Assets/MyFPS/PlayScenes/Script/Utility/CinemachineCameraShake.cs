using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

/* [0] ���� : CinemachineCameraShake
		- �ó׸ӽ� ī�޶��� ��鸲 ȿ�� ���� �� �̱���.
*/

namespace MyFPS
{
    public class CinemachineCameraShake : Singleton<CinemachineCameraShake>
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private CinemachineBasicMultiChannelPerlin channelPrelin;
        // [ ] - 2) ��鸲 üũ.
        private bool isShake = false;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - ) ����.
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
        // [ ] - 1) Shake �� ī�޶� ����.
        public void Shake(float amplitudeGain, float FrequencyGain, float shakeTime)        // ) amplitudeGain : ��鸲�� ũ��, FrequencyGain : ��鸲�� �ӵ�, shakeTime : ��鸲�� �ð�.
        {
            // [ ] - [ ] - ) ��鸱 ��� ��鸮�� �ʰ� �ϱ�.
            if (isShake)
                return;
            // [ ] - [ ] - ) .
            StartCoroutine(StartShake(amplitudeGain, FrequencyGain, shakeTime));
        }

        // [ ] - 2) StartShake.
        IEnumerator StartShake(float amplitudeGain, float FrequencyGain, float shakeTime)
        {
            // [ ] - [ ] - ) ��鸲�� ����.
            isShake = true;
            // [ ] - [ ] - ) ��鸲�� ����.
            channelPrelin.AmplitudeGain = amplitudeGain;
            channelPrelin.FrequencyGain = FrequencyGain;
            // [ ] - [ ] - ) ��鸲�� ���ӽð�.
            yield return new WaitForSeconds(shakeTime);
            // [ ] - [ ] - ) ��鸲�� �ʱ�ȭ(����).
            channelPrelin.AmplitudeGain = 0;
            channelPrelin.FrequencyGain = 0;
            // [ ] - [ ] - ) ��鸲�� ��.
            isShake = false;
        }
        #endregion Custom Method
    }
}
