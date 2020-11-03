﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Creatures m_CommandboardCreature;
    
    private delegate void CommandOptions();
    private List<CommandOptions> m_Commands;
    
    public List<HighlightButton> m_CommandTab;
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.SquareButton.performed += SquareButton => SelectCommand();
      // m_MenuControls.Player.TriangleButton.performed += TriangleButton => SpawnDomainBoard();
       // m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();

       m_Commands.Add(AttackCommand);
       m_Commands.Add(SkillCommand);
       m_Commands.Add(DomainCommand);
       m_Commands.Add(ItemCommand);
       m_Commands.Add(EscapeCommand);
       
       
        m_MenuControls.Disable();
    }

    public void SetCreatureReference(Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;
        //     m_CommandObjects.transform.position = screenPosition;
    }
    
    public override void ResetCursorPosition()
    {
        m_CursorYMax = m_CommandTab.Count - 1;
        m_CursorYCurrent = 0;
        m_CursorYMin = 0;
        
        m_CursorXMax = 0;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;

        foreach (HighlightButton arena in m_CommandTab)
        {
            arena.ChangeColorToDefault();
        }
        m_CommandTab[m_CursorYCurrent].ChangeColorToSelected();    
    }
    
    
    public void SelectCommand()
    {
        m_Commands[m_CursorYCurrent]();
    }

    public override void OnPop()
    {
        base.OnPop();
        m_MenuControls.Disable();
    }

    

    public override void OnPush()
    {
        ResetCursorPosition();
        base.OnPush();
    }



    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_CommandTab[m_CursorYCurrent].ChangeColorToSelected();        
        m_CommandTab[m_CursorYPrevious].ChangeColorToDefault();

    }

    public void AttackCommand()
    {
        
    }

    public void SkillCommand()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasAttackedForThisTurn)
        {
            return;
        }

        m_MenuControls.Disable();
        
        m_CommandboardCreature.m_CreatureAi.DeselectAllPaths();
        
        UiManager.Instance.PopScreen();
        UiManager.Instance.PushScreen(UiManager.Screen.SkillBoard);

        UiSkillBoard ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.Screen.SkillBoard) as UiSkillBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }

    
    public void DomainCommand()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasAttackedForThisTurn)
        {
            return;
        }

        m_MenuControls.Disable();

        m_CommandboardCreature.m_CreatureAi.DeselectAllPaths();
        
        UiManager.Instance.PopScreen();
        UiManager.Instance.PushScreen(UiManager.Screen.DomainBoard);

        //  UiDomainBoard ScreenTemp =
        //      UiManager.Instance.GetScreen(UiManager.Screen.DomainBoard) as UiDomainBoard;
//
        //   ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }


    public void ItemCommand()
    {
        
    }
    
    public void EscapeCommand()
    {
        
    }

    public override void ReturnToLastScreen()
    {
        base.ReturnToLastScreen();

      //  GameManager.Instance.m_CombatCameraController.SetCameraState(CombatCameraController.CameraState.Normal);
        InputManager.Instance.m_MovementControls.Enable();
    }
}