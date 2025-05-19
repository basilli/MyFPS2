using UnityEngine;
using UnityEngine.InputSystem;

/* [0] ���� : MouseLook
*/

namespace MyFPS
{

    public class MouseLook : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) ����.
        public Transform cameraTrans;       // ) ī�޶� ������Ʈ.
        // [ ] - 2) ���콺 �Է� ������.
        [SerializeField] private float sensivity = 100f;
        // [ ] - 3) ī�޶� ȸ��.
        private float rotateX;
        // ���콺 �Է�(��ġ) ��.
        private Vector2 inputLook;
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) ���콺 �������� �¿츦 �Է� �� OLD Input.
            // )        float mouseX = Input.GetAxis("Mouse X") * sensivity;
            // [ ] - [ ] - 2) �÷��̾ �¿�� ȸ����.
            this.transform.Rotate(Vector3.up * Time.deltaTime * inputLook.x * sensivity);
            // [ ] - [ ] - 3) ���콺 �������� ���Ʒ��� �Է� �� OLD Input.
            // )        float mouseY = Input.GetAxis("Mouse Y") * sensivity * -1;
            // [ ] - [ ] - 4) �÷��̾ ���Ʒ��� ȸ����.
            rotateX -= inputLook.y * Time.deltaTime * sensivity;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f);        // ) ���콺 Y�� ȸ����(���Ʒ� �ü�)�� ������.
            cameraTrans.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
        }
        #endregion Unity Event Method





        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) OnLook.
        public void OnLook(InputAction.CallbackContext context)
        {
            inputLook = context.ReadValue<Vector2>();
        }
        #endregion Custom Method
    }
}




// [ ] - ) 
// [ ] - [ ] - )