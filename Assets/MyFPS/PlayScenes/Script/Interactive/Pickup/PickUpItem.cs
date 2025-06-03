using UnityEngine;

/* [0] 개요 : PickUpItem
		- 플레이어가 부딪히는 충돌 체크.
            - 충돌시 탄환 7개 지급.
            - 아이템 킬.

*/

namespace MyFPS
{
    public class PickUpItem : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 2) 회전속도.
        [SerializeField] private float rotateSpeed = 5f;
        // [ ] - 3) 상하 이동속도.
        [SerializeField] private float verticalBobFrequency = 1f;
        // [ ] - 4) 상하 진폭크기.
        [SerializeField] private float bobingAmount = 1f;
        // [ ] - 5) 아이템 초기 위치값.
        private Vector3 startPosition;
        #endregion Variable





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 초기화.
            startPosition = transform.position;
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 아이템의 상하 이동.
            float bobingAnimationPhase = Mathf.Sin(Time.time * verticalBobFrequency) * bobingAmount;
            transform.position = startPosition + Vector3.up * bobingAnimationPhase;
            // [ ] - [ ] - 2) 아이템의 회전.
            transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
        }

        // [ ] - 2) OnTriggerEnter → 충돌체크.
        private void OnTriggerEnter(Collider other)
        {
            // [ ] - [ ] - 1) Player만 먹을 수 있게 태그 확인.
            if (other.tag == "Player")
            {
                // [ ] - [ ] - [ ] - 1) .
                if (OnPickup())
                {
                    Debug.Log("플레이어가 아이템을 먹었습니다.");
                    // [ ] - [ ] - [ ] - 2) 아이템 제거.
                    Destroy(gameObject);
                }
            }
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) OnPickup → 조건을 체크하여 아이템을 먹으면 true, 못 먹으면 false.
        protected virtual bool OnPickup()
        {
            // [ ] - [ ] - 1) 아이템을 먹었을 때의 효과 구현 → 아이템을 먹을 수 있는지 체크.
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
