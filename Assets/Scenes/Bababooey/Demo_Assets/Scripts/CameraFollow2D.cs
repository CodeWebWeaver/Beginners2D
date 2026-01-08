using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    public Vector3 minValue = new(-9, -23, float.NegativeInfinity), maxValue = new(45, 13, float.PositiveInfinity);

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z; // keep camera Z
        desiredPosition = Vector3.Min(maxValue, Vector3.Max(minValue, desiredPosition));
        

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
