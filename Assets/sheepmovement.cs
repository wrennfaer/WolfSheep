
using UnityEngine;

public class sheepmovement : MonoBehaviour
{
    // Sheep movement speed
    public float moveSpeed = 5f;

    void Update()
    {
        // Get keyboard input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create movement direction
        Vector3 movement = new Vector3(
            horizontal,
            vertical,
            0f
        );

        // Move the sheep
        transform.position +=
            movement.normalized * moveSpeed * Time.deltaTime;
    }
}
