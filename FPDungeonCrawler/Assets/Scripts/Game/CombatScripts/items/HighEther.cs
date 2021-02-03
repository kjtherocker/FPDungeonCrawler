using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighEther  : Items
{
    // Start is called before the first frame update

    private int m_AddedHealth;


    public override void Start()
    {
        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Heal;
        m_SkillName = "High Ether";
        m_SkillDescription = "Recover 300 MP for one ally";

        m_SkillRange = false;

        m_AddedHealth = 300;
    }

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker)
    {
        return aVictum.IncrementMana(m_AddedHealth);
    }

}


