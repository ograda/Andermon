using UnityEngine;
using System.Collections;

public class NPCteste : MonoBehaviour {
	
	Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void talk(int facingDirection){
		animator.SetInteger ("Direction", facingDirection);
		Debug.Log ("Hello");
	}
}
