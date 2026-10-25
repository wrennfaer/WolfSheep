using UnityEngine;
using UnityEngine.InputSystem;

public class sheepmovement : MonoBehaviour
{
    // Sheep movement speed
    public float moveSpeed = 5f;
    Vector2 movement;

    Rigidbody2D RB;
    void Start()
    {
        
        RB=GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        
        movement= InputSystem.actions["Move"].ReadValue<Vector2>();
       
    }

    void FixedUpdate()
    {
        
        RB.linearVelocity=movement*moveSpeed;
    }
}
