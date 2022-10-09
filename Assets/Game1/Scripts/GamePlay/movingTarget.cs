using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTarget : MonoBehaviour {
	public Rigidbody2D body;

	public float speed;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		body = GetComponent<Rigidbody2D>();
		if (transform.position.y <= -2 || transform.position.y >= 2) {
			speed = -speed;
		}
		body.velocity = new Vector2 (0, speed);
	}
}
