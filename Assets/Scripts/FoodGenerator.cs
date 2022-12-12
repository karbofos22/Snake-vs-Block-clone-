using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject food;

    //private Vector3 spawnPoint;

    public Transform player;

    public bool canSpawn;
    public int totalFoodAtScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && totalFoodAtScene < 2)
        {
            Instantiate(food, SpawnPointGenerator(), food.transform.rotation);
            totalFoodAtScene++;
        }
    }
    Vector3 SpawnPointGenerator()
    {
        return new Vector3(Random.Range(-9, 9), player.transform.position.y + 23f, -0.6f);
    }
}
