using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set;}

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public Button retryButton;

    private DinoPlayer player;
    private Spawner spawner;
    private float score;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy() {
        if(Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        player = FindObjectOfType<DinoPlayer>();
        spawner = FindObjectOfType<Spawner>();
        NewGame();
    }

    public void NewGame() {

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void GameOver() {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHighscore();

    }

    private void Update() {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighscore() {
        float highscore = PlayerPrefs.GetFloat("highscore", 0);

        if(score > highscore) {
            highscore = score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        highscoreText.text = Mathf.FloorToInt(highscore).ToString("D5");
    }
}
