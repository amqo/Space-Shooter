using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public float tilt = 4f;
    public float fireRate = 0.25f;

    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    Rigidbody playerRigidbody;
    Vector3 movement;
    float nextFire = 0f;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        playerRigidbody.velocity = movement * speed;

        playerRigidbody.position = new Vector3(
            Mathf.Clamp(playerRigidbody.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(playerRigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        playerRigidbody.rotation = Quaternion.Euler(0f, 0f, playerRigidbody.velocity.x * -tilt);
    }
}
