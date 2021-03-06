﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixSpirit : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Light;
        m_SkillType = SkillType.Heal;
        m_Damagetype = DamageType.Magic;
        m_Damage = 0;
        m_SkillName = "Phoenix Spirit";
        m_SkillDescription = "Resurrect one dead party member";
    }

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
            0.1f, 0.1f, 1);
    }
    
}

