using UnityEngine;
using TMPro;

public class Interfaz : MonoBehaviour
{
    [Header("Textos UI")]
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI energyText;

    private int bananas = 0;
    private int energy = 0;

    void Start()
    {
        ActualizarUI();
    }

    public void AgregarBanana(int cantidad)
    {
        bananas += cantidad;
        ActualizarUI();
    }

    public void AgregarEnergia(int cantidad)
    {
        energy += cantidad;
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (bananaText != null)
            bananaText.text = "Vidas: " + bananas;

        if (energyText != null)
            energyText.text = "Energía: " + energy;
    }
}
