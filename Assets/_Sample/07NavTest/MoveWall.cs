using UnityEngine;

/* [0] 개요 : MoveWall
		- 좌우로 이동하며, 타이머 시간마다 방향을 바꿔서 이동하는 벽 → 패트롤.
*/

namespace MySample
{
    public class MoveWall : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) .
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float moveTime = 1f;
        [SerializeField] private float countdown = 0f;
        // [ ] - 2) 이동방향 → 1이면 오른쪽, -1이면 왼쪽.
        [SerializeField] private float dir = 1f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 타이머 시간마다 방향을 바꿔서 이동.
            countdown += Time.deltaTime;
            if (countdown >= moveTime)
            {
                // [ ] - [ ] - [ ] - 1) 방향을 바꿈.
                dir *= -1f;
                // [ ] - [ ] - [ ] - 2) 타이머 초기화.
                countdown = 0f;
            }
            // [ ] - [ ] - 2) .
            transform.Translate((Vector3.right * dir) * Time.deltaTime* moveSpeed, Space.World);
        }
        #endregion Unity Event Method
    }
}
