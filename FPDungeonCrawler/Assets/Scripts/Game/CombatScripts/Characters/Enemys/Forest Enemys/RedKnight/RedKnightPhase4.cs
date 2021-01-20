using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase4 : Enemy
{

    // Use this for initialization
    public override void Initialize ()
    {
        m_CurrentHealth = 45;
        m_MaxHealth = 45;
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        if (Name == "No Name")
        {
            Name = GameManager.Instance.m_NameGenerator.GetName();
            transform.name = Name;
        }


        SetCreature();

        m_Attack = m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Attack);
        
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.icerain));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.FireBall));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.LightRay));

        m_SkillLootTable.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.FireBall));
        m_SkillLootTable.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.HolyWater));
        m_SkillLootTable.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Restrict));


        AmountOfTurns = 1;
        

        
        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase4", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = Skills.ElementalType.Lightning;
        elementalWeakness = Skills.ElementalType.Wind;
    }


}