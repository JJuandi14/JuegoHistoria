using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;          // Velocidad de avance/reversa
    public float rotationSpeed = 200f;    // Velocidad de giro
    private Rigidbody2D rb;

    [Header("UI")]
    public TextMeshProUGUI scoreText;     // Referencia al UI de puntaje
    private int score = 0;

    [Header("Meta")]
    public GameObject winPanel;           // Panel opcional para mostrar "Ganaste"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Evitar que el carro rote por físicas
        rb.freezeRotation = true;
        rb.angularVelocity = 0f; // resetear cualquier giro extraño al inicio

        UpdateScoreUI();
    }

    void Update()
    {
        // Input de movimiento
        float moveInput = Input.GetAxis("Vertical");        // W/S o ↑↓
        float rotationInput = -Input.GetAxis("Horizontal"); // A/D o ←→ (el "-" es importante)

        // Movimiento hacia adelante / atrás
        Vector2 forward = transform.up * moveInput * moveSpeed;
        rb.linearVelocity = forward;

        // Rotación controlada
        rb.MoveRotation(rb.rotation + rotationInput * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Coleccionable: energía
        if (other.CompareTag("Energy"))
        {
            score++;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }

        // Obstáculo: banana
        if (other.CompareTag("Banana"))
        {
            Debug.Log("¡Pisaste una cáscara!");
            score = Mathf.Max(0, score - 1); // restar sin ir negativo
            UpdateScoreUI();
        }

        // Meta: fin de la carrera
        if (other.CompareTag("Finish"))
        {
            Debug.Log("¡Ganaste la carrera!");
            if (winPanel != null) winPanel.SetActive(true);
            Time.timeScale = 0f; // pausa el juego
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score.ToString();
        }
    }
}
