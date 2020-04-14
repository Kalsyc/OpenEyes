using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFacts : MonoBehaviour
{
    private string[] facts = new string[10];
    private System.Random rnd;

    public void Start()
    {
        rnd = new System.Random();
        facts[0] = "LMAO ZE DONG";
        facts[1] = "HEY YO";
        facts[2] = "ISNT CIRCUIT BREAKER FUN";
        facts[3] = "EEIFEIFMEAF";
        facts[4] = "WHAT A DADDY";
        facts[5] = "HITLER SALUTE";
        facts[6] = "MEIYEN NOONA";
        facts[7] = "JARON SENPAI";
        facts[8] = "MAKNAE YANG";
        facts[9] = "DAYUM";
    }

    public string GetRandomFact()
    {
        return facts[this.rnd.Next(0, facts.Length)];
    }
}
