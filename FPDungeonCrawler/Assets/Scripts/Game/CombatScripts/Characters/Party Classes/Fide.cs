using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fide : Ally
{
    // Use this for initialization
    public override void Initialize ()
    {
        m_CurrentHealth = 300;
        m_MaxHealth = 300;
        m_MaxMana = 20;
        m_CurrentMana = m_MaxMana;
        
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        Name = "Fide";

        AmountOfTurns = 1;


        SetCreature();

        CurrentDomainpoints = 0;
                
        m_Domain = new CrystalFool();
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        m_Domain.m_Creature = this;
        
        m_Attack = m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.ShadowBlast));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.icerain));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.FireBall));

        m_BloodArts.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.BloodRelief));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Fide/Pref_Fide", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = Skills.ElementalType .Earth;
        elementalWeakness = Skills.ElementalType .Shadow;

    }
}
