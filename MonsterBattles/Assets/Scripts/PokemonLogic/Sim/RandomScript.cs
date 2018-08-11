using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomScript {

    //NEEDS TO BE CONTROLLED BY SEED
    //Returns a random value from 0 to 1
    public static float GetRandomValue()
    {
        //Random controlled here with seed and stuff
        return Random.value;
    }

    public static bool RandomChance(int numerator, int denominator)
    {
        return Random.Range(0, denominator) < numerator;
    }

    public static int Randomizer(int baseDamage)
    {
        return Mathf.FloorToInt(baseDamage * (100 - Random.Range(0,16)) / 100f);
    }

    public static int RandomBetween(int min, int max)
    {
        return Random.Range(min, max);
    }
}
