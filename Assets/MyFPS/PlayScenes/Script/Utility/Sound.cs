using UnityEngine;

/* [0] ���� : Sound
		- ���� �Ӽ� �����͸� �����ϴ� Ŭ����.
*/

namespace MyFPS
{
    [System.Serializable]
    public class Sound
    {
        // [1] Audio Source ������Ʈ�� �Ӽ�.
        // [ ] - [ ] - 1) ���� �Ӽ� ������ �̸�.
        public string name;
        // [ ] - [ ] - 2) ���� Ŭ�� ���ҽ�.
        public AudioClip clip;
        // [ ] - [ ] - 3) ������ ũ�⸦ 0~1���̷� ����. 
        [Range(0f,1f)] public float volume;
        // [ ] - [ ] - 4) ����ӵ�.
        [Range(0.1f, 3f)] public float pitch;
        // [ ] - [ ] - 5) �ݺ���� ����.
        public bool loop;

        // [2] ������ �Ӽ����� ����� AudioSource.
        public AudioSource source;
    }
}
