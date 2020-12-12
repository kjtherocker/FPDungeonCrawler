using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavia : Ally
{


    // Use this for initialization
    public override void Initialize ()
    {
        CurrentHealth = 50;
        MaxHealth = 50;
        
        
        MaxMana = 80;
        CurrentMana = MaxMana;
        
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        Name = "Cavia";

        AmountOfTurns = 1;

        m_CreatureMovement = 8;
        
        SetCreature();
        
                
        m_Domain = new PatchWorkChimera();
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        
        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));

        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Flying);



        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Cavia/Prefab/Pref_Cavia", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = Skills.ElementalType .Wind;
        elementalWeakness = Skills.ElementalType .Lightning;
    }

}
