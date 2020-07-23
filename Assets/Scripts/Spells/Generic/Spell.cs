using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public string name = "name"; //Name of spell
    [SerializeField] public float manaCost = 0; //Mana consumed on spell use or rate of mana consumed if streamed
    [SerializeField] public float magnitude = 0; //Magnitude of spells effect (damage, health healed, speed modifier, etc)
    [SerializeField] public float duration = 0; //Time until spell effect is destroyed

    [SerializeField] public Texture2D icon;
    [SerializeField] public AudioClip castSound;
   
    [SerializeField] public TargetingType targetingType; //This notes what a given spell will target
    [SerializeField] public FireType fireType; //The fire type is the method in which a spell is cast

    [HideInInspector] public GameObject castPoint;
    [HideInInspector] public Crosshair crosshair;

    public abstract void Init();
    public abstract void Cast();

    public enum TargetingType
    {
        Enemy,
        Player,
        Cursor

    }

    public enum FireType
    { 
        Charge, //Spell must be charged and released to be cast
        Stream, //Spell will constantly be cast while firing
        Instant, //Spell will fire on instance of firing
    }
}
