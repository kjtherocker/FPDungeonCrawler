using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Skills
{



    // Use this for initialization
    public override void Start()
    {

        //m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.Single;
        m_Damagetype = DamageType.Magic;
        m_SkillAilment = SkillAilment.Rage;
        m_Damage = 0;
        m_SkillName = "Rage";
        m_SkillDescription = "Make the enemy unable to do anything but attack";
    }


    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
            0.1f, 0.1f, 1);
        
    }
}
