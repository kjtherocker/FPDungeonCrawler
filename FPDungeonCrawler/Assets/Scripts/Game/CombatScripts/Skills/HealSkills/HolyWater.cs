using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Heal;
        m_SkillFormation = SkillFormation.Single;
        m_Damagetype = DamageType.Magic;
        m_Damage = 300;
        m_SkillName = "Holy Water";
        m_SkillDescription = "Heals the whole party a small amount";

    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.IncrementHealth(m_Damage);
    }
}
