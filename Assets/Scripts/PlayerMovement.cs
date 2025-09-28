using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;          // Velocidad base del carro
    public float rotationSpeed = 200f;    // Velocidad de giro
    public float speedBoost = 3f;         // Cuánto aumenta la velocidad por cada rayito
    public int maxBananas = 3;            // Límite de bananas antes de perder

    private Rigidbody2D rb;
    private float moveInput;
    private float rotationInput;
    private int bananaCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Controles de movimiento
        moveInput = Input.GetAxis("Vertical");   // Flechas ↑↓ o W/S
        rotationInput = -Input.GetAxis("Horizontal"); // Flechas ←→ o A/D
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante/atrás (invertido corregido con "-")
        Vector2 forward = transform.up * (-moveInput) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forward);

        // Rotación
        float rotation = rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ⚡ Energía (tag = "Energy")
        if (collision.CompareTag("Energy"))
        {
            moveSpeed += speedBoost; // Aumenta la velocidad acumulativamente
            Destroy(collision.gameObject);
        }

        // 🍌 Banana (tag = "Banana")
        if (collision.CompareTag("Banana"))
        {
            bananaCount++;
            Debug.Log("Pisaste una banana: " + bananaCount);

            if (bananaCount >= maxBananas)
            {
                Debug.Log("¡Perdiste! Reiniciando...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
