using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantSpell : Spell
{
    [SerializeField] public float cooldown = 0; //Period of cooldown time
}
