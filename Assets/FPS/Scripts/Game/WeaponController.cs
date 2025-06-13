using UnityEngine;

/* [0] 개요 : WeaponController
		- 무기를 제어하는 클래스.
		- 모든 무기에 부착됨.
*/

namespace Unity.FPS.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        public GameObject weaponRoot;       // ) 무기 비쥬얼 활성화 및 비활성화.
        private AudioSource shootAudioSource;
        public AudioClip switchWeaponSfx;       // ) 무기를 바꿀 때의 효과음.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ 123.
        public GameObject Owner { get; set; }       // 무기를 장착한 주인 오브젝트.
        public GameObject SourcePrefab { get; set; }        // ) 무기를 생성한 프리팹.
        public bool IsWeaponActive { get; set; }        // ) 현재 장착한 무기가 액티브한 상태인 무기.
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 참조.
            shootAudioSource = this.GetComponent<AudioSource>();
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲



        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ ShowWeapon.
        public void ShowWeapon(bool show)
        {
            weaponRoot.SetActive(show);
            IsWeaponActive = show;
            // [◇] - [◆] - ) 무기교체.
            if (show == true && switchWeaponSfx) 
            {
                shootAudioSource.PlayOneShot(switchWeaponSfx);
            }
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}
