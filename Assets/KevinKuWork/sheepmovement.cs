
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class sheepmovement : MonoBehaviour
{
    [Header("Sheep Movement")]
    public float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D RB;

    [Header("Sheep Death")]
    private bool isDead = false;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        movement = InputSystem.actions["Move"]
            .ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        RB.linearVelocity = movement * moveSpeed;
    }

    // Sheep dies
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        movement = Vector2.zero;
        RB.linearVelocity = Vector2.zero;

        Debug.Log("Sheep died!");

        Destroy(gameObject);
    }

    // Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
