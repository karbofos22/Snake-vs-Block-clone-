using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenFoodDestroyer : MonoBehaviour
{
    private FoodGenerator foodGenerator;
    public Transform player;

    private float offset = -35f;

    // Start is called before the first frame update
    void Start()
    {
        foodGenerator = GameObject.Find("FoodGenerator").GetComponent<FoodGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y + offset, gameObject.transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            foodGenerator.totalFoodAtScene--;
        }
    }
}
