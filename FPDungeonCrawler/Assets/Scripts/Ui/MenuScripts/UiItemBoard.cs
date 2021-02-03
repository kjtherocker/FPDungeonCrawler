using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiItemBoard : UiScreen
{

    public Creatures m_SkillBoardCreature;
    public List<ItemWrapper> m_CurrentSkillMenuButtonsMenu;
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
        m_MenuControls.Disable();
    }
    // Update is called once per frame
    

    public override void ResetCursorPosition()
    {
        m_CursorYMax = ItemManager.instance.m_AllItems.Count;
        m_CursorYCurrent = 0;
        m_ToggleSkillRange = false;



        SetUpButtons();

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
        AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.Selection,AudioManager.Soundtypes.SoundEffects);
    }

    public void SetSkill()
    {

        UiSkillExecutionScreen UiSkillExecution =
            (UiSkillExecutionScreen) UiManager.instance.GetScreen(UiManager.UiScreens.SkillExecutionScreen);

        Items Tempitem = ItemManager.instance.m_AllItems[m_CursorYCurrent].m_Items;
        
        UiSkillExecution.SetSkill( Tempitem,m_SkillBoardCreature,Tempitem.m_SkillRange);

        if (m_SkillBoardCreature.charactertype == Creatures.Charactertype.Ally &&
            m_SkillBoardCreature.m_Skills[m_CursorYCurrent].m_SkillType == Skills.SkillType.Attack)
        {
            UiSkillExecution.SelectedCreatures(UiSkillExecutionScreen.SkillExecutionSelectedCreatures.Enemys);
        }
        
        if (m_SkillBoardCreature.charactertype == Creatures.Charactertype.Ally &&
            m_SkillBoardCreature.m_Skills[m_CursorYCurrent].m_SkillType == Skills.SkillType.Heal)
        {
            UiSkillExecution.SelectedCreatures(UiSkillExecutionScreen.SkillExecutionSelectedCreatures.Players);
        }
        
        if (m_SkillBoardCreature.charactertype == Creatures.Charactertype.Ally &&
            m_SkillBoardCreature.m_Skills[m_CursorYCurrent].m_SkillType == Skills.SkillType.Buff)
        {
            UiSkillExecution.SelectedCreatures(UiSkillExecutionScreen.SkillExecutionSelectedCreatures.Players);
        }

        
        
        UiManager.Instance.PopScreen();
        UiManager.instance.PushScreen(UiManager.UiScreens.SkillExecutionScreen);
        
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
        ResetCursorPosition();
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
        List<ItemCore> TempItemCore = ItemManager.instance.m_AllItems;
        
        
        for (int i = 0; i < TempItemCore.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu[i].gameObject.SetActive(true);
            m_CurrentSkillMenuButtonsMenu[i].SkillHoveredOver(true);
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, TempItemCore[i]);
        }
        m_CurrentSkillMenuButtonsMenu[m_CursorYCurrent].SkillHoveredOver(false);
    }




}
