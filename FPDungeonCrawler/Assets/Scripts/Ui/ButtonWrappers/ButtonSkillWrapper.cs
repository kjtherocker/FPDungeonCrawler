using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ButtonSkillWrapper : MonoBehaviour
{


    private Creatures m_ButtonTurnHolder;

    private List<Creatures> m_ListReference;
    public List<Material> m_ElementIconsList;

    private Skills.ElementalType m_ElementalIconType;
    private Skills.SkillType m_SkillType;

    public Skills m_ButtonSkill;
    public TextMeshProUGUI m_CostToUseText;
    public TextMeshProUGUI m_Text_NameOfSkill;
    public Image m_Image_ElementalIcon;

    public Image Highlight;



    Color m_Color_TransparentWhite;
    Color m_Color_HalfTransparentWhite;
    Color m_Color_White;

    // Use this for initialization
    void Start()
    {
         m_Color_TransparentWhite = new Color(1, 1, 1, 0.0f);
         m_Color_HalfTransparentWhite = new Color(1,1,1,0.5f);
         m_Color_White = new Color(1, 1, 1, 1);
    }
    
    public void SetElementalIcon(Skills.ElementalType aSkills, string sourceName = "Global")
    {
        m_Image_ElementalIcon.material = m_ElementIconsList[(int)aSkills];
    }


    public void SetupButton(Creatures a_TurnHolder, Skills a_Skill)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_ButtonSkill = a_Skill;
        m_SkillType = a_Skill.GetSkillType();

        SetElementalIcon(a_Skill.GetElementalType());
        m_Text_NameOfSkill.text = a_Skill.m_SkillName;

        if (a_Skill.m_SkillName == "")
        {
            m_Text_NameOfSkill.text = "Name Is Empty";
        }


        
       if (m_ButtonTurnHolder.CurrentMana <= m_ButtonSkill.m_Cost)
       {
           m_Text_NameOfSkill.color = m_Color_HalfTransparentWhite;
       }
       else if (m_ButtonTurnHolder.CurrentMana >= m_ButtonSkill.m_Cost)
       {
           m_Text_NameOfSkill.color = m_Color_White;
       }
    }

    public void SkillHoveredOver(bool isHoverovered)
    {
        if (isHoverovered)
        {
            Highlight.color = m_Color_TransparentWhite;
        }
        else
        {
            Highlight.color = m_Color_White;
        }
    }
}
