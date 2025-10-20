using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelCreditos;
    public GameObject panelConfig;

    public string MainScene = "MainScene"; // cambia si tu escena se llama distinto

    void Start()
    {
        Time.timeScale = 1f; // por si vienes de una pausa
        if (panelCreditos) panelCreditos.SetActive(false);
        if (panelConfig) panelConfig.SetActive(false);
    }

    // Botón Jugar
    public void Jugar()
    {
        SceneManager.LoadScene(MainScene);
    }

    // Botones Créditos
    public void AbrirCreditos() { if (panelCreditos) panelCreditos.SetActive(true); }
    public void CerrarCreditos() { if (panelCreditos) panelCreditos.SetActive(false); }

    // Botones Config
    public void AbrirConfig() { if (panelConfig) panelConfig.SetActive(true); }
    public void CerrarConfig() { if (panelConfig) panelConfig.SetActive(false); }

    // Botón Salir (funciona en build)
    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

