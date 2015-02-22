using UnityEngine;
using System.Collections;

public class ActiveBattle : MonoBehaviour {

	PlayerBehaviour player; //The main player script
	AndermonList andermonList; //List of ALL andermon
	Vector3[] P1pos;
	Vector3[] P2pos;

	//On first battle, load automatically
	void Start () {
		//Treta  incoming
		GameObject gameObject = GameObject.FindWithTag ("Player"); //Finding the player
		player = gameObject.GetComponent<PlayerBehaviour>(); //Getting the script within the player
		andermonList = gameObject.GetComponent<AndermonList> (); //Getting the script with all andermon information
		P1pos = new Vector3[6];
		P2pos = new Vector3[6];

		//Had to hardcode due to problems, it's faster than calling Find() 12 times anyways;
		SetPositions ();

		Load ();
	}


	
	//Update is called once per frame
	void Update () {
		//TODO
		float input = Input.GetAxisRaw("Vertical");
		if (input == 1 && player.inCombat) {
			player.EndBattle();
		}
	}

	void SetPositions(){
		P1pos [0] = new Vector3 (9971.716f, 9991.0f, 10030.0f);
		P1pos [1] = new Vector3 (9988.396f, 9991.0f, 10030.0f);
		P1pos [2] = new Vector3 (9971.716f, 9991.0f, 10052.76f);
		P1pos [3] = new Vector3 (9988.396f, 9991.0f, 10052.76f);
		P1pos [4] = new Vector3 (9971.716f, 9991.0f, 10075.17f);
		P1pos [5] = new Vector3 (9988.396f, 9991.0f, 10075.17f);
		P2pos [0] = new Vector3 (9971.716f, 9991.0f, 10001.28f);
		P2pos [1] = new Vector3 (9988.396f, 9991.0f, 10001.28f);
		P2pos [2] = new Vector3 (9971.716f, 9991.0f, 9978.759f);
		P2pos [3] = new Vector3 (9988.396f, 9991.0f, 9978.759f);
		P2pos [4] = new Vector3 (9971.716f, 9991.0f, 9955.848f);
		P2pos [5] = new Vector3 (9988.396f, 9991.0f, 9955.848f);
	}

	//Loading battle on the same battlefield, hardcoded pq o unity e fdp e eh mais rapido anyways
	//Quartenium formula x = sin(Y)sin(Z)cos(X)+cos(Y)cos(Z)sin(X) y = sin(Y)cos(Z)cos(X)+cos(Y)sin(Z)sin(X) z = cos(Y)sin(Z)cos(X)-sin(Y)cos(Z)sin(X) w = cos(Y)cos(Z)cos(X)-sin(Y)sin(Z)sin(X)
	void Load(){
		if(player.playerTeam[0].id != 0){

			Instantiate(andermonList.andermon[player.playerTeam[0].id], P1pos[0], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));

			
		}
		if(player.playerTeam[1].id != 0){
			Instantiate(andermonList.andermon[player.playerTeam[1].id], P1pos[1], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			
		}
		if(player.playerTeam[2].id != 0){
			Instantiate(andermonList.andermon[player.playerTeam[2].id], P1pos[2], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			
		}
		if(player.playerTeam[3].id != 0){
			Instantiate(andermonList.andermon[player.playerTeam[3].id], P1pos[3], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			
		}
		if(player.playerTeam[4].id != 0){
			Instantiate(andermonList.andermon[player.playerTeam[4].id], P1pos[4], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			
		}
		if(player.playerTeam[5].id != 0){
			Instantiate(andermonList.andermon[player.playerTeam[5].id], P1pos[5], new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[0].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[0].id], P2pos[0], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[1].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[1].id], P2pos[1], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[2].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[2].id], P2pos[2], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[3].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[3].id], P2pos[3], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[4].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[4].id], P2pos[4], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
		if(player.enemyTeam[5].id != 0){
			Instantiate(andermonList.andermon[player.enemyTeam[5].id], P2pos[5], new Quaternion(0.5f, -0.5f, -0.5f, -0.5f));
			
		}
	}

}
