using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

	//Ander reprogramar tudo aki

	/****** Global Var ******/

	//conditions of battle end
	public enum BattleEndState{
		player1win,
		player2win,
		player1escape,
		player2escape,
		none
	}

	//This script manages both players turn
	public ActiveBattle activeBattle; //Active battle script
	bool turn; //This can be acessed to verify who's playing, 0 = P1  1 = P2
	BattleEndState endState; //This stores the condition of the end battle, will be set to none if the battle is in progress
	List<int> andermonActionOrder; //This actually stores 

	//UI variables
	public Canvas leftCanvas;

	/****** System ******/
	
	//Will set the first turn and handicaps for the second turn if necessary && call BattleManagement && call special IA if necessary
	public void ConfigureBattle(){
		turn = true; //TODO mechanic to decide firsdt turn
		Handicap();
	
		Debug.Log ("Battle state none");
		endState = BattleEndState.none;


		//Calls the function that manages battle
		BattleStateMachine();
	}

	
	//TODO more advanced way to setup first round of action points with handicaps for the second player
	void Handicap(){
		for (int i = 0; i < 6; i++) {
			activeBattle.P1team [i].actionPoint = activeBattle.P1team [i].actualAgility + 30;
			activeBattle.UpdateAndermonInfo (true, i, ActiveBattle.Info.action); //This will update P2 AP
		}
		for (int i = 0; i < 6; i++) {
			activeBattle.P2team [i].actionPoint = activeBattle.P2team [i].actualAgility + 45;
			activeBattle.UpdateAndermonInfo(false, i, ActiveBattle.Info.action); //This will update P2 AP
		}
	}

	//Manages battle
	void BattleStateMachine(){
		//WIP
		//while inCombat
		if (endState == BattleEndState.none) { //The  battle should continue
			if (turn) { //P1 TURN
				activeBattle.ComputeTurnActionPoint(true);
				activeBattle.selectedAndermon = 0;
				TurnOrder(true);
				activeBattle.leftCanvas.gameObject.SetActive(true);
			} 
			else { //P2 Turn
				activeBattle.ComputeTurnActionPoint(false);
				TurnOrder(false);
				activeBattle.artificialIntelligence.Play();
			}
		} 
		else {			
			//Calls the function to handle the end of the battle and inform how it ended
			activeBattle.EndBattle (endState);
		}
	}

	//Setup turnOrder WIP, i need better code here
	void TurnOrder(bool team){
		Andermon[] actualTeam = (team) ? activeBattle.P1team : activeBattle.P2team;
		int[] actualTurnOrder = (team) ? activeBattle.turnOrderP1 : activeBattle.turnOrderP2;
		//getting turn order by highest action point, TODO: Better code here, something better than bucket sort
		for (int i = 0; i < 6; i++) {
			int first = actualTurnOrder[i];
			for(int j = 0; j < 6; j++){
				int second = actualTurnOrder[j];
				if(first != -1 && second != -1){
					if(actualTeam[first].actionPoint < actualTeam[second].actionPoint){
						Common.IntSwap(ref actualTurnOrder[i], ref actualTurnOrder[j]); //Swap
					}
					else if(first < second){
						Common.IntSwap(ref actualTurnOrder[i], ref actualTurnOrder[j]); //Swap
					}
				}
				else if(first != -1 && second == -1){
					Common.IntSwap(ref actualTurnOrder[i], ref actualTurnOrder[j]); //Swap
				}
			}		
		}
		string turns = "";
		for(int i = 0; i < 6; i++)
			turns += actualTurnOrder[i] + " "; 
		Debug.Log ("Turn order: " + turns);

		if (team) //returning turnOrder to the right variable
			activeBattle.turnOrderP1 = actualTurnOrder;
		else
			activeBattle.turnOrderP2 = actualTurnOrder;
	}

	//Battle end by P1 or P2 being defeated
	void BattleOver(){
		//TODO
	}

	/** Left Menu **/

	//Pass turn WIP
	public void Pass(){
		if (turn){
			activeBattle.leftCanvas.gameObject.SetActive (false); //temp
			Debug.Log ("Enemy turn");
		}
		else {
			Debug.Log ("Player Turn");
		}
		turn = !turn;
		BattleStateMachine();
	}

	//Try to end battle by escaping WIP
	public void Escape(){
		//TODO escape mechanic
		Debug.Log ("Trying to escape");
		endState = BattleEndState.player1escape;
		activeBattle.EndBattle (endState);
	}
}
