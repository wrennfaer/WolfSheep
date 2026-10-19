using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 targetPosition;
    Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float distanceToTarget = 0.1f;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        targetPosition = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make sure they arent right next to each other, if they are then dont move
        if (Vector3.Distance(transform.position, targetPosition) > distanceToTarget)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
