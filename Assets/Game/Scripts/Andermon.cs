using UnityEngine;
using System.Collections;

public class Andermon : MonoBehaviour {

	public enum Type{
		water,
		fire,
		eletric,
		wind,
		earth,
		grass,
		normal,
		dark,
		spirit,
		psy
	};
	
	public enum Condition{
		dead,
		alive,
		poison
	};

	public int id;
	public string name;
	public string nick_name;
	public Type type;
	public int level;
	public int experience;
	public int maxHealth;
	public int actualHealth;
	public int attack;
	public int actualAttack;
	public int magic;
	public int actualMagic;
	public int defense;
	public int actualDefense;
	public int agility;
	public int actualAgility;
	public Condition condition;
	//public Ability[] ability; //TODO
	//public string serial; //TODO

	//Construtor para o player e trainer
	public Andermon(int id, string name, string nick_name, int level, Type type, int maxHealth, int actualHealth, int attack, int magic, int defense, int agility, Condition condition){
		this.id = id;
		this.name = name;
		this.nick_name = nick_name;
		this.level = level;
		this.experience = 0;
		this.type = type;
		this.maxHealth = maxHealth;
		this.actualHealth = actualHealth;
		this.attack = attack;
		this.actualAttack = attack;
		this.magic = magic;
		this.actualMagic = magic;
		this.defense = defense;
		this.actualDefense = defense;
		this.agility = agility;
		this.actualAgility = agility;
		this.condition = condition;
	}

	//Construtor para wild
	public Andermon(int id, string name, int level,Type type, int maxHealth, int attack, int magic, int defense, int agility){
		this.id = id;
		this.name = name;
		this.nick_name = name;
		this.level = level;
		this.experience = 0;
		this.type = type;
		this.maxHealth = maxHealth;
		this.actualHealth = maxHealth;
		this.attack = attack;
		this.actualAttack = attack;
		this.magic = magic;
		this.actualMagic = magic;
		this.defense = defense;
		this.actualDefense = defense;
		this.agility = agility;
		this.actualAgility = agility;
		this.condition = Condition.alive;
	}

	//Cura o monstro
	public void Heal(){
		condition = Condition.alive;
		actualHealth = maxHealth;
	}

	//Recupera status
	public void Recover(){
		if (actualHealth > 0)
						condition = Condition.alive;
				else 
						condition = Condition.dead;
	}

	//AdjustStats after battle
	public void AdjustStats(){
		this.actualAttack = attack;
		this.actualMagic = magic;
		this.actualDefense = defense;
		this.actualAgility = agility;
	}

	//LevelUp
	public void LevelUp(){

	}

}
