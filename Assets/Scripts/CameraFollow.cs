using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 smoothedPostition = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        transform.position = smoothedPostition;

        transform.LookAt(target);
    }
}
