using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions
{
    NoTarget,
    YourCreatures, 
    AllCharacters, 
    EnemyCharacters,
    YourCharacters
}

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]
	public string cardName;
    public CharacterAsset characterAsset;  // if this is null, it`s a neutral card
    [TextArea(2,3)]
    public string description;  // Description for spell or character
	public Sprite cardImage;
    public int TPCost;

    [Header("Creature Info")]
    public int maxHealth; // For skill and movement card it is 0
    public int attack;
    public int attacksForOneTurn = 1;
    public string CreatureScriptName;
    public int specialCreatureAmount;

    [Header("SpellInfo")]
    public string spellScriptName;
    public TargetingOptions targets;
	public bool exile;
	public bool isCopy;
}
