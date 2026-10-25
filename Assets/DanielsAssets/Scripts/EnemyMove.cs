using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public LayerMask wallMask;
    public float moveSpeed = 3f;
    public float stopDistance = 0.3f;
    public float detectionRange = 5f;
    public float searchDuration = 3f;
    public float lookAhead = 0.5f;
    public float baddieThiccness = 0.4f;
    public float maxAngle = 90f;
    public float angleStep = 15f;
    Vector3 targetPosition;
    Rigidbody2D rb;
    enum State {Idle, Chase, Search, Attack}
    State state = State.Idle;
    GameObject player;
    Vector3 lastKnownPos;
    Vector3 lastChosenDirection;
    bool hasLastKnown = false;
    bool isTouchingPlayer = false;
    bool hasArrived = false;
    int prefSide = 1;
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
        // What state we in
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
                prefSide = 1;
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
        lastChosenDirection = (targetPos - transform.position).normalized;
        lastChosenDirection = PickDirection(lastChosenDirection);
        Debug.DrawLine(transform.position, lastKnownPos, Color.blue);

        rb.MovePosition(transform.position + lastChosenDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position + lastChosenDirection * lookAhead, baddieThiccness);
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition, wallMask);
        bool clearPath = hit.collider == null;
        Color debugColor = clearPath ? Color.green : Color.red;
        Debug.DrawLine(transform.position, targetPosition, debugColor);
        return clearPath;
    }

    Vector3 PickDirection(Vector3 desiredDir)
    {
        Debug.DrawRay(transform.position, desiredDir * lookAhead, Color.hotPink);
        if(IsClear(desiredDir))
            return desiredDir;
        float angle = angleStep;
        for (; angle <= maxAngle; angle+=angleStep)
        {
            Vector3 testAng;
            if (IsClear(testAng = Quaternion.Euler(0,0, angle*prefSide) * desiredDir))
                return testAng;
            if (IsClear(testAng = Quaternion.Euler(0,0, -angle*prefSide) * desiredDir))
            {
                prefSide = -prefSide;
                return testAng;
            }
        }
        return Vector3.zero;
    }

    bool IsClear(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, baddieThiccness, dir, lookAhead, wallMask);
        return hit.collider == null;
    }
}
