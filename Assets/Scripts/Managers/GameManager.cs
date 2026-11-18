using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private string MainMenuSceneName = "MainMenu";


    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;

        // reset the distance so that the score starts at 0
        PlayerCar.Distance = 0f;

        // hide the game over panel and unpause the game
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        // stop the game and show the game over panel
        Time.timeScale = 0f;
        finalScoreText.text = "Score: " + ScoreManager.Instance.Score;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}