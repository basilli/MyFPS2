using UnityEngine;
using System;
using UnityEditor.ShaderGraph.Internal;

/* [0] 개요 : WeaponController
		- 무기를 제어하는 클래스.
		- 모든 무기에 부착됨.
*/

namespace Unity.FPS.Game
{
    // [1] 크로스헤어 데이터 구조체.
    [Serializable] public struct CrosshairData
    {
        public Sprite crosshairSprite;        // ) 이미지.
        public float crosshairSize;           // ) 크기.
        public Color crosshairColor;        // ) 색상.
    }


    // [2] 무기 슈팅타입 Enum.
    public enum WeaponShootType
    { 
    Manual,
    Automatic,
    Charge,
    Sniper
    }


    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ .
        public GameObject weaponRoot;                // ) 무기 비쥬얼 활성화 및 비활성화.
        public Transform weaponMuzzle;               // ) .
        private AudioSource shootAudioSource;       // ) .
        public AudioClip switchWeaponSfx;             // ) 무기를 바꿀 때의 효과음.


        // [◆] - ▶▶▶ 크로스 헤어.
        public CrosshairData defaultCrosshair;                // ) 기본 CrosshairData의 정보.
        public CrosshairData targetInSightCrosshair;         // ) 적이 타겟팅 되었을 때의 CrosshairData의.


        // [◆] - ▶▶▶ 조준 Aim.
        [Range(0,1)] public float aimZoomRatio = 1f;        // ) 조준 시 줌 확대 배율 → 60을 1f로 기준으로 하여 0~1까지의 슬라이드 바를 생성하여 조절할 수 있게 함.
        public Vector3 aimOffset;                               // ) 조준 위치로 이동 시 무기별 위치 Offset(조정)값.


        // [◆] - ▶▶▶ 슈팅.
        [SerializeField] private WeaponShootType shootType;         // ) 무기 슈팅타입 Enum.
        [SerializeField] private float maxAmmo = 8f;                    // ) 최대 탄약 수.
        private float currentAmmo;                                        // ) 현재 탄약 수.
        [SerializeField] private float delayBetweenShots = 0.5f;        // ) 연사시 발사 간격.
        private float lastTimeShot;                                          // ) 마지막 발사 시간.


        // [◆] - ▶▶▶ 발사효과.
        public GameObject muzzleFlashPrefab;        // ) VFX.
        public AudioClip shootSfx;                      // ) SFX(총기 발사 효과음).


        // [◆] - ▶▶▶ 반동(Recoil).
        public float recoilForce = 0.5f;


        // [◆] - ▶▶▶ 발사체(Projectile).
        public ProjectileBase projectPrefab;
        [SerializeField] private int bulletsPerShot = 1;                          // ) 방아쇠를 당길 때 발사되는 불릿의 갯수.
        [SerializeField] private float bulletSpreadAngle = 0f;                  // ) 발사체가 발사될 때 퍼져 나가는 각도.
        private Vector3 LastMuzzlePosition;                                     // ) 지난 프레임에 Muzzle의 위치.


        // [◆] - ▶▶▶ 재장전(Reload).
        [SerializeField] private bool automaticReload = true;      // ) 자동 재장전.
        private float ammoReloadRate = 1f;      // ) 초당 재장전 되는 Ammo의 량.
        private float ammoReloadDelay = 2f;         // ) 총을 쏜 후 delay 시간 이후 재장전 시간.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ .
        public GameObject Owner { get; set; }       // 무기를 장착한 주인 오브젝트.
        public GameObject SourcePrefab { get; set; }        // ) 무기를 생성한 프리팹.
        public bool IsWeaponActive { get; set; }        // ) 현재 장착한 무기가 액티브한 상태인 무기.


        // [◆] - ▶▶▶ Projectile.
        public Vector3 MuzzleWorldVelocity { get; private set; }        // ) 발사시 프레임의 총구의 이동속도.
        public float CurrentCharge { get; private set; }        // ) 슛 타입의 무기의 발사 충전량.


        // [◆] - ▶▶▶ 슛 타입 읽기전용.
        public WeaponShootType ShootType => shootType;


