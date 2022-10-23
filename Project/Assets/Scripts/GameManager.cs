using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isGameOver;
    public bool isNextLevel;

    private int level;
    [HideInInspector]

    public Image titleScreen;
    public Image restartScreen;
    public Image winScreen;

    public TextMeshProUGUI restartLevelText;
    public TextMeshProUGUI winLevelText;

    private SnakeBehavour snakeBehavour;
    private LevelGenerator levelGenerator;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        snakeBehavour = GameObject.Find("SnakeHead").GetComponent<SnakeBehavour>();
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        NextLevelCheck();
        GameOverCheck();
        LevelUpdate();
        KillFoodIfNextLevel();
    }
    private void GameOverCheck()
    {
        if (isGameOver)
        {
            isGameActive = false;
            restartScreen.gameObject.SetActive(true);
        }
    }
    private void NextLevelCheck()
    {
        if (isNextLevel)
        {
            isGameActive = false;
            winScreen.gameObject.SetActive(true);
        }
    }
    void LevelUpdate()
    {
        restartLevelText.text = $"Shame, but snake died...\nLevel reached {level}";
        winLevelText.text = $"Congratulations,\r\nLevel {level} completed!";
    }
    public void GameStart()
    {
        isGameOver = false;
        isNextLevel = false;
        level = 1;

        titleScreen.gameObject.SetActive(false);

        audioSource.Play();
        isGameActive = true;
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevelSetUp()
    {
        snakeBehavour.head.transform.position = new Vector2(0,0);
        snakeBehavour.SnakeCreate();
        levelGenerator.BuildLevel();

        level += 1;

        winScreen.gameObject.SetActive(false);
        isNextLevel = false;
        isGameActive = true;
    }
    
    void KillFoodIfNextLevel()
    {
        if (isNextLevel)
        {
            Destroy(GameObject.FindWithTag("Food"));
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
