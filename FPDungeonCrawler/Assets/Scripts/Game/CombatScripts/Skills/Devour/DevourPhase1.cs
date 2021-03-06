﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourPhase1 : Skills
{
    public override void Start()
    {

        m_ElementalType = ElementalType.Fire;
        m_SkillAilment = SkillAilment.Poison;
        m_SkillType = SkillType.Attack;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/FireWave/ParticleEffect_FireWave", typeof(ParticleSystem));
        m_Damage = 10;
        m_SkillRange = 1;
        m_SkillName = "Devour Phase 1";
        m_SkillDescription = "Devour the domain directly around creature";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
