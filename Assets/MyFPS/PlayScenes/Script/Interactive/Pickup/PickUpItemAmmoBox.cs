using UnityEngine;

/* [0] ���� : PickUpItemAmmoBox
		- ������ ������ ����.
            - �������� 360�� ȸ��.
            - �� �Ʒ��� �Դٰ��� ��鸲 ����(Mathf : Sine � Ȱ��).
		- 
		- 
		- 
		- 
*/

namespace MyFPS
{
    public class PickUpItemAmmoBox : PickUpItem
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) �Ѿ����� �� ������ �Դ� ȿ��.
        [SerializeField] private int giveAmmo = 7;
        #endregion Variable





        // [4] Custom Method.
        #region Custom Method
        // [ ] - ) OnPickup.
        protected override bool OnPickup()
        {
            // [ ] - [ ] - ) . Ammobox �������� �Ծ��� ���� ȿ�� ����.
            // ) PlayerDataManager.Instance.AddAmmo(giveAmmo);
            return true;
        }
        #endregion Custom Method
    }
}
