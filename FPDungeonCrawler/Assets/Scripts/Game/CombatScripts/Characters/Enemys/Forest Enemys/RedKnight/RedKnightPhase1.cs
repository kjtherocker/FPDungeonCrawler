using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase1 : Enemy
{

    // Use this for initialization
    public override void Initialize ()
    {
        m_CurrentHealth = 65;
        m_MaxHealth = m_CurrentHealth;
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

        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.icerain));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.LightRay));

        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Restrict));
        
        AmountOfTurns = 1;
        
        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase1", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = Skills.ElementalType.Ice;
        elementalWeakness = Skills.ElementalType .Fire;
    }

    public override Skills AiSetup()
    {
        int RandomSkillChooser = Random.Range(0, m_Skills.Count - 1);
        
        return m_Skills[RandomSkillChooser];
    }
    

}