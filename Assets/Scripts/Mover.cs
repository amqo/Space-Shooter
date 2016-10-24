using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed = 20f;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }
}
