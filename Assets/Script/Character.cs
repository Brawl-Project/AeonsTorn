using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	private List<Buff> buffs = new List<>();

	// Basic
	private double health;
	private double tp;
	private double agility;
	private int cardDrawQuantity;
	private List<Skill> skills = new List<>();
	private List<Card> cards = new List<>();
	private List<Item> items = new List<>();

	// Getters
	public double getHealth() { return health;}
	public double getTp() { return tp;}
	public double getAgility() { return agility;}
	public int getCardDrawQuantity() { return cardDrawQuantity;}
	public List<Skill> getSkills() { return skills;}
	public List<Buff> getSkills() { return buffs;}
	public List<Card> getSkills() { return cards;}
	public List<Buff> getBuffs() { return buffs;}

	// Setters
	public double setHealth(double health) { this.health = health;}
	public double setTp(double tp) { this.tp = tp;}
	public double setAgility(double agility) { this.agility = agility;}
	public int setCardDrawQuantity(int cardDrawQuantity) { this.cardDrawQuantity = cardDrawQuantity;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
