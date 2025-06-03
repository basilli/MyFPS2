using MyFPS;
using TMPro;
using UnityEngine;

/* [0] 개요 : Interactive
		- 인터랙티브 액션의 부모클래스.
*/

public class Interactive : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) 문과 플레이어간의 거리.
    [SerializeField]
    protected private float theDistance;
    // [ ] - 2) 액션 UI.
    public GameObject actionUI;
    public TextMeshProUGUI actionText;
    // [ ] - [ ] - 1) 크로스헤어.
    public GameObject extraCross;
    // [ ] - [ ] - 2) 인터랙티브 기능 사용 여부.
    protected bool unInteractive = false;
    // [ ] - [ ] - 3) 문구.
    [SerializeField] protected string action = "Do Interactive Action";       // ) 유니티의 Inspector에서 ActionText의 내용을 적을 수 있음.
    #endregion Variable





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Update.
    protected private void Update()
    {
        // [ ] - [ ] - 1) 총과 플레이어간의 거리 가져오기.
        theDistance = PlayCasting.distanceFromTarget;
    }

    // [ ] - 2) OnMouseOver.
    private void OnMouseOver()
    {
        // [ ] - [ ] - 1) 인터랙티브 기능 끄기.
        if (unInteractive)
            return;
        // [ ] - [ ] - 2) 크로스헤어 키기.
        extraCross.SetActive(true);
        // [ ] - [ ] - 3) UI 키기.
        if (theDistance <= 2f)
        {
            ShowActionUI();
            // [ ] - [ ] - [ ] - 1) 키 입력 체크.
            if (Input.GetKeyDown(KeyCode.E))
            {
                // [ ] - [ ] - [ ] - [ ] - 1) .
                extraCross.SetActive(false);
                // [ ] - [ ] - [ ] - [ ] - 2) UI 숨기기.
                HideActionUI();
                // [ ] - [ ] - [ ] - [ ] - 3) 액션 실행.
                DoAction();
            }
        }
        else
        {
            // [ ] - [ ] - [ ] - [ ] - 4) 오브젝트와 거리가 멀어지면 UI 숨기기.
            HideActionUI();
        }
    }

    // [ ] - 3) OnMouseExit.
    private void OnMouseExit()
    {
        // [ ] - [ ] - 1) 크로스헤어 끄기.
        extraCross.SetActive(false);
        // [ ] - [ ] - 2) UI 끄기.
        HideActionUI();
    }
    #endregion Unity Event Method





    // [3] Custom Method.
    #region Custom Method
    // [ ] - 1) ShowActionUI → Action UI 보여주기.
    protected void ShowActionUI()
    {
        actionUI.SetActive(true);
        actionText.text = action;
    }

    // [ ] - 2) HideActionUI → Action UI 숨기기.
    protected void HideActionUI()
    {
        actionUI.SetActive(false);
        actionText.text = "";
    }

    // [ ] - 3) DoAction → 액션 함수.
    protected virtual void DoAction()
    { 
    }
    #endregion Custom Method
}