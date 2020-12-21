using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTab : UiTab
{
    // Start is called before the first frame update

    public Slider m_Healthbar;
    public TextMeshProUGUI m_EnemyName;

    public Image ElementalWeakness;
    public Image ElementalStrength;
    
    public Image m_ImageHightlight;
    
    public void SetupTab(Creatures aTurnHolder)
    {
         
         m_Healthbar.value = aTurnHolder.m_CurrentHealth / aTurnHolder.m_MaxHealth;

         ElementalStrength.material = SkillList.instance.GetElementalIcon(aTurnHolder.elementalStrength);
         ElementalWeakness.material = SkillList.instance.GetElementalIcon(aTurnHolder.elementalWeakness);
         

          m_EnemyName.text = aTurnHolder.Name;
        
     //   m_CostToUseText.text = aSkill.m_Cost.ToString();
    }
}