        // [◆] - ▶▶▶ 현재 소유한 Ammo의 비율.
        public float CurrentAmmoRate { get; private set; }
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 참조.
            shootAudioSource = this.GetComponent<AudioSource>();
        }


        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 초기화.
            currentAmmo = maxAmmo;
            lastTimeShot = Time.time;
        }


        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) .
            UpdateAmmo();
            // [◇] - [◆] - ) .
            if (Time.deltaTime > 0f)
            {
                // [◇] - [◇] - [◆] - ) 이번 프레임의 Muzzle 속도.
                MuzzleWorldVelocity = (weaponMuzzle.position - LastMuzzlePosition) / Time.deltaTime;
                // [◇] - [◇] - [◆] - ) Muzzle 위치 저장.
                LastMuzzlePosition = weaponMuzzle.position;
            }

        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ UpdateAmmo → Ammo 연산.
        private void UpdateAmmo()
        {
            // [◇] - [◆] - 재장전.
            if (automaticReload && currentAmmo < maxAmmo && (lastTimeShot + ammoReloadDelay) <= Time.time)
            {
                currentAmmo += ammoReloadRate * Time.deltaTime;
                currentAmmo = Mathf.Clamp(currentAmmo, 0f, maxAmmo);
            }
            // [◇] - [◆] - CurrentAmmoRate 연산.
            if (maxAmmo == 0 || maxAmmo == Mathf.Infinity)
            {
                CurrentAmmoRate = 1f;
            }
            else
            {
                CurrentAmmoRate = currentAmmo / maxAmmo;
            }
        }


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


        // [◆] - ▶▶▶ 슛 인풋 처리 → 매개변수로 fire down, held, released 받아서 슛팅 타입 처리.
        public bool HandleShootInput(bool inputDown, bool inputHeld, bool inputUp)
        {
            // [◇] - [◆] - ) ???.
            switch (shootType)
            {
                // [◇] - [◇] - [◆] ) WeaponShootType의 Manual.
                case WeaponShootType.Manual:
                    if (inputDown == true)
                    {
                        // [◇] - [◇] - [◇] - [◆] ) 슛.
                        return TryShoot();
                    }
                    break;
                // [◇] - [◇] - [◆] ) WeaponShootType의 Automatic.
                case WeaponShootType.Automatic:
                    if (inputHeld == true)
                    {
                        // [◇] - [◇] - [◇] - [◆] ) 슛.
                        return TryShoot();
                    }
                    break;
                // [◇] - [◇] - [◆] ) WeaponShootType의 Charge.
                case WeaponShootType.Charge:
                    break;
                // [◇] - [◇] - [◆] ) WeaponShootType의 Sniper.
                case WeaponShootType.Sniper:
                    if (inputHeld == true)
                    {
                        // [◇] - [◇] - [◇] - [◆] ) 슛.
                        return TryShoot();
                    }
                    break;
            }
            return false;
        }


        // [◆] - ▶▶▶ 발사 처리.
        private bool TryShoot()
        {
            // [◇] - [◆] - ) ???.
            if (currentAmmo >= 1f && (lastTimeShot + delayBetweenShots) < Time.time)
            {
                // [◇] - [◇] - [◆] ) ???.
                currentAmmo -= 1f;
                Debug.Log($"currentAmmo : {currentAmmo}");
                HandleShoot();
                return true;
            }
           return false;
        }


        // [◆] - ▶▶▶ HandleShoot.
        private void HandleShoot()
        {
            // [◇] - [◆] - ) 불릿 발사시(fire) 최종적으로 발생하는 발사체의 갯수.
            int bulletsPerShotFinal = bulletsPerShot;
            for (int i = 0; i < bulletsPerShotFinal; i++)
            {
                // [◇] - [◇] - [◆] ) 발사체가 나가는 방향을 랜덤하게 구함.
                Vector3 shotDirector = GetShotDirectionWithinSpread(weaponMuzzle);
                // [◇] - [◇] - [◆] ) 발사체 생성 후 .
                ProjectileBase projectileBase = Instantiate(projectPrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirector));
                projectileBase.Shoot(this);
            }
            // [◇] - [◆] - ) VFX(Muzzle Effect).
            if (muzzleFlashPrefab)
            {
                GameObject effectGo = Instantiate(muzzleFlashPrefab, weaponMuzzle.position, weaponMuzzle.rotation, weaponMuzzle);
                Destroy(effectGo, 1f);
            }
            // [◇] - [◆] - ) SFX.
            if (shootSfx)
            {
                shootAudioSource.PlayOneShot(shootSfx);
            }
            // [◇] - [◆] - ) 발사한 시간을 저장.
            lastTimeShot = Time.time;
        }


        // [◆] - ▶▶▶ GetShotDirectionWithinSpread → 발사체가 나가는 방향 구하기.
        private Vector3 GetShotDirectionWithinSpread(Transform shotTransform)
        {
            // [◇] - [◆] - ) .
            float spreadAngleRatio = bulletSpreadAngle / 180f;
            // [◇] - [◆] - ) .
            return Vector3.Slerp(shotTransform.forward, UnityEngine.Random.insideUnitSphere, spreadAngleRatio);
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}
// [◆] - ▶▶▶ 123.


// [◆] - ▶▶▶ 456.


// [◇] - [◆] - ) 789.
// [◇] - [◇] - [◆] ) 147.
// [◇] - [◇] - [◇] - [◆] ) 258.
// [◇] - [◇] - [◇] - [◇] - [◆] ) 369.