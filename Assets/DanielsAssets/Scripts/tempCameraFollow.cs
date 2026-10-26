using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player or object we are following
    public BoxCollider2D boundaryBox; // The area we are allowed to move in
    public float smoothTime = 0.0f; // How long it takes to catch up to the target
    private float minX, maxX, minY, maxY; // The bounds of the box
    private Vector3 velocity = Vector3.zero;
    private float fixedZ;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep initial camera depth
        fixedZ = transform.position.z;
        cam = GetComponent<Camera>();
        if (boundaryBox != null)
        {
            CalculateBoundaries();
        }
    }
    void CalculateBoundaries()
    {
        float boxMinX = boundaryBox.bounds.min.x;
        float boxMaxX = boundaryBox.bounds.max.x;
        float boxMinY = boundaryBox.bounds.min.y;
        float boxMaxY = boundaryBox.bounds.max.y;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        minX = boxMinX + camWidth;
        maxX = boxMaxX - camWidth;
        minY = boxMinY + camHeight;
        maxY = boxMaxY - camHeight;
    }

    // LateUpdate is called after update so we can do this after player moves
    void LateUpdate()
    {
        if (target==null) return; // No point wasting resources

        float targetX = target.position.x;
        float targetY = target.position.y;

        targetX = Mathf.Clamp(targetX, minX, maxX);
        targetY = Mathf.Clamp(targetY, minY, maxY);

        Vector3 boundedTargetPosition = new Vector3(targetX, targetY, fixedZ);

        // Glide there
        transform.position = Vector3.SmoothDamp(transform.position, boundedTargetPosition, ref velocity, smoothTime);
    }
}
