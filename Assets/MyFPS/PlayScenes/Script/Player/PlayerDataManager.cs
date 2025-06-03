using MyFPS;
using UnityEngine;

namespace MyFPS
{
    //���� ���� Ÿ�� enum
    public enum WeaponType
    {
        None,
        Pistol,
    }

    //���� ������ enum�� ����
    public enum PuzzleKey
    {
        ROOM01_KEY,
        LEFTEYE_KEY,
        RIGHTEYE_KEY,
        MAX_KEY              //���� ������ ����
    }

    //�÷��̾� ������ ���� Ŭ���� - �̱���(���� ������ ������ ����)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        private int ammoCount;

        private bool[] hasKeys;         //���� ������ ���� ���� üũ
        #endregion

        #region Property
        //���� Ÿ��
        public WeaponType Weapon { get; set; }

        //źȯ ���� �����ϴ� �б� ���� ������Ƽ
        public int AmmoCount => ammoCount;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //�÷��� ������ �ʱ�ȭ 
            InitPlayerData();
        }

        //�÷��� ������ �ʱ�ȭ 
        private void InitPlayerData()
        {
            ammoCount = 0;
            Weapon = WeaponType.None;

            //���� ������ ����: ���� ������ ������ŭ ���� ���� ��Ҽ� ����
            hasKeys = new bool[(int)PuzzleKey.MAX_KEY];

        }
        #endregion

        #region Custom Method
        //ammo ���� �Լ�
        public void AddAmmo(int amount)
        {
            ammoCount += amount;
        }

        //ammo ��� �Լ�
        public bool UseAmmo(int amount)
        {
            //���� ammo üũ
            if (ammoCount < amount)
            {
                Debug.Log("You need to reload");
                return false;
            }

            ammoCount -= amount;
            return true;
        }

        //���� ������ ȹ�� - �Ű������� ����Ű Ÿ�� �޴´�
        public void GainPuzzleKey(PuzzleKey key)
        {
            hasKeys[(int)key] = true;
        }

        //���� ������ ���� ���� üũ - �Ű������� ����Ű Ÿ�� �޴´�
        public bool HasPuzzleKey(PuzzleKey key)
        {
            return hasKeys[(int)key];
        }
        #endregion

    }
}