using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Stream Spells constantly fire
 */
public abstract class StreamSpell : Spell
{

    [SerializeField] public float frequency = 0; //Frequency of spell streaming
   
}
