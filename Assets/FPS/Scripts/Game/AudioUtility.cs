using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

/* [0] 개요 : AudioUtility
		- 오디오 플레이 관련 클래스.
		- 
		- 
		- 
		- 
*/

namespace Unity.FPS.Game
{
    public class AudioUtility
    {
        // [] ETC.
        #region ▼▼▼▼▼ ETC ▼▼▼▼▼
        // [◆] - ▶▶▶ CreateSFX → 게임 오브젝트 생성하여 지정하는 효과음을 플레이하는 함수.
        public static void CreateSFX(AudioClip clip, Vector3 point, float spatialBlend, float rolloffDistanceMin = 1f)
        {
            GameObject impactSfxInstance = new GameObject();                                    // ) Hierarchy창에서 빈 오브젝트 만들기.
            impactSfxInstance.transform.position = point;                                             // ) 위치지정.
            AudioSource source = impactSfxInstance.AddComponent<AudioSource>();         // ) 새로 생성한 게임 오브젝트에 AudioSource 컴포넌트를 추가.
            source.clip = clip;                                                                              // ) 플레이 할 오디오 클립.
            source.spatialBlend = spatialBlend;                                                         // ) 3D 사운드 효과 설정.
            source.minDistance = rolloffDistanceMin;                                                  // ) 3D 사운드 효과의 최소 거리.
            source.Play();


            // [◆] - ▶▶▶ 사운드 플레이 후 자동 제거.
            TimeSelfDestruct timeSelfDestruct = impactSfxInstance.AddComponent<TimeSelfDestruct>();
            timeSelfDestruct.lifeTime = clip.length;
        }

        // [◆] - ▶▶▶ 456.


        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ ETC ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ 123.


        // [◆] - ▶▶▶ 456.


        // [◇] - [◆] - ) 789.
        // [◇] - [◇] - [◆] ) 147.
        // [◇] - [◇] - [◇] - [◆] ) 258.
        // [◇] - [◇] - [◇] - [◇] - [◆] ) 369.
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ 123.


        // [◆] - ▶▶▶ 456.


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