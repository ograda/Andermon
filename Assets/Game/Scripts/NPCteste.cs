using UnityEngine;
using System.Collections;

public class NPCteste : MonoBehaviour {
	
	Animator animator;
	public string[] text;
	
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void talk(PlayerBehaviour.Direction facingDirection){
		animator.SetInteger ("Direction", (int) facingDirection);
		Debug.Log (text[0]);
	}
}