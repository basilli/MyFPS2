using UnityEngine;
using System;

/* [0] 개요 : PlayData
		- 파일에 저장할 게임 플에이 데이터 목록.
*/

namespace MyFPS
{
    [Serializable]
    public class PlayData
    {
        public int sceneNumber;
        public int ammoCount;
        public float playerHealth;

        // ETC.

        // 생성자 → 멤버변수의 초기화.
        public PlayData()
        {
            sceneNumber = PlayerDataManager.Instance.SceneNumber;
            ammoCount = PlayerDataManager.Instance.AmmoCount;
            playerHealth = PlayerDataManager.Instance.PlayerHealth;

            //etc...
        }
    }
}
