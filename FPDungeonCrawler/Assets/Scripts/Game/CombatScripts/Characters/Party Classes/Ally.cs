using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Creatures
{
    private UiStatus m_Status;
    public void SetCreature()
    {
        m_Skills = new List<Skills>();
        m_BloodArts = new List<Skills>();
        m_StatusEffectsOnCreature = new List<StatusEffects>();
        
        m_Devour = new Devour();
        
        FloatingUiElementsController.Initalize();
        
        m_CreatureSkillList = SkillList.instance;
        m_IsUi = true;
        CurrentDomainpoints = MaxDomainPoints;
    }

    public void SetStatus(UiStatus aStatus)
    {
        m_SpawnObject = aStatus.gameObject;
    }

    public override void Death()
    {
        base.Death();
        
    //    Destroy(ModelInGame.gameObject);
    }
}
