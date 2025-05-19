using UnityEngine;

/* [0] ���� : MoveObject
*/

public class MoveObject : MonoBehaviour
{
    // [1] Variables.
    #region Variables
    // [ ] - 1) ��ü(Rigid).
    private Rigidbody rb;
    // [ ] - 2) �̵� �� ������Ʈ�� �������� ��.
    [SerializeField] private float movePower = 3f;
    // [ ] - 3) ������Ʈ �÷� �ٲٱ�.
    private Renderer render;
    private Color originColor;
    #endregion Variables





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) Rigidbody ����.
        rb = this.gameObject.GetComponent<Rigidbody>();
        render = this.GetComponent<Renderer>();
        // [ ] - [ ] - 2) �ʱ�ȭ.
        originColor = render.material.color;
        // [ ] - [ ] - 3) ������ �� ���������� ��(3f)�� ����.
        MoveRight();
    }
    #endregion Unity Event Method





    // [3] Custyom Method.
    #region Custyom Method
    // [ ] - 1) MoveLeft �� �÷��̾�� �������� 3��ŭ ���� ����.
    public void MoveLeft()
    {
        rb.AddForce(Vector3.left * movePower, ForceMode.Impulse);
    }

    // [ ] - 2) MoveRight �� �÷��̾�� ���������� 3��ŭ ���� ����.
    public void MoveRight()
    {
        rb.AddForce(Vector3.right * movePower, ForceMode.Impulse);
    }

    // [ ] - 3) ChangeColorRed �� ���������� �ٲ�.
    public void ChangeColorRed()
    {
        render.material.color = Color.red;
    }

    // [ ] - 4) ChangeColorOrigin �� �������� �÷��� �ٲ�.
    public void ChangeColorOrigin()
    {
            render.material.color = Color.red;
    }
    #endregion Custyom originColor
}




// [ ] - ) 
// [ ] - [ ] - ) 



