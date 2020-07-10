using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using FireType = Spell.FireType;
using SpellType = Spell.SpellType;
using TargetingType = Spell.TargetingType;

public abstract class SpellHandler : MonoBehaviour
{
    [SerializeField] public Spell spell;
    [SerializeField] public Spell lastSpell;
    [SerializeField] public GameObject player;
    public PlayerControl playerControl;

    public string name = "name"; //Name of spell
    public float manaCost = 0; //Mana consumed on spell use or rate of mana consumed if streamed
    public float cooldown = 0;
    public float magnitude = 0; //Magnitude of spells effect (damage, health healed, speed modifier, etc)
    public float duration = 0; //Time until spell effect is destroyed

    public Texture2D icon;
    public AudioSource castSound;


    public InputProcess instantFire;
    public InputProcess constantFire;

    private FireType fireType;
    public SpellType spellType; 
    public TargetingType targetingType; 


    public float nextReadyTime;
    public float coolDownTimeLeft;

    private bool initialized = false;

    private void Awake()
    {
        OnAwake();
    }

    public abstract void OnAwake();

    private void Start()
    {
        lastSpell = spell;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpellChange();
        CheckInitialization();

        switch ( fireType ) 
        {
            case FireType.Instant:
                FireWithCooldown();
                break;
            case FireType.Charge:
                break;
            case FireType.Stream:
                break;
            default:
                System.Exception exception = new System.Exception("Unidentified Fire Type or Null");
                Trace.TraceError(exception.Message);
                break;
        }

       

        lastSpell = spell;
    }

    private void SpellCast()
    {  
        castSound.Play();
        spell.cast();
    }

   
    void CheckSpellChange()
    {
        if ( spell != lastSpell )
        {
            initialized = false;
        }
    }

    void CheckInitialization()
    {
        if ( !initialized )
        {
            Init();
        }
    }

    public void Init()
    {
        this.name = spell.name;
        this.manaCost = spell.manaCost;
        this.cooldown = spell.cooldown;
        this.magnitude = spell.magnitude;
        this.duration = spell.duration;

        //this.icon = spell.icon;
        this.castSound = GetComponent<AudioSource>();
        this.castSound.clip = spell.castSound;

        this.spellType = spell.spellType;
        this.targetingType = spell.targetingType;
        fireType = spell.fireType;

        initialized = true;
    }

    private void FireWithCooldown() 
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if ( coolDownComplete && initialized )
        {

            if ( instantFire.inputDown )
            {
                nextReadyTime = cooldown + Time.time;
                coolDownTimeLeft = cooldown;


                castSound.Play();
                spell.cast();
            }
        }
        else
        {
            coolDownTimeLeft -= Time.deltaTime;
        }
    }
}
