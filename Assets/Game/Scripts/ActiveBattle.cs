﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActiveBattle : MonoBehaviour {

	PlayerBehaviour player; //The main player script
	AndermonList andermonList; //List of ALL andermon, this script can be found in player
	ActionList actionList; //List of ALL actions in the game with action code
	public TurnManager turnManager; //Reference to the script that manages player turn
	public ArtificialIntelligence.IAlevel IAtype; //This is used by TurnManager, see artificialIntelligence script for details about the structure
	Vector3[] P1pos;
	Vector3[] P2pos;
	Object[] andermonObject; //0 a 5 P1 , 6 a 11 P2
	public Andermon[] P2team;
	public Andermon[] P1team;

	//activeAndermon UI elements
	public GameObject[] andermonBattleInfo; //0 a 5 P1 , 6 a 11 P2 // GameObject thast contains all UI elements of the andermon in the battlefield
	public Text[] healthText; 
	public Text[] actionText;
	public Text[] nameText;
	public Canvas leftCanvas;


	//On first battle, load automatically
	void Start(){
		//Treta  incoming
		GameObject gameObject = GameObject.FindWithTag ("Player"); //Finding the player
		player = gameObject.GetComponent<PlayerBehaviour>(); //Getting the script within the player
		andermonList = gameObject.GetComponent<AndermonList> (); //Getting the script with all andermon information
		P1pos = new Vector3[6]; //nao mexe Anderson
		P2pos = new Vector3[6]; //nao mexe Anderson
		andermonObject = new GameObject[12]; //activeAndermonObject on battlefield
		SetPositions ();	//Had to hardcode due to problems, it's faster than calling Find() 12 times anyways;
		Load ();
	}

	//Finishing  the battle
	public void EndBattle(){
		for (int i = 0; i < 6; i++) //Recover stats changes after battle
						P1team [i].AdjustStats ();
		player.playerTeam = P1team; //Update player team after battle
		player.EndBattle(this); //Cache this script in playerBehaviour for reaload
		DestroyAndermonObject();
		//TODO GAMEOVER
	}

	//Loading battle on the same battlefield, hardcoded pq o unity e fdp e eh mais rapido anyways
	//Quartenium formula x = sin(Y)sin(Z)cos(X)+cos(Y)cos(Z)sin(X) y = sin(Y)cos(Z)cos(X)+cos(Y)sin(Z)sin(X) z = cos(Y)sin(Z)cos(X)-sin(Y)cos(Z)sin(X) w = cos(Y)cos(Z)cos(X)-sin(Y)sin(Z)sin(X)
	public void Load(){
		//Cache both teams
		P1team = player.playerTeam;
		P2team = player.enemyTeam;
		IAtype = player.IAtype;
		LoadAndermonObject(); //Carrega os andermon como objeto no battlefield
		LoadUI (); //Load the UI
		turnManager.ConfigureBattle(); //Decides first turn and set handcaps if necessary
	}

	//Load every andermon to the battlefield
	void LoadAndermonObject(){
		for (int i = 0; i < 6; i++) {
			if(P1team[i].id != 0)
				andermonObject[i] = Instantiate(andermonList.andermon[P1team[i].id], P1pos[i], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			else
				andermonObject[i] = null;
		}

		for (int i = 0; i < 6; i++) {
			if(P2team[i].id != 0)
				andermonObject[i+6] = Instantiate(andermonList.andermon[P2team[i].id], P2pos[i], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			else
				andermonObject[i+6] = null;
		}
	}

	void LoadUI(){
		Andermon[] actualTeam = P1team; 
		for (int i = 0, j = 0; i < 12; i++, j++) { //j goes through the actualTeam array position, i for everything else
			if(i == 6){
				actualTeam = P2team;
				j = 0; //reset j to go through the second team
			}
			if(actualTeam[j].id != 0){
				andermonBattleInfo[i].SetActive (true);
				healthText[i].text = "" + actualTeam[j].actualHealth + " / " + actualTeam[j].maxHealth;
				actionText[i].text = "" + (50 + actualTeam[j].actualAgility); //TODO battle mechanic 
				nameText[i].text = actualTeam[j].nickName;
			}
			else
				andermonBattleInfo[i].SetActive (false);
		}
	}

	void DestroyAndermonObject(){
		for (int i = 0; i < 6; i++) {
			if(P1team[i].id != 0)
				Destroy(andermonObject[i]);
		}
		
		for (int i = 0; i < 6; i++) {
			if(P2team[i].id != 0)
				Destroy(andermonObject[i+6]);
		}
	}

	//Hardcoded positions, don't touch
	void SetPositions(){
		P2pos [0] = new Vector3 (9971.716f, 9991.0f, 10030.0f);
		P2pos [1] = new Vector3 (9988.396f, 9991.0f, 10030.0f);
		P2pos [2] = new Vector3 (9971.716f, 9991.0f, 10052.76f);
		P2pos [3] = new Vector3 (9988.396f, 9991.0f, 10052.76f);
		P2pos [4] = new Vector3 (9971.716f, 9991.0f, 10075.17f);
		P2pos [5] = new Vector3 (9988.396f, 9991.0f, 10075.17f);
		P1pos [0] = new Vector3 (9971.716f, 9991.0f, 10001.28f);
		P1pos [1] = new Vector3 (9988.396f, 9991.0f, 10001.28f);
		P1pos [2] = new Vector3 (9971.716f, 9991.0f, 9978.759f);
		P1pos [3] = new Vector3 (9988.396f, 9991.0f, 9978.759f);
		P1pos [4] = new Vector3 (9971.716f, 9991.0f, 9955.848f);
		P1pos [5] = new Vector3 (9988.396f, 9991.0f, 9955.848f);
	}
	
}
