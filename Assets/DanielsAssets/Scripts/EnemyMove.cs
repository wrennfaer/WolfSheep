using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 targetPosition;
    Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float stopDistance = 0.3f;
    public float detectionRange = 5f;
    enum State {Idle, Chase, Attack}
    State state = State.Idle;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    // Calculate where player is
    void Update()
    {
        if (player != null)
            targetPosition = player.transform.position;
        // Make sure they arent right next to each other, if they are then dont move
        if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
        {
            state = State.Attack;
        }
        else if (Vector3.Distance(transform.position, targetPosition) < detectionRange)
        {
            state = State.Chase;
        } else
        {
            state = State.Idle;
        }
    }

    // Move to player
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Attack:
                break;
            case State.Chase:
                MoveTowardsTarget();
                break;
            case State.Idle:
            default:
                break;
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
