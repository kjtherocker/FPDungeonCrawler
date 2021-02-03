using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ItemWrapper : MonoBehaviour
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
    
    public void SetElementalIcon(Skills.ElementalType aElementalIcon, string sourceName = "Global")
    {
        m_Image_ElementalIcon.material = SkillList.instance.GetElementalIcon(aElementalIcon);
    }


    public void SetupButton(Creatures aTurnHolder, ItemCore aItemCore)
    {
        m_ButtonTurnHolder = aTurnHolder;
        m_ButtonSkill = aItemCore.m_Items;
        m_SkillType = aItemCore.m_Items.GetSkillType();

        SetElementalIcon(aItemCore.m_Items.GetElementalType());
        m_Text_NameOfSkill.text = aItemCore.m_Items.m_SkillName;

        if (aItemCore.m_Items.m_SkillName == "")
        {
            m_Text_NameOfSkill.text = "Name Is Empty";
        }
        

      m_CostToUseText.text = aItemCore.m_Amount.ToString();

    }

    public void SkillHoveredOver(bool isHoverovered)
    {
        Highlight.color = isHoverovered ? m_Color_TransparentWhite : m_Color_White;
    }
}
