using UnityEngine;
public class Sorin : CharacterController
{
    
    private void Awake()
    {
        origStats.Add("atk", atk);
        origStats.Add("def", def);
        origStats.Add("spd", spd);
        origStats.Add("hp", hp);
        origStats.Add("mp", mp);
        origStats.Add("jump", jump);
        origStats.Add("dist", movedist);
        origStats.Add("range", atkrange);
        origStats.Add("attackHeight", atkheight);
        currentStats = origStats;

        gameObject.AddComponent(typeof (Heal));
        abilityList.Add(GetComponent<Heal>());  
        gameObject.AddComponent(typeof(Heal));
        abilityList.Add(GetComponent<Heal>());
    }

}
