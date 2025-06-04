using Unity.VisualScripting;
using UnityEngine;

/* [0] 개요 : AudioManager
		- 사운드 속성 데이터를 관리하는 클래스.
*/

namespace MyFPS
{
    public class AudioManager : Singleton<AudioManager>
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 사운드 데이터 목록.
        public Sound[] sounds;
        // [ ] - 2) 배경음 이름.
        private string bgmSound = "";
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();
            // [ ] - 1) 사운드 데이터 셋팅.
            foreach (var s in sounds)
            {
                // [ ] - [ ] - [ ] - 1) AudioSource 컴포넌트 추가 및 생성. 
                s.source = this.gameObject.AddComponent<AudioSource>();
                // [ ] - [ ] - [ ] - 2) AudioSource 컴포넌트 데이터 셋팅. 
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.playOnAwake = false;
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) Play → 사운드 플레이.
        public void Play(string name)
        {
            // [ ] - [ ] - 1) .
            Sound sound = null;
            // [ ] - [ ] - 2) 사운드 목록에서 같은 이름의 사운드 찾기. 
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

        // [ ] - 2) 사운드 정지.
        public void Stop(string name)
        {
            // [ ] - [ ] - 1) .
            Sound sound = null;
            // [ ] - [ ] - 2) 사운드 목록에서 같은 이름의 사운드 찾기. 
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

        // [ ] - 3) 배경음 플레이.
        public void PlayBgm(string name)
        {
            // [ ] - [ ] - 1) 배경음 이름 체크.
            if (bgmSound == name)
            {
                return;
            }
            // [ ] - [ ] - 2) 현재 재생되고 있는 배경음 스톱.
            Stop(bgmSound);
            // [ ] - [ ] - 3) .
            Sound sound = null;
            // [ ] - [ ] - 4) 사운드 목록에서 같은 이름의 사운드 찾기. 
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    // [ ] - [ ] - [ ] - 1) 배경음 이름 저장.
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

        // [ ] - 4) StopBgm → 배경음 플레이.
        public void StopBgm()
        {
            Stop(bgmSound);
        }
        #endregion Custom Method
    }
}
