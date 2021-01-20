using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creatures
{

    public List<Skills> m_SkillLootTable;
    
    public override void SetCreature()
    {
        m_Skills = new List<Skills>();
        m_BloodArts = new List<Skills>();
        m_StatusEffectsOnCreature = new List<StatusEffects>();
        
        m_Devour = new Devour();
        
        m_CreatureSkillList = SkillList.instance;

        FloatingUiElementsController.Initalize();
        
        //m_Attack = gameObject.AddComponent<Attack>();
        
        m_IsUi = false;
        CurrentDomainpoints = MaxDomainPoints;
    }
    public override void Death()
    {
        base.Death();
      // Grid.Instance.GetNode(m_CreatureAi.m_Position.x, m_CreatureAi.m_Position.y).m_CreatureOnGridPoint = null;

      // Grid.Instance.GetNode(m_CreatureAi.m_Position.x, m_CreatureAi.m_Position.y).m_IsCovered = false;

       Destroy(gameObject);
    }

    public virtual Skills AiSetup()
    {
        int RandomizedSkill = Random.Range(0, m_Skills.Count - 1);
        
        return m_Skills[RandomizedSkill];
    }

}
