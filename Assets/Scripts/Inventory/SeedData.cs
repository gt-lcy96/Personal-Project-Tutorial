using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;

    //item to yield after the seed grow
    public ItemData cropToYield;
}