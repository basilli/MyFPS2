using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
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
        Up,                          // ) 무기가 액티브해서 들려져 있는 상태.
        Down,                      // ) 무기가 내려져있는 상태.
        PutDownPrevious,        // ) 무기가 내리기 이전 상태.
        PutUpNew,                // ) 새로 올리는 상태.
    }

    public class PlayerWeaponManager : MonoBehaviour
    {
        // [1] Variable.
        #region ▼▼▼▼▼ Variable ▼▼▼▼▼
        // [◆] - ▶▶▶ 참조.
        private PlayerInputHandler inputHandler;
        private PlayerCharacterController playerCharacterController;         // ) PlayerCharacterController 스크립트 가져오기.


        // [◆] - ▶▶▶ 무기 3개(Prefab) 지급.
        public List<WeaponController> StartingWeapons = new List<WeaponController>();       // ) GameObject로 총을 가져오는 것이 아닌 WeaponController로 일괄적으로 받을 수 있음.
        public Transform weaponParentSocket;                                                            // ) 무기장착 → 무기가 장착이 되는 오브젝트.
        private WeaponController[] weaponSlots = new WeaponController[9];                       // ) 플레이어가 게임중에 들고다니는 무기 리스트.
        private Vector3 weaponMainLocalPosition;                                                        // ) 무기 위치 정보.


        // [◆] - ▶▶▶ 무기 교체.
        public UnityAction<WeaponController> OnSwitchToWeapon;              // ) 무기 교체시 호출되는 UnityAction 함수.
        public UnityAction<WeaponController, int> OnAddedWeapon;            // ) .
        public UnityAction<WeaponController, int> OnRemovedWeapon;         // ) 무기 교체시 호출되는 UnityAction 함수.
        public Transform defaultWeaponPosition;                                     // ) 무기 교체시 기본 위치.
        public Transform downWeaponPosition;                                       // ) 무기 상태에 따른 교체시 계산되는 위치.
        public Transform AimingWeaponPosition;                                     // ) 무기 상태에 따른 Aim이 계산되는 위치. 
        public WeaponSwitchState weaponSwitchState;                              // ) 무기 교체 상태.
        private int weaponSwitchNewWeaponIndex;                                 // ) 새로 교체할 무기 인덱스.


        // [◆] - ▶▶▶ 무기 연출.
        private float weaponSwitchTimeStarted = 0f;                          // ) 연출 시작 시간.
        [SerializeField] private float weaponSwitchDelay = 1f;                // ) 연출 플레이 시간.


        // [◆] - ▶▶▶ 적 포착.
        public Camera weaponCamera;


        // [◆] - ▶▶▶ 카메라 FoV(Field of View).
        [SerializeField] private float defaultFov = 60f;                  // ) Fov의 기본값.
        [SerializeField] private float weaponFovMultiplier = 1f;       // ) 무기에 따른 FoV 줌 계수.


        // [◆] - ▶▶▶ 조준 Aim.
        [SerializeField] private float aimingAnimationSpeed = 10f;        // ) 조준 연출 이동속도.
        private float aimingFov;                                                 // ) 조준 연출 FoV 값.


        // [◆] - ▶▶▶ 흔들림(Bob).
        [SerializeField] private float bobFrequency = 10f;                // ) 무기의 흔들림 속도.
        [SerializeField] private float bobSharpness = 10f;                // ) Bob Factor을 구하는 Lerp 계수 속도.
        [SerializeField] private float defaultBobAmount = 0.05f;        // ) 흔들림의 기본값.
        [SerializeField] private float aimingBobAmount = 0.02f;        // ) 조준 시 발생하는 흔들림 값.
        private float m_WeaponBobFactor;                                // ) 이동속도(매 프레임)에 따른 흔들림 계수.
        private Vector3 m_LastCharacterPosition;                         // ) 이번 프레임의 캐릭터 최종위치.
        private Vector3 m_WeaponBobLocalPosition;                    // ) 이번 프레임에 흔들린 량의 최종 계산값.


        // [◆] - ▶▶▶ ) 반동(Recoil).
        [SerializeField] private float recoilSharpness = 50f;                    // ) 반동 연출 뒤로 밀리는 속도.
        [SerializeField] private float maxRecoilDistance = 0.5f;                // ) 반동시 뒤로 밀리는 최대거리.
        [SerializeField] private float recoilRepositionSharpness = 10f;       // ) 반동 연출 회복 속도. 
        private Vector3 accumulateRecoil;                                       // ) 반동 힘에 의한 이동값(Vector3).
        private Vector3 WeaponRecoilLocalPosition;                           // ) 반동에 의해 이동한 최종 계산값.


        // [◆] - ▶▶▶ ) 저격 모드(Sniper).
        private bool isScopeOn = false;
        public UnityAction OnScopedWeapon;         // ) 저격모드 시작시 등록된 함수를 호출하는 이벤트 함수.
        public UnityAction OffScopedWeapon;         // ) 저격모드 해제시 등록된 함수를 호출하는 이벤트 함수.
        #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





        // [2] Property.
        #region ▼▼▼▼▼ Property ▼▼▼▼▼
        // [◆] - ▶▶▶ 무기 리스트(WeaponSlots)를 관리하는 인덱스 → 현재 액티브한 무기의 인덱스.
        public int ActiveWeaponIndex { get; set; }


        // [◆] - ▶▶▶ 적 포착 체크.
        public bool IsPointingAtEnemy { get; private set; }


        // [◆] - ▶▶▶ 조준 여부 체크.
        public bool IsAiming { get; private set; }
        #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





        // [3] Unity Event Method.
        #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
        // [◆] - ▶▶▶ Start.
        private void Start()
        {
            // [◇] - [◆] - ) 참조.
            inputHandler = this.GetComponent<PlayerInputHandler>();
            playerCharacterController = this.GetComponent<PlayerCharacterController>(); //?
            // [◇] - [◆] - ) 초기화.
            ActiveWeaponIndex = -1;         // ) 장착된 무기가 없음을 뜻함.
            weaponSwitchState = WeaponSwitchState.Down;
            SetFov(defaultFov);
            // [◇] - [◆] - ) 무기교체시 호출될 함수 등록.
            OnSwitchToWeapon += OnWeaponSwitched;
            // [◇] - [◆] - ) 저격모드 ON/OFF시 호출될 함수 등록.
            OnScopedWeapon += OnScope;
            OffScopedWeapon += OffScope;
            // [◇] - [◆] - ) 처음에 지급받은 무기를 장착함.
            foreach (var w in StartingWeapons)
            {
                // [◇] - [◇] - [◆] ) 무기 리스트 추가.
                AddWeapon(w);
            }
            // [◇] - [◆] - ) 무기 교체시 호출 될 함수 등록.
            SwitchWeapon(true);
        }


        // [◆] - ▶▶▶ Update.
        private void Update()
        {
            // [◇] - [◆] - ) 현재 액티브 무기 가져오기.
            WeaponController activeWeapon = GetActiveWeapon();
            if (weaponSwitchState == WeaponSwitchState.Up)
            {
                // [◇] - [◇] - [◆] - ) 키 인풋을 받아서 조준.
                IsAiming = inputHandler.GetAimInputHeld();
                // [◇] - [◇] - [◆] - ) .
                if (activeWeapon.ShootType == WeaponShootType.Sniper)
                {
                    if (inputHandler.GetAimInputDown())
                    {
                        isScopeOn = true;       // ) 저격모드 체크.
                    }
                    else if (inputHandler.GetAimInputReleased())
                    {
                        OffScopedWeapon?.Invoke();      // ) 저격모드 해제.
                    }
                }
                // [◇] - [◇] - [◆] - ) 발사.
                bool isFire = activeWeapon.HandleShootInput(inputHandler.GetFireInputDown(), inputHandler.GetFireInputHeld(), inputHandler.GetFireInputReleased());
                // [◇] - [◇] - [◆] - ) 발사 성공 시 총이 뒤로 밀림.
                if (isFire)
                {
                    accumulateRecoil += Vector3.back * activeWeapon.recoilForce;
                    accumulateRecoil = Vector3.ClampMagnitude(accumulateRecoil, maxRecoilDistance);
                }
            }
            // [◇] - [◆] - ) 키 인풋을 받아 무기를 교체.
            if (weaponSwitchState == WeaponSwitchState.Up || weaponSwitchState == WeaponSwitchState.Down)
            {
                int switchWeaponInput = inputHandler.GetSwitchWeaponInput();
                if (switchWeaponInput != 0)
                {
                    // ) Debug.Log($"switchWeaponInput:{switchWeaponInput}");
                    bool switchUp = switchWeaponInput > 0f;
                    // [◇] - [◇] - [◆] - ) 무기 교체.
                    SwitchWeapon(switchUp);
                }
            }
            // [◇] - [◆] - ) 적 포착.
            IsPointingAtEnemy = false;
            if (activeWeapon)
            {
                if (Physics.Raycast(weaponCamera.transform.position, weaponCamera.transform.forward, out RaycastHit hit, 1000))
                {
                    // [◇] - [◇] - [◆] - ) 충돌체 중에서 적을 판정.
                    if (hit.collider.GetComponentInParent<Health>() != null)
                    {
                        IsPointingAtEnemy = true;
                    }
                }
            }
        }


        // [◆] - ▶▶▶ LateUpdate.
        private void LateUpdate()
        {
            // [◇] - [◆] - ) 반동 효과 연출.
            UpdateWeaponRecoil();
            // [◇] - [◆] - ) 무기 조준 연출.
            UpdateWeaponAiming();
            // [◇] - [◆] - ) 무기 흔들림량 구하기.
            UpdateWeaponBob();
            // [◇] - [◆] - ) 무기 교체 연출.
            UpdateWeaponState();
            // [◇] - [◆] - ) 무기 최종 위치에 적용.
            weaponParentSocket.localPosition = weaponMainLocalPosition + m_WeaponBobLocalPosition + WeaponRecoilLocalPosition;
        }
        #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





        // [4] Custom Method.
        #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
        // [◆] - ▶▶▶ 카메라 FoV 조정.
        private void SetFov(float fov)
        {
            playerCharacterController.PlayerCamera.fieldOfView = fov;
            weaponCamera.fieldOfView = fov * weaponFovMultiplier;
        }


        // [◆] - ▶▶▶ 조준 연출에 따른 무기 위치 변경.
        private void UpdateWeaponAiming()
        {
            // [◇] - [◆] - ) .
            if (weaponSwitchState != WeaponSwitchState.Up)
                return;
            // [◇] - [◆] - ) .
            WeaponController activeWeapon = GetActiveWeapon();
            // [◇] - [◆] - ) 조준 모드 ON.
            if (IsAiming && activeWeapon)
            {
                // [◇] - [◇] - [◆] - ) .
                if (isScopeOn)
                {
                    // [◇] - [◇] - [◇] - [◆] - ) 거리 측정 후 isScopeOn = false;.
                    float distance = Vector3.Distance(weaponMainLocalPosition, AimingWeaponPosition.localPosition + activeWeapon.aimOffset);
                    if (distance < 0.05f)
                    {
                        isScopeOn = false;                   // ) 저격모드 시작.
                        OnScopedWeapon?.Invoke();       // ) Scope UI 활성화 및 무기가 안보이도록 만들기.
                    }
                }
                // [◇] - [◇] - [◆] - ) 위치조정.
                weaponMainLocalPosition = Vector3.Lerp(weaponMainLocalPosition, AimingWeaponPosition.localPosition + activeWeapon.aimOffset, aimingAnimationSpeed * Time.deltaTime);
                // [◇] - [◇] - [◆] - ) FoV 조정.
                if (isScopeOn == false)
                {
                    aimingFov = Mathf.Lerp(playerCharacterController.PlayerCamera.fieldOfView, defaultFov * activeWeapon.aimZoomRatio, aimingAnimationSpeed * Time.deltaTime);
                    SetFov(aimingFov);
                }
            }
            // [◇] - [◆] - ) 조준 모드 OFF.
            else
            {
                // [◇] - [◇] - [◆] - ) 위치 고정.
                weaponMainLocalPosition = Vector3.Lerp(weaponMainLocalPosition, defaultWeaponPosition.localPosition, aimingAnimationSpeed * Time.deltaTime);
                // [◇] - [◇] - [◆] - ) FoV 조정.
                aimingFov = Mathf.Lerp(playerCharacterController.PlayerCamera.fieldOfView, defaultFov, aimingAnimationSpeed * Time.deltaTime);
                SetFov(aimingFov);
            }
            // ) float fov = playerCharacterController.PlayerCamera.fieldOfView;


            // [◇] - [◆] - ) 조준 모드.
            if (IsAiming && activeWeapon) 
            {
                weaponMainLocalPosition = Vector3.Lerp(weaponMainLocalPosition, AimingWeaponPosition.localPosition + activeWeapon.aimOffset, aimingAnimationSpeed*Time.deltaTime);      // ) 위치조정.
                aimingFov = Mathf.Lerp(playerCharacterController.PlayerCamera.fieldOfView, defaultFov * activeWeapon.aimZoomRatio, aimingAnimationSpeed * Time.deltaTime);                     // ) FoV 조정.
                SetFov(aimingFov);
            }
            // [◇] - [◆] - ) 조준 모드 해제.
            else
            {
                weaponMainLocalPosition = Vector3.Lerp(weaponMainLocalPosition, defaultWeaponPosition.localPosition, aimingAnimationSpeed * Time.deltaTime);      // ) 위치조정.
                aimingFov = Mathf.Lerp(playerCharacterController.PlayerCamera.fieldOfView, defaultFov, aimingAnimationSpeed * Time.deltaTime);                            // ) FoV 조정.
                SetFov(aimingFov);
            }
        }


        // [◆] - ▶▶▶ 반동 연출에 따른 무기의 뒤로 밀린 량 구하기.
        private void UpdateWeaponRecoil()
        {
            // accumulateRecoil : 힘에 의해 뒤로 밀린 량.
            // weaponRecoilLocalPosition : 연출을 위한 뒤로 밀린 량.
            // [◇] - [◆] - ) 뒤로 밀리는 연출.
            if (WeaponRecoilLocalPosition.z >= accumulateRecoil.z * 0.99f) 
            {
                WeaponRecoilLocalPosition = Vector3.Lerp(WeaponRecoilLocalPosition, accumulateRecoil, recoilSharpness * Time.deltaTime);
            }
            // [◇] - [◆] - ) 원래 위치로 회복하는 연출.
            else
            {
                WeaponRecoilLocalPosition = Vector3.Lerp(WeaponRecoilLocalPosition, Vector3.zero, recoilRepositionSharpness * Time.deltaTime);
                accumulateRecoil = WeaponRecoilLocalPosition;
            }
        }


        // [◆] - ▶▶▶ 이동에 따른 무기 흔들림량 구하기.
        private void UpdateWeaponBob()
        {
            // [◇] - [◆] - ) .
            if (Time.deltaTime > 0)
            {
                // [◇] - [◇] - [◆] - ) 플레이어의 이동속도 = 이번 프레임에 이동한 거리 / 시간.
                Vector3 playerCharacterVelocity = (playerCharacterController.transform.position - m_LastCharacterPosition) / Time.deltaTime;
                // [◇] - [◇] - [◆] - ) 게임에서 캐릭터의 이동속도 계수 (0~1) → 정지하면 0, 최대 이동속도는 1.
                float charactorMovementFactor = 0f;
                // [◇] - [◇] - [◆] - ) 게임에서 캐릭터의 이동속도 계수 (0~1) → 공중에서는 0.
                if (playerCharacterController.IsGrounded)
                {
                    charactorMovementFactor = Mathf.Clamp01(playerCharacterVelocity.magnitude / (playerCharacterController.MaxSpeedOnGround*playerCharacterController.SprintSpeedModifier));
                }
                // [◇] - [◇] - [◆] - ) .
                m_WeaponBobFactor = Mathf.Lerp(m_WeaponBobFactor, charactorMovementFactor, bobSharpness * Time.deltaTime);
                // [◇] - [◇] - [◆] - ) Bob Factor에 따른 흔들림량과 흔들림 속도로 m_WeaponBobPosition를 구하기.
                float bobAmount = IsAiming ? aimingBobAmount : defaultBobAmount;
                float frequency = bobFrequency;
                // [◇] - [◇] - [◆] - ) 좌우 이동량.
                float hBobValue = Mathf.Sin(Time.time * frequency) * bobAmount * m_WeaponBobFactor;
                // [◇] - [◇] - [◆] - ) 상하 이동량.
                float vBobValue = (Mathf.Sin(Time.time * frequency * 2) *0.5f + 0.5f) * bobAmount * m_WeaponBobFactor;
                // [◇] - [◇] - [◆] - ) 흔들림의 최종위치 적용.
                m_WeaponBobLocalPosition.x = hBobValue;
                m_WeaponBobLocalPosition.y = vBobValue;
                // [◇] - [◇] - [◆] - ) 플레이 최종 위치 저장.
                m_LastCharacterPosition = playerCharacterController.transform.position;
            }
        }


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
                    weaponSwitchState = WeaponSwitchState.Up;
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
                    WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);        // ) .
                    weaponInstance.transform.localPosition = Vector3.zero;                                              // ) ParentSocket와 동일한 위치에 배치됨.
                    weaponInstance.transform.localRotation = Quaternion.identity;
                    weaponInstance.Owner = this.gameObject;
                    weaponInstance.SourcePrefab = weaponPrefab.gameObject;
                    weaponInstance.ShowWeapon(false);
                    weaponSlots[i] = weaponInstance;                                                                       // ) 슬롯에 무기 추가.
                    OnAddedWeapon?.Invoke(weaponInstance, i);                                                         // ) 무기 추가와 관련되어 있는 등록된 함수를 호출.
                    return true;
                }
            }
            Debug.Log("WeaponSlots Full");
            return false;       // ) 아이템창이 다 찼을 경우, 아이템을 추가적으로 먹을 수 없음.
        }


        // [◆] - ▶▶▶ RemovedWeapon → 매개변수로 받은 무기(WeaponController)를 무기 리스트에서 제거.
        private bool RemovedWeapon(WeaponController weaponInstance)
        {
            // [◇] - [◆] - ) .
            if (weaponInstance == null)
                return false;
            // [◇] - [◆] - ) 슬롯에서 같은 무기 찾기.
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                if (weaponSlots[i] == weaponInstance)
                {
                    // [◇] - [◇] - [◆] - ) 슬롯값 초기화.
                    weaponSlots[i] = null;
                    // [◇] - [◇] - [◆] - ) 무기 제거와 관련되어 있는 등록된 함수를 호출.
                    OnRemovedWeapon?.Invoke(weaponInstance, i);
                    // [◇] - [◇] - [◆] - ) 무기 오브젝트 제거.
                    Destroy(weaponInstance.gameObject);
                    // [◇] - [◇] - [◆] - ) 제거한 무기가 현재 들고있는 무기일 경우, 액티브 무기로 교체.
                    if (i == ActiveWeaponIndex)
                    {
                        SwitchWeapon(true);
                    }
                }
            }
            return false;
        }


        // [◆] - ▶▶▶ HasWeapon → 매개변수로 받은 프리팹으로 생성된 무기가 있으면 반환시킴.
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
            // [◇] - [◆] - ) .
            if (newWeapon != null)
            {
                newWeapon.ShowWeapon(true);
            }
        }


        // [◆] - ▶▶▶ 저격모드 ON(무기 전용 카메라 OFF).
        private void OnScope()
        {
            weaponCamera.enabled = false;
        }


        // [◆] - ▶▶▶ 저격모드 OFF(무기 전용 카메라 ON).
        private void OffScope()
        {
            weaponCamera.enabled = true;
        }
        #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
    }
}