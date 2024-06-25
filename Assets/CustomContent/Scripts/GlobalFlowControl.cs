using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalFlowControl : MonoBehaviour
{
    public static GlobalFlowControl instance;
    [SerializeField] private string[] levelsName;
    private int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //print("GLOBALFLOWSTART");
    }

    private void ResetFlowControl()
    {
        currentLevelIndex = 0;
    }

    /// <summary>
    /// ����� ������� ���������
    /// </summary>
    public void GameOver()
    {
        PlayerController.instance.DisablePlayerInput();
        GameUI.instance.ShowGameOver();
    }

    /// <summary>
    /// ���������� ������� �����
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToTitle()
    {
        ResetFlowControl();
        GlobalValueDispatcher.instance.ResetValueDispatcher();
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// ������������� ������
    /// </summary>
    public void Finish()
    {
        currentLevelIndex++;

        //��� �������� -1 ������ ��� ���� � ������ ������ ����� ������ �������������� ������ ������
        if (currentLevelIndex >= levelsName.Length)
        {
            ReturnToTitle();
        }
        else
        {
            SceneManager.LoadScene(levelsName[currentLevelIndex]);
        }
    }

}
