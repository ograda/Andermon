using UnityEngine;
using System.Collections;

public class ActionList : MonoBehaviour {

	public ActiveBattle battleScript; //script that contains all battle code

	//Code to identify wich action was used
	//id = id da açao, performerPos = posiçao do andermon que usou a habilidade, bool player = 1 se foi P1 e 0 se foi P2
	void Action(int id, int performerPos, bool player){
		switch (id) {
			case 0:
				Pass ();
				break;
			case 1:
				Attack (performerPos, player);
				break;
			case 2:
				Roar (performerPos, player);
				break;
			default:
				Debug.Log ("Action not found " + id);
				break;
		}
	}

	//ALL Actions code
	//Use ctrl+f  [ id = # ]
	//id = 0
	void Pass(){
		//TODO
	}

	//id = 1
	void Attack(int performerPos, bool player){
		//Target TODO
		if (player) {
			Debug.Log ("P1 used attack with" + battleScript.P1team[performerPos].nickName);
		} 
		else {
			Debug.Log ("P2 used attack with" + battleScript.P2team[performerPos].nickName);
		}
	}

	//id = 2 
	void Roar(int performerPos, bool player){
		if (player) {
			Debug.Log ("P1 used roar with" + battleScript.P1team[performerPos].nickName);
		} 
		else {
			Debug.Log ("P2 used roar with" + battleScript.P2team[performerPos].nickName);
		}
	}


}
