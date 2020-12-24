using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invigorate : StatusEffects
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Buff;
        m_Damagetype = DamageType.Magic;
        m_SkillName = "Invigorate";
        m_SkillDescription = "A buff that greatly increases damage";
            
        m_SingleTargetCost = 5;
        m_MultiTargetCost = m_SingleTargetCost * 2;
        
        ActivatedCreature = new Dictionary<Creatures, int>();
    }
    

    public override List<IEnumerator> UseSkill(List<Creatures> aVictum, Creatures aAttacker )
    {
        List<IEnumerator> AllSkillActions = new List<IEnumerator>();

        Length = 1;
        
        for (int i = 0; i < aVictum.Count; i++)
        {
            aVictum[i].BuffStrength = aVictum[i].BaseStrength / 4;
        //    ActivatedCreature.Add(aVictum[i], Length);
            AllSkillActions.Add(aVictum[i].SetStatusEffect(this));
        }

        return AllSkillActions;
    }
    
    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        Length = 1;

        aVictum.BuffStrength = aVictum.BaseStrength / 4;

  //      ActivatedCreature.Add(aVictum, Length);
        
        return aVictum.SetStatusEffect(this);
    }

    public override void RevertStatusEffect(Creatures aCreature)
    {
       // ActivatedCreature[aCreature].BuffStrength = 0;

    }

}
