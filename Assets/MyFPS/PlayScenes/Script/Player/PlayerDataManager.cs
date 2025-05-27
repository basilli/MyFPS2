using UnityEngine;

/* [0] ���� : PlayerDataManager
		- �÷��̾� ������ ���� Ŭ���� �� �̱���(���������� ������ ����)���� ����.
*/

namespace MyFPS
{
    // [1] ���� ���� Ÿ�� �� enum.
    public enum WeaponType
    { 
        None, 
        Pistol,
    }


    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        // [2] Variable.
        #region Variable
        #endregion Variable





        // [2] Property.
        #region Property
        // [ ] - 1) ���� Ÿ��.
        public WeaponType Weapon { get; set; }
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) �÷��� ������ �ʱ�ȭ.
            Weapon = WeaponType.None;
        }
        #endregion Unity Event Method





        // [] Custom Method.
        #region Custom Method
        #endregion Custom Method
    }
}
// [ ] - ) 
// [ ] - [ ] - ) 