using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * List of spells and used for index of equipped spells.
 * 
 * For the current build, please change spells through the Unity Inspector.
 */
public class Spells : MonoBehaviour
{
    [SerializeField] public List<Spell> spells; //List of all spells in the game
    [SerializeField] public List<Spell> leftSpells; //Currently equipped left handed spells
    [SerializeField] public List<Spell> rightSpells; //Currently equipped right handed spells
}
