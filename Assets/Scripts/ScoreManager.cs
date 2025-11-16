using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;         
    public TextMeshProUGUI gameOverScoreText; 
    public TextMeshProUGUI mainMenuScoreText; 

    string filePath;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        filePath = Path.Combine(Application.dataPath, "highscore.txt");
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

    public void SaveScoreToFile()
    {
        int oldHighscore = 0;

        if (File.Exists(filePath))
            int.TryParse(File.ReadAllText(filePath), out oldHighscore);

        if (score > oldHighscore)
            File.WriteAllText(filePath, score.ToString());

        if (gameOverScoreText != null)
            gameOverScoreText.text = "Your Score: " + score;
    }

    void LoadHighscore()
    {
        if (mainMenuScoreText == null) return;

        if (File.Exists(filePath))
            mainMenuScoreText.text = "Highscore: " + File.ReadAllText(filePath);
        else
            mainMenuScoreText.text = "Highscore: 0";
    }

    public void ResetHighscore()
    {
        if (File.Exists(filePath))
            File.WriteAllText(filePath, "0");
        LoadHighscore();
    }
}
