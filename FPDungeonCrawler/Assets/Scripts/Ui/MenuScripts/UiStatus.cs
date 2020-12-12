using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.AnimatedValues;

public class UiStatus : UiTabScreen
{
    
    public RawImage Image_Portrait;
    public Creatures Creature;


    public List<Material> m_ElementIconsList;

    public Image m_ElementalStrength;
    public Image m_ElementalWeakness;
    
    private int m_CurrentHealth = 150;
    private int m_MaxHealth = 150;

    private int m_CurrentMana = 150;
    private int m_MaxMana = 150;
    public bool m_IsSelected;

    public TextMeshProUGUI m_HealthText;
    public TextMeshProUGUI m_ManaText;
    
    public Slider m_HealthbarSlider;
    public Slider m_ManaSlider;
    public List<UiStatusDomainPointWrapper> m_DomainPointWrappers;

    // Use this for initialization
    void Start()
    {
        UpdateSliders();
        m_IsSelected = false;
    }

    public void SetCharacter(Creatures Character)
    {
        
        gameObject.SetActive(true);
        if (Creature != null)
        {
            Creature.gameObject.layer = 0;

        }

        Creature = Character;
        
        m_CurrentHealth = Creature.CurrentHealth;
        m_MaxHealth = Creature.MaxHealth;
        m_HealthText.text = m_CurrentHealth.ToString();
        
        m_CurrentMana = Creature.CurrentMana;
        m_MaxMana = Creature.MaxMana;
        m_ManaText.text = m_CurrentMana.ToString();


        m_ElementalStrength.material = m_ElementIconsList[(short)Creature.elementalStrength];
        
        m_ElementalWeakness.material = m_ElementIconsList[(short)Creature.elementalWeakness];
        
        m_HealthbarSlider.value = m_CurrentHealth / m_MaxHealth;
        if (m_CurrentMana != 0 || m_MaxMana != 0)
        {
            m_ManaSlider.value = m_CurrentMana / m_MaxMana;     
        }

       
        
        
        UpdateSliders();
        
        
        foreach (UiStatusDomainPointWrapper aSlider in m_DomainPointWrappers)
        {
            aSlider.SetDomainPointOpacity(false);
        }

        for (int i = 0; i < Creature.CurrentDomainpoints; i++)
        {
            m_DomainPointWrappers[i].SetDomainPointOpacity(true);
        }

        StartCoroutine(CatchAFrame());
     
    }

    IEnumerator CatchAFrame()
    {
        yield return new WaitForEndOfFrame();
       
       UpdateSliders();
       foreach (UiStatusDomainPointWrapper aSlider in m_DomainPointWrappers)
       {
           aSlider.SetDomainPointOpacity(false);
       }

       for (int i = 0; i < Creature.CurrentDomainpoints; i++)
       {
           m_DomainPointWrappers[i].SetDomainPointOpacity(true);
       }
    }
    
    // Update is called once per frame
    void UpdateSliders()
    {
        if (Creature == null)
        {
            return;
        }

        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
        }
        
       // m_CurrentHealth = Mathf.Max(m_CurrentHealth, 0);
       // m_CurrentHealth = Mathf.Min(m_CurrentHealth, m_MaxHealth);
//
       // float HealthRatio = (float)m_CurrentHealth / (float)m_MaxHealth;
       // m_HealthbarSlider.value  = HealthRatio;
//
        SetSliders(ref m_CurrentHealth, m_MaxHealth, m_HealthbarSlider);
        SetSliders(ref m_CurrentMana, m_MaxMana, m_ManaSlider);

    }

    public void SetSliders(ref int aCurrentValue, int aMaxValue, Slider aSlider)
    {
        aCurrentValue = Mathf.Max(aCurrentValue, 0);
        aCurrentValue = Mathf.Min(aCurrentValue, aMaxValue);

        float SliderPercentage = (float)aCurrentValue / (float)aMaxValue;
        aSlider.value  = SliderPercentage;
    }


    public IEnumerator SetStatusPoints(int aValueToChangeToo,int aBaseValue, TextMeshProUGUI aTextToSet)
    {
        if (aTextToSet == null)
        {
            yield break;
        }

        if (aBaseValue == aValueToChangeToo)
        {
            yield break;
        }

        aBaseValue++;
        aTextToSet.text = aBaseValue.ToString();


        yield return new WaitForEndOfFrame();

    }
    
    public void SetDomainHighlighting(int aNumberOfDomainPoints)
    {
        
       for (int i = 0; i < aNumberOfDomainPoints; i++)
       {
          // m_DomainPointWrappers[i].SetDomainHighlighting(false);
           m_DomainPointWrappers[i].SetDomainHighlighting(true);
       }

    }
}
