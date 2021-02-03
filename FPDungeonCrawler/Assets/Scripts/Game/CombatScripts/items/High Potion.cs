using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPotion : Items
{
    // Start is called before the first frame update

    private int m_AddedHealth;


    public override void Start()
    {
        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Heal;
        m_SkillName = "High Potion";
        m_SkillDescription = "Recover 500 HP for one ally";

        m_SkillRange = false;

        m_AddedHealth = 500;
    }

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker)
    {
        return aVictum.IncrementHealth(m_AddedHealth);
    }

}
