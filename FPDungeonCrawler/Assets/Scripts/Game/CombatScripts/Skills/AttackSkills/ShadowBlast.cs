using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBlast : Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Shadow;
        m_SkillType = SkillType.Attack;
        m_Damagetype = DamageType.Magic;

        m_Damage = 10;
        m_SkillName = "Shadow Blast";
        m_SkillDescription = "Blast that will hit the whole enemy team";
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/DarkWave/ParticleEffect_DarkWave", typeof(ParticleSystem));
        
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