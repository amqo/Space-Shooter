using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public bool isPlayerDestroyer = true;

    public int scoreValue = 10;

    GameController gameController;
    AudioSource playerContactSound;
    MeshRenderer mesh;
    CapsuleCollider capsule;

    void Start()
    {
        playerContactSound = GetComponent<AudioSource>();
        capsule = GetComponent<CapsuleCollider>();
        mesh = GetComponent<MeshRenderer>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    bool WillExplodeOnContactWith(Collider other)
    {
        return isPlayerDestroyer || !other.CompareTag("Player");
    }

	void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Boundary") && !other.CompareTag("Enemy"))
        {   
            if (other.CompareTag("Player"))
            {
                if (isPlayerDestroyer)
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                    gameController.GameOver();
                    
                } else if (playerContactSound != null)
                {
                    playerContactSound.Stop();
                    playerContactSound.Play();
                }
            }

            bool willExplode = WillExplodeOnContactWith(other);

            if (willExplode)
            {
                if (explosion != null)
                    Instantiate(explosion, transform.position, transform.rotation);
                
                Destroy(other.gameObject);
                Destroy(gameObject);
                
            } else if (mesh != null && capsule != null)
            {
                mesh.enabled = false;
                capsule.enabled = false;
            }
            gameController.AddScore(willExplode ? scoreValue : -scoreValue);
        }
    }
}
