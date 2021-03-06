﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : Ally
{


   

    // Use this for initialization
    void Start ()
    {
        m_CurrentHealth = 50;
        m_MaxHealth = 50;
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        Name = "Alpha";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 5;
		
        m_Attack = m_CreatureSkillList.GetSkill(SkillList.SkillEnum.Attack);

        m_DomainList = DomainList.DomainListEnum.PatchworkChimera;
        
      // m_Domain = DomainList.Instance.SetDomain(m_DomainList);
      // m_Domain.Start();
      // m_Domain.DomainUser = Name;
        
        m_Skills.Add(m_CreatureSkillList.GetSkill(SkillList.SkillEnum.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Dolls/Alpha/Pref_Alpha", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Objects/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = Skills.ElementalType.Ice;
        elementalWeakness = Skills.ElementalType.Fire;

    }
	
    // Update is called once per frame

}
