﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Skills
{

    // Use this for initialization


    public enum ElementalType
    {
        Fire,
        Ice,
        Light,
        Shadow,
        Wind,
        Earth,
        Lightning,
        Null
    }

    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Domain,

    }
    public enum DamageType
    {
        Strength,
        Magic
    }

    public enum SkillAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }

    [SerializeField]
    public ElementalType m_ElementalType;
    [SerializeField]
    public SkillType m_SkillType;

    [SerializeField]
    public DamageType m_Damagetype;
    [SerializeField]
    public SkillAilment m_SkillAilment;

    public int m_SingleTargetCost;
    public int m_MultiTargetCost;
    
    [SerializeField]
    public string m_SkillName;
    [SerializeField]
    public string m_SkillDescription;

    [SerializeField]
    public ParticleSystem m_SkillParticleEffect;

    [SerializeField]
    public int m_Damage;

    [SerializeField]
    public string m_AnimationName;

    
    [SerializeField]
    public int m_SkillRange;

    public virtual void Start()
    {
    
    }
    public virtual void Update()
    {

    }
    public ParticleSystem GetSkillParticleEffect()
    {
        return m_SkillParticleEffect;
    }

    public string GetSkillName()
    {
        return m_SkillName;
    }

    public string GetSkillDescription()
    {
        return m_SkillDescription;
    }

    public DamageType GetDamageType()
    {
        return m_Damagetype;
    }

    public ElementalType GetElementalType()
    {
        return m_ElementalType;
    }

    public SkillAilment GetAlimentType()
    {
        return m_SkillAilment;
    }

    public SkillType GetSkillType()
    {
        return m_SkillType;
    }

    public virtual List<IEnumerator> UseSkill(List<Creatures> aVictum, Creatures aAttacker )
    {
        
        Debug.Log( m_SkillName + " Skill was not overwritten");
        
        return null;
    }
    
    public virtual IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        
        Debug.Log( m_SkillName + " Skill was not overwritten");
        
        return null;
    }

    public int GetSkillDamage()
    {
        return m_Damage;
    }

}
