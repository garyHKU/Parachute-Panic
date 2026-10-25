using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;      // unfreeze after a restart
        PlayerCar.Distance = 0f;  // statics survive scene reloads, so reset them
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}