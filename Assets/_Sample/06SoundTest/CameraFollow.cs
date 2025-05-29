using UnityEngine;

/* [0] 개요 : CameraFollow
		- 플레이어를 offset 위치에서 쫒아감.
*/

namespace MySample
{
    public class CameraFollow : MonoBehaviour
    {

        // [1] Variable.
        #region Variable
        public Transform thePlayer;
        public Vector3 offset;
        #endregion Variable





        // [2] Property.
        #region Property
        #endregion Property





        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) LateUpdate.
        private void LateUpdate()
        {
            // [ ] - [ ] - 1) 목표 위치 계산.
            transform.position = thePlayer.position + offset;
        }
        #endregion Unity Event Method





        // [4] Custom Method.
        #region Custom Method
        #endregion Custom Method
    }
}

// [ ] - ) 
// [ ] - [ ] - ) 