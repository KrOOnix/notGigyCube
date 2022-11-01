using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static T[] ShuffleArray<T>(T[] array,int seed)
    {
        System.Random rng = new System.Random();

        for(int i = 0; i < array.Length-1; i++)
        {
            int randomInedx = rng.Next(i,array.Length);
            T tempItem = array[randomInedx];
            array[randomInedx] = array[i];
            array[i] = tempItem;
        }

        return array;
    }

}
