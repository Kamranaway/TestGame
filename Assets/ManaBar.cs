using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    Slider slider;
    PlayerStats stats;
    private void Awake()
    {
        this.slider = GetComponent<Slider>();
        this.stats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = stats.mana;
    }
}
