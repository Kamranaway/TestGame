using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using FireType = Spell.FireType;
using SpellType = Spell.SpellType;
using TargetingType = Spell.TargetingType;

public abstract class SpellHandler : MonoBehaviour
{
    [SerializeField] public Spell spell;
    [HideInInspector][SerializeField] public Spell lastSpell;
    [SerializeField] public GameObject player;
    public PlayerControl playerControl;

    [HideInInspector] public string name = "name"; //Name of spell
    [HideInInspector] public float manaCost = 0; //Mana consumed on spell use or rate of mana consumed if streamed
    [HideInInspector] public float cooldown = 0;
    [HideInInspector] public float magnitude = 0; //Magnitude of spells effect (damage, health healed, speed modifier, etc)
    [HideInInspector] public float duration = 0; //Time until spell effect is destroyed
    [HideInInspector] public float frequency = 0;
    [HideInInspector] public float chargeTime = 0;

    [HideInInspector] public Texture2D icon;
    [HideInInspector] public AudioSource spellSound;


    public InputProcess instantFire;
    public InputProcess constantFire;

    [HideInInspector] private FireType fireType;
    [HideInInspector] public SpellType spellType;
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
        spellSound.Play();
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
        this.manaCost = spell.manaCost;
        this.cooldown = spell.cooldown;
        this.magnitude = spell.magnitude;
        this.duration = spell.duration;
        this.frequency = spell.frequency;
        this.chargeTime = spell.chargeTime;

        //this.icon = spell.icon;
        this.spellSound = GetComponent<AudioSource>();
        this.spellSound.clip = spell.castSound;
  

        this.spellType = spell.spellType;
        this.targetingType = spell.targetingType;
        fireType = spell.fireType;

        spell.Init();

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


                spellSound.PlayOneShot(spellSound.clip);
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
        if ( instantFire.inputDown )
        {
            charge = StartCoroutine(ChargeCoroutine());
        }
        else if ( instantFire.inputUp )
        {
            spellSound.Stop();
            spellSound.clip = spell.castSound;
            spellSound.loop = false;
            StopAllCoroutines();
            StopAllCoroutines();
        }
    }

    private void FireWithStream()
    { 
        if ( instantFire.inputDown && streamCycled )
        {
          stream =  StartCoroutine(StreamCoroutine());
        }
    }

    IEnumerator StreamCoroutine() 
    {
        while ( constantFire.inputDown )
        {
            streamCycled = false;
            spellSound.PlayOneShot(spellSound.clip);
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
                spellSound.clip = spell.chargeSound;
                spellSound.loop = true;
                spellSound.Play();
                yield return new WaitForSeconds(chargeTime);
                spellSound.Stop();
                spellSound.clip = spell.castSound;
                spellSound.loop = false;
                AudioSource.PlayClipAtPoint(spellSound.clip, spellSound.transform.position, 500);
                //spellSound.PlayOneShot(spellSound.clip);
                spell.Cast();
                cycleComplete = true;
                
            }
        } 
    }
}
