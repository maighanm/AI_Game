using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;     // Text to show the current score
    public GameObject levelPanel;         // Black transparent panel behind level text
    public TextMeshProUGUI levelText;     // Level text (white)
    public TextMeshProUGUI youWinText;    // You Win text

    private int currentLevel = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreDisplay();
        ShowLevel("Level 1");
        if (youWinText != null)
            youWinText.gameObject.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
        CheckLevelProgression();
    }

    void UpdateScoreDisplay()
    {
        if(scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void ShowLevel(string levelString)
    {
        if (levelText != null && levelPanel != null)
        {
            levelText.text = levelString;
            levelPanel.SetActive(true);
            CancelInvoke(nameof(HideLevel));   // Cancel previous hide invoke if any
            Invoke(nameof(HideLevel), 3f);     // Hide panel after 3 seconds
        }
    }

    void HideLevel()
    {
        if (levelPanel != null)
            levelPanel.SetActive(false);
    }

    void CheckLevelProgression()
    {
        if (score >= 200)
        {
            ShowWinMessage();
            return;
        }

        if (score >= 150 && currentLevel < 3)
        {
            currentLevel = 3;
            ShowLevel("Level 3");
        }
        else if (score >= 100 && currentLevel < 2)
        {
            currentLevel = 2;
            ShowLevel("Level 2");
        }
    }

    void ShowWinMessage()
    {
        if (youWinText != null)
            youWinText.gameObject.SetActive(true);

        if (levelPanel != null)
            levelPanel.SetActive(false);

        Time.timeScale = 0f; // Pause the game
    }
}
