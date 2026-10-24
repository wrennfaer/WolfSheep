using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 0.3f;
    public float detectionRange = 5f;
    public LayerMask wallMask;
    public float searchDuration = 3f;
    Vector3 targetPosition;
    Rigidbody2D rb;
    enum State {Idle, Chase, Search, Attack}
    State state = State.Idle;
    GameObject player;
    Vector3 lastKnownPos;
    bool hasLastKnown = false;
    bool isTouchingPlayer = false;
    bool hasArrived = false;
    float searchTimer;
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
        else
        {
            state = State.Idle;
            return;
        }
        bool seePlayer = CanSeePlayer();
        // Make sure they arent right next to each other, if they are then dont move
        if (isTouchingPlayer)
        {
            state = State.Attack;
        }
        else if(Vector3.Distance(transform.position, targetPosition) < detectionRange && seePlayer)
        {
            state = State.Chase;
            lastKnownPos = targetPosition;
            hasLastKnown = true;
            searchTimer = searchDuration;
        }
        else if (hasLastKnown)
        {
            state = State.Search;
            
            if (Vector3.Distance(transform.position, lastKnownPos) <= stopDistance)
                hasArrived = true;
            else 
                hasArrived = false;
            if (hasArrived)
            {
                searchTimer -= Time.deltaTime;
                if (searchTimer <= 0)
                    hasLastKnown = false;
            }
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
                // Animation here
                // Player die script here
                isTouchingPlayer = false;
                break;
            case State.Chase:
                MoveTowardsTarget(targetPosition);
                break;
            case State.Search:
                if (!hasArrived)
                    MoveTowardsTarget(lastKnownPos);
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
        }
    }

    void MoveTowardsTarget(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
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
        bool clearPath = hit.collider == null;
        Color debugColor = clearPath ? Color.green : Color.red;
        Debug.DrawLine(transform.position, targetPosition, debugColor);
        return clearPath;
    }
}
