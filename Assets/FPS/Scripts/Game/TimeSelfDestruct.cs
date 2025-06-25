using UnityEngine;

/* [0] 개요 : TimeSelfDestruct
		- TimeSelfDestruct 컴포넌트가 있으면 생성 후 LeftTime이 지나면 자동으로 킬.
*/

namespace Unity.FPS.Game
{
    public class TimeSelfDestruct : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 123.
        public float lifeTime = 5f;       // ) .
        private float spawnTime;                          // ) 생성시간.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Awake.
        private void Awake()
        {
            // [◇] - [◆] - ) 생성되는 시간 저장.
            spawnTime = Time.time;
        }


        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) 789.
            if ((spawnTime + lifeTime) < Time.time)
            {
                Destroy(gameObject);
            }
        }

        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ 123.


        // [◆] - ▶▶▶ 456.


        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}