using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
	
	public float speed = 2.5f;
	

	public static Rotation ins;
	// Use this for initialization
	void Start () {
		if (ins == null)
			ins = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			transform.Rotate (Vector3.forward * speed * Time.deltaTime);
		}
      
	}

}
