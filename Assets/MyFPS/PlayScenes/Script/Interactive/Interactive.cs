using MyFPS;
using TMPro;
using UnityEngine;

/* [0] ���� : Interactive
		- ���ͷ�Ƽ�� �׼��� �θ�Ŭ����.
*/

public class Interactive : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) ���� �÷��̾�� �Ÿ�.
    [SerializeField]
    protected private float theDistance;
    // [ ] - 2) �׼� UI.
    public GameObject actionUI;
    public TextMeshProUGUI actionText;
    // [ ] - [ ] - 1) ũ�ν����.
    public GameObject extraCross;
    // [ ] - [ ] - 2) ���ͷ�Ƽ�� ��� ��� ����.
    protected bool unInteractive = false;
    // [ ] - [ ] - 3) ����.
    [SerializeField] protected string action = "Do Interactive Action";       // ) ����Ƽ�� Inspector���� ActionText�� ������ ���� �� ����.
    #endregion Variable





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Update.
    protected private void Update()
    {
        // [ ] - [ ] - 1) �Ѱ� �÷��̾�� �Ÿ� ��������.
        theDistance = PlayCasting.distanceFromTarget;
    }

    // [ ] - 2) OnMouseOver.
    private void OnMouseOver()
    {
        // [ ] - [ ] - 1) ���ͷ�Ƽ�� ��� ����.
        if (unInteractive)
            return;
        // [ ] - [ ] - 2) ũ�ν���� Ű��.
        extraCross.SetActive(true);
        // [ ] - [ ] - 3) UI Ű��.
        if (theDistance <= 2f)
        {
            ShowActionUI();
            // [ ] - [ ] - [ ] - 1) Ű �Է� üũ.
            if (Input.GetKeyDown(KeyCode.E))
            {
                // [ ] - [ ] - [ ] - [ ] - 1) .
                extraCross.SetActive(false);
                // [ ] - [ ] - [ ] - [ ] - 2) UI �����.
                HideActionUI();
                // [ ] - [ ] - [ ] - [ ] - 3) �׼� ����.
                DoAction();
            }
        }
        else
        {
            // [ ] - [ ] - [ ] - [ ] - 4) ������Ʈ�� �Ÿ��� �־����� UI �����.
            HideActionUI();
        }
    }

    // [ ] - 3) OnMouseExit.
    private void OnMouseExit()
    {
        // [ ] - [ ] - 1) ũ�ν���� ����.
        extraCross.SetActive(false);
        // [ ] - [ ] - 2) UI ����.
        HideActionUI();
    }
    #endregion Unity Event Method





    // [3] Custom Method.
    #region Custom Method
    // [ ] - 1) ShowActionUI �� Action UI �����ֱ�.
    protected void ShowActionUI()
    {
        actionUI.SetActive(true);
        actionText.text = action;
    }

    // [ ] - 2) HideActionUI �� Action UI �����.
    protected void HideActionUI()
    {
        actionUI.SetActive(false);
        actionText.text = "";
    }

    // [ ] - 3) DoAction �� �׼� �Լ�.
    protected virtual void DoAction()
    { 
    }
    #endregion Custom Method
}