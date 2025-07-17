using UnityEngine;

public class Rob : CharacterController
{
    private void Awake()
    {
        origStats.Add("atk", 5);
        origStats.Add("def", 7);
        origStats.Add("spd", 2);
        origStats.Add("hp", 50);
        origStats.Add("mp", 15);
        origStats.Add("jump", 1);
        origStats.Add("dist", 3);
        origStats.Add("range", 1);
        origStats.Add("attackHeight", 1);
        currentStats = origStats;
    }
}
