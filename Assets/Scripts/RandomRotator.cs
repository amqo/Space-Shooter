using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

    public float tumble = 4f;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
    }
	
}
