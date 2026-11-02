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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
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
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            bm.bombCount++;
        }
    }

}
