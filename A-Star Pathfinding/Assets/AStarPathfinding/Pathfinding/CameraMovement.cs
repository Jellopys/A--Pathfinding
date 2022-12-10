using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float speed = 100f;
    Vector3 vel, acc;
    public float playerAccMagnitude = 400; // meters per second^2
    public float drag = 1.6f;

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement() {
		// local function
		void TestInput( KeyCode key, Vector3 dir ) {
			if( Input.GetKey( key ) )
				acc += dir;
		}

		acc = Vector3.zero;
		TestInput( KeyCode.W, transform.up );
		TestInput( KeyCode.S, -transform.up );
		TestInput( KeyCode.A, -transform.right );
		TestInput( KeyCode.D, transform.right );

		if( acc != Vector3.zero )
			acc = acc.normalized * playerAccMagnitude;
		vel += acc * Time.deltaTime;

		// 1 meter per second
		float dt = Time.deltaTime;
		transform.position += vel * dt;
	}

    void FixedUpdate() {
		vel /= drag; // movement dampening
	}
}
