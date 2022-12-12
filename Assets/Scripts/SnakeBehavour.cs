using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeBehavour : MonoBehaviour
{
    public GameObject head;
    public GameObject tail;
    private GameObject newSnakePart;
    public List<GameObject> snakeParts = new();
    public List<Vector2> positions = new();

    public TextMeshPro snakeLengthText;
    public ParticleSystem collisionParticle;
    public ParticleSystem deathParticle;

    const float snakePartDiameter = 1f;

    public int snakeLength = 5;

    public int increaseAmount;
    public int decreaseAmount;

    private FoodGenerator foodGenerator;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        foodGenerator = GameObject.Find("FoodGenerator").GetComponent<FoodGenerator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        SnakeCreate();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            SnakeBodyControl();
            SnakeLengthTextUpdate();
        }
        DestroySnakePartsIfNextLevel();

    }
    public void SnakeCreate()
    {
        positions.Add(head.transform.position);

        for (int i = 1; i < snakeLength; i++)
        {
            newSnakePart = Instantiate(tail, new Vector2(0, positions[positions.Count - 1].y - snakePartDiameter), Quaternion.identity, transform);
            snakeParts.Add(newSnakePart);
            positions.Add(newSnakePart.transform.position);
        }
    }
    void SnakeIncrease()
    {
        for (; increaseAmount > 0; increaseAmount--)
        {
            newSnakePart = Instantiate(tail, new Vector2(positions[positions.Count - 1].x - snakePartDiameter, positions[positions.Count - 1].y - snakePartDiameter), Quaternion.identity, transform);
            snakeParts.Add(newSnakePart);
            positions.Add(newSnakePart.transform.position);
            snakeLength++;
        }
    }
    void SnakeDecrease()
    {
        for (; decreaseAmount > 0; decreaseAmount--)
        {
            if (snakeParts.Count == 0)
            {
                gameManager.isGameOver = true;
                audioSource.Play();
                deathParticle.Play();
                head.GetComponent<MeshRenderer>().enabled = false;

                break;
            }
            else
            {
                GameObject partToDsetroy = snakeParts[snakeParts.Count - 1];
                Destroy(partToDsetroy);
                snakeParts.RemoveAt(snakeParts.Count - 1);
                positions.RemoveAt(positions.Count - 1);
                snakeLength--;
            }
        }
    }
    void SnakeBodyControl()
    {
        float distance = ((Vector2)head.transform.position - positions[0]).magnitude;
        if (distance > snakePartDiameter)
        {
            Vector2 direction = ((Vector2)head.transform.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * snakePartDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= snakePartDiameter;
        }
        for (int i = 0; i < snakeParts.Count; i++)
        {
            snakeParts[i].transform.position = Vector2.Lerp(positions[i + 1], positions[i], distance / snakePartDiameter);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            increaseAmount = collision.gameObject.GetComponent<Food>().foodAmount;
            SnakeIncrease();

            Destroy(collision.gameObject);
            foodGenerator.totalFoodAtScene--;
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            decreaseAmount = collision.gameObject.GetComponent<Block>().blockStrengthAmount;

            SnakeDecrease();
            if (decreaseAmount == 0)
            {
                collisionParticle.Play();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            gameManager.isGameActive = false;
            gameManager.isNextLevel = true;
        }
    }
    void SnakeLengthTextUpdate()
    {
        snakeLengthText.text = $"{snakeLength}";
    }
    void DestroySnakePartsIfNextLevel()
    {
        if (gameManager.isNextLevel)
        {
            foreach (var item in snakeParts)
            {
                Destroy(item);
            }
            snakeParts.Clear();
            positions.Clear();
            snakeLength = 5;
            foodGenerator.totalFoodAtScene = 0;
        }
    }
}
