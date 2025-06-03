using MyFPS;
using UnityEngine;

/* [0] ���� : MainMenu
		- ���θ޴� ���� �����ϴ� Ŭ����.
*/

public class MainMenu : MonoBehaviour
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) ����.
    private AudioManager audioManager;
    // [ ] - 2) �� ����.
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
        // [ ] - [ ] - 1) ����. 
        audioManager = AudioManager.Instance;
        // [ ] - [ ] - 2) �� ���۽� ���̵��� ȿ��. 
        fader.FadeStart();
        // [ ] - [ ] - 3) �޴� ����� �÷���.
        AudioManager.Instance.PlayBgm("MenuMusic");
    }
    #endregion Unity Event Method





    // [4] Custom Method.
    #region Custom Method
    // [ ] - 1) NewGame.
    public void NewGame()
    {
        // [ ] - [ ] - 1) �޴� ���� ����.
        audioManager.Play("MenuSelect");
        // [ ] - [ ] - 2) �� �����Ϸ� ����.
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
