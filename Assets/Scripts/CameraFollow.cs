using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector2 offset = new Vector2(0f, 0f);
    
    private Transform player;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Find the player using the singleton pattern
        if (Player.Instance != null)
        {
            player = Player.Instance.transform;
            
            // Инициализируем позицию камеры с учетом смещения
            Vector3 initialPosition = transform.position;
            initialPosition.x = player.position.x + offset.x;
            initialPosition.y = player.position.y + offset.y;
            initialPosition.z = -10f; // Фиксируем Z для 2D камеры
            transform.position = initialPosition;
            
            Debug.Log("CameraFollow: Найден игрок " + player.name);
        }
        else
        {
            Debug.LogError("Player instance not found! Make sure the Player script is initialized.");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Целевая позиция только по осям X и Y с учетом смещения
            Vector3 targetPosition = new Vector3(
                player.position.x + offset.x,
                player.position.y + offset.y,
                -10f // Фиксируем Z-координату
            );
            
            // Плавно перемещаем камеру к целевой позиции
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                targetPosition, 
                ref velocity, 
                smoothSpeed
            );
        }
    }
}