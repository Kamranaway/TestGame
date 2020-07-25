using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This class is temporary
public class CurrentSpell : MonoBehaviour
{
    [SerializeField] GameObject text;

    SpellHandler spellHandler;
    [Tooltip("side can either be left or right")] [SerializeField] string side;
    string display;

    private void Awake()
    {
        if ( side.Equals("left") )
        {
            spellHandler = FindObjectOfType<SpellHandlerLeft>();
        }
        else
        {
            spellHandler = FindObjectOfType<SpellHandlerRight>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        display = side + " Spell \n" + spellHandler.name;
        text.GetComponent<TextMeshProUGUI>().text = display;
     
        
        
    }
}
