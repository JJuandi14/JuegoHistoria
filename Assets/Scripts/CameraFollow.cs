using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // arrastra el Player aquí
    public float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        if (target == null) return;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target == null) return;
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
    }
}
