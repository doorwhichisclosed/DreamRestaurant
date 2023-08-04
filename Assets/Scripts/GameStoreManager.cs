using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class GameStoreManager : MonoBehaviour
{
    public TextMeshProUGUI orderText;
    public GameObject orderPage;
    private List<Dictionary<string, object>> orderSentences;
    // Start is called before the first frame update
    async void Start()
    {
        if (orderSentences == null)
        {
            orderSentences = new List<Dictionary<string, object>>();
            orderSentences = await UtilClasses.CSVReader.Read("OrderSentences");
        }
    }
}
