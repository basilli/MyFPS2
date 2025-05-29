using UnityEngine;

/* [0] 개요 : Sound
		- 사운드 속성 데이터를 관리하는 클래스.
*/

namespace MyFPS
{
    [System.Serializable]
    public class Sound
    {
        // [1] Audio Source 컴포넌트의 속성.
        // [ ] - [ ] - 1) 사운드 속성 데이터 이름.
        public string name;
        // [ ] - [ ] - 2) 사운드 클립 리소스.
        public AudioClip clip;
        // [ ] - [ ] - 3) 볼륨의 크기를 0~1사이로 조절. 
        [Range(0f,1f)] public float volume;
        // [ ] - [ ] - 4) 재생속도.
        [Range(0.1f, 3f)] public float pitch;
        // [ ] - [ ] - 5) 반복재생 여부.
        public bool loop;

        // [2] 설정한 속성값을 재생할 AudioSource.
        public AudioSource source;
    }
}
