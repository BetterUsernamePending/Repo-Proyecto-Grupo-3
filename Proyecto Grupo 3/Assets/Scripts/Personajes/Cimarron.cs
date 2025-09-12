using UnityEngine;
public class Cimarron : CharacterController
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
        currentStats.Add("atk", atk);
        currentStats.Add("def", def);
        currentStats.Add("spd", spd);
        currentStats.Add("hp", hp);
        currentStats.Add("mp", mp);
        currentStats.Add("jump", jump);
        currentStats.Add("dist", movedist);
        currentStats.Add("range", atkrange);
        currentStats.Add("attackHeight", atkheight);

        gameObject.AddComponent(typeof (Heal));
        abilityList.Add(GetComponent<Heal>());  
    }

}
