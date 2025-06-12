using UnityEngine;
using System;

/* [0] ���� : PlayData
		- ���Ͽ� ������ ���� �ÿ��� ������ ���.
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

        // ������ �� ��������� �ʱ�ȭ.
        public PlayData()
        {
            sceneNumber = PlayerDataManager.Instance.SceneNumber;
            ammoCount = PlayerDataManager.Instance.AmmoCount;
            playerHealth = PlayerDataManager.Instance.PlayerHealth;

            //etc...
        }
    }
}
