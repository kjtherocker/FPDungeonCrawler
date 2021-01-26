using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Creatures
{
    private UiStatus m_Status;
    public void SetCreature()
    {
        m_Skills = new List<Skills>();
        m_BloodArts = new List<Skills>();
        m_StatusEffectsOnCreature = new List<StatusEffects>();
        
        m_Devour = new Devour();
        
        FloatingUiElementsController.Initalize();
        
        m_CreatureSkillList = SkillList.instance;
        m_IsUi = true;
        CurrentDomainpoints = MaxDomainPoints;
    }

    public void SetStatus(UiStatus aStatus)
    {
        m_SpawnObject = aStatus.gameObject;
        m_Status = aStatus;
    }
    
    
    public override IEnumerator DecrementHealth(int Decrementby, Skills.ElementalType elementalType,float TimeTillInitalDamage, float TimeTillHoveringUiElement, float TimeTillDamage)
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
       
       
        yield return new WaitForSeconds(TimeTillInitalDamage);
        
        FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(),  m_SpawnObject.transform, FloatingUiElementsController.UiElementType.Text,m_IsUi);
        m_CurrentHealth -= Decrementby;
        DeathCheck();
       // m_Status.RedTest();
        yield return new WaitForSeconds(3.0f);
        TacticsManager.instance.CharacterSkillFinished(this,PressturnReaction );
    }


}
