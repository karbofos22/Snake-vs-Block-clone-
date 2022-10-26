using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public int foodAmount;
    public TextMeshProUGUI foodAmountText;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        foodAmount = Random.Range(2, 5);
        foodAmountText.text = $"{foodAmount}";
    }
}
