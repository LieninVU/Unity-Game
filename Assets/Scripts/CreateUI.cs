using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateUI : MonoBehaviour
{
    void Start()
    {
        // Создаем Canvas, если его нет
        GameObject canvas = GameObject.Find("GameUI");
        if (canvas == null)
        {
            canvas = new GameObject("GameUI");
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            
            // Настраиваем Canvas
            Canvas canvasComponent = canvas.GetComponent<Canvas>();
            canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
            
            // Настраиваем CanvasScaler
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
        }
        
        // Создаем Panel для HUD
        Transform panel = canvas.transform.Find("HUDPanel");
        if (panel == null)
        {
            GameObject panelObj = new GameObject("HUDPanel");
            panelObj.transform.SetParent(canvas.transform, false);
            
            // Добавляем RectTransform и настраиваем его
            RectTransform panelRect = panelObj.AddComponent<RectTransform>();
            panelRect.anchoredPosition = Vector2.zero;
            panelRect.sizeDelta = new Vector2(0, 0);
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            
            panel = panelObj.transform;
        }
        
        // Создаем текст для счета
        if (panel.Find("ScoreText") == null)
        {
            CreateTextElement("ScoreText", panel, "Счет: 0", new Vector2(20, -20), new Vector2(0, 1), new Vector2(0, 1));
        }
        
        // Создаем текст для времени
        if (panel.Find("TimeText") == null)
        {
            CreateTextElement("TimeText", panel, "Время: 00:00", new Vector2(-20, -20), new Vector2(1, 1), new Vector2(1, 1));
        }
        
        // Настраиваем UIManager
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            TextMeshProUGUI scoreTextComponent = panel.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeTextComponent = panel.Find("TimeText").GetComponent<TextMeshProUGUI>();
            
            if (scoreTextComponent != null)
            {
                uiManager.scoreText = scoreTextComponent;
            }
            
            if (timeTextComponent != null)
            {
                uiManager.timeText = timeTextComponent;
            }
            
            Debug.Log("UIManager настроен успешно");
        }
        
        // Удаляем этот скрипт после настройки
        Destroy(gameObject);
    }
    
    private void CreateTextElement(string name, Transform parent, string text, Vector2 anchoredPosition, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);
        
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.sizeDelta = new Vector2(300, 50);
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.fontSize = 36;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.white;
    }
}