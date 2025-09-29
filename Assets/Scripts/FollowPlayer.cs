using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;   // El carro
    public Vector3 offset;     // Distancia de la cámara al carro

    [Header("Límites del Fondo")]
    public Transform background; // El objeto del fondo
    private float minX, maxX, minY, maxY;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        // Calcular tamaño visible de la cámara
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        // Calcular límites del fondo usando su escala
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        float bgWidth = bgRenderer.bounds.size.x;
        float bgHeight = bgRenderer.bounds.size.y;

        minX = background.position.x - bgWidth / 2f + camHalfWidth;
        maxX = background.position.x + bgWidth / 2f - camHalfWidth;
        minY = background.position.y - bgHeight / 2f + camHalfHeight;
        maxY = background.position.y + bgHeight / 2f - camHalfHeight;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Posición deseada de la cámara
            Vector3 desiredPosition = player.position + offset;

            // Restringir dentro de los límites
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

            transform.position = new Vector3(clampedX, clampedY, desiredPosition.z);
        }
    }
}
