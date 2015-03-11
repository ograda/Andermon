using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActiveBattle : MonoBehaviour {

	PlayerBehaviour player; //The main player script
	AndermonList andermonList; //List of ALL andermon
	Vector3[] P1pos;
	Vector3[] P2pos;
	Object[] andermonObject;
	Andermon[] P2team;
	Andermon[] P1team;
	public Text activeAndermon;


	//On first battle, load automatically
	void Start(){
		//Treta  incoming
		GameObject gameObject = GameObject.FindWithTag ("Player"); //Finding the player
		player = gameObject.GetComponent<PlayerBehaviour>(); //Getting the script within the player
		andermonList = gameObject.GetComponent<AndermonList> (); //Getting the script with all andermon information
		P1pos = new Vector3[6];
		P2pos = new Vector3[6];
		andermonObject = new GameObject[12];
		SetPositions ();	//Had to hardcode due to problems, it's faster than calling Find() 12 times anyways;
		Load ();
	}


	
	//Update is called once per frame
	void Update () {
		//TODO
		float input = Input.GetAxisRaw("Vertical");
		if (input == 1 && player.inCombat) {
			player.EndBattle(this); //Cache this script in playerBehaviour for reaload
			DestroyAndermonObject();
		}
	}

	//Loading battle on the same battlefield, hardcoded pq o unity e fdp e eh mais rapido anyways
	//Quartenium formula x = sin(Y)sin(Z)cos(X)+cos(Y)cos(Z)sin(X) y = sin(Y)cos(Z)cos(X)+cos(Y)sin(Z)sin(X) z = cos(Y)sin(Z)cos(X)-sin(Y)cos(Z)sin(X) w = cos(Y)cos(Z)cos(X)-sin(Y)sin(Z)sin(X)
	public void Load(){
		//Cache both teams
		P1team = player.playerTeam;
		P2team = player.enemyTeam;

		LoadAndermonObject(); //Carrega os andermon como objeto no battlefield
		//Update UI
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
