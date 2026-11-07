using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class tempSheepMove : MonoBehaviour
{
    // Sheep movement speed
    public float moveSpeed = 5f;
    Vector2 movementInput;
    Rigidbody2D rb;
    public BombManager bm;
    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(isDead || GameManager.GameIsPaused)
        {
            movementInput = Vector2.zero;
            return;
        }
        // Get keyboard input
        movementInput = InputSystem.actions["Move"].ReadValue<Vector2>();
    
    }
    void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }

    // collecting bomb
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (isDead || GameManager.GameIsPaused)
        {
            return;
        }
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            bm.CollectBomb();
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead || GameManager.GameIsPaused) 
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        movementInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        FindAnyObjectByType<GameManager>().LoseScene();
    }

}
