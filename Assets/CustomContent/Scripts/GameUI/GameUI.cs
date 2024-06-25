using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [SerializeField] private TMP_Text livesLabel;
    [SerializeField] private Image livesIcon;

    [SerializeField] private Image gameOver;
    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject finishScreen;
    [SerializeField] private Button finishRestartButton;
    [SerializeField] private Button finishNextButton;

    [SerializeField] private TMP_Text timerLabel;

    private int currentGameTime;
    [SerializeField] private int gameTimeLength;

    private bool timerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        IconBobRight();
        restartButton.onClick.AddListener(() => Retry());

        finishRestartButton.onClick.AddListener(() => Retry());
        finishNextButton.onClick.AddListener(() => GlobalFlowControl.instance.Finish());

        StartCoroutine(GameTimer());
        Time.timeScale = 1;
    }

    public void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
        gameOver.DOFade(1f, 3f);
        Time.timeScale = 0;
    }

    public void FinishLevel()
    {
        finishScreen.SetActive(true);
        timerActive = false;
        Time.timeScale = 0;
    }

    private void Retry()
    {
        GlobalFlowControl.instance.Restart();
    }

    // Update is called once per frame
    void Update()
    {
        livesLabel.text = GlobalValueDispatcher.instance.PlayerLivesAmount.ToString();
        timerLabel.text = currentGameTime.ToString();
    }


    //реализовано в UI потому что GlobalFlowControl не ретриггерит старт при загрузке сцены, а добавлять onSceneLoaded в данной ситуации кажется оверкиллом
    private IEnumerator GameTimer()
    {
        for (int time = gameTimeLength; time > 0; time--)
        {
            if (!timerActive) break;
            currentGameTime = time;
            yield return new WaitForSeconds(1f);
        }
        if (timerActive) ShowGameOver();
    }

    private void IconBobLeft()
    {
        livesIcon.transform.DORotate(new Vector3(0, 0, -20f), 3f).OnComplete(() => IconBobRight());
    }

    private void IconBobRight()
    {
        livesIcon.transform.DORotate(new Vector3(0, 0, 20f), 3f).OnComplete(() => IconBobLeft());
    }
}
