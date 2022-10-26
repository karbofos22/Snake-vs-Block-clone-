using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject blocksSet;
    public GameObject smallObstacle;
    public GameObject bigObstacle;

    public int blockSetsAtLevel = 7;

    Vector3 blocksStartPoint = new (-2.20f, 100f, -8.30f);
    Vector2 obstacleStartPoint = new(0, 75f);
    Vector2 obstacleStartPointSecond = new(0, 28f);

    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BuildLevel();
    }
    private void Update()
    {
        if (gameManager.isNextLevel)
        {
            NextLevelSetUp();
        }
    }
    public void BuildLevel()
    {
        for (int i = 0; i < blockSetsAtLevel; i++)
        {
            obstacleStartPoint.x = Random.Range(-6, 6f);
            obstacleStartPointSecond.x = Random.Range(-6, 6f);

            Instantiate(blocksSet, blocksStartPoint, Quaternion.identity, transform);
            Instantiate(smallObstacle, obstacleStartPoint, Quaternion.identity, transform);
            Instantiate(bigObstacle, obstacleStartPointSecond, Quaternion.identity, transform);

            blocksStartPoint.y += 100;
            obstacleStartPoint.y += 100;
            obstacleStartPointSecond.y += 100;
        }
    }
    void NextLevelSetUp()
    {
        Destroy(GameObject.FindWithTag("Block"));
        Destroy(GameObject.FindWithTag("Obstacle"));

        blocksStartPoint = new(-2.20f, 100f, -8.30f);
        obstacleStartPoint = new(0, 75f);
        obstacleStartPointSecond = new(0, 28f);
    }
}
