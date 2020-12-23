using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSkillBoard : UiScreen
{

    public Creatures m_SkillBoardCreature;
    public List<ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;

    public TextMeshProUGUI m_SkillRange;
    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;
    public bool m_ToggleSkillRange;
    public int m_CreatureSkillCount;
    // Use this for initialization
    public override void Initialize()
    {
        m_SkillBoardPointerPosition = 0;
        m_MenuControls = new PlayerInput();

        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();
        m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();
        m_MenuControls.Player.TriangleButton.performed += TriangleButton => ToggleSkillRange();
        m_MenuControls.Disable();
    }
    // Update is called once per frame

    public void ToggleSkillRange()
    {
        m_ToggleSkillRange = !m_ToggleSkillRange;

        m_SkillRange.text = m_ToggleSkillRange ?  "MULTI" : "SINGLE" ;
        
        SetUpButtons();
    }

    public override void ResetCursorPosition()
    {
        m_CursorYMax = m_CreatureSkillCount;
        m_CursorYCurrent = 0;
        m_ToggleSkillRange = false;
        m_SkillRange.text = "SINGLE";
        m_CurrentSkillMenuButtonsMenu[m_CursorYCurrent].SkillHoveredOver(false);
        
    }
    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;

        
        m_CursorXCurrent = MenuDirectionCalculationLooping(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_SkillBoardPointerPosition = aCursorY;
        
        m_CurrentSkillMenuButtonsMenu[m_CursorYPrevious].SkillHoveredOver(true);
        m_CurrentSkillMenuButtonsMenu[m_CursorYCurrent].SkillHoveredOver(false);
        
        m_DescriptionText.text =
            m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition].m_ButtonSkill.m_SkillDescription;

    }

    public void SetSkill()
    {

        ((UiEnemyScreen) UiManager.instance.GetScreen(UiManager.UiScreens.EnemyScreen)).
            SetSkill(m_SkillBoardCreature.m_Skills[m_CursorYCurrent],m_SkillBoardCreature,m_ToggleSkillRange);
        
        UiManager.instance.PushScreen(UiManager.UiScreens.EnemyScreen);
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
        m_MenuControls.Enable();
    }

    public void SpawnSkills(Creatures aCreatures)
    {

        m_SkillBoardCreature = aCreatures;

        
        
        for (int i = 0; i < m_CurrentSkillMenuButtonsMenu.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu[i].gameObject.SetActive(false);
            m_CurrentSkillMenuButtonsMenu[i].m_ButtonSkill = null;

        }


        SetUpButtons();
        
        m_CreatureSkillCount = m_SkillBoardCreature.m_Skills.Count - 1;
        
       ResetCursorPosition();

    }

    public void SetUpButtons()
    {
        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu[i].gameObject.SetActive(true);
            m_CurrentSkillMenuButtonsMenu[i].SkillHoveredOver(true);
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i],m_ToggleSkillRange);
        }
    }




}
