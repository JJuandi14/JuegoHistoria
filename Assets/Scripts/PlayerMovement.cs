using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input bruto para movimiento 4 direcciones (WASD / Flechas)
        movement.x = Input.GetAxisRaw("Horizontal"); // -1,0,1
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; // evita velocidad diagonal > 1
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
