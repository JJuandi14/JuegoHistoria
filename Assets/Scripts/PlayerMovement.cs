using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 8f;          // Velocidad base del carro
    public float rotationSpeed = 200f;    // Velocidad de giro
    public float speedBoost = 0.5f;       // Cuánto aumenta la velocidad por cada rayito
    public float maxSpeed = 15f;          // 👈 límite de velocidad máxima

    [Header("Límites")]
    public int maxBananas = 3;            // Límite de bananas antes de perder
    private int vidas;                    
    private int energia = 0;              

    [Header("UI")]
    public GameObject GameOverText;
    public GameObject WinText;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI energiaText;
    public TextMeshProUGUI tiempoText;

    private Rigidbody2D rb;
    private float moveInput;
    private float rotationInput;
    private int bananaCount = 0;

    // Cronómetro
    private float tiempo = 0f;
    private bool corriendo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        vidas = 3;

        // Ocultar textos de Game Over y Win al inicio
        if (GameOverText != null) GameOverText.SetActive(false);
        if (WinText != null) WinText.SetActive(false);

        ActualizarUI();
    }

    void Update()
    {
        // ⏱️ Contador de tiempo
        if (corriendo)
        {
            tiempo += Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);

            if (tiempoText != null)
                tiempoText.text = string.Format("Tiempo: {0:00}:{1:00}", minutos, segundos);
        }

        // Movimiento con WASD
        if (Input.GetKey(KeyCode.W))
            moveInput = 1;
        else if (Input.GetKey(KeyCode.S))
            moveInput = -1;
        else
            moveInput = 0;

        if (Input.GetKey(KeyCode.A))
            rotationInput = 1;
        else if (Input.GetKey(KeyCode.D))
            rotationInput = -1;
        else
            rotationInput = 0;
    }

    void FixedUpdate()
    {
        Vector2 forward = -transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forward);

        float rotation = rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ⚡ Energía
        if (collision.CompareTag("Energy"))
        {
            energia++;

            // Solo aumentar velocidad si no ha llegado al límite
            if (moveSpeed < maxSpeed)
            {
                moveSpeed = Mathf.Min(moveSpeed + speedBoost, maxSpeed);
            }

            ActualizarUI();
            Destroy(collision.gameObject);
        }

        // 🍌 Banana
        if (collision.CompareTag("Banana"))
        {
            bananaCount++;
            vidas--;
            ActualizarUI();

            if (vidas <= 0)
            {
                if (GameOverText != null) GameOverText.SetActive(true);
                corriendo = false; // parar cronómetro
                Time.timeScale = 0f;
            }

            Destroy(collision.gameObject);
        }

        // 🏁 Meta
        if (collision.CompareTag("Finish"))
        {
            if (WinText != null) WinText.SetActive(true);
            corriendo = false; // parar cronómetro
            Time.timeScale = 0f;
        }
    }

    void ActualizarUI()
    {
        if (vidasText != null)
            vidasText.text = "Vidas: " + vidas;

        if (energiaText != null)
            energiaText.text = "Energía: " + energia;
    }
}
