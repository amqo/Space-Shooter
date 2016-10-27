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
	public SimpleTouchPad touchPad;
	public SimpleTouchButtonArea touchButton;

    AudioSource shotSound;
    Rigidbody playerRigidbody;
    Vector3 movement;
    Quaternion calibrationquaternion;
    float nextFire = 0f;

    void Start()
    {
        CalibrateAccelerometer();
        shotSound = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
		if (Application.platform == RuntimePlatform.Android) {
			if (touchButton.CanFire () && Time.time > nextFire) {
				Fire ();
			}
		} else if (Input.GetButton("Fire1") && Time.time > nextFire) {
			Fire ();
        }
    }

	void Fire() {
		nextFire = Time.time + fireRate;
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		shotSound.Stop();
		shotSound.Play();
	}

    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), accelerationSnapshot);
        calibrationquaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationquaternion * acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        if (Application.platform == RuntimePlatform.Android)
        {
            //Vector3 acceleration = Input.acceleration;
            //Vector3 fixedAcceleration = FixAcceleration(acceleration);
			//movement = new Vector3(fixedAcceleration.x, 0f, fixedAcceleration.y);

			Vector2 direction = touchPad.GetDirection();
			movement = new Vector3 (direction.x, 0f, direction.y);
        }
        
        playerRigidbody.velocity = movement * speed;

        playerRigidbody.position = new Vector3(
            Mathf.Clamp(playerRigidbody.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(playerRigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        playerRigidbody.rotation = Quaternion.Euler(0f, 0f, playerRigidbody.velocity.x * -tilt);
    }
}
