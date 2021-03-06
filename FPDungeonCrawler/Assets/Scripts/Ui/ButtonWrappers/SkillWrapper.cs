﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class SkillWrapper : MonoBehaviour
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



    public Color m_Color_TransparentWhite;
    public Color m_Color_HalfTransparentWhite;
    public Color m_Color_White;

    // Use this for initialization
    void Start()
    {

    }
    
    public void SetElementalIcon(Skills.ElementalType aSkills, string sourceName = "Global")
    {
        m_Image_ElementalIcon.material = m_ElementIconsList[(int)aSkills];
    }


    public void SetupButton(Creatures aTurnHolder, Skills aSkill, bool aSkillSize)
    {
        m_ButtonTurnHolder = aTurnHolder;
        m_ButtonSkill = aSkill;
        m_SkillType = aSkill.GetSkillType();

        SetElementalIcon(aSkill.GetElementalType());
        m_Text_NameOfSkill.text = aSkill.m_SkillName;

        if (aSkill.m_SkillName == "")
        {
            m_Text_NameOfSkill.text = "Name Is Empty";
        }


        int SkillSize = aSkillSize ?  m_ButtonSkill.m_MultiTargetCost : m_ButtonSkill.m_SingleTargetCost ;
        
        if (m_ButtonTurnHolder.m_CurrentMana <= SkillSize)
        {
            m_Text_NameOfSkill.color = m_Color_HalfTransparentWhite;
        }
        else if (m_ButtonTurnHolder.m_CurrentMana >= SkillSize)
        {
            m_Text_NameOfSkill.color = m_Color_White;
        }

      m_CostToUseText.text = SkillSize.ToString();

    }

    public void SkillHoveredOver(bool isHoverovered)
    {
        Highlight.color = isHoverovered ? m_Color_TransparentWhite : m_Color_White;
    }
}
