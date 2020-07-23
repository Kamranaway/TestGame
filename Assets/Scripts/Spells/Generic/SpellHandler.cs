using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using FireType = Spell.FireType;

using TargetingType = Spell.TargetingType;


/*
 * Pulls data from spell data containers and effectively equips a spell. Inherited by left and right instances of SpellHandler. 
 * Need to add cooldown for charge.
 */
public abstract class SpellHandler : MonoBehaviour
{
    [SerializeField] public Spell spell;
   
    [HideInInspector][SerializeField] public Spell lastSpell;
    [SerializeField] public GameObject player;
    public PlayerControl playerControl;
    public PlayerStats stats;

    [HideInInspector] public string name = "name"; //Name of spell
    [HideInInspector] public float manaCost = 0; //Mana consumed on spell use or rate of mana consumed if streamed
    [HideInInspector] public float cooldown = 0;
    [HideInInspector] public float magnitude = 0; //Magnitude of spells effect (damage, health healed, speed modifier, etc)
    [HideInInspector] public float duration = 0; //Time until spell effect is destroyed
    [HideInInspector] public float frequency = 0;
    [HideInInspector] public float chargeTime = 0;

    [HideInInspector] public Texture2D icon;
    [HideInInspector] public AudioSource castSound;
    [HideInInspector] public AudioSource chargeSound;


    public InputProcess instantFire;
    public InputProcess constantFire;

    [HideInInspector] private FireType fireType;

    [HideInInspector] public TargetingType targetingType;


    [HideInInspector] public float nextReadyTime;
    [HideInInspector] public float coolDownTimeLeft;

    private bool initialized = false;
    private bool streaming = false;
    private bool streamCycled = true;
    private bool chargeCycled = true;

    private Coroutine stream;
    private Coroutine charge;

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
                FireWithCharge();
                break;
            case FireType.Stream:
                FireWithStream();
                break;
            default:
                try
                {
                    throw new System.Exception("Unidentified FireType or Null");
                }
                catch ( System.Exception exception )
                {
                    ErrorEvent.Trace(exception);
                }
                break;
        }

       

        lastSpell = spell;
    }

    private void SpellCast()
    {  
        castSound.Play();
        spell.Cast();
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
        //this.icon = spell.icon;
        this.manaCost = spell.manaCost;
        this.magnitude = spell.magnitude;
        this.duration = spell.duration;

        this.fireType = spell.fireType;

        AudioSource[] audioSources = GameObject.Find("Spells").GetComponents<AudioSource>();

        this.castSound = audioSources[0];
        this.castSound.clip = spell.castSound;
        this.castSound.clip = spell.castSound;
        this.targetingType = spell.targetingType;

        switch ( fireType ) { 
            case FireType.Instant:
                this.cooldown = ((InstantSpell) spell).cooldown;
                break;
            case FireType.Charge:
                this.cooldown = ((ChargeSpell) spell).cooldown;
                this.chargeTime = ((ChargeSpell) spell).chargeTime;
                this.chargeSound = audioSources[ 1 ];
                this.chargeSound.clip = ((ChargeSpell) spell).chargeSound;
                break;
            case FireType.Stream:
                this.frequency = ((StreamSpell) spell).frequency;
                break;
            default:
                try
            {
                throw new System.Exception("Unidentified FireType or Null");
            }
            catch ( System.Exception exception )
            {
                ErrorEvent.Trace(exception);
            }
            break;
        }

        spell.Init();

        initialized = true;
    }

    private void FireWithCooldown() 
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if ( coolDownComplete && initialized )
        {

            if ( instantFire.inputDown && stats.mana >= manaCost )
            {
                stats.mana -= manaCost;
                nextReadyTime = cooldown + Time.time;
                coolDownTimeLeft = cooldown;


                castSound.PlayOneShot(castSound.clip);
                spell.Cast();
            }
        }
        else
        {
            coolDownTimeLeft -= Time.deltaTime;
        }
    }

    private void FireWithCharge()
    {
        if ( instantFire.inputDown && stats.mana >= manaCost )
        {
            charge = StartCoroutine(ChargeCoroutine());
        }
        else if ( instantFire.inputUp )
        {
            castSound.Stop();
            castSound.clip = spell.castSound;
            castSound.loop = false;
            StopAllCoroutines();
            StopAllCoroutines();
        }
    }

    private void FireWithStream()
    { 
        if ( instantFire.inputDown && streamCycled && stats.mana >= manaCost )
        {
          stream =  StartCoroutine(StreamCoroutine());
        }
    }

    IEnumerator StreamCoroutine() 
    {
        while ( constantFire.inputDown && stats.mana >= manaCost )
        {
            stats.mana -= manaCost;
            streamCycled = false;
            castSound.PlayOneShot(castSound.clip);
            spell.Cast();
           
            yield return new WaitForSeconds(frequency);
            streamCycled = true;

            if ( instantFire.inputUp ) 
            {
                StopAllCoroutines();
            }
        }
    }

    IEnumerator ChargeCoroutine()
    {
        bool cycleComplete = false;
        while ( !cycleComplete )
        {
            if ( constantFire.inputDown && !cycleComplete)
            {
   
                chargeSound.loop = true;
                chargeSound.Play();
                yield return new WaitForSeconds(chargeTime);
                stats.mana -= manaCost;
                chargeSound.Stop();
                chargeSound.loop = false;
                castSound.PlayOneShot(castSound.clip);             
                spell.Cast();
                cycleComplete = true;
                
            }
        } 
    }
}
