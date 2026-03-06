using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public Collectables collectables;
    public TMP_Text coinText;
    void Start()
    {
        //coinText.text = Collectables.coins.ToString();
        coinText.text = Collectables.coins.ToString();
    }

    void Update()
    {
        //coinText.text = Collectables.coins.ToString();
        coinText.text = "Coins: " + Collectables.coins.ToString();
    }
}
