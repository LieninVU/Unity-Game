using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  
    public static Player Instance { get; private set; }
    private Rigidbody2D rb;
    [SerializeField] private float Speed = 10f;
    private float minSpeed = 0.1f;
    private bool isRun = false;
    private bool flipX = false;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();       
    }


    

    private void FixedUpdate()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (Speed * Time.fixedDeltaTime));
        if(Mathf.Abs(inputVector.x) > minSpeed || (Mathf.Abs(inputVector.y) > minSpeed)){
            isRun = true;
        } else
        {
            isRun = false;
        }
        if(inputVector.x > minSpeed && inputVector.x != 0)
        {
            flipX = false;
        }
        else if (inputVector.x < minSpeed && inputVector.x != 0)
        {
            flipX= true;
        }
    }
    public bool isRunning() { return isRun;}
    public bool isFlipX() { return flipX;}
}
