using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public int coinCount;
   public TextMeshProUGUI cointText;
   private void Awake()
   {
        instance = this;
   }
   private void Start() 
   {
        UpdateUI();
   }
   public void CollectCoin(int amount)
   {
        coinCount += amount;
        UpdateUI();
    }
    public void UpdateUI()
    {
        cointText.text = "x" + coinCount;
    }
}
