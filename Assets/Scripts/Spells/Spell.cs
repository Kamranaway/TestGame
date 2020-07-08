using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * META data for spell
 */
public class Spell : SpellScript
{
    [SerializeField] public int id;
    [SerializeField] public float manaCost;
    [SerializeField] public string name;

    [SerializeField] public Texture2D icon;
    [SerializeField] public AudioSource useSound;

    [SerializeField] public SpellType spellType;
    [SerializeField] public TargetingType targetingType;

    public enum SpellType
    {
        Healing,
        Mobility,
        Melee,
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
