﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigma : Ally {


   

	// Use this for initialization
	public override void Initialize ()
	{

		m_MaxHealth = 300;
		m_CurrentHealth = m_MaxHealth;

        m_MaxMana = 100;
        m_CurrentMana = m_MaxMana;
        
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        Name = "Sigma";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 8;
		
        m_Attack = m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Attack);

        m_DomainList = DomainList.DomainListEnum.PatchworkChimera;
        
        m_Domain = new PatchWorkChimera();
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        m_Domain.m_Creature = this;

        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Invigorate));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.FireBall));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Objects/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = Skills.ElementalType .Fire;
        elementalWeakness = Skills.ElementalType .Null;

    }
	
	// Update is called once per frame

}
