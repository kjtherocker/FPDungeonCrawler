using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Light;
        m_SkillType = SkillType.Attack;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/LightWave/ParticleEffect_LightWave", typeof(ParticleSystem));
        m_Damage = 10;
        m_SkillName = "Light Ray";
        m_SkillDescription = "a Ray that will hit the whole enemy team";
        
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
