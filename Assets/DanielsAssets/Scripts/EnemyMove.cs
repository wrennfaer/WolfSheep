using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 0.3f;
    public float detectionRange = 5f;
    public LayerMask wallMask;
    Vector3 targetPosition;
    Rigidbody2D rb;
    enum State {Idle, Chase, Attack}
    State state = State.Idle;
    GameObject player;
    bool isTouchingPlayer = false;
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
        else return;
        bool seePlayer = CanSeePlayer();
        // Make sure they arent right next to each other, if they are then dont move
        if (isTouchingPlayer)
        {
            state = State.Attack;
        }
        else if(Vector3.Distance(transform.position, targetPosition) < detectionRange && seePlayer)
        {
            state = State.Chase;
        }
        else
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
                Debug.Log("Player attacked");

                break;
            case State.Chase:
                MoveTowardsTarget();
                break;
            case State.Idle:
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            Destroy(collision.gameObject);
            isTouchingPlayer = false;
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

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition, wallMask);
        bool touchWall = hit.collider == null;
        Color debugColor = touchWall ? Color.green : Color.red;
        Debug.DrawLine(transform.position, targetPosition, debugColor);
        return touchWall;
    }
}
