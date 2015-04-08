using UnityEngine;
using System.Collections;

public class ArtificialIntelligence : MonoBehaviour {

	//Enum that contains IA levels, this structure is used across all game
	public enum IAlevel{
		wildEasy,
		wildMedium,
		wildHard,
		custom
	}

	ActiveBattle activeBattle; //Script to acess team information and turnManager
	TurnManager turnMager; //Store turnMager to acess public functions
	IAlevel level; //Stores the level

	public void Load(IAlevel level, ActiveBattle activeBattle){
		this.level = level;
		this.activeBattle = activeBattle;
	}

	public void Play(){
		switch (level) {
			case IAlevel.wildEasy:
				Easy ();
				break;
			case IAlevel.wildMedium:
				Medium ();
				break;
			case IAlevel.wildHard:
				Hard ();
				break;
			default:
				Debug.Log("No IA selected");
				break;
		}
	}

	//TEMP
	IEnumerator Wait(){
		yield return new WaitForSeconds (3);
	}

	//Standart Easy IA
	void Easy(){
		Wait ();
		activeBattle.turnManager.Pass();
	}

	//Standart Medium IA
	void Medium(){
		//TODO
	}

	//Standart Hard IA
	void Hard(){
		//TODO
	}
}
