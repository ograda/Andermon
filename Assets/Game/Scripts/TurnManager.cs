using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	//Ander reprogramar tudo aki

	//conditions of battle end
	enum BattleEndState{
		player1win,
		player2win,
		player1escape,
		player2escape,
		none
	}

	//This script manages both players turn
	public ActiveBattle activeBattle; //Every UI element can be found in this script, please check activeBattle variable list
	public ArtificialIntelligence IA; //Script that contains moves 
	public ArtificialIntelligence.IAlevel IAtype;
	bool turn; //This can be acessed to verify who's playing, 0 = P1  1 = P2
	bool useIA; //Activates IA
	BattleEndState endState;
	
	//Will set the first turn and handicaps for the second turn if necessary && call BattleManagement && call special IA if necessary
	public void ConfigureBattle(bool IA){
		turn = true; //TODO mechanic to decide firsdt turn
		this.IA = IA;  //Activates IA, there's no multplayer yet
		endState = BattleEndState.none;
		if(useIA) LoadIA (); //Get the IA script

		//Calls the function that manages battle
		BattleStateMachine();
	}

	void LoadIA(){
		IAtype = activeBattle.IAtype;
		//TODO parei aki
	}

	//Manages battle
	void BattleStateMachine(){
		//while inCombat
		while(endState == BattleEndState.none){
			//TODO
		}

		//Calls the function to handle the end of the battle and inform how it ended
		activeBattle.EndBattle(endState);
	}

	//TEMP
	IEnumerator Wait(){
				yield return new WaitForSeconds (3);
	}

	//Pass turn WIP
	public void SwitchTurn(){
		if (turn){
			activeBattle.leftCanvas.gameObject.SetActive (false); //temp
			Debug.Log ("Enemy turn");
			Wait();
			if(IA){
				//TODO
				//IAturn
				SwitchTurn ();
			} 
		}
		else {
			activeBattle.leftCanvas.gameObject.SetActive (true); //temp
			Debug.Log ("Player Turn");
		}
		turn = !turn;
	}

	//Try to end battle by escaping WIP
	public void Escape(){
		//TODO escape mechanic
		Debug.Log ("Trying to escape");
		activeBattle.EndBattle ();

	}

	//Battle ned by P1 or P2 being defeated
	void BattleOver(){
		//TODO
	}
}
