using UnityEngine;

/* [0] ���� : Destructable
		- �׾��� �� Health�� �������ִ� ������Ʈ�� ų�ϴ� Ŭ����.
*/

namespace Unity.FPS.Game
{
    public class Destructable : MonoBehaviour
    {
        // [1] Variable.
        #region ������ Variable ������
        // [��] - ������ ����.
        private Health health;
        #endregion ������ Variable ������





        // [2] Unity Event Method.
        #region ������ Unity Event Method ������
        // [��] - ������ Awake.
        private void Awake()
        {
            // [��] - [��] - ) ����.
            health = this.GetComponent<Health>();
        }


        // [��] - ������ Start.
        private void Start()
        {
            // [��] - [��] - ) �ʱ�ȭ �� Health�� UnityAction �Լ��� ���. 
            health.OnDamaged += OnDamaged;
            health.OnDie += OnDie;
        }
        #endregion ������ Unity Event Method ������





        // [3] Custom Method.
        #region ������ Custom Method ������
        // [��] - ������ OnDamaged �� Health�� UnityAction �Լ��� OnDamaged�� ��ϵ� �Լ�.
        private void OnDamaged(float damage, GameObject damageSource)
        { 
        // TODO : ������ ����.
        }


        // [��] - ������ OnDie.
        void OnDie()
        {
            // [��] - [��] - ) ����ó��.
            ;
            // [��] - [��] - ) ������Ʈ ų.
            Destroy(gameObject);
        }
        #endregion ������ Custom Method ������

    }
}
