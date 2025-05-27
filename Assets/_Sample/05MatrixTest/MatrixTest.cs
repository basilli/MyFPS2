using UnityEngine;
using TMPro;

/* [0] 개요 : MatrixTest
		- 매개변수로 들어온 플레이어의 위치간의 거리 구하기.
*/

namespace MySample
{

    public class MatrixTest : MonoBehaviour
    {
		public TextMeshProUGUI text;
		public Matrix4x4 matrix;

        private void Update()
		{
			// 트랜스폰의 매트릭스 값 가져오기.
			matrix = transform.localToWorldMatrix;
			text.text = $"{matrix.m00:0.##}, {matrix.m01:0.##}, {matrix.m02:0.##}, {matrix.m03:0.##}\n";
            text.text = $"{matrix.m10:0.##}, {matrix.m11:0.##}, {matrix.m12:0.##}, {matrix.m13:0.##}\n";
            text.text = $"{matrix.m20:0.##}, {matrix.m21:0.##}, {matrix.m22:0.##}, {matrix.m23:0.##}\n";
            text.text = $"{matrix.m30:0.##}, {matrix.m31:0.##}, {matrix.m32:0.##}, {matrix.m33:0.##}\n";

        }

		public float GetDistanceToPlayer(float playerX, float playerY)
		{
			float a2 = Mathf.Pow(transform.position.y - playerY, 2);
			float b2 = Mathf.Pow(transform.position.x - playerX, 2);
            float distance = Mathf.Sqrt(a2 + b2);

			return distance;
		}

		public float GetDistanceToPlayer(Transform player)
		{
			float distance = Vector2.Distance(transform.position, player.position);
			return distance;
		}
    }
}
// [ ] - ) 
// [ ] - [ ] - ) 