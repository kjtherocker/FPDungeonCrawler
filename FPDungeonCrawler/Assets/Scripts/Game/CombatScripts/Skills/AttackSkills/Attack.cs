﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skills
{



    public override void Start()
    {

        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.Single;
        m_Damagetype = DamageType.Strength;
        m_Damage = 10;
        m_SkillName = "Attack";
        m_SkillDescription = "Attack a single enemy";
        m_AnimationName = "t_IsAttack";
        m_SkillRange = 1;
    }

    // Update is called once per frame

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
            0.1f, 0.1f, 1);
        
    }
}
