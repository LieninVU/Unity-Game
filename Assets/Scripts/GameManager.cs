using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // Счет
    public int score = 0;
    public int enemiesKilled = 0;
    public int zombiesKilled = 0;
    
    // Таймер
    private float playTime = 0f;
    private bool isGameRunning = false;
    
    // События
    public Action<int> OnScoreChanged;
    public Action<float> OnTimeChanged;
    
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
        StartGame();
    }
    
    private void Update()
    {
        if (isGameRunning)
        {
            playTime += Time.deltaTime;
            OnTimeChanged?.Invoke(playTime);
        }
    }
    
    public void StartGame()
    {
        isGameRunning = true;
        Debug.Log("Игра началась!");
    }
    
    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log($"Игра окончена! Время: {FormatTime(playTime)}, Счет: {score}");
    }
    
    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Счет изменен: {score}");
    }
    
    public void EnemyKilled()
    {
        enemiesKilled++;
        AddScore(1);
    }
    
    public void ZombieKilled()
    {
        zombiesKilled++;
        AddScore(-1);
    }
    
    public float GetPlayTime()
    {
        return playTime;
    }
    
    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}