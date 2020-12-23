using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Heal;
        m_Damagetype = DamageType.Magic;
        m_Damage = 300;
        m_SkillName = "Holy Water";
        m_SkillDescription = "Heals the whole party a small amount";

        m_SingleTargetCost = 5;
        m_MultiTargetCost = m_SingleTargetCost * 2;
    }
    
    public override List<IEnumerator> UseSkill(List<Creatures> aVictum, Creatures aAttacker )
    {
        List<IEnumerator> AllSkillActions = new List<IEnumerator>();

        for (int i = 0; i < aVictum.Count; i++)
        {
            AllSkillActions.Add(aVictum[i].IncrementHealth(m_Damage));
        }
        

        return AllSkillActions;
    }
    
    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.IncrementHealth(m_Damage);
    }
}
