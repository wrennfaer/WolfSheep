
using UnityEngine;

public class cameraScene : MonoBehaviour
{
    public Transform sheep;

    public Vector3 offset = new Vector3(0f, 3f, -5f);

    public float followSpeed = 8f;
    public float rotationSpeed = 8f;

    void LateUpdate()
    {
        if (sheep == null)
            return;

        Vector3 targetPosition =
            sheep.position + sheep.rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Mathf.Clamp01(followSpeed * Time.deltaTime)
        );

        Vector3 direction = sheep.position - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Mathf.Clamp01(rotationSpeed * Time.deltaTime)
            );
        }
    }
}
