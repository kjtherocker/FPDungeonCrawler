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
        m_MenuControls.Player.XButton.performed += xButton => SelectCommand();
      // m_MenuControls.Player.TriangleButton.performed += TriangleButton => SpawnDomainBoard();
       // m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();

       
       m_Commands = new List<CommandOptions>();
       m_Commands.Add(AttackCommand);
       m_Commands.Add(SkillCommand);
       m_Commands.Add(DomainCommand);
       m_Commands.Add(ItemCommand);
       m_Commands.Add(EscapeCommand);
       m_Commands.Add(PassCommand);
       
       
        m_MenuControls.Disable();
    }

    public void SetCreatureReference(Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;
        //     m_CommandObjects.transform.position = screenPosition;
    }

    public void SetStatus(bool aStatusBool)
    {
        if (m_CommandboardCreature != null)
        {
            UiTabsPartyStatusManager UiPartyStatusManager = (UiTabsPartyStatusManager)UiManager.instance.GetUiTab(UiManager.UiTab.PlayerStatus);

            UiPartyStatusManager.m_Portrait.gameObject.SetActive(aStatusBool);
            m_CommandboardCreature.m_Status.SetStatus(aStatusBool);
        }
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
        AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.Accept,AudioManager.Soundtypes.SoundEffects);
    }

    public override void OnPop()
    {
        base.OnPop();
        m_MenuControls.Disable();
        SetStatus(false);
    }

    

    public override void OnPush()
    {
        ResetCursorPosition();
        base.OnPush();
        SetStatus(true);
    }

    public override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;
        
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }

    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_CommandTab[m_CursorYPrevious].ChangeColorToDefault();
        m_CommandTab[m_CursorYCurrent].ChangeColorToSelected();        
        
        AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.Selection,AudioManager.Soundtypes.SoundEffects);
        
    }

    public void AttackCommand()
    {
        m_MenuControls.Disable();
 
        UiManager.Instance.PopScreen();
        UiSkillExecutionScreen UiSkillExecution =
            (UiSkillExecutionScreen) UiManager.instance.GetScreen(UiManager.UiScreens.SkillExecutionScreen);
        
        UiSkillExecution.SetSkill(SkillList.instance.GetSkill(SkillList.SkillEnum.Attack),m_CommandboardCreature,false);
        UiSkillExecution.SelectedCreatures(UiSkillExecutionScreen.SkillExecutionSelectedCreatures.Enemys);
        
        
        UiManager.Instance.PushScreen(UiManager.UiScreens.SkillExecutionScreen);

    }

    public void SkillCommand()
    {
        m_MenuControls.Disable();
 
        UiManager.Instance.PopScreen();
      
        UiScreen ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.UiScreens.SkillBoard) ;

        ((UiSkillBoard)ScreenTemp).SpawnSkills(m_CommandboardCreature);
        
        
        UiManager.Instance.PushScreen(UiManager.UiScreens.SkillBoard);

      


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
        UiManager.Instance.PushScreen(UiManager.UiScreens.DomainBoard);

        //  UiDomainBoard ScreenTemp =
        //      UiManager.Instance.GetScreen(UiManager.Screen.DomainBoard) as UiDomainBoard;
//
        //   ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }


    public void ItemCommand()
    {
        m_MenuControls.Disable();
 
        UiManager.Instance.PopScreen();
      
        UiScreen ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.UiScreens.ItemBoard) ;

        ((UiItemBoard)ScreenTemp).SpawnSkills(m_CommandboardCreature);
        
        
        UiManager.Instance.PushScreen(UiManager.UiScreens.ItemBoard);

    }
    
    public void EscapeCommand()
    {
        
    }
    
    public void PassCommand()
    {
        OnPop();
        TacticsManager.instance.AddCreaturesInActiveSkill(m_CommandboardCreature);
        TacticsManager.instance.CharacterSkillFinished(m_CommandboardCreature, PressTurnManager.PressTurnReactions.Pass);
    }
}
