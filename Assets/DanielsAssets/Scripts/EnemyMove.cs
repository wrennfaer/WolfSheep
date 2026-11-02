using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public LayerMask wallMask;
    public float moveSpeed = 3f;
    public float wanderSpeedMulti = .75f;
    public float stopDistance = 0.3f;
    public float detectionRange = 10f;
    public float searchDuration = 3f;
    public float idleDuration = 3f;
    public float lookAhead = 0.5f;
    public float baddieThiccness = 0.4f;
    public float maxAngle = 90f;
    public float angleStep = 15f;
    public float wanderRadius = 5f;
    public int wanderAttempts = 10;
    Vector3 targetPosition;
    Vector3 wanderPosition;
    Vector3 homePosition;
    Rigidbody2D rb;
    enum State {Idle, Wander, Chase, Search, Attack}
    State state = State.Idle;
    GameObject player;
    Vector3 lastKnownPos;
    Vector3 lastChosenDirection;
    bool hasLastKnown = false;
    bool isTouchingPlayer = false;
    bool hasArrived = true;
    bool wandering = false;
    int prefSide = 1;
    const float speedMultiplier = 1f;
    float searchTimer;
    float idleTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        wanderPosition = transform.position;
        homePosition = transform.position;
        idleTimer = idleDuration;
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
            CheckHasArrived(targetPosition);
            lastKnownPos = targetPosition;
            hasLastKnown = true;
            searchTimer = searchDuration;
        }
        else if (hasLastKnown)
        {
            state = State.Search;
            
            CheckHasArrived(lastKnownPos);
            
            if (hasArrived)
            {
                searchTimer -= Time.deltaTime;
                if (searchTimer <= 0)
                    hasLastKnown = false;
            }
        }
        else if (wandering)
        {
            CheckHasArrived(wanderPosition);
            if (hasArrived)
            {
                state = State.Idle;
                wandering = false;
            }
            else
                state = State.Wander;
            idleTimer = idleDuration;
        }
        else
        {
            state = State.Idle;
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                if (PickWanderPos())
                    wandering = true;
                else
                    idleTimer = idleDuration;
            }

        }
    }

    // Move to player
    void FixedUpdate()
    {
        DebugLines();
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
            case State.Wander:
                if (!hasArrived)
                    MoveTowardsTarget(wanderPosition, wanderSpeedMulti);
                break;
            case State.Idle:
            default:
                prefSide = 1;
                break;
        }
    }

    void DebugLines()
    {
        Debug.DrawLine(transform.position, lastKnownPos, Color.blue);
        Debug.DrawLine(transform.position, wanderPosition, Color.orange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    void MoveTowardsTarget(Vector3 targetPos, float speedMulti = speedMultiplier)
    {
        lastChosenDirection = (targetPos - transform.position).normalized;
        lastChosenDirection = PickDirection(lastChosenDirection);
        
        rb.MovePosition(transform.position + lastChosenDirection * (moveSpeed * speedMulti) * Time.fixedDeltaTime);
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

    void CheckHasArrived(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) <= stopDistance)
            hasArrived = true;
        else 
            hasArrived = false;
    }

    bool PickWanderPos()
    {
        Vector3 tempPos;
        for (int i = 0; i < wanderAttempts; i++)
        {
            tempPos = (Vector2)homePosition + (Random.insideUnitCircle * wanderRadius);
            if (!Physics2D.OverlapCircle(tempPos, baddieThiccness, wallMask) && 
                Vector3.Distance(transform.position, tempPos) >= stopDistance && 
                !Physics2D.Linecast(transform.position, tempPos, wallMask))
            {
                wanderPosition = tempPos;
                return true;
            }
        }
        return false;
    }

    bool IsClear(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, baddieThiccness, dir, lookAhead, wallMask);
        return hit.collider == null;
    }
}
