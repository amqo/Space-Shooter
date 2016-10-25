using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount = 10;
    public float spawnWait = 0.5f;
    public float startWait = 1f;
    public float waitWait = 4f;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    int score = 0;
    bool gameOver;
    bool restart;

    void Start()
    {
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";

        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(!gameOver)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                SpawnHazard();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waitWait);
        }
    }

    void SpawnHazard()
    {
        Vector3 spawnPosition = new Vector3(
            UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0f, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
    }

    public void AddScore(int addScoreValue)
    {
        score += addScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
        restartText.text = "Press 'R' for Restart";
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public static implicit operator GameController(GameObject v)
    {
        throw new NotImplementedException();
    }
}
