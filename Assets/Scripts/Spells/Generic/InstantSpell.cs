using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spell fires on trigger frame and has cooldown
 */
public abstract class InstantSpell : Spell
{
    [SerializeField] public float cooldown = 0; //Period of cooldown time
}
