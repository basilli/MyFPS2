using UnityEngine;
using UnityEngine.Audio;

/* [0] ���� : AudioManager
		- ���� �Ӽ� �����͸� �����ϴ� Ŭ����.
*/

namespace MyFPS
{
    public class AudioManager : Singleton<AudioManager>
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ���� ������ ���.
        public Sound[] sounds;
        // [ ] - 2) ����� �̸�.
        private string bgmSound = "";
        // [ ] - 3) Audio Mixer.
        public AudioMixer audioMixer;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();
            // [ ] - 1) Audio Mixer���� ����� �׷� ��������.
            AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups("Master");

            // [ ] - 2) ���� ������ ����.
            foreach (var s in sounds)
            {
                // [ ] - [ ] - [ ] - 1) AudioSource ������Ʈ �߰� �� ����. 
                s.source = this.gameObject.AddComponent<AudioSource>();
                // [ ] - [ ] - [ ] - 2) AudioSource ������Ʈ ������ ����. 
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.playOnAwake = false;
                // [ ] - [ ] - [ ] - 3) . 
                if (s.source.loop)      // ) BGM.
                {
                    s.source.outputAudioMixerGroup = audioMixerGroups[1];
                }
                else      // ) SFX.
                {
                    s.source.outputAudioMixerGroup = audioMixerGroups[2];
                }
                // s.source.outputAudioMixerGroup = Bgm/SFX
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) Play �� ���� �÷���.
        public void Play(string name)
        {
            // [ ] - [ ] - 1) .
            Sound sound = null;
            // [ ] - [ ] - 2) ���� ��Ͽ��� ���� �̸��� ���� ã��. 
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;
                }
            }
            // [ ] - [ ] - 3) .
            if (sound == null)
            {
                Debug.Log("Cannot find" + name + "Sound");
                return;
            }
            // [ ] - [ ] - 4) .
            sound.source.Play();
        }

        // [ ] - 2) ���� ����.
        public void Stop(string name)
        {
            // [ ] - [ ] - 1) .
            Sound sound = null;
            // [ ] - [ ] - 2) ���� ��Ͽ��� ���� �̸��� ���� ã��. 
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;
                }
            }
            // [ ] - [ ] - 3) .
            if (sound == null)
            {
                Debug.Log("Cannot find" + name + "Sound");
                return;
            }
            // [ ] - [ ] - 4) .
            sound.source.Stop();
        }

        // [ ] - 3) ����� �÷���.
        public void PlayBgm(string name)
        {
            // [ ] - [ ] - 1) ����� �̸� üũ.
            if (bgmSound == name)
            {
                return;
            }
            // [ ] - [ ] - 2) ���� ����ǰ� �ִ� ����� ����.
            Stop(bgmSound);
            // [ ] - [ ] - 3) .
            Sound sound = null;
            // [ ] - [ ] - 4) ���� ��Ͽ��� ���� �̸��� ���� ã��. 
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    // [ ] - [ ] - [ ] - 1) ����� �̸� ����.
                    bgmSound = s.name;
                    break;
                }
            }
            // [ ] - [ ] - 5) .
            if (sound == null)
            {
                Debug.Log("Cannot find" + name + "Sound");
                return;
            }
            // [ ] - [ ] - 6) .
            sound.source.Play();
        }

        // [ ] - 4) StopBgm �� ����� �÷���.
        public void StopBgm()
        {
            Stop(bgmSound);
        }
        #endregion Custom Method
    }
}
