using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEyes : Enemy
{

    // Use this for initialization
    void Start()
    {
        m_CurrentHealth = 2000;
        m_MaxHealth = 2000;
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        Name = "Red Eyes";

        AmountOfTurns = 2;


        SetCreature();

        m_Attack = GameManager.Instance.m_SkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(GameManager.Instance.m_SkillList.SetSkills(SkillList.SkillEnum.icerain));
  

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Bosses/Prefab_RedEyes", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_RedEyes", typeof(Material));


        charactertype = Charactertype.Enemy;
        elementalStrength = Skills.ElementalType.Fire;
        elementalWeakness = Skills.ElementalType.Ice;
    }


}