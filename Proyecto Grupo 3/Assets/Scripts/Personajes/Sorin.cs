using UnityEngine;

public class Sorin : CharacterController
{
    private void Awake()
    {
        origStats.Add("atk",3);
        origStats.Add("def",3);
        origStats.Add("spd",3);
        origStats.Add("hp",30);
        origStats.Add("mp",30);
        origStats.Add("jump",2);
        origStats.Add("dist",4);
        origStats.Add("range",4);
        origStats.Add("attackHeight", 3);
        currentStats = origStats;

        origStats.Add("totalAbilities", 3);
    }
}
