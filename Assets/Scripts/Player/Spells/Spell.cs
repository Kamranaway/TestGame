using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public float manaCost;
    [SerializeField] public string name;

    [SerializeField] Texture2D texture;

    [SerializeField] public SpellType spellType;

   
    //class constructor
    public Spell(SpellType spellType, int id, string name, float manaCost) 
    {
        this.spellType = spellType;
        this.id = id;
        this.name = name;
        this.manaCost = manaCost;
    }

    public  enum SpellType
    {
        Healing,
        Mobility,
        Projectile,
        Defensive

    }

    public enum TargetingType
    { 
        Enemy,
        Player,
        Area

    }
}
