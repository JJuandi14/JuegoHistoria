using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    private Rigidbody2D rb;

    [Header("UI")]
    public Text scoreText;
    public Text messageText;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateScoreUI();
        messageText.text = "";
    }

    void Update()
    {
        // Movimiento hacia adelante/atrás
        float moveInput = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo
        // Giro
        float turnInput = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha

        // Mover el carro
        rb.linearVelocity = transform.up * moveInput * moveSpeed;
        rb.angularVelocity = -turnInput * turnSpeed * Mathf.Deg2Rad;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Energy")) // Rayitos de energía
        {
            score += 1;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Banana")) // Cáscaras de banana
        {
            // Opcional: reiniciar nivel o reducir velocidad temporalmente
            // Ejemplo: reiniciar nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(other.CompareTag("Finish")) // Meta / línea de llegada
        {
            messageText.text = "¡Ganaste!";
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            this.enabled = false; // Bloquear movimiento
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Rayitos: " + score;
    }
}
