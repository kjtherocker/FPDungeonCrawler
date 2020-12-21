using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEnemyScreen : UiScreen
{
    public Creatures m_Creature;
    public List<EnemyTab> m_EnemyTabs;

    private Skills m_CurrentSkillInUse;

    // Use this for initialization
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();

        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => ExecuteSkill();
        m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }
    // Update is called once per frame
    
    public override void ResetCursorPosition()
    {
        m_CursorXMax = TacticsManager.instance.TurnOrderEnemy.Count -1;
        m_CursorXCurrent = 0;
  
        
    }
    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;

        
        m_CursorXCurrent = MenuDirectionCalculationEndInvertAxis(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_EnemyTabs[m_CursorXPrevious].TabHoveredOver(false);
        m_EnemyTabs[m_CursorXCurrent].TabHoveredOver(true);
    }

    public void SetSkill(Skills aSetSkill, Creatures aCreature)
    {
        m_Creature = aCreature;
        m_CurrentSkillInUse = aSetSkill;
    }

    public void ExecuteSkill()
    {
        TacticsManager.instance.ProcessTurn(m_CurrentSkillInUse.UseSkill
            (TacticsManager.instance.TurnOrderEnemy[m_CursorXCurrent], m_Creature));
        OnPop();
    }

    public override void OnPop()
    {
        gameObject.SetActive((false));
        m_MenuControls.Disable();
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
     //   InputManager.Instance.m_MovementControls.Disable();
        ResetCursorPosition();
        SetEnemyTabs();
        m_MenuControls.Enable();
    }

    public void SetEnemyTabs()
    {

        List <Creatures> CurrentEnemysInScene  = TacticsManager.instance.TurnOrderEnemy;

        for (int i = 0; i < CurrentEnemysInScene.Count; i++)
        {
            m_EnemyTabs[i].SetupTab(CurrentEnemysInScene[i]);
        }
        
        m_EnemyTabs[m_CursorXCurrent].TabHoveredOver(true);


    }


}
