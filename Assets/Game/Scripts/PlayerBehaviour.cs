﻿using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	/****** Global Var ******/

	public enum Direction{
		down,
		up,
		right,
		left
	}

	float maxSpeed;
	public Direction facingDirection;
	Animator animator; //Animator attached to the player, this is used in movement
	public bool inCombat; 
	public ArtificialIntelligence.IAlevel IAtype; //See artificialintelligence script for the full structure, this is for battle
	public Camera mainCamera;
	GameObject currentBattle; //should be destroyed when a new scene needs to be loaded
	string currentSceneBattleName; //auxiliar variable to destroy battle scenes
	string sceneBattleName; //auxiliar variable to destroy battle scenes
	ActiveBattle activeBattleScript; //this is attached in the battlecamera of a battlescenes, this is the main battle script
	public Andermon[] playerTeam; //the player team
	public Andermon[] enemyTeam; //this is generated by a script called EnemyManager, WIP ... i need to cache this here to send this to the battle script

	//this is called on game load
	void Start () {
		maxSpeed = 250f;
		animator = GetComponent<Animator> ();
		sceneBattleName = "BattleForest";
		facingDirection = Direction.down;
		//Temp
		playerTeam = new Andermon[6];
		playerTeam [0] = new Andermon (1, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [1] = new Andermon (1, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [2] = new Andermon (0, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [3] = new Andermon (0, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [4] = new Andermon (0, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [5] = new Andermon (0, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		enemyTeam = new Andermon[6];
		enemyTeam [0] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [1] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [2] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [3] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [4] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [5] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
	}

	//FixedUpdate para melhor mecanica de movimento
	void FixedUpdate () {
		//Input
		float moveV = Input.GetAxisRaw("Vertical");
		float moveH = Input.GetAxisRaw("Horizontal");
		bool interaction = Input.GetButtonDown("Jump");



		if (!inCombat) {

			//Fisica de movimento, a gravidade do jogo e 0, a implementacao do movimento diagonal eh mais facil
			//Anderson: Eu vou implementar o movimento em 8 direcoes quando os spirtes apropriados estiverem no lugar
			if (moveV != 0 && moveH == 0){
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, moveV * maxSpeed);
				animator.SetInteger ("VerticalSpeed", (int)moveV);
				animator.SetInteger ("HorizontalSpeed", 0);
				if(moveV > 0) facingDirection = Direction.up;
				else facingDirection = Direction.down;
			}
			else if(moveH != 0 && moveV == 0){
				GetComponent<Rigidbody2D>().velocity = new Vector2 (moveH * maxSpeed, 0);
				animator.SetInteger ("VerticalSpeed", 0);
				animator.SetInteger ("HorizontalSpeed", (int)moveH);
				if(moveH > 0) facingDirection = Direction.right;
				else facingDirection = Direction.left; //Left
			}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				animator.SetInteger ("VerticalSpeed", 0);   //Codigo de animao dentro dos ifs
				animator.SetInteger ("HorizontalSpeed", 0); //Fora dos ifs gera um bug pequeno
			}

			//Codigo de interacao
			if(interaction) 
				Examine();
		}
	}

	//Codigo de interacao com o ambiente, manda um laser pra onde o player ta olhando e ve que tipo de objeto que o laser colidiu
	void Examine(){
		RaycastHit2D hit;
		Vector2 startPoint;
		Vector2 direction;
		float distance = 3.0f;
		
		//Checa para onde vou mandar o laser
		if (facingDirection == Direction.down) {
			startPoint = new Vector2 (transform.position.x, transform.position.y - 17);
			direction = Vector2.up;
			direction *= -1;
		}
		else if (facingDirection == Direction.up){
			startPoint = new Vector2 (transform.position.x, transform.position.y + 17);
			direction = Vector2.up;
			
		}
		else if (facingDirection == Direction.right){
			startPoint = new Vector2 (transform.position.x + 17, transform.position.y);
			direction = Vector2.right;
		}
		else { //left
			startPoint = new Vector2 (transform.position.x - 17, transform.position.y);
			direction = Vector2.right;
			direction *= -1;
		}
		
		//checa o que o laser colidiu
		if (hit = Physics2D.Raycast (startPoint, direction, distance)) {
			if(hit.collider.tag == "NPC"){ //essa e a tag de npc padrao que so fala
				hit.collider.SendMessage("talk", facingDirection); //Manda pro npc o lado em que o player esta olhando
			}
			if(hit.collider.tag == "AndermonP"){ //essa e a tag de andermon que nao se mexe
				AndermonPassive script;
				inCombat = true;
				script = hit.collider.GetComponent<AndermonPassive>();
				enemyTeam = script.GenerateTeam();
				script.TalkToPlayer(facingDirection);
				currentSceneBattleName = script.Terrain();
				IAtype = script.IAtype;
				StartBattle();
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				animator.SetInteger ("HorizontalSpeed", 0);
				animator.SetInteger ("VerticalSpeed", 0);
			}
		}
	}

	/****** Battle ******/
	
	//Combat method, calling battle scene
	void StartBattle(){
		//Eu fiz todos os loads de modo Async pq sme Async tava dando muita dor de cabeca (Loadlevel sem Async NAO PARA A THREAD MAIN!!!!!!!)
		//Na documentacao Async coloca o level para dar load numa thread de background, para o jogo ocntinuar durante o load mas a porra do LoadLevel normal tambem nao para -_- ,,|,, unit
		if (currentBattle == null ) { //If no combat is in memory, load
			Application.LoadLevelAdditive (sceneBattleName);
		} 
		else if(currentBattle != null && currentSceneBattleName != sceneBattleName) { //If a different battle is in memory
			Destroy (currentBattle);
			Application.LoadLevelAdditive (sceneBattleName);
		}
		else{
			activeBattleScript.Load(); //Reload battle
		}
		Debug.Log("Entrou em batalha"); //Erase
		mainCamera.enabled = false;
	}

	//Battle has ended, returning to game
	public void EndBattle(ActiveBattle script){
		inCombat = false;
		currentBattle = GameObject.FindWithTag("Battle"); //This is the folder game.object
		currentSceneBattleName = sceneBattleName;
		activeBattleScript = script;
		mainCamera.enabled = true;
		Debug.Log("Saiu da batalha"); //Erase
	}

}
