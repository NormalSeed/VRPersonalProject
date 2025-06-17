using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private void Awake() => Init();
    private void Init()
    {
        base.SingletonInit();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.EBgm.BGM_TITLE);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnStartButtonClicked()
    {
        LoadScene("SampleScene");
        SoundManager.Instance.PlayBGM(SoundManager.EBgm.BGM_STAGE);
    }

    public void OnTitleButtonClicked()
    {
        LoadScene("TitleScene");
        SoundManager.Instance.PlayBGM(SoundManager.EBgm.BGM_TITLE);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
