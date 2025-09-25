using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10; // Cambiar según tipo
    public string type; // "Moneda" o "Gema"
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gm.AddPoints(type, points);
            Destroy(gameObject);
        }
    }
}
