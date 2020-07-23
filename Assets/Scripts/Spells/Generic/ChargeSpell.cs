using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargeSpell : Spell 
{

    [SerializeField] public float cooldown = 0; //Period of cooldown time
    [SerializeField] public float chargeTime = 0; //Time until spell is released while held

    [SerializeField] public AudioClip chargeSound;
}
