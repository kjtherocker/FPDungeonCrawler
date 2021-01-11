﻿using System.Collections;
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
        aAttacker.CurrentMana -= aSkillRange ? aSkill.m_MultiTargetCost : aSkill.m_SingleTargetCost;
        
        //Single Target Attack
        if (aSkill.m_SkillType == Skills.SkillType.Attack && !aSkillRange)
        {
            AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.FireBall,AudioManager.Soundtypes.SoundEffects);
            SingleTargetAttack(aSkill, aAttacker, aSkillPosition);
        }

        //Party Wide Attacks
        if (aSkill.m_SkillType == Skills.SkillType.Attack && aSkillRange)
        {
            AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.FireBall,AudioManager.Soundtypes.SoundEffects);
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
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderAlly[aSkillPosition]);
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderEnemy[aSkillPosition]);
        }
    }
    
    
    public void PartyWideBuff(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker),TacticsManager.instance.TurnOrderAlly);
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker),TacticsManager.instance.TurnOrderEnemy);
        }
    }
    
    public void SingleTargetAttack(Skills aSkill, Creatures aAttacker, int aSkillPosition)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderEnemy[aSkillPosition]);
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderAlly[aSkillPosition]);
        }
    }
    
    
    public void PartyWideAttack(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker),TacticsManager.instance.TurnOrderEnemy);
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker),TacticsManager.instance.TurnOrderAlly);
        }
    }
    public void SingleTargetHeal(Skills aSkill, Creatures aAttacker, int aSkillPosition)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderAlly[aSkillPosition]);
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy[aSkillPosition], aAttacker),TacticsManager.instance.TurnOrderEnemy[aSkillPosition]);
        }
    }

    public void PartyWideHeal(Skills aSkill, Creatures aAttacker)
    {
        if (aAttacker.charactertype == Creatures.Charactertype.Ally)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderAlly, aAttacker),TacticsManager.instance.TurnOrderAlly );
        }
        else if (aAttacker.charactertype == Creatures.Charactertype.Enemy)
        {
            m_TacticsManager.ProcessAction(aSkill.UseSkill
                (TacticsManager.instance.TurnOrderEnemy, aAttacker),TacticsManager.instance.TurnOrderEnemy);
        }
    }


}
