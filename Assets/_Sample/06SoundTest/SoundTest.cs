using UnityEngine;

/* [0] ���� : SoundTest
		- 
		- 
		- 
		- 
		- 
*/

namespace MySample
{
    public class SoundTest : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        private AudioSource audioSource;
        // [ ] - 2) Audio Source �Ӽ�.
        public AudioClip clip;
        [SerializeField] private float volume = 1.0f;
        [SerializeField] private float pitch = 1.0f;
        [SerializeField] private bool loop = false;
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) ����.
            audioSource = this.GetComponent<AudioSource>();
            // [ ] - [ ] - 2) SoundPlay.
            SoundPlay();
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) SoundOnShot �� ���� �÷���.
        private void SoundOnShot()
        {
            // )        audioSource.Play();
            audioSource.PlayOneShot(clip);
        }

        // [ ] - 2) SoundPlay.
        private void SoundPlay()
        {
            // [ ] - [ ] - 1) Audio Source �Ӽ� ����.
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            // [ ] - [ ] - 2) ���� �÷���.
            audioSource.Play();
        }
        #endregion Custom Method

        // [ ] - ) 
        // [ ] - [ ] - ) 
    }
}

