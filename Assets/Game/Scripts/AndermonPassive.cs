using UnityEngine;
using System.Collections;

public class AndermonPassive : MonoBehaviour {
	
	Animator animator;
	public string[] text;
	public string terrainName;
	public Andermon[] enemyTeam;
	public ArtificialIntelligence.IAlevel IAtype; //Check ArtificialInteligence script for details

	/// <summary>
	/// TODO
    /// Vou fazer um script que vai funcionar como randomizador de times em cada area, dependendo qual regiao o player esta,
	/// o manger vai dar spawn num npc no mapa em um spawn point pre determinado e vai enviar o time para o npc e enviar as texturas correspontes...
	/// por enquanto o time vai ser hardcodded
	/// </summary>

	void Start () {
		animator = GetComponent<Animator> ();
		enemyTeam = new Andermon[6];
	}

	public void TalkToPlayer(PlayerBehaviour.Direction facingDirection){
		animator.SetInteger("Direction", (int) facingDirection);
		Debug.Log (text[0]); //This can be changed in the editor
	}

	//Generates the enemy team for the player
	public Andermon[] GenerateTeam(){
		//Temporary solution, we dont have a manager codded YET
		IAtype = ArtificialIntelligence.IAlevel.wildEasy;
		if(Random.Range (1,7) % 2 != 0) enemyTeam [1] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		else enemyTeam [1] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		if(Random.Range (1,7) % 2 != 0) enemyTeam [2] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		else enemyTeam [2] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		if(Random.Range (1,7) % 2 != 0) enemyTeam [3] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		else enemyTeam [3] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		if(Random.Range (1,7) % 2 != 0) enemyTeam [4] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		else enemyTeam [4] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		if(Random.Range (1,7) % 2 != 0) enemyTeam [5] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		else enemyTeam [5] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);

		enemyTeam [0] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		//enemyTeam [1] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		//enemyTeam [2] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		//enemyTeam [3] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		//enemyTeam [4] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		//enemyTeam [5] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		return enemyTeam;
	}

	public string Terrain(){
		return terrainName;
	}

}