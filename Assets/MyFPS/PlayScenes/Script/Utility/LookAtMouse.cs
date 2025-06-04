using System.Drawing;
using UnityEngine;

/* [0] ���� : LookAtMouse
		- ������Ʈ�� ���콺 �����͸� �ٶ�.
*/
public class LookAtMouse : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) ���콺 �����Ͱ� ����Ű�� ���� �����ǰ�
    private Vector3 worldPosition;
    #endregion Variable





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) ���� �����ǰ� �������� �� Ray�� �̿�.
        worldPosition = ScreenToWorld();

        // [ ] - [ ] - 2) ���� ������ �� �ٶ󺸱�.
        transform.LookAt(worldPosition);
    }
    #endregion Unity Event Method





    // [3] Custom Method.
    #region Custom Method
    // [ ] - 1) ���� �����ǰ� �������� �� ���콺�� ��ġ���� �̿�.
    private Vector3 ScreenToWorld()
    {
        Vector3 worldPos = Vector3.zero;
        Vector3 mousePosition = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0.8f));
        return worldPos;
    }
    // [ ] - 2) ���� �����ǰ� �������� �� ���콺�� ��ġ�� ��� Ray�� �̿�.
    private Vector3 RayToWorld()
    {
        Vector3 worldPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }
        return worldPos;
    }
    #endregion Custom Method
}