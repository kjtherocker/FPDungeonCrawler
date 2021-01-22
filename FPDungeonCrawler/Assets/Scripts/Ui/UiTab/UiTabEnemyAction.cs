using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UiTabEnemyAction : UiTabs
{
    private Skills m_Skill;
    private string m_Name;

    public TextMeshProUGUI m_TextName;
    public TextMeshProUGUI m_TextSkillDescription;
    public Image m_ElementalIcon; 
    public void SetEnemyActionUi(Skills aSkill, string aCreatureName, string aAttackedCreature)
    {
        m_TextName.text = aCreatureName;
        m_TextSkillDescription.text = "Casted " + "[" + aSkill.m_SkillName + "]" + " To " + aAttackedCreature;
        m_ElementalIcon.material = SkillList.instance.GetElementalIcon(aSkill.m_ElementalType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
