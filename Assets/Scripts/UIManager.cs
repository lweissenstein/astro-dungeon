using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("Text References")]
    public TextMeshProUGUI inGameScoreText; 
    public TextMeshProUGUI gameOverScoreText; 
    public TextMeshProUGUI mainMenuScoreText;

    private bool isPaused = false;
    public static bool isRetry = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RefreshReferences(inGameScoreText, gameOverScoreText, mainMenuScoreText);
        }

        if (isRetry)
        {
            MainMenuPanel.SetActive(false);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 1f;
            isRetry = false;
        }
        else
        {
            MainMenuPanel.SetActive(true);
            PausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;

        ScoreManager.Instance.ResetScore();
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RetryGame()
    {
        isRetry = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreManager.Instance.ResetScore();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHighscoreUI()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetHighscore();

            if (mainMenuScoreText != null)
            {
                mainMenuScoreText.text = "Highscore: 0";
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }
    }
}