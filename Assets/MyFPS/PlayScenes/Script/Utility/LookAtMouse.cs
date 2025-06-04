using System.Drawing;
using UnityEngine;

/* [0] 개요 : LookAtMouse
		- 오브젝트가 마우스 포인터를 바라봄.
*/
public class LookAtMouse : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) 마우스 포인터가 가리키는 월드 포지션값
    private Vector3 worldPosition;
    #endregion Variable





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) 월드 포지션값 가져오기 → Ray를 이용.
        worldPosition = ScreenToWorld();

        // [ ] - [ ] - 2) 월드 포지션 값 바라보기.
        transform.LookAt(worldPosition);
    }
    #endregion Unity Event Method





    // [3] Custom Method.
    #region Custom Method
    // [ ] - 1) 월드 포지션값 가져오기 → 마우스의 위치값을 이용.
    private Vector3 ScreenToWorld()
    {
        Vector3 worldPos = Vector3.zero;
        Vector3 mousePosition = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0.8f));
        return worldPos;
    }
    // [ ] - 2) 월드 포지션값 가져오기 → 마우스의 위치에 쏘는 Ray를 이용.
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