using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSkillExecutionScreen : UiScreen
{
    public enum SkillExecutionSelectedCreatures
    {
        Enemys,
        Players
    }

    public Creatures m_Creature;
    public EnemyTabs m_EnemytabPrefab;
    
    public List<EnemyTabs> m_EnemyTabs;
    public List<EnemyTabs> m_EnemyTabsPool;
    
    private bool m_SkillRange;
    private Skills m_CurrentSkillInUse;
    public GameObject m_EnemyTab;
    private SkillExecutionSelectedCreatures m_SelectedCreatures;
    public UiTabsPartyStatusManager m_UiStatus;
    // Use this for initialization
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();

        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => ExecuteSkill();
        m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();
        m_MenuControls.Disable();

        m_UiStatus = (UiTabsPartyStatusManager)UiManager.instance.GetUiTab(UiManager.UiTab.PlayerStatus);
    }
    // Update is called once per frame
    
    public override void ResetCursorPosition()
    {
        m_CursorXCurrent = 0;

        ResetEnemyTabPool();
        
        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Enemys)
        {
            m_CursorXMax = TacticsManager.instance.m_TurnOrderEnemy.Count - 1;


            for (int i = 0; i < m_EnemyTabsPool.Count; i++)
            {
                m_EnemyTabsPool[i].TabHoveredOver(false);
     
            }
        }

        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Players)
        {
            m_CursorXMax = TacticsManager.instance.m_TurnOrderAlly.Count - 1;
            
            for (int i = 0; i < m_UiStatus.m_Playerstatus.Count; i++)
            {
                m_UiStatus.m_Playerstatus[i].TabHoveredOver(false);
            }
        }
    }
    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        if (m_SkillRange)
        {
            return;
        }

        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;

        
        m_CursorXCurrent = MenuDirectionCalculationEndInvertAxis(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Enemys)
        {

            m_EnemyTabs[m_CursorXPrevious].TabHoveredOver(false);
            m_EnemyTabs[m_CursorXCurrent].TabHoveredOver(true);
        }

        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Players)
        {
            m_UiStatus.m_Playerstatus[m_CursorXPrevious].TabHoveredOver(false);
            m_UiStatus.m_Playerstatus[m_CursorXCurrent].TabHoveredOver(true);
        }

    }

    public void SelectedCreatures(SkillExecutionSelectedCreatures aSelectedCreatures)
    {
        m_EnemyTab.gameObject.SetActive(false);
        m_UiStatus.gameObject.SetActive(false);

        m_SelectedCreatures = aSelectedCreatures;
        
    }

    public void DefaultSetup()
    {
        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Players)
        {
            m_UiStatus.gameObject.SetActive(true);

            if (m_SkillRange)
            {
                m_CursorXMax = TacticsManager.instance.m_TurnOrderAlly.Count - 1;
            
                for (int i = 0; i < m_UiStatus.m_Playerstatus.Count; i++)
                {
                    m_UiStatus.m_Playerstatus[i].TabHoveredOver(true);
                }
            }

            m_UiStatus.m_Playerstatus[m_CursorXCurrent].TabHoveredOver(true);
        }
        
        if (m_SelectedCreatures == SkillExecutionSelectedCreatures.Enemys)
        {
            m_EnemyTab.gameObject.SetActive(true);
            SetEnemyTabs();

        }
    }


    public void SetSkill(Skills aSetSkill, Creatures aCreature,bool aSkillRange)
    {
        m_Creature = aCreature;
        m_CurrentSkillInUse = aSetSkill;
        m_SkillRange = aSkillRange;
        
    }

    public void ExecuteSkill()
    {
        TacticsManager.instance.m_SkillExecutionManager.ExecuteSkill(m_CurrentSkillInUse,m_SkillRange,m_CursorXCurrent,m_Creature);
        m_MenuControls.Disable();
    }

    public override void OnPop()
    {
        gameObject.SetActive(false);
        m_MenuControls.Disable();
        
        m_UiStatus.gameObject.SetActive(true);
        ResetCursorPosition();
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
        m_MenuControls.Enable();
        
        ResetCursorPosition();
        DefaultSetup();

    }

    public void ResetEnemyTabPool()
    {
        for (int i = 0; i < m_EnemyTabsPool.Count; i++)
        {
            m_EnemyTabsPool[i].m_InUse = false;
            m_EnemyTabsPool[i].gameObject.SetActive(false);

        }
    }

    public EnemyTabs GetEnemyTabFromPool()
    {
        for (int i = 0; i < m_EnemyTabsPool.Count; i++)
        {
            if (m_EnemyTabsPool[i].m_InUse == false)
            {
                m_EnemyTabsPool[i].m_InUse = true;
                m_EnemyTabsPool[i].gameObject.SetActive(true);
                return m_EnemyTabsPool[i];
            }
        }

        
        Debug.Log("EnemyTabsRanOut");
        return null;
    }

    public void SetEnemyTabs()
    {

        List <Creatures> CurrentEnemysInScene  = TacticsManager.instance.m_TurnOrderEnemy;
        Camera Camera = TacticsManager.instance.m_CombatCamera;
        for (int i = 0; i < CurrentEnemysInScene.Count; i++)
        {
            m_EnemyTabs.Add(GetEnemyTabFromPool());

            m_EnemyTabs[i].SetupTab(CurrentEnemysInScene[i]);
            Vector2 screenPosition = Camera.WorldToScreenPoint(CurrentEnemysInScene[i].transform.position + Vector3.down/ 2);
            m_EnemyTabs[i].transform.position = screenPosition;
            if (m_SkillRange)
            {
                m_EnemyTabs[i].TabHoveredOver(true);
            }
        }
        
        m_EnemyTabs[m_CursorXCurrent].TabHoveredOver(true);


    }


}
