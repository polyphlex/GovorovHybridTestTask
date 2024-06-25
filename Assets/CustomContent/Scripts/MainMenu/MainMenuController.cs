using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private string firstLevelName;

    // Start is called before the first frame update
    void Start()
    {
        //честно говоря я больше люблю кидать в инспектор, но людям это не нравится
        startGameButton.onClick.AddListener(() => StartGame());
        quitGameButton.onClick.AddListener(() => QuitGame());
    }

    private void StartGame()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadSceneAsync("Level1");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    
}
