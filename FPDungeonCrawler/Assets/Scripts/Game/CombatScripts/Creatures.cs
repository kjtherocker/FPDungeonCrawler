using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = System.Random;

[Serializable]
public class Creatures : MonoBehaviour
{

    public enum Charactertype
    {
        Undefined,
        Ally,
        Enemy
    }




    public enum CreaturesAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }
    public enum DomainStages
    {
        NotActivated,
        Encroaching,
        Finished,
        End
    }
    
    public Skills m_Attack;
    public List<Skills> m_Skills { get; protected set; }
    public List<Skills> m_BloodArts { get; protected set; }

    public AiController m_CreatureAi;

    public CreaturesAilment m_creaturesAilment;
    public Charactertype charactertype;
    public Skills.ElementalType elementalStrength;
    public Skills.ElementalType  elementalWeakness;

    public int m_CurrentHealth;
    public int m_MaxHealth;

    public int CurrentMana;
    public int MaxMana;
    
    
    public int BaseStrength;
    public int BuffStrength;
    public int DebuffStrength;
    public int DomainStrength;

    public int BaseMagic;
    public int BuffMagic;
    public int DebuffMagic;
    public int DomainMagic;
    
    public int BaseHit;
    public int BuffHit;
    public int DebuffHit;
    public int DomainHit;
    
    public int BaseEvasion;
    public int BuffEvasion;
    public int DebuffEvasion;
    public int DomainEvasion;
    
    public int BaseDefence;
    public int BuffDefence;
    public int DebuffDefence;
    public int DomainDefence;
    
    
    public int BaseResistance;
    public int BuffResistance;
    public int DebuffResistance;
    public int DomainResistance;

    public int CurrentDomainpoints;
    public int MaxDomainPoints = 3;

    public int m_CreatureMovement = 4;

    public int AmountOfTurns;
    

    public string DomainAffectingCreature;
    public string Name = "No Name";

    public Material m_Texture;

    public GameObject Model;
    public GameObject ModelInGame;

    public DomainList.DomainListEnum m_DomainList;
    public Domain m_Domain;
    public Devour m_Devour;

    public List<StatusEffects> m_StatusEffectsOnCreature;

    public GameObject m_SpawnObject;
    
    bool m_IsAlive;

    protected bool m_IsUi;
    protected SkillList m_CreatureSkillList;


    public virtual void Initialize()
    {
        
    }

    // Update is called once per frame
    public virtual void SetCreature()
    {
        m_Skills = new List<Skills>();
        m_BloodArts = new List<Skills>();
        m_StatusEffectsOnCreature = new List<StatusEffects>();
        
        m_Devour = new Devour();
        
        m_CreatureSkillList = SkillList.instance;

        //m_Attack = gameObject.AddComponent<Attack>();

        CurrentDomainpoints = MaxDomainPoints;
    }

    public virtual void EndTurn()
    {
        if (m_StatusEffectsOnCreature.Count == 0)
        {
            return;
        }

        for(int i =  m_StatusEffectsOnCreature.Count -1 ; i >= 0;i--)
        {
            m_StatusEffectsOnCreature[i].EndOfTurn();

            if (m_StatusEffectsOnCreature[i].CheckIfStatusEffectIsActive() == false)
            {
                m_StatusEffectsOnCreature.RemoveAt(i);
            }
        }

    }

    public virtual int GetAllStrength()
    {
        int TemporaryStat;

        TemporaryStat = BuffStrength + DebuffStrength + BaseStrength + DomainStrength;

        return TemporaryStat;
    }

    public virtual int GetAllMagic()
    {
        int TemporaryStat;

        TemporaryStat = BuffMagic + DebuffMagic + BaseMagic + DomainMagic;

        return TemporaryStat;
    }
    
    
    public virtual int GetAllHit()
    {
        int TemporaryStat;

        TemporaryStat = BuffHit + DebuffHit + BaseHit + DomainHit;

        return TemporaryStat;
    }

    public virtual int GetAllEvasion()
    {
        int TemporaryStat;

        TemporaryStat = BuffEvasion+ DebuffEvasion + BaseEvasion + DomainEvasion;

        return TemporaryStat;
    }
    
    
    public virtual int GetAllDefence()
    {
        int TemporaryStat;

        TemporaryStat = BuffDefence + DebuffDefence + BaseDefence + DomainDefence;

        return TemporaryStat;
    }

    public virtual int GetAllResistance()
    {
        int TemporaryMagic;

        TemporaryMagic = BuffResistance + DebuffResistance + BaseResistance + DomainResistance;

        return TemporaryMagic;
    }

    public virtual IEnumerator SetStatusEffect(StatusEffects aStatusEffect)
    {
 
        m_StatusEffectsOnCreature.Add(aStatusEffect);
        
        yield return new WaitForSeconds(0.1f);
        FloatingUiElementsController.CreateFloatingText(0.ToString(), m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Attackup,m_IsUi);
        yield return new WaitForSeconds(0.7f);
        
        TacticsManager.instance.CharacterSkillFinished(this, PressTurnManager.PressTurnReactions.Normal);
    }

    public virtual void DecrementHealth(int Decremenby)
    {
  
        FloatingUiElementsController.CreateFloatingText(Decremenby.ToString(),  m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Text,m_IsUi);
        m_CurrentHealth -= Decremenby;
    }
    
    public virtual IEnumerator DecrementHealth(int Decrementby, Skills.ElementalType elementalType,float TimeTillInitalDamage, float TimeTillHoveringUiElement, float TimeTillDamage)
    {

        string AttackingElement = elementalType.ToString();
        string ElementalWeakness = elementalWeakness.ToString();
        string ElementalStrength = elementalStrength.ToString();
        
        int ArgumentReference = Decrementby;
        float ConvertToFloat = ArgumentReference / 1.5f;
        int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
        Decrementby = ConvertToInt;

        PressTurnManager.PressTurnReactions PressturnReaction = PressTurnManager.PressTurnReactions.Normal;
        

        if (AttackingElement.Equals(ElementalWeakness))
       {
           Decrementby += Decrementby / 4;
           yield return new WaitForSeconds(TimeTillHoveringUiElement);
           FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Weak,m_IsUi);
           PressturnReaction = PressTurnManager.PressTurnReactions.Weak;
       }
       if (AttackingElement.Equals(ElementalStrength))
       {
           Decrementby -= Decrementby / 4;
           yield return new WaitForSeconds(TimeTillHoveringUiElement);
           FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Strong,m_IsUi);
           PressturnReaction = PressTurnManager.PressTurnReactions.Strong;
       }
       
       
        yield return new WaitForSeconds(TimeTillDamage);
        
        FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(),  m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Text,m_IsUi);
        m_CurrentHealth -= Decrementby;
        TacticsManager.instance.CharacterSkillFinished(this,PressturnReaction );
        DeathCheck();
    }


    public virtual IEnumerator IncrementHealth(int Increment)
    {
        m_CurrentHealth += Increment;
        yield return new WaitForSeconds(0.5f);

        FloatingUiElementsController.CreateFloatingText(Increment.ToString(), m_SpawnObject.transform,
                FloatingUiElementsController.UiElementType.Text, m_IsUi);
                
        yield return new WaitForSeconds(1.5f);
        
        TacticsManager.instance.CharacterSkillFinished(this,PressTurnManager.PressTurnReactions.Normal);
    }

    public virtual Charactertype GetCharactertype()
    {

        return charactertype;
    }

    public virtual void Resurrection()
    {
        ModelInGame.gameObject.SetActive(true);
    }

    public void DeathCheck()
    {
        if (m_CurrentHealth <= 0)
        {
            m_IsAlive = false;

            Death();
        }

        m_CurrentHealth = Mathf.Min(m_CurrentHealth, m_MaxHealth);
    }

    public virtual void Death()
    {
        m_CurrentHealth = 0;
    

       TacticsManager.Instance.RemoveDeadFromList(this);
    }

    public Skills GetSkill()
    {
     //   float test = Random.Range(-10.0f, 10.0f);
        
        return m_Skills[0];
    }


}
