using UnityEngine;
using System.Collections;

public class NPCteste : MonoBehaviour {
	
	Animator animator;
	public string[] text;
	
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void talk(int facingDirection){
		animator.SetInteger ("Direction", facingDirection);
		Debug.Log (text[0]);
	}
}