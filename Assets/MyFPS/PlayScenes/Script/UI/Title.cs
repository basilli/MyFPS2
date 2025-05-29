using MyFPS;
using UnityEngine;
using System.Collections;

/* [0] 개요 : Title
		- 타이틀 씬을 관리하는 클래스.
		- 3초 후에 애니키가 보이고, 10초 후에 메인 메뉴로 감.
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
    // [ ] - 2) 애니키의 점멸효과.
    private bool isShow = false;
    public GameObject anykey;
    #endregion Property





    // [3] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) 페이드인 효과
        fader.FadeStart();
        // [ ] - [ ] - 2) 배경음 플레이.
        AudioManager.Instance.PlayBgm("TitleBgm");
        // [ ] - [ ] - 3) 코루틴 함수 실행.
        StartCoroutine(TitleProcress());

    }

    // [ ] - 1) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) 애니키가 보인 후 아무키나 누르면 메인메뉴로 가기.
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
    // [ ] - 1) 코루틴 함수
    IEnumerator TitleProcress()
    {
        // [ ] - [ ] - 1) 3초 후에 애니키가 보이고
        yield return new WaitForSeconds(3f);
        isShow = true;
        anykey.SetActive(true);
        // [ ] - [ ] - 2) 10초 후에 메인 메뉴로 감.
        yield return new WaitForSeconds(10f);
        AudioManager.Instance.Stop("TitleBgm");
        fader.FadeTo(loadToScene);
    }
    #endregion Custom Method
}