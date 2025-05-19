using UnityEngine;
using UnityEngine.InputSystem;

/* [0] 개요 : MouseLook
*/

namespace MyFPS
{

    public class MouseLook : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 참조.
        public Transform cameraTrans;       // ) 카메라 오브젝트.
        // [ ] - 2) 마우스 입력 보정값.
        [SerializeField] private float sensivity = 100f;
        // [ ] - 3) 카메라 회전.
        private float rotateX;
        // 마우스 입력(위치) 값.
        private Vector2 inputLook;
        #endregion Variables





        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 마우스 포지션의 좌우를 입력 → OLD Input.
            // )        float mouseX = Input.GetAxis("Mouse X") * sensivity;
            // [ ] - [ ] - 2) 플레이어를 좌우로 회전함.
            this.transform.Rotate(Vector3.up * Time.deltaTime * inputLook.x * sensivity);
            // [ ] - [ ] - 3) 마우스 포지션의 위아래를 입력 → OLD Input.
            // )        float mouseY = Input.GetAxis("Mouse Y") * sensivity * -1;
            // [ ] - [ ] - 4) 플레이어를 위아래로 회전함.
            rotateX -= inputLook.y * Time.deltaTime * sensivity;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f);        // ) 마우스 Y축 회전값(위아래 시선)을 제한함.
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