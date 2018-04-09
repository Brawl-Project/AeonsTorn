using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	private List<Buff> buffs = new List<Buff>();

	// Basic
	private double health;
	private double tp;
	private double agility;
	private int cardDrawQuantity;
	private List<Skill> skills = new List<Skill>();
	private List<Card> cards = new List<Card>();
	private List<Item> items = new List<Item>();

	// Getters
	public double getHealth() { return health;}
	public double getTp() { return tp;}
	public double getAgility() { return agility;}
	public int getCardDrawQuantity() { return cardDrawQuantity;}
	public List<Skill> getSkills() { return skills;}
	public List<Card> getCards() { return cards;}
	public List<Buff> getBuffs() { return buffs;}

	// Setters
	public void setHealth(double health) { this.health = health;}
	public void setTp(double tp) { this.tp = tp;}
	public void setAgility(double agility) { this.agility = agility;}
	public void setCardDrawQuantity(int cardDrawQuantity) { this.cardDrawQuantity = cardDrawQuantity;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
