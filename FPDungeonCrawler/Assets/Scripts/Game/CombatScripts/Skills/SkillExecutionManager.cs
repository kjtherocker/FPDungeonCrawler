using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecutionManager
{
    private TacticsManager m_TacticsManager;
    public SkillExecutionManager(TacticsManager aTacticsManager)
    {
        m_TacticsManager = aTacticsManager;
    }

    public void ExecuteSkill(Skills aSkill, bool aSkillRange,int aSkillPosition, Creatures aAttacker)
    {
        aAttacker.CurrentMana -= aSkillRange ? aSkill.m_SingleTargetCost : aSkill.m_MultiTargetCost;
        
        //Single Target Attack
        if (aSkill.m_SkillType == Skills.SkillType.Attack && !aSkillRange)
        {
            SingleTargetAttack(aSkill, aAttacker, aSkillPosition);
        }

        //Party Wide Attacks
        if (aSkill.m_SkillType == Skills.SkillType.Attack && aSkillRange)
        {
            PartyWideAttack(aSkill, aAttacker);
        }
        
        // Single target Heals
        if (aSkill.m_SkillType == Skills.SkillType.Heal && !aSkillRange)
        {
            SingleTargetHeal(aSkill, aAttacker, aSkillPosition);
        }
        
        // Party Wide Heals
        if (aSkill.m_SkillType == Skills.SkillType.Heal && aSkillRange)
        {
            PartyWideHeal( aSkill,  aAttacker);
        }
        
        // Single target Heals
        if (aSkill.m_SkillType == Skills.SkillType.Buff && !aSkillRange)
        {
            SingleTargetBuff(aSkill, aAttacker, aSkillPosition);
        }
        
        // Party Wide Heals
        if (aSkill.m_SkillType == Skills.SkillType.Buff && aSkillRange)
        {
            PartyWideBuff( aSkill,  aAttacker);
        }
    }
    
    public void SingleTargetBuff(Skills aSkill, Creatures aAttacker, int aSkillPosition)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker));
        }
    }
    
    
    public void PartyWideBuff(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker));
        }
    }
    
    public void SingleTargetAttack(Skills aSkill, Creatures aAttacker, int aSkillPosition)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker));
        }
    }
    
    
    public void PartyWideAttack(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker));
        }
    }
    public void SingleTargetHeal(Skills aSkill, Creatures aAttacker, int aSkillPosition)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker));
        }
    }

    public void PartyWideHeal(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker));
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessTurn(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker));
        }
    }


}
