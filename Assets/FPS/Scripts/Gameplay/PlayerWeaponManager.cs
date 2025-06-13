using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.Events;

/* [0] 개요 : PlayerWeaponManager
		- 플레이어가 가진 무기들을 관리하는 클래스.
*/

namespace Unity.FPS.Gameplay
{
    // [◆] - ▶▶▶ 무기 교체 상태.
    public enum WeaponSwitchState
    { 
        Up,       // ) 무기가 액티브해서 들려져 있는 상태.
        Down,       // ) 무기가 내려져있는 상태.
        PutDownPrevious,         // ) 무기가 내리기 이전 상태.
        PutUpNew,       // ) 새로 올리는 상태.
    }

    public class PlayerWeaponManager : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private PlayerInputHandler inputHandler;


        // [◆] - ▶▶▶ 무기 3개(Prefab) 지급.
        public List<WeaponController> StartingWeapons = new List<WeaponController>();       // ) GameObject로 총을 가져오는 것이 아닌 WeaponController로 일괄적으로 받을 수 있음.
        public Transform weaponParentSocket;       // ) 무기장착 → 무기가 장착이 되는 오브젝트.
        private WeaponController[] weaponSlots = new WeaponController[9];       // ) 플레이어가 게임중에 들고다니는 무기 리스트.

        private Vector3 weaponMainLocalPosition;        // ) 무기 위치 정보.


        // [◆] - ▶▶▶ 무기 교체.
        public UnityAction<WeaponController> OnSwitchToWeapon;      // ) 무기 교체시 호출되는 UnityAction 함수.
        public Transform defaultWeaponPosition;             // ) 무기 교체.
        public Transform downWeaponPosition;        // ) 무기 교체시 계산되는 위치.
        private WeaponSwitchState weaponSwitchState;        // ) 무기 교체 상태.

        private int weaponSwitchNewWeaponIndex;         // ) 새로 교체할 무기 인덱스.

