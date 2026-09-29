using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Reference to the UI text element that displays the player's money
    public TextMeshProUGUI waveText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$: " + GameManager.Instance.money; // Update the money text in the UI to reflect the current amount of money
        waveText.text = GameManager.Instance.currentWave.ToString();
    }
}
