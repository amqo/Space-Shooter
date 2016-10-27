using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;

    public Vector3 spawnValues;
    public int hazardCount = 10;
    public float spawnWait = 0.5f;
    public float startWait = 1f;
    public float waitWait = 4f;

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Text scoreText;
    public Text restartText;
	public GameObject restartButton;
    public Text gameOverText;

    int score = 0;
    bool gameOver;
    bool restart;
    bool damaged = false;

    void Start()
    {
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
		restartButton.SetActive (false);

        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
				RestartGame ();
            }
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        
        GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
        Instantiate(hazard, spawnPosition, spawnRotation);
        
    }

    public void AddScore(int addScoreValue)
    {
        score += addScoreValue;
        UpdateScore();

        if (addScoreValue < 0)
        {
            damaged = true;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
		if (Application.platform == RuntimePlatform.Android) {
			restartButton.SetActive (true);
		} else {
			restartText.text = "Press 'R' for Restart";
		}
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
