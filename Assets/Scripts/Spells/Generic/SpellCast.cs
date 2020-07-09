using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{

    [SerializeField] Spell spell;
    [SerializeField] private GameObject player;
    SpellControl spellControl;
    private float coolDownDuration;
    private AudioSource castSound;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private void Awake()
    {
        spellControl = FindObjectOfType<SpellControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init(spell, player);
    }

    public void Init(Spell spell, GameObject player)
    {
        this.spell = spell;
        castSound = GetComponent<AudioSource>();
        coolDownDuration = spell.cooldown;
        spell.Initialize(player);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if ( coolDownComplete )
        {
            AbilityReady();
            if ( spellControl.instantFireR.inputDown )
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        Debug.Log(roundedCd);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;

        castSound.clip = spell.castSound;
        castSound.Play();
        Debug.Log("cast");
        spell.cast();
    }

}