        // [◆] - ▶▶▶ 무기 연출.
        private float weaponSwitchTimeStarted = 0f;         // ) 연출 시작 시간.
        private float weaponSwitchDelay = 1f;       // ) 연출 플레이 시간.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ 무기 리스트(WeaponSlots)를 관리하는 인덱스 → 현재 액티브한 무기의 인덱스.
        public int ActiveWeaponIndex { get; set; }
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 참조.
            inputHandler = this.GetComponent<PlayerInputHandler>();
            // [◇] - [◆] - ) 초기화.
            ActiveWeaponIndex = -1;         // ) 장착된 무기가 없음을 뜻함.
            weaponSwitchState = WeaponSwitchState.Down;
            // [◇] - [◆] - ) 무기교체시 호출될 함수 등록.
            OnSwitchToWeapon += OnWeaponSwitched;
            // [◇] - [◆] - ) 처음에 지급받은 무기를 장착함.
            foreach (var w in StartingWeapons)
            {
                // [◇] - [◇] - [◆] ) 무기 리스트 추가.
                AddWeapon(w);
            }
            // [◇] - [◆] - ) 무기 교체.
            SwitchWeapon(true);
        }


        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) .
        }


        // [◆] - ▶▶▶ LateUpdate.
        private void LateUpdate()
        {
            // [◇] - [◆] - ) 무기 교체 연출.
            UpdateWeaponState();
            // [◇] - [◆] - ) 무기 최종 위치에 적용.
            weaponParentSocket.localPosition = weaponMainLocalPosition;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ 무기 교체 연출 및 상태 변경 구현.
        private void UpdateWeaponState()
        {
            // [◇] - [◆] - ) Lerp T변수.
            float switchingTimeFactor = 0f;
            // [◇] - [◆] - ) 연출없이 바로 변경.
            if (weaponSwitchDelay <= 0f)
            {
                switchingTimeFactor = 1f;
            }
            else
            {
                switchingTimeFactor = Mathf.Clamp01((Time.time - weaponSwitchTimeStarted) / weaponSwitchDelay);
            }
            // [◇] - [◆] - ) 타이머가 완료되었을 때 연출 완료하고 상태변경.
            if (switchingTimeFactor >= 1f)
            {
                // [◇] - [◇] - [◆] ) 디폴트 위치에서 아래 위치로 이동 완료한 상태.
                if (weaponSwitchState == WeaponSwitchState.PutDownPrevious)
                {
                    // [◇] - [◇] - [◇] - [◆] ) 무기 교체 → 이전무기는 false, 새로운 무기는 true.
                    WeaponController oldWeapon = GetWeaponAtSlotIndex(ActiveWeaponIndex);
                    if (oldWeapon != null)
                    {
                        oldWeapon.ShowWeapon(false);
                    }
                    // [◇] - [◇] - [◆] ) 새로운 무기 인덱스를 액티브 인덱스로 저장.
                    ActiveWeaponIndex = weaponSwitchNewWeaponIndex;
                    // [◇] - [◇] - [◆] ) 액티브 인덱스에 해당되는 무기(weaponController)로 가져오기.
                    WeaponController newWeapon = GetWeaponAtSlotIndex(ActiveWeaponIndex);
                    // [◇] - [◇] - [◆] ) 액티브 무기(weaponController)를 매개변수로 등록된 함수를 호출.
                    OnSwitchToWeapon?.Invoke(newWeapon);

                    switchingTimeFactor = 0f;
                    if (newWeapon != null) // 새로운 무기가 있으면 연출 시작.
                    {
                        weaponSwitchTimeStarted = Time.time;
                        weaponSwitchState = WeaponSwitchState.PutUpNew;
                    }
                    else // 새로운 무기가 없음.
                    {
                        weaponSwitchState = WeaponSwitchState.Down;
                    }

                }
                // 아래 위치에서 디폴트 위치로 이동 완료.
                else if (weaponSwitchState == WeaponSwitchState.PutUpNew)
                { 
                }
            }
            // [◇] - [◆] - ) 0에서 1로 가고 있음 → 무기의 위치이동을 연출 중.
            else 
            {
                if (weaponSwitchState == WeaponSwitchState.PutDownPrevious)
                {
                    weaponMainLocalPosition = Vector3.Lerp(defaultWeaponPosition.localPosition, downWeaponPosition.localPosition, switchingTimeFactor);
                }
                else if (weaponSwitchState == WeaponSwitchState.PutUpNew)
                {
                    weaponMainLocalPosition = Vector3.Lerp(downWeaponPosition.localPosition, defaultWeaponPosition.localPosition, switchingTimeFactor);
                }
            }
        }


        // [◆] - ▶▶▶ 매개변수로 받은 무기(WeaponController Prefab)를 무기 리스트에 추가.
        private bool AddWeapon(WeaponController weaponPrefab)
        {
            // [◇] - [◆] - ) 새로 추기하는 무기를 소지하였는지 중복검사.
            if (HasWeapon(weaponPrefab) != null)
            {
                Debug.Log("Has Same Weapon");
                return false;
            }
            // [◇] - [◆] - ) weaponSlots의 배열을 처리하기 위한반복문.
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                // [◇] - [◇] - [◆] ) 빈 슬롯 찾기.
                if (weaponSlots[i] == null)
                {
                    WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
                    weaponInstance.transform.localPosition = Vector3.zero;      // ) ParentSocket와 동일한 위치에 배치됨.
                    weaponInstance.transform.localRotation = Quaternion.identity;
                    weaponInstance.Owner = this.gameObject;
                    weaponInstance.SourcePrefab = weaponPrefab.gameObject;
                    weaponInstance.ShowWeapon(false);
                    weaponSlots[i] = weaponInstance;
                    return true;
                }
            }
            Debug.Log("WeaponSlots Full");
            return false;       // ) 아이템창이 다 찼을 경우, 아이템을 추가적으로 먹을 수 없음.
        }


        // [◆] - ▶▶▶ 매개변수로 받은 프리팹으로 생성된 무기가 있으면 반환시킴.
        private WeaponController HasWeapon(WeaponController weaponPrefab)
        {
            foreach (var w in weaponSlots)
            {
                if (w != null && w.SourcePrefab == weaponPrefab.gameObject)
                {
                    return w;
                }
            }
            return null;
        }


        // [◆] - ▶▶▶ 현재 액티브한 무기 가져오기.
        public WeaponController GetActiveWeapon()
        {
            return GetWeaponAtSlotIndex(ActiveWeaponIndex);
        }


        // [◆] - ▶▶▶ 지정 인덱스의 무기 가져오기.
        private WeaponController GetWeaponAtSlotIndex(int index)
        {
            if (index < 0 || index >= weaponSlots.Length)
                return null;
            return weaponSlots[index];
        }


        // [◆] - ▶▶▶ 현재 들고 있는 무기는 false, 새로운 무기는 true.
        private void SwitchWeapon(bool acendingOrder)       // ) acendingOrder는 다음 무기를 가져오는 기준이 오름차순인지 내림차순인지 
        {
            // [◇] - [◆] - ) 새로운 무기의 인덱스.
            int newWeaponIndex = -1;
            // [◇] - [◆] - ) 현재 액티브한 무기와 가장 가까운 무기 찾기.
            int closestSlotDistance = weaponSlots.Length;
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                if (i != ActiveWeaponIndex && GetWeaponAtSlotIndex(i) != null)
                {
                    int distanceToActiveIndex = GetDistanceBetweenWeaponSlots(ActiveWeaponIndex, i, acendingOrder);
                    if (distanceToActiveIndex < closestSlotDistance)
                    {
                        closestSlotDistance = distanceToActiveIndex;
                        newWeaponIndex = i;
                    }
                }
            }
            // [◇] - [◆] - ) 새로운 무기의 인덱스로 무기를 교체.
            SwitchToWeaponIndex(newWeaponIndex);
        }


        // [◆] - ▶▶▶ 매개변수로 받은 무기로 교체.
        private void SwitchToWeaponIndex(int newWeaponIndex)
        {
            // [◇] - [◆] - ) .
            if (newWeaponIndex == ActiveWeaponIndex)
                return;
            // [◇] - [◆] - ) .
            if (newWeaponIndex < 0 || newWeaponIndex >= weaponSlots.Length)
                return;
            // [◇] - [◆] - ) .
            weaponSwitchNewWeaponIndex = newWeaponIndex;        // ) .
            weaponSwitchTimeStarted = Time.time;        // ) 연출 시작 시간을 저장.
            // [◇] - [◆] - ) .
            if (GetActiveWeapon() == null)
            {
                // [◇] - [◇] - [◆] ) 무기위치를 아래 위치에 가져다 놓음.
                weaponMainLocalPosition = downWeaponPosition.localPosition;
                // [◇] - [◇] - [◆] ) 올리는 상태로 변경.
                weaponSwitchState = WeaponSwitchState.PutUpNew;
                // [◇] - [◇] - [◆] ) 새로운 무기 인덱스를 액티브 인덱스로 저장.
                ActiveWeaponIndex = newWeaponIndex;
                // [◇] - [◇] - [◆] ) 액티브 인덱스에 해당되는 무기(weaponController)로 가져오기.
                WeaponController weaponController = GetWeaponAtSlotIndex(ActiveWeaponIndex);
                // [◇] - [◇] - [◆] ) 액티브 무기(weaponController)를 매개변수로 등록된 함수를 호출.
                OnSwitchToWeapon?.Invoke(weaponController);
            }
            else
            {
                weaponSwitchState = WeaponSwitchState.PutDownPrevious;
            }
        }


        // [◆] - ▶▶▶ 무기 슬롯간의 거리 구하기.
        private int GetDistanceBetweenWeaponSlots(int fromSlotIndex, int toSlotIndex, bool ascendingOrder)
        {
            int distance = 0;
            if (ascendingOrder == true)
            {
                distance = toSlotIndex - fromSlotIndex;
            }
            else
            {
                distance = -1*(toSlotIndex - fromSlotIndex);
            }
            if (distance < 0)
            {
                distance = distance + weaponSlots.Length;
            }
            return distance;
        }


        // [◆] - ▶▶▶ 매개변수로 받은 무기로 활성화.
        private void OnWeaponSwitched(WeaponController newWeapon)
        {
            // [◇] - [◆] - ) 789.
            if (newWeapon != null)
            {
                newWeapon.ShowWeapon(true);
            }
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}