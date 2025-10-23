using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelInstrucciones;   // Activo al iniciar
    public GameObject panelPausa;           // Oculto al iniciar
    public GameObject panelConfig;          // Oculto al iniciar (dummy)
    public GameObject panelGameOver;        // Opcional: si usas panel
    public GameObject panelVictoria;        // Opcional: si usas panel

    [Header("HUD (Opcional)")]
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI energiaText;
    public TextMeshProUGUI tiempoText;
    public TextMeshProUGUI energiaTotalText; // Para "X / total" si quieres

    [Header("Escenas")]
    public string nombreEscenaMenu = "Menu"; // Cambia si tu menú tiene otro nombre

    [Header("Coleccionables (Opcional)")]
    public string tagEnergy = "Energy";      // Para contar total al iniciar

    int totalEnergias = 0;

    void Awake()
    {
        // Asegurar que haya EventSystem en la escena
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            var es = new GameObject("EventSystem",
                typeof(UnityEngine.EventSystems.EventSystem),
                typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }
    }

    void Start()
    {
        // Estado inicial de paneles
        if (panelInstrucciones) panelInstrucciones.SetActive(true);
        if (panelPausa)         panelPausa.SetActive(false);
        if (panelConfig)        panelConfig.SetActive(false);
        if (panelGameOver)      panelGameOver.SetActive(false);
        if (panelVictoria)      panelVictoria.SetActive(false);

        // Pausar juego al arrancar
        Time.timeScale = 0f;

        // Contar total de energías si quieres mostrar "X / total"
        if (!string.IsNullOrEmpty(tagEnergy))
        {
            totalEnergias = GameObject.FindGameObjectsWithTag(tagEnergy).Length;
            if (energiaTotalText) energiaTotalText.text = totalEnergias.ToString();
        }
    }

    // ===== Botones =====

    // Panel de instrucciones → Comenzar
    public void ComenzarJuego()
    {
        if (panelInstrucciones) panelInstrucciones.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("ComenzarJuego()");
    }

    // Abrir pausa (botón fijo en HUD)
    public void AbrirPausa()
    {
        if (panelPausa) panelPausa.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("AbrirPausa()");
    }

    // Continuar desde pausa
    public void Continuar()
    {
        if (panelPausa)  panelPausa.SetActive(false);
        if (panelConfig) panelConfig.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Continuar()");
    }

    // Abrir panel de configuración (dummy)
    public void AbrirConfig()
    {
        if (panelConfig) panelConfig.SetActive(true);
        Debug.Log("AbrirConfig()");
    }

    // Cerrar panel de configuración (si lo usas en juego)
    public void CerrarConfig()
    {
        if (panelConfig) panelConfig.SetActive(false);
        Debug.Log("CerrarConfig()");
    }

    // Reiniciar nivel actual
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("ReiniciarNivel()");
    }

    // Volver al menú
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
        Debug.Log("VolverAlMenu()");
    }

    // ===== Métodos para actualizar HUD desde PlayerMovement (opcional) =====

    public void SetVidas(int v)
    {
        if (vidasText) vidasText.text = "Vidas: " + v;
    }

    public void SetEnergia(int e)
    {
        if (energiaText) energiaText.text = "Energía: " + e;
        if (energiaTotalText) energiaTotalText.text = e + " / " + totalEnergias;
    }

    public void SetTiempo(string t)
    {
        if (tiempoText) tiempoText.text = t;
    }

    // Mostrar paneles de fin si quieres controlarlos desde PlayerMovement
    public void MostrarGameOver()
    {
        if (panelGameOver) panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MostrarVictoria()
    {
        if (panelVictoria) panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }
}
