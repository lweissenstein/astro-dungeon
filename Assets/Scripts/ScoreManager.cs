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
        SceneManager.sceneLoaded += OnSceneLoaded;
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

    void Start()
    {
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

    void LoadHighscore()
    {
        if (mainMenuScoreText == null) return;

        int highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
        mainMenuScoreText.text = "Highscore: " + highscore;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt(HighscoreKey, 0);
        LoadHighscore();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.Find("inGameScore")?.GetComponent<TextMeshProUGUI>();
        gameOverScoreText = GameObject.Find("GameOverScore")?.GetComponent<TextMeshProUGUI>();
        mainMenuScoreText = GameObject.Find("MainMenuHighScore")?.GetComponent<TextMeshProUGUI>();

        UpdateScoreUI();
        LoadHighscore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }


}
