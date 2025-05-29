using MyFPS;
using UnityEngine;
using System.Collections;

/* [0] ���� : Title
		- Ÿ��Ʋ ���� �����ϴ� Ŭ����.
		- 3�� �Ŀ� �ִ�Ű�� ���̰�, 10�� �Ŀ� ���� �޴��� ��.
*/
public class Title : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    #endregion Variable





    // [2] Property.
    #region Property
    // [ ] - 1) .
    public SceneFader fader;
    [SerializeField] private string loadToScene = "MainMenu";
    // [ ] - 2) �ִ�Ű�� ����ȿ��.
    private bool isShow = false;
    public GameObject anykey;
    #endregion Property





    // [3] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) ���̵��� ȿ��
        fader.FadeStart();
        // [ ] - [ ] - 2) ����� �÷���.
        AudioManager.Instance.PlayBgm("TitleBgm");
        // [ ] - [ ] - 3) �ڷ�ƾ �Լ� ����.
        StartCoroutine(TitleProcress());

    }

    // [ ] - 1) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) �ִ�Ű�� ���� �� �ƹ�Ű�� ������ ���θ޴��� ����.
        if (Input.anyKeyDown && isShow)
        {
            StopAllCoroutines();
            AudioManager.Instance.Stop("TitleBgm");
            fader.FadeTo(loadToScene);
        }
    }
    #endregion Unity Event Method





    // [4] Custom Method.
    #region Custom Method
    // [ ] - 1) �ڷ�ƾ �Լ�
    IEnumerator TitleProcress()
    {
        // [ ] - [ ] - 1) 3�� �Ŀ� �ִ�Ű�� ���̰�
        yield return new WaitForSeconds(3f);
        isShow = true;
        anykey.SetActive(true);
        // [ ] - [ ] - 2) 10�� �Ŀ� ���� �޴��� ��.
        yield return new WaitForSeconds(10f);
        AudioManager.Instance.Stop("TitleBgm");
        fader.FadeTo(loadToScene);
    }
    #endregion Custom Method
}