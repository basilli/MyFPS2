
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/* [0] 개요 : PistolShoot
		- 피스톨 제어 클래스.
*/

namespace MyFPS
{
    public class PistolShoot : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 참조.
        private Animator animator;
        public Transform firePoint;
        public GameObject muzzleEffect;
        public AudioSource pistolShot;
        // [ ] - 2) 총구 발사 이펙트
        public ParticleSystem muzzleFlash;
        // [ ] - 3) 피격 이펙트.
        public GameObject hitImpactPrefab;
        // [ ] - 4) hit 충격강도.
        [SerializeField] private float impactForce = 10f;
        // [ ] - 5) 연사방지.
        private bool isFire = false;
        // [ ] - 6) 공격력.
        [SerializeField] private float attackDamage = 5f;
        // [ ] - 7) 최대사거리.
        private float maxAttackDistance = 200f;
        // [ ] - 8) 애니메이션 파라미터.
        private string fire = "Fire";
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - ) Start.
        private void Start()
        {
            // [ ] - [ ] - ) 참조.
            animator = this.GetComponent<Animator>();
        }

        // [ ] - 2) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            //FirePoint에서 DrawRay(Red) 최대 200으로
            //레이를 쏴서 200 안에 충돌체가 있으면 충돌체 까지 레이를 그리고
            //충돌체가 없으면 레이를 200까지 그린다
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, maxAttackDistance);

            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * maxAttackDistance);
            }
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) Fire.
        public void Fire()
        {
            // [ ] - [ ] - 1) .
            if (isFire)
                return;

            StartCoroutine(Shoot());
        }

        // [ ] - 2) SequercePlayer.
        IEnumerator Shoot()
        {
            // 연사 방지 → 발사 후 1초동안 발사가 되지않게 함.
            isFire = false;
            // 레이를 쏴서 200 안에 적(로봇)이 있으면 적(로봇)에게 데미지를 준다.
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, maxAttackDistance);
            if (isHit)
            {
                Debug.Log($"{hit.transform.name}에게 {attackDamage}만큼의 데미지를 준다.");

                if (hitImpactPrefab)
                {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effectGo, 2f);
                }

                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                }

                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
            }

            // 애니메이션 플레이
            animator.SetTrigger(fire);

            // 연출 VFX, SFX
            // 발사 이팩트 플래시 활성화
            muzzleEffect.SetActive(true);
            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }

            // 발사 사운드 플레이
            pistolShot.Play();

            // 0.３초 딜레이
            yield return new WaitForSeconds(0.3f);

            // 발사 이팩트 플래시 비활성화
            muzzleEffect.SetActive(false);
            if (muzzleFlash)
            {
                muzzleFlash.Stop();
            }

            yield return new WaitForSeconds(0.7f);

            isFire = false;
            #endregion Custom Method
        }
    }
}
