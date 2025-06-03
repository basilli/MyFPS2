using UnityEngine;

/* [0] ���� : PickUpItem
		- �÷��̾ �ε����� �浹 üũ.
            - �浹�� źȯ 7�� ����.
            - ������ ų.

*/

namespace MyFPS
{
    public class PickUpItem : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 2) ȸ���ӵ�.
        [SerializeField] private float rotateSpeed = 5f;
        // [ ] - 3) ���� �̵��ӵ�.
        [SerializeField] private float verticalBobFrequency = 1f;
        // [ ] - 4) ���� ����ũ��.
        [SerializeField] private float bobingAmount = 1f;
        // [ ] - 5) ������ �ʱ� ��ġ��.
        private Vector3 startPosition;
        #endregion Variable





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) �ʱ�ȭ.
            startPosition = transform.position;
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) �������� ���� �̵�.
            float bobingAnimationPhase = Mathf.Sin(Time.time * verticalBobFrequency) * bobingAmount;
            transform.position = startPosition + Vector3.up * bobingAnimationPhase;
            // [ ] - [ ] - 2) �������� ȸ��.
            transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
        }

        // [ ] - 2) OnTriggerEnter �� �浹üũ.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) Player�� ���� �� �ְ� �±� Ȯ��.
            if (other.tag == "Player")
            {
                // [ ] - [ ] - [ ] - 1) .
                if (OnPickup())
                {
                    Debug.Log("�÷��̾ �������� �Ծ����ϴ�.");
                    // [ ] - [ ] - [ ] - 2) ������ ����.
                    Destroy(gameObject);
                }
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) OnPickup �� ������ üũ�Ͽ� �������� ������ true, �� ������ false.
        protected virtual bool OnPickup()
        {
            // [ ] - [ ] - 1) �������� �Ծ��� ���� ȿ�� ���� �� �������� ���� �� �ִ��� üũ.
            // )        PlayerDataManager.Instance.AddAmmo(giveAmmo);
            return true;
        }
        #endregion Custom Method

        // [ ] - ) 
        // [ ] - [ ] - ) 
        // [ ] - [ ] - [ ] - ) 
        // [ ] - [ ] - [ ] - [ ] - ) 


    }
}
