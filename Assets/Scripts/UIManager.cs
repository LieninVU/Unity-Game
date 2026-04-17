using UnityEngine;
using UnityEngine.UI;
using TMPro; // Добавлено

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI timeText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged += UpdateScore;
            GameManager.Instance.OnTimeChanged += UpdateTime;
            
            UpdateScore(GameManager.Instance.score);
            UpdateTime(GameManager.Instance.GetPlayTime());
        }
    }
    
    private void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Счет: " + score;
        }
    }
    
    private void UpdateTime(float time)
    {
        if (timeText != null)
        {
            timeText.text = "Время: " + GameManager.Instance.FormatTime(time);
        }
    }
    
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnTimeChanged -= UpdateTime;
        }
    }
}