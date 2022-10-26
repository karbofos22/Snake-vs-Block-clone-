using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockStrengthAmount;
    public TextMeshProUGUI blockStrengthAmountText;
    private Material material;

    private float gradChangeValue;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;


        blockStrengthAmount = Random.Range(10, 25);

        if (blockStrengthAmount > 0 && blockStrengthAmount <= 15)
        {
            gradChangeValue = 0.01f;
        }
        else
        {
            gradChangeValue = 0.1f;
        }

        material.SetFloat("_Gradient_change", blockStrengthAmount * gradChangeValue);
        blockStrengthAmountText.text = $"{blockStrengthAmount}";
    }
}
