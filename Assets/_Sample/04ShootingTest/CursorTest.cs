using UnityEngine;

/* [0] ���� : CursorTest
*/

namespace MyFPS
{
    public class CursorTest : MonoBehaviour
    {

        // [1] Start.
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
}