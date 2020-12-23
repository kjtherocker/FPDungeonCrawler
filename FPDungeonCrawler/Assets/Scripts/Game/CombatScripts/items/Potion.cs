using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Items
{
    // Start is called before the first frame update

    private int m_AddedHealth;
    
    
    public virtual void Initialize()
    {
        m_ItemElementalType = Skills.ElementalType.Null;
        m_ItemSkillType = Skills.SkillType.Heal;
        m_ItemName = "Potion";
        m_ItemDescription = "Recover 500 HP for one ally";

        m_AddedHealth = 500;
    }
    

    public virtual IEnumerator UseItem(Creatures aVictum, Creatures aAttacker )
    {
        aAttacker.IncrementHealth(m_AddedHealth);

        return null;

    }
}
