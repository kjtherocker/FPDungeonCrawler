using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRain : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Attack;
        m_Damagetype = DamageType.Magic;
        m_Damage = 5;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/IceWave/ParticleEffect_IceWave", typeof(ParticleSystem));
        m_SkillName = "Ice Rain";
        m_SkillDescription = "IceRain that will hit the whole enemy team";
        
        m_SingleTargetCost = 5;
        m_MultiTargetCost = m_SingleTargetCost * 2;
    }
    

    public override List<IEnumerator> UseSkill(List<Creatures> aVictum, Creatures aAttacker )
    {
        List<IEnumerator> AllSkillActions = new List<IEnumerator>();

        for (int i = 0; i < aVictum.Count; i++)
        {
            AllSkillActions.Add(aVictum[i].DecrementHealth(m_Damage + aAttacker.GetAllStrength() / 3, GetElementalType(),
                0.1f, 0.1f, 1));
        }

        return AllSkillActions;
    }
    
    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength() /3, GetElementalType(),
            0.1f, 0.1f, 1);
    }
}