﻿using UnityEngine;
using System.Collections;

public class AndermonPassive : MonoBehaviour {
	
	Animator animator;
	public string[] text;
	public Andermon[] enemyTeam;

	/// <summary>
	/// TODO
    /// Vou fazer um script que vai funcionar como randomizador de times em cada area, dependendo qual regiao o player esta,
	/// o manger vai dar spawn num npc no mapa em um spawn point pre determinado e vai enviar o time para o npc e enviar as texturas correspontes...
	/// por enquanto o time vai ser hardcodded
	/// </summary>

	void Start () {
		animator = GetComponent<Animator> ();
	}

	//Generates the enemy team for the player
	public Andermon[] GenerateTeam(int facingDirection){
		animator.SetInteger ("Direction", facingDirection);
		Debug.Log (text[0]);
		//Temporary solution, we dont have a manager codded YET
		enemyTeam = new Andermon[6];
		enemyTeam [0] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [1] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [2] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [3] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [4] = new Andermon (2, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		enemyTeam [5] = new Andermon (0, "Ratty", "Ratty", 1, Andermon.Type.normal, 6, 6, 2, 0, 1, 45, Andermon.Condition.alive);
		return enemyTeam;
	}

}