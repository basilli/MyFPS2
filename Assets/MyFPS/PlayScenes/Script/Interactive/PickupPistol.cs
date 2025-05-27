using TMPro;
using UnityEngine;

/* [0] ���� : PickupPistol
		- ������(����) ȹ�� ���ͷ�Ƽ�� ����.
*/

namespace MyFPS
{
    public class PickupPistol : Interactive
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) ���ͷ�Ƽ�� �׼�.
        public GameObject realPistol;
        public GameObject theArrow;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) ���ݱ� + ȭ��ǥ ���� + ���ⵥ���� ���� + Box Collider ����.
        protected override void DoAction()
        {
            realPistol.SetActive(true);
            theArrow.SetActive(false);
            PlayerDataManager.Instance.Weapon = WeaponType.Pistol;
            this.gameObject.SetActive(false);
        }
        #endregion Custom Method
    }
}