using UnityEngine;

/* [0] 개요 : MoveObject
*/

public class MoveObject : MonoBehaviour
{
    // [1] Variables.
    #region Variables
    // [ ] - 1) 강체(Rigid).
    private Rigidbody rb;
    // [ ] - 2) 이동 → 오브젝트에 가해지는 힘.
    [SerializeField] private float movePower = 3f;
    // [ ] - 3) 오브젝트 컬러 바꾸기.
    private Renderer render;
    private Color originColor;
    #endregion Variables





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) Rigidbody 참조.
        rb = this.gameObject.GetComponent<Rigidbody>();
        render = this.GetComponent<Renderer>();
        // [ ] - [ ] - 2) 초기화.
        originColor = render.material.color;
        // [ ] - [ ] - 3) 시작할 때 오른쪽으로 힘(3f)을 가함.
        MoveRight();
    }
    #endregion Unity Event Method





    // [3] Custyom Method.
    #region Custyom Method
    // [ ] - 1) MoveLeft → 플레이어에게 왼쪽으로 3만큼 힘을 가함.
    public void MoveLeft()
    {
        rb.AddForce(Vector3.left * movePower, ForceMode.Impulse);
    }

    // [ ] - 2) MoveRight → 플레이어에게 오른쪽으로 3만큼 힘을 가함.
    public void MoveRight()
    {
        rb.AddForce(Vector3.right * movePower, ForceMode.Impulse);
    }

    // [ ] - 3) ChangeColorRed → 빨간색으로 바꿈.
    public void ChangeColorRed()
    {
        render.material.color = Color.red;
    }

    // [ ] - 4) ChangeColorOrigin → 오리지널 컬러로 바꿈.
    public void ChangeColorOrigin()
    {
            render.material.color = Color.red;
    }
    #endregion Custyom originColor
}




// [ ] - ) 
// [ ] - [ ] - ) 



