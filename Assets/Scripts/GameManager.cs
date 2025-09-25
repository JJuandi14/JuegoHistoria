using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text coinsText;
    public Text gemsText;
    public int coins = 0;
    public int gems = 0;

    public void AddPoints(string type, int points)
    {
        if(type == "Moneda")
            coins += points;
        else if(type == "Gema")
            gems += points;

        UpdateUI();
    }

    void UpdateUI()
    {
        coinsText.text = "Monedas: " + coins;
        gemsText.text = "Gemas: " + gems;
    }
}
