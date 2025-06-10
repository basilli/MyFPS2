using MyFPS;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

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
    // [ ] - 3) �޴�.
    public GameObject mainMenuUI;
    public GameObject optionUI;
    public GameObject creditCanvas;
    private bool isShowOption = false;
    private bool isShowCredit = false;
    // [ ] - 4) ��������.
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    #endregion Variable






    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) �ɼ� ���尪 ��������. 
        LoadOptions();
        // [ ] - [ ] - 2) ����. 
        audioManager = AudioManager.Instance;
        // [ ] - [ ] - 3) �� ���۽� ���̵��� ȿ��. 
        fader.FadeStart();
        // [ ] - [ ] - 4) �޴� ����� �÷���.
        audioManager.PlayBgm("MenuMusic");
        // [ ] - [ ] - 5) .
        isShowOption = false;
        isShowOption = false;
    }

    // [ ] - 2) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) ESCŰ�� . 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShowOption)
            {
                HideOptionsUI();
            }
            else if (isShowCredit)
            {
                HideCreditUI();
            }
        }
    }
    #endregion Unity Event Method





    // [3] Custom Method.
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
    public void OptionsUI()
    {
        isShowOption = true;
        // [ ] - [ ] - 1) �޴� ���� ����.
        audioManager.Play("MenuSelect");
        // [ ] - [ ] - 1 �ɼ� UI �����ֱ�.
        mainMenuUI.SetActive(false);
        optionUI.SetActive(true);
    }

    // [ ] - 3) Options.
    public void HideOptionsUI()
    {
        isShowOption = false;
        mainMenuUI.SetActive(true);
        optionUI.SetActive(true);
    }

    // [ ] - 4) Credits.
    public void Credits()
    {
        // [ ] - [ ] - 1) .
        audioManager.Play("MenuSelect");
        // [ ] - [ ] - 2) .
        StartCoroutine(ShowCreditUI());
        mainMenuUI.SetActive(false);
        creditCanvas.SetActive(true);
    }

    // [ ] - 5) QuitGame.
    public void QuitGame()
    {
        // TODO : Cheating
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // [ ] - 7) BGM ���� ����.
    public void SetBGMVolume(float value)
    {
        // [ ] - [ ] - 1) ��������.
        PlayerPrefs.SetFloat("BGM", value);
        // [ ] - [ ] - 2) ��������.
        audioMixer.SetFloat("BGM", value);
    }

    // [ ] - 8) SFX ���� ����.
    public void SetSFXVolume(float value)
    {
        // [ ] - [ ] - 1) ��������.
        PlayerPrefs.SetFloat("SFX", value);
        // [ ] - [ ] - 2) ��������.
        audioMixer.SetFloat("SFX", value);
    }

    // [ ] - 9) �ɼ� ���尪�� �����ͼ� ���ӿ� ����.
    private void LoadOptions()
    {
        // [ ] - [ ] - 1) BGM ������ ��������.
        float bgmVolume = PlayerPrefs.GetFloat("BGM", 0f);
        // [ ] - [ ] - 2) ����� �ͼ��� ����.
        SetBGMVolume(bgmVolume);
        // [ ] - [ ] - 3) UI�� ����.
        bgmSlider.value = bgmVolume;
        // [ ] - [ ] - 4) SFX ������ ��������.
        float sfxVolume = PlayerPrefs.GetFloat("SFX", 0f);
        // [ ] - [ ] - 5) ����� �ͼ��� ����.
        SetSFXVolume(sfxVolume);
        // [ ] - [ ] - 6) UI�� ����.
        sfxSlider.value = sfxVolume;
        // [ ] - [ ] - 7) ETC.
    }

    // [ ] - 10) ũ���� UI �����ֱ�.
    IEnumerator ShowCreditUI()
    {
        isShowCredit = true;
        mainMenuUI.SetActive(false);
        creditCanvas.SetActive(true);
        yield return new WaitForSeconds(7f);
        HideCreditUI();
    }

    // [ ] - 11) ũ���� UI ������.
    public void HideCreditUI()
    {
        // [ ] - [ ] - 1) .
        mainMenuUI.SetActive(true);
        creditCanvas.SetActive(false);
        isShowCredit = false;
    }

    #endregion Custom Method
}
