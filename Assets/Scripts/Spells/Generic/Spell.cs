using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float manaCost = 0;
    [SerializeField] public string name = "name";
    [SerializeField] public float cooldown = 0;
    [SerializeField] public float magnitude = 0;
    [SerializeField] public float duration = 0;

    [SerializeField] public Texture2D icon;
    [SerializeField] public AudioClip castSound;

    [SerializeField] public SpellType spellType;
    [SerializeField] public TargetingType targetingType;

    public abstract void Initialize(GameObject player);
    public abstract void cast();

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
        Cursor

    }
}
