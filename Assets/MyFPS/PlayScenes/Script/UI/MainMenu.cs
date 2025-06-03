using MyFPS;
using UnityEngine;

/* [0] 개요 : MainMenu
		- 메인메뉴 씬을 관리하는 클래스.
*/

public class MainMenu : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) 참조.
    private AudioManager audioManager;
    // [ ] - 2) 씬 변경.
    public SceneFader fader;
    [SerializeField] private string loadToScene = "MainScene01";

    #endregion Variable





    // [2] Property.
    #region Property
    #endregion Property





    // [3] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) 참조. 
        audioManager = AudioManager.Instance;
        // [ ] - [ ] - 2) 씬 시작시 페이드인 효과. 
        fader.FadeStart();
        // [ ] - [ ] - 3) 메뉴 배경음 플레이.
        AudioManager.Instance.PlayBgm("MenuMusic");
    }
    #endregion Unity Event Method





    // [4] Custom Method.
    #region Custom Method
    // [ ] - 1) NewGame.
    public void NewGame()
    {
        // [ ] - [ ] - 1) 메뉴 선택 사운드.
        audioManager.Play("MenuSelect");
        // [ ] - [ ] - 2) 새 게임하러 가기.
        fader.FadeTo(loadToScene);
        // )        Debug.Log("New Game");
    }

    // [ ] - 2) LoadGame.
    public void LoadGame()
    {
        Debug.Log("Load Game");
    }

    // [ ] - 3) Options.
    public void Options()
    {
        Debug.Log("Show Options");
    }

    // [ ] - 4) Credits.
    public void Credits()
    {
        Debug.Log("Show Credits");
    }

    // [ ] - 5) QuitGame.
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    #endregion Custom Method


}
// [ ] - ) 
// [ ] - [ ] - ) 
