using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHandler : MonoBehaviour
{
    [SerializeField] public Spell spell;
    [SerializeField] public Spell lastSpell;
    [SerializeField] public GameObject player;
    public PlayerControl playerControl;

    public float coolDownDuration;
    public AudioSource castSound;
    public float nextReadyTime;
    public float coolDownTimeLeft;
    public InputProcess instantFire;
    public InputProcess constantFire;

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


        bool coolDownComplete = (Time.time > nextReadyTime);
        if ( coolDownComplete && initialized )
        {

            if ( instantFire.inputDown )
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }

        lastSpell = spell;
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
        castSound = GetComponent<AudioSource>();
        coolDownDuration = spell.cooldown;
        initialized = true;
    }

}
