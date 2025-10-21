using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelInstrucciones; // activo al inicio
    public GameObject panelPausa;         // desactivado al inicio
    public GameObject panelConfig;        // desactivado al inicio (dummy)
    public GameObject panelGameOver;      // si usas panel en vez de sólo texto
    public GameObject panelVictoria;      // si usas panel en vez de sólo texto

    [Header("HUD")]
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI energiaText;
    public TextMeshProUGUI tiempoText;
    public TextMeshProUGUI energiaTotalText; // opcional "X/Y"

    [Header("Tags & Conteo")]
    public string tagEnergy = "Energy";
    private int totalEnergias = 0;

    void Start()
    {
        Time.timeScale = 0f; // empieza en pausa
        if (panelInstrucciones) panelInstrucciones.SetActive(true);
        if (panelPausa) panelPausa.SetActive(false);
        if (panelConfig) panelConfig.SetActive(false);
        if (panelGameOver) panelGameOver.SetActive(false);
        if (panelVictoria) panelVictoria.SetActive(false);

        // Contar coleccionables para mostrar "X / total"
        totalEnergias = GameObject.FindGameObjectsWithTag(tagEnergy).Length;
        if (energiaTotalText) energiaTotalText.text = totalEnergias.ToString();
    }

    // Botón "Comenzar" en Panel de Instrucciones
    public void ComenzarJuego()
    {
        if (panelInstrucciones) panelInstrucciones.SetActive(false);
        Time.timeScale = 1f;
    }

    // ---- Menú de Pausa ----
    public void AbrirPausa()
    {
        if (panelPausa) panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        if (panelPausa) panelPausa.SetActive(false);
        if (panelConfig) panelConfig.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AbrirConfig()
    {
        if (panelConfig) panelConfig.SetActive(true);
    }

    public void CerrarConfig()
    {
        if (panelConfig) panelConfig.SetActive(false);
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // cambia si tu menú tiene otro nombre
    }

    // Helpers opcionales para actualizar HUD desde PlayerMovement
    public void SetVidas(int v) { if (vidasText) vidasText.text = "Vidas: " + v; }
    public void SetEnergia(int e)
    {
        if (energiaText) energiaText.text = "Energía: " + e;
        if (energiaTotalText) energiaTotalText.text = e + " / " + totalEnergias;
    }
    public void SetTiempo(string t) { if (tiempoText) tiempoText.text = t; }

    // Mostrar paneles de fin (si quieres usarlos desde PlayerMovement)
    public void MostrarGameOver()
    {
        if (panelGameOver) panelGameOver.SetActive(true);
    }
    public void MostrarVictoria()
    {
        if (panelVictoria) panelVictoria.SetActive(true);
    }
}
