using MyFPS;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

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
    // [ ] - 3) 메뉴.
    public GameObject mainMenuUI;
    public GameObject optionUI;
    public GameObject creditCanvas;
    private bool isShowOption = false;
    private bool isShowCredit = false;
    // [ ] - 4) 볼륨조절.
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    #endregion Variable






    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) 옵션 저장값 가져오기. 
        LoadOptions();
        // [ ] - [ ] - 2) 참조. 
        audioManager = AudioManager.Instance;
        // [ ] - [ ] - 3) 씬 시작시 페이드인 효과. 
        fader.FadeStart();
        // [ ] - [ ] - 4) 메뉴 배경음 플레이.
        audioManager.PlayBgm("MenuMusic");
        // [ ] - [ ] - 5) .
        isShowOption = false;
        isShowOption = false;
    }

    // [ ] - 2) Update.
    private void Update()
    {
        // [ ] - [ ] - 1) ESC키를 . 
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
    public void OptionsUI()
    {
        isShowOption = true;
        // [ ] - [ ] - 1) 메뉴 선택 사운드.
        audioManager.Play("MenuSelect");
        // [ ] - [ ] - 1 옵션 UI 보여주기.
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

    // [ ] - 7) BGM 볼륨 조절.
    public void SetBGMVolume(float value)
    {
        // [ ] - [ ] - 1) 볼륨저장.
        PlayerPrefs.SetFloat("BGM", value);
        // [ ] - [ ] - 2) 볼륨조절.
        audioMixer.SetFloat("BGM", value);
    }

    // [ ] - 8) SFX 볼륨 조절.
    public void SetSFXVolume(float value)
    {
        // [ ] - [ ] - 1) 볼륨저장.
        PlayerPrefs.SetFloat("SFX", value);
        // [ ] - [ ] - 2) 볼륨조절.
        audioMixer.SetFloat("SFX", value);
    }

    // [ ] - 9) 옵션 저장값을 가져와서 게임에 적용.
    private void LoadOptions()
    {
        // [ ] - [ ] - 1) BGM 볼륨값 가져오기.
        float bgmVolume = PlayerPrefs.GetFloat("BGM", 0f);
        // [ ] - [ ] - 2) 오디오 믹서에 적용.
        SetBGMVolume(bgmVolume);
        // [ ] - [ ] - 3) UI에 적용.
        bgmSlider.value = bgmVolume;
        // [ ] - [ ] - 4) SFX 볼륨값 가져오기.
        float sfxVolume = PlayerPrefs.GetFloat("SFX", 0f);
        // [ ] - [ ] - 5) 오디오 믹서에 적용.
        SetSFXVolume(sfxVolume);
        // [ ] - [ ] - 6) UI에 적용.
        sfxSlider.value = sfxVolume;
        // [ ] - [ ] - 7) ETC.
    }

    // [ ] - 10) 크레딧 UI 보여주기.
    IEnumerator ShowCreditUI()
    {
        isShowCredit = true;
        mainMenuUI.SetActive(false);
        creditCanvas.SetActive(true);
        yield return new WaitForSeconds(7f);
        HideCreditUI();
    }

    // [ ] - 11) 크레딧 UI 나가기.
    public void HideCreditUI()
    {
        // [ ] - [ ] - 1) .
        mainMenuUI.SetActive(true);
        creditCanvas.SetActive(false);
        isShowCredit = false;
    }

    #endregion Custom Method
}
