using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    

    public Image m_Background;
    public Color m_BackgroundColorDefault;
    public Color m_BackgroundColorSelected;
    
    public Color m_TextColorDefault;
    public Color m_TextColorSelected;

    public TextMeshProUGUI m_Text;
    public void Start()
    {
      //  if (m_Arena != null)
      //  {
      //      m_TextName.text = m_Gridformation.m_ArenaName;
      //      m_TextMission.text = m_Gridformation.m_MissionTag;
      //      m_TextDescription.text = m_Gridformation.m_Description;
      //  }

    }

    public void ChangeColorToDefault()
    {
        m_Background.color = m_BackgroundColorDefault;
        m_Text.color = m_TextColorDefault;
    }

    public void ChangeColorToSelected()
    {
        m_Background.color = m_BackgroundColorSelected;
        m_Text.color = m_TextColorSelected;
    }


}
