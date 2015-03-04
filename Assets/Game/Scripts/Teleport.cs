using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Vector3 teleportLocation;

	void OnTriggerEnter2D(Collider2D c){
		c.transform.position = teleportLocation;
	}
}

