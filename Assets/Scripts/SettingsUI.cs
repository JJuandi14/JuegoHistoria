using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("UI")]
    public Slider sldVolumen;          
    public Toggle tglMusica;           
    public TextMeshProUGUI idiomaText; 

    [Header("Audio (opcional)")]
    public AudioSource musica; // arrastra un AudioSource si tienes música de fondo

    private string idiomaActual = "ES";

    void Start()
    {
        if (sldVolumen != null)
        {
            sldVolumen.minValue = 0f;
            sldVolumen.maxValue = 1f;
            sldVolumen.value = 1f;
            sldVolumen.onValueChanged.AddListener(OnVolumenCambiado);
        }

        if (tglMusica != null)
        {
            tglMusica.isOn = true;
            tglMusica.onValueChanged.AddListener(OnMusicaToggle);
        }

        if (idiomaText != null) idiomaText.text = "Idioma: " + idiomaActual;
        OnVolumenCambiado(sldVolumen != null ? sldVolumen.value : 1f);
    }

    public void OnVolumenCambiado(float v) { AudioListener.volume = Mathf.Clamp01(v); }
    public void OnMusicaToggle(bool activo) { if (musica != null) musica.mute = !activo; }
    public void SetIdiomaES() { idiomaActual = "ES"; if (idiomaText) idiomaText.text = "Idioma: ES"; }
    public void SetIdiomaEN() { idiomaActual = "EN"; if (idiomaText) idiomaText.text = "Idioma: EN"; }
}
