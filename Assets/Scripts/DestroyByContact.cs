using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue = 10;

    GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

	void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Boundary"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
           
            gameController.AddScore(scoreValue);
        }
    }
}
