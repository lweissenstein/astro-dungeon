using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI mainMenuScoreText;

    private const string HighscoreKey = "Highscore";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshReferences(TextMeshProUGUI gameScore, TextMeshProUGUI overScore, TextMeshProUGUI menuScore)
    {
        scoreText = gameScore;
        gameOverScoreText = overScore;
        mainMenuScoreText = menuScore;

        LoadHighscore();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        int oldHighscore = PlayerPrefs.GetInt(HighscoreKey, 0);

        if (score > oldHighscore)
            PlayerPrefs.SetInt(HighscoreKey, score);

        PlayerPrefs.Save();

        if (gameOverScoreText != null)
            gameOverScoreText.text = "Your Score: " + score;
    }

    public void LoadHighscore()
    {
        if (mainMenuScoreText == null) return;

        int highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
        mainMenuScoreText.text = "Highscore: " + highscore;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt(HighscoreKey, 0);
        PlayerPrefs.Save();
        LoadHighscore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}