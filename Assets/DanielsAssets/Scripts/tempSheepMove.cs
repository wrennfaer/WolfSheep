using UnityEngine;
using UnityEngine.InputSystem;

public class tempSheepMove : MonoBehaviour
{
    // Sheep movement speed
    public float moveSpeed = 5f;
    Vector2 movementInput;
    Rigidbody2D rb;
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
}
