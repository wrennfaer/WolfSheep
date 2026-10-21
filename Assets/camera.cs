
using UnityEngine;

public class cameraScene : MonoBehaviour
{
    public Transform sheep;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (sheep == null)
            return;

        Vector3 targetPosition = sheep.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}
