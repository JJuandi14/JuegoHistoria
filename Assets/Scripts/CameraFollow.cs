using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // El jugador que sigue la cámara
    public Transform background;  // El fondo (sprite del mapa)

    private float minX, maxX, minY, maxY;
    private float camHalfHeight, camHalfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * Screen.width / Screen.height;

        // Obtener tamaño del sprite del fondo
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        float bgWidth = sr.bounds.size.x;
        float bgHeight = sr.bounds.size.y;

        // Calcular límites
        minX = background.position.x - bgWidth / 2 + camHalfWidth;
        maxX = background.position.x + bgWidth / 2 - camHalfWidth;
        minY = background.position.y - bgHeight / 2 + camHalfHeight;
        maxY = background.position.y + bgHeight / 2 - camHalfHeight;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            float targetX = Mathf.Clamp(player.position.x, minX, maxX);
            float targetY = Mathf.Clamp(player.position.y, minY, maxY);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}
