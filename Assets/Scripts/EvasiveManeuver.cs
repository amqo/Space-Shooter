using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;

    public Boundary boundary;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    float currentSpeed;
    float targetManeuver;
    Rigidbody rb;

    GameObject playerObject;

	void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            if (playerObject == null)
            {
                targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            } else
            {
                targetManeuver = playerObject.transform.position.x - transform.position.x; 
                //Random.Range(1, dodge) * -Mathf.Sign(playerObject.transform.position.x);
            }
            
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0f;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0f, currentSpeed);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
    }
}
