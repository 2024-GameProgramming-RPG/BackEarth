using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;    
    public float moveSpeed = 2f;

    void OnMove(InputValue movementValue) // 메서드 이름 수정
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    void FixedUpdate() 
    {
        Vector2 movement = movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        if (movementInput != Vector2.zero)
        {
            float directionX = movementInput.x;
            float directionY = movementInput.y;
            this.animator.SetBool("isWalking", true);
            this.animator.SetFloat("DirectionX", directionX);
            this.animator.SetFloat("DirectionY", directionY);
            Debug.Log($"DirectionX: {directionX}, DirectionY: {directionY}");

        }
        else
        {
            this.animator.SetBool("isWalking", false);
        }
    }
}
