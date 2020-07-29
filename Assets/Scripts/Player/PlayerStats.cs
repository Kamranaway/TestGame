using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles player health and mana. Will later handle the intitiation of death states.
 */
public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxMana = 100;
    [Range(0f, 100f)] [SerializeField] public float mana = 100;
    [Range(0f, 100f)] [SerializeField] public float health = 100;
    [SerializeField] public float healthRegenMagnitude= 10;
    [SerializeField] public float manaRegenMagnitude = 10;
    public bool healthRegen = false;
    public bool manaRegen = false;

    private void Update()
    {
        if ( mana < maxMana  && manaRegen == false)
        {
            StartCoroutine(RegenMana(1));
            manaRegen = true;
        }

        if ( health < maxHealth && healthRegen == false) 
        {
        
            StartCoroutine(RegenHealth(1));
            healthRegen = true;
        }

        mana = Mathf.Clamp(mana, 0, maxMana);
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    IEnumerator RegenHealth(float frequency)
    {
        while ( health < maxHealth ) 
        {
            if ( health < maxHealth ) { health += healthRegenMagnitude; }
            yield return new WaitForSeconds(frequency);
        }
        healthRegen = false;
    }

    IEnumerator RegenMana(float frequency)
    {
        while ( mana < maxMana )
        {
            if ( mana < maxMana ) { mana += manaRegenMagnitude; }
            yield return new WaitForSeconds(frequency);
        }
        manaRegen = false;
    }

}
