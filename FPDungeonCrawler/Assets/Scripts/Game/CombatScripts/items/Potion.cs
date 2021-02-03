using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Items
{
    // Start is called before the first frame update

    private int m_AddedHealth;
    
    
    public override void Start()
    {
        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Heal;
        m_SkillName = "Potion";
        m_SkillDescription = "Recover 500 HP for one ally";

        m_SkillRange = false;
        
        m_AddedHealth = 100;
    }
    
    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.IncrementHealth(m_AddedHealth);
    }

}
