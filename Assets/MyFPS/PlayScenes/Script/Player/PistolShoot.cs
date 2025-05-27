
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/* [0] ���� : PistolShoot
		- �ǽ��� ���� Ŭ����.
*/

namespace MyFPS
{
    public class PistolShoot : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ����.
        private Animator animator;
        public Transform firePoint;
        public GameObject muzzleEffect;
        public AudioSource pistolShot;
        // [ ] - 2) �ѱ� �߻� ����Ʈ
        public ParticleSystem muzzleFlash;
        // [ ] - 3) �ǰ� ����Ʈ.
        public GameObject hitImpactPrefab;
        // [ ] - 4) hit ��ݰ���.
        [SerializeField] private float impactForce = 10f;
        // [ ] - 5) �������.
        private bool isFire = false;
        // [ ] - 6) ���ݷ�.
        [SerializeField] private float attackDamage = 5f;
        // [ ] - 7) �ִ��Ÿ�.
        private float maxAttackDistance = 200f;
        // [ ] - 8) �ִϸ��̼� �Ķ����.
        private string fire = "Fire";
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - ) Start.
        private void Start()
        {
            // [ ] - [ ] - ) ����.
            animator = this.GetComponent<Animator>();
        }

        // [ ] - 2) OnDrawGizmosSelected.
        private void OnDrawGizmosSelected()
        {
            //FirePoint���� DrawRay(Red) �ִ� 200����
            //���̸� ���� 200 �ȿ� �浹ü�� ������ �浹ü ���� ���̸� �׸���
            //�浹ü�� ������ ���̸� 200���� �׸���
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
            // ���� ���� �� �߻� �� 1�ʵ��� �߻簡 �����ʰ� ��.
            isFire = false;
            // ���̸� ���� 200 �ȿ� ��(�κ�)�� ������ ��(�κ�)���� �������� �ش�.
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, maxAttackDistance);
            if (isHit)
            {
                Debug.Log($"{hit.transform.name}���� {attackDamage}��ŭ�� �������� �ش�.");

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

            // �ִϸ��̼� �÷���
            animator.SetTrigger(fire);

            // ���� VFX, SFX
            // �߻� ����Ʈ �÷��� Ȱ��ȭ
            muzzleEffect.SetActive(true);
            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }

            // �߻� ���� �÷���
            pistolShot.Play();

            // 0.���� ������
            yield return new WaitForSeconds(0.3f);

            // �߻� ����Ʈ �÷��� ��Ȱ��ȭ
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
