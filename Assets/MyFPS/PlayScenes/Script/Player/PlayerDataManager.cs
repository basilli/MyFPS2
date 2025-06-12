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
        private int sceneNumber;        //�÷��� ���� �� ���� ��ȣ
        private int ammoCount;      //������ źȯ ����
        private float playerHealth;         // �÷��̾� ü��

        private bool[] hasKeys;         //���� ������ ���� ���� üũ

        [SerializeField] private float maxPlayerHealth = 20;
        #endregion

        #region Property
        //���� Ÿ��
        public WeaponType Weapon { get; set; }

        //�÷��̾����� �� ���� ��ȣ
        public int SceneNumber
        {
            get
            {
                return sceneNumber;
            }
            set
            {
                sceneNumber = value;
            }
        }

        //źȯ ���� ������Ƽ
        public int AmmoCount
        {
            get
            {
                return ammoCount;
            }
            set
            {
                ammoCount = value;
            }
        }

        //
        public float PlayerHealth
        {

            get
            {
                return playerHealth;
            }
            set
            {
                playerHealth = value;
            }
        }
        #endregion



        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();
            //�÷��� ������ �ʱ�ȭ 
            InitPlayerData();
        }

        private void Start()
        {
        }
        #endregion

        #region Custom Method
        //�÷��� ������ �ʱ�ȭ 
        public void InitPlayerData(PlayData pData = null    )
        {
            if (pData != null)
            {
                sceneNumber = pData.sceneNumber;
                ammoCount = pData.ammoCount;
                playerHealth = pData.playerHealth;
            }
            else
            {
                sceneNumber = -1;
                ammoCount = 0;
                playerHealth = maxPlayerHealth;
            }
            Weapon = WeaponType.None;

            //���� ������ ����: ���� ������ ������ŭ ���� ���� ��Ҽ� ����
            hasKeys = new bool[(int)PuzzleKey.MAX_KEY];

        }
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