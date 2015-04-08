using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActiveBattle : MonoBehaviour {

	/****** Global Var ******/

	PlayerBehaviour player; //The main player script
	AndermonList andermonList; //List of ALL andermon, this script can be found in player
	ActionList actionList; //List of ALL actions in the game with action code
	public TurnManager turnManager; //Reference to the script that manages player turn
	Vector3[] P1pos;
	Vector3[] P2pos;
	Object[] andermonObjectP1; //This variable stores every andermonObject on the  field related to P1
	Object[] andermonObjectP2;
	public Andermon[] P2team; //This is the only case in the entire code where the convention above isn't used
	public Andermon[] P1team; //Here both teams are separated
	public int actionPointCap; //This is the maximum action point a andermon can have
	public int selectedAndermon; //the andermon being played at current time
	public int[] turnOrderP1; //Playing andermon order P1
	public int[] turnOrderP2; //Playing andermon order P2

	//IA variables
	bool useIA; //Activates IA
	public ArtificialIntelligence artificialIntelligence; //Script that contains moves 
	ArtificialIntelligence.IAlevel IAtype;

	//activeAndermon UI elements
	public GameObject[] andermonBattleInfoP1, andermonBattleInfoP2; //This is a folder to store every UI label under a certain andermon
	public Text[] healthTextP1, healthTextP2; //Uhu... health text ... DO NOT ACESS THIS FOR THE ACTUAL ANDERMON HEALTH, this is for GUI only!!!!
	public Text[] actionTextP1, actionTextP2; //action text field, DO NOT ACESS THIS IF YOU WANT THE ACTUAL AP of the andermon
	public Text[] nameTextP1, nameTextP2; //This stores the name of the andermon as a text for GUI, for simplicity please do not use this. Use andermon.nickName instead
	public Canvas battleCanvas; //This is the folder that holds the entire blattle canvas
	public Canvas leftCanvas; //Same as above, but this only holds the left canvas

	public enum Info{ //This is used to update andermon info on the battlefield
		health,
		action
	}

	/****** System ******/

	//On first battle, load automatically
	void Start(){
		//Treta  incoming
		GameObject gameObject = GameObject.FindWithTag ("Player"); //Finding the player
		player = gameObject.GetComponent<PlayerBehaviour>(); //Getting the script within the player
		andermonList = gameObject.GetComponent<AndermonList> (); //Getting the script with all andermon information
		P1pos = new Vector3[6]; //nao mexe Anderson
		P2pos = new Vector3[6]; //nao mexe Anderson
		actionPointCap = 350; //This is the maxixmum AP an andermon can hold
		andermonObjectP1 = new GameObject[6]; //activeAndermonObject on battlefield
		andermonObjectP2 = new GameObject[6]; 
		turnOrderP1 = new int[6]; //turn order for p1
		turnOrderP2 = new int[6]; //turn order p2
		SetPositions ();	//Had to hardcode due to problems, it's faster than calling Find() 12 times anyways; Maybe another solution will take place down the line
		Load ();
	}

	//Loading battle on the same battlefield, hardcoded pq o unity e fdp e eh mais rapido anyways
	//Quartenium formula x = sin(Y)sin(Z)cos(X)+cos(Y)cos(Z)sin(X) y = sin(Y)cos(Z)cos(X)+cos(Y)sin(Z)sin(X) z = cos(Y)sin(Z)cos(X)-sin(Y)cos(Z)sin(X) w = cos(Y)cos(Z)cos(X)-sin(Y)sin(Z)sin(X)
	public void Load(){
		//Cache both teams
		P1team = player.playerTeam;
		P2team = player.enemyTeam;
		IAtype = player.IAtype;

		//Clear actionPoints
		for (int i = 0; i < 6; i++) 
			P1team [i].actionPoint = 0;
		for (int i = 0; i < 6; i++) 
			P2team [i].actionPoint = 0;

		LoadAndermonObject(); //Carrega os andermon como objeto no battlefield
		LoadUI (); //Load the UI
		useIA = true;  //Activates IA, there's no multplayer yet
		if(useIA) LoadIA (); //Get the IA script
		Debug.Log ("Calling battle configuration");



		turnManager.ConfigureBattle(); //Decides first turn and set handcaps if necessary
	}

	//Load every andermon to the battlefield
	void LoadAndermonObject(){
		for (int i = 0; i < 6; i++) {
			if(P1team[i].id != 0){ //This will generate the andermon on the field, the qartenion is the rotation of the sprite, nothing fancy just 90 degree rotation
				andermonObjectP1[i] = Instantiate(andermonList.andermonObject[P1team[i].id], P1pos[i], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
				Debug.Log("AndermonP1 i: " + i + " " + andermonObjectP1[i].ToString());
				turnOrderP1[i] = i; //This will store turnOrder, later on the turnManager will fix the turn order
			}
			else{
				andermonObjectP1[i] = null;
				turnOrderP1[i] = -1; //-1 menas there's no andermon to order in i pos
			}
		}

		for (int i = 0; i < 6; i++) {
			if(P2team[i].id != 0){
				andermonObjectP2[i] = Instantiate(andermonList.andermonObject[P2team[i].id], P2pos[i], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
				Debug.Log("AndermonP1 i:" + i + " " + andermonObjectP2[i].ToString());
				turnOrderP2[i] = i; //same as p1

			}
			else{
				andermonObjectP2[i] = null;
				turnOrderP2[i] = -1;
			}
		}
	}

	void LoadUI(){
		for (int i = 0; i < 6; i++) {  //This is pretty self explanatory... activates the info if the andermon exists
			if(P1team[i].id != 0){
				andermonBattleInfoP1[i].SetActive (true);
				healthTextP1[i].text = "" + P1team[i].actualHealth + " / " + P1team[i].maxHealth;
				actionTextP1[i].text = "" + 0; 
				nameTextP1[i].text = P1team[i].nickName;
			}
			else
				andermonBattleInfoP1[i].SetActive (false);
		}

		for (int i = 0; i < 6; i++) { 
			if(P2team[i].id != 0){
				andermonBattleInfoP2[i].SetActive (true);
				healthTextP2[i].text = "" + P2team[i].actualHealth + " / " + P2team[i].maxHealth;
				actionTextP2[i].text = "" + 0; 
				nameTextP2[i].text = P2team[i].nickName;
			}
			else
				andermonBattleInfoP2[i].SetActive (false);
		}

		battleCanvas.gameObject.SetActive(true);
	}

	//Load the IA script
	void LoadIA(){
		artificialIntelligence.Load (IAtype, this);
		Debug.Log ("IA load");
	}

	//Finishing  the battle
	public void EndBattle(TurnManager.BattleEndState endState){
		if (endState == TurnManager.BattleEndState.player1win || endState == TurnManager.BattleEndState.player1escape || endState == TurnManager.BattleEndState.player2escape) {
			for (int i = 0; i < 6; i++) //Recover stats changes after battle
				P1team [i].AdjustStats ();
			player.playerTeam = P1team; //Update player team after battle
			DestroyAndermonObject();
			battleCanvas.gameObject.SetActive(false);
			player.EndBattle (this); //Cache this script in playerBehaviour for reaload
		}
		else {
			//TODO GAMEOVER
		}
	}

	void DestroyAndermonObject(){
		for (int i = 0; i < 6; i++) {
			if(P1team[i].id != 0)
				Destroy(andermonObjectP1[i]);
		}
		
		for (int i = 0; i < 6; i++) {
			if(P2team[i].id != 0)
				Destroy(andermonObjectP2[i]);
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

	/****** Battle ******/

	//parei aki.... conserta o unity no info editor

	//Should be called when you want to update a certain andermon info
	//team true = P1 , false = P2 ... andermonNumber = pos andermon in the array ... like the ocnvention mentioned at the beggining of the code: 0 to 5 means P1 team, 6 to 11 P2
	public void UpdateAndermonInfo(bool team,int andermonNumber, Info info){
		Andermon andermon; //The andermon that needs to be updated
		andermon = (team) ? P1team[andermonNumber] : P2team[andermonNumber]; //simple trernary, andermonNumber has the pos in the array of the andermon
		switch (info) {
		case Info.health:
			if(team) healthTextP1[andermonNumber].text = "" + andermon.actualHealth + " / " + andermon.maxHealth;
			else healthTextP2[andermonNumber].text = "" + andermon.actualHealth + " / " + andermon.maxHealth; //DO not use GUI info to know an andermon stat
			break;
		case Info.action:
			if(team) actionTextP1[andermonNumber].text = "" + andermon.actionPoint;
			else actionTextP2[andermonNumber].text = "" + andermon.actionPoint;
			break;
		}
	}

	//This function computes andermon actionpoints and should be called at the start of the battle & between turns
	public void ComputeTurnActionPoint(bool team){
		Andermon[] actualTeam = (team) ? P1team : P2team; //true p1 , false p2
		for (int i = 0; i < 6; i++) {
			actualTeam[i].actionPoint += actualTeam[i].actualAgility;
			if(actualTeam[i].actionPoint > actionPointCap) actualTeam[i].actionPoint = actionPointCap; //Can't go over the maximum cap for actionPoints
			if(team) actionTextP1[i].text = "" + actualTeam[i].actionPoint; //Update gUI text
			else actionTextP2[i].text = "" + actualTeam[i].actionPoint;
		}
	}	
}
