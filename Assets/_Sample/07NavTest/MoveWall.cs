using UnityEngine;

/* [0] ���� : MoveWall
		- �¿�� �̵��ϸ�, Ÿ�̸� �ð����� ������ �ٲ㼭 �̵��ϴ� �� �� ��Ʈ��.
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
        // [ ] - 2) �̵����� �� 1�̸� ������, -1�̸� ����.
        [SerializeField] private float dir = 1f;
        #endregion Variable





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) Ÿ�̸� �ð����� ������ �ٲ㼭 �̵�.
            countdown += Time.deltaTime;
            if (countdown >= moveTime)
            {
                // [ ] - [ ] - [ ] - 1) ������ �ٲ�.
                dir *= -1f;
                // [ ] - [ ] - [ ] - 2) Ÿ�̸� �ʱ�ȭ.
                countdown = 0f;
            }
            // [ ] - [ ] - 2) .
            transform.Translate((Vector3.right * dir) * Time.deltaTime* moveSpeed, Space.World);
        }
        #endregion Unity Event Method
    }
}
