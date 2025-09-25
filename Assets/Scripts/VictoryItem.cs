using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryItem : MonoBehaviour
{
    public Text message; // Texto de mensaje de victoria en UI

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            message.text = "¡Nivel completado!";
            // Opcional: bloquear movimiento
            other.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
