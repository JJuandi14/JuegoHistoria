using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // El carro
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public SpriteRenderer mapRenderer; // Asigna aquí el sprite del circuito en el Inspector

    float minX, maxX, minY, maxY;

    void Start()
    {
        // Tamaño del mapa en mundo
        float mapWidth = mapRenderer.bounds.size.x;
        float mapHeight = mapRenderer.bounds.size.y;

        // Tamaño visible de la cámara
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Screen.width / Screen.height;

        // Calcular límites para que no se salga de los bordes
        minX = -mapWidth / 2f + camWidth / 2f;
        maxX =  mapWidth / 2f - camWidth / 2f;
        minY = -mapHeight / 2f + camHeight / 2f;
        maxY =  mapHeight / 2f - camHeight / 2f;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Posición deseada con suavizado
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Restringir la cámara dentro de los límites
            float clampX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }
    }
}
