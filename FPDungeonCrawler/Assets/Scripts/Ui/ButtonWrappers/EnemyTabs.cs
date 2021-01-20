using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTabs : UiTabs
{
    // Start is called before the first frame update

    public Slider m_Healthbar;
    public TextMeshProUGUI m_EnemyName;

    public Image ElementalWeakness;
    public Image ElementalStrength;
    
    public Image m_ImageHightlight;

    public Creatures m_CreatureInUse;

    public bool m_InUse;
    
    public void SetupTab(Creatures aTurnHolder)
    {
        m_CreatureInUse = aTurnHolder;
         m_Healthbar.value = aTurnHolder.m_CurrentHealth / aTurnHolder.m_MaxHealth;

         ElementalStrength.material = SkillList.instance.GetElementalIcon(aTurnHolder.elementalStrength);
         ElementalWeakness.material = SkillList.instance.GetElementalIcon(aTurnHolder.elementalWeakness);
         

          m_EnemyName.text = aTurnHolder.Name;
        
     //   m_CostToUseText.text = aSkill.m_Cost.ToString();
    }

    public void Update()
    {
        
        float SliderPercentage = (float) m_CreatureInUse.m_CurrentHealth / (float)m_CreatureInUse.m_MaxHealth;
        m_Healthbar.value = SliderPercentage;

        if (SliderPercentage <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
