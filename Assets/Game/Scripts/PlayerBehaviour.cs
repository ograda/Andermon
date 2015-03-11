using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	//Global by necessity
	float maxSpeed;
	public int facingDirection; //0 foward, 1 up, 2 right, 3 left
	Animator animator;
	public bool inCombat;
	public Camera mainCamera;
	GameObject currentBattle;
	string currentSceneBattleName;
	string sceneBattleName;
	ActiveBattle activeBattleScript;
	public Andermon[] playerTeam;
	public Andermon[] enemyTeam;
	
	void Start () {
		maxSpeed = 250f;
		animator = GetComponent<Animator> ();
		sceneBattleName = "BattleForest";
		facingDirection = 0;
		//Temp
		playerTeam = new Andermon[6];
		playerTeam [0] = new Andermon (1, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
		playerTeam [1] = new Andermon (0, "Kliy", "MeuMon", 2, Andermon.Type.normal, 10, 10, 3, 1, 2, 50, Andermon.Condition.alive);
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
				if(moveV > 0) facingDirection = 1; //Up
				else facingDirection = 0; //Down
			}
			else if(moveH != 0 && moveV == 0){
				GetComponent<Rigidbody2D>().velocity = new Vector2 (moveH * maxSpeed, 0);
				animator.SetInteger ("VerticalSpeed", 0);
				animator.SetInteger ("HorizontalSpeed", (int)moveH);
				if(moveH > 0) facingDirection = 2; //Right
				else facingDirection = 3; //Left
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
		currentBattle = GameObject.FindWithTag("Battle"); //This is the field game.object
		currentSceneBattleName = sceneBattleName;
		activeBattleScript = script;
		mainCamera.enabled = true;
		Debug.Log("Saiu da batalha"); //Erase
	}

	//Codigo de interacao com o ambiente
	void Examine(){
		RaycastHit2D hit;
		Vector2 startPoint;
		Vector2 direction;
		float distance = 3.0f;

		//Checa para onde vou mandar o laser
		if (facingDirection == 0) { // Foward
			startPoint = new Vector2 (transform.position.x, transform.position.y - 17);
			direction = Vector2.up;
			direction *= -1;
		}
		else if (facingDirection == 1){ //Up
			startPoint = new Vector2 (transform.position.x, transform.position.y + 17);
			direction = Vector2.up;

		}
		else if (facingDirection == 2){ //Right
			startPoint = new Vector2 (transform.position.x + 17, transform.position.y);
			direction = Vector2.right;
		}
		else {
			startPoint = new Vector2 (transform.position.x - 17, transform.position.y);
			direction = Vector2.right;
			direction *= -1;
		}

		if (hit = Physics2D.Raycast (startPoint, direction, distance)) {
			if(hit.collider.tag == "NPC"){
				hit.collider.SendMessage("talk", facingDirection);
			}
			if(hit.collider.tag == "AndermonP"){
				AndermonPassive script;
				inCombat = true;
				script = hit.collider.GetComponent<AndermonPassive>();
				enemyTeam = script.GenerateTeam(facingDirection);
				currentSceneBattleName = script.Terrain();
				StartBattle();
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				animator.SetInteger ("HorizontalSpeed", 0);
				animator.SetInteger ("VerticalSpeed", 0);
			}
		}
	}

}
