﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using UnityEngine.AddressableAssets;


public class TacticsManager : Singleton<TacticsManager>
{

    delegate void TurnProcessor();

    private TurnProcessor NextTurn;
    
    public PartyManager m_PartyManager;
    public EnemyManager m_EnemyManager;
    
    bool WhichSidesTurnIsIt;
    bool CombatHasStarted;

    private int m_EnemyAiCurrentlyInList;

    public HealthBar m_Healthbar;

    public UiTabTurnKeeper m_UiTabTurnKeeper;
    public TextMeshProUGUI m_TurnSwitchText;
    
    public List<Creatures> DeadAllys;
    public List<Creatures> TurnOrderAlly;
    public List<Creatures> TurnOrderEnemy;

    public List<PressTurnManager.PressTurnReactions> m_PressTurnReactions;
    public List<Creatures> m_CreaturesInSkill;

    private FloorManager m_CurrentFloorManager;
    public int m_Turns;
    
    private GameObject m_MemoriaPrefab;
    public Dictionary<Creatures, Creatures> m_CreaturesWhosDomainHaveClashed;

    private CombatArena m_CombatArena;
    
    public Camera m_CombatCamera;
    public SkillExecutionManager m_SkillExecutionManager;
    public enum CombatStates
    {
        NoTurn,
        Spawn,
        EnemyTurn,
        AllyTurn,
        DomainClash,
    
        EndOfCombat


    }
    
    public enum TurnStates
    {
        Add,
        Set
        
    }
    

    public CombatStates m_BattleStates;

    void Start()
    {
     //   CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_EnemyManager = EnemyManager.instance;
        m_PartyManager = PartyManager.Instance;
        //CombatStart();
        m_SkillExecutionManager = new SkillExecutionManager(this);
    }

    public void StartCombat(CombatArena aArena,Floor aCurrentFloor, FloorManager aFloorManager)
    {

        AudioManager.instance.PlaySoundRepeating(AudioManager.AudioClips.Combat,AudioManager.Soundtypes.Music);
        
        m_CurrentFloorManager = aFloorManager;
        EnemyManager.instance.AddEnemysToManager(aCurrentFloor.EnemySet1(),aArena);

        m_CombatArena = aArena;
        
         for (int i = 0; i < m_EnemyManager.m_EnemyList.Count; i++)
         {
             m_EnemyManager.m_EnemyList[i].Initialize();
             AddCreatureToCombat(m_EnemyManager.m_EnemyList[i], TurnOrderEnemy);
         }

         AddCreatureToCombat(m_PartyManager.m_CurrentParty[0], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[1], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[2], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[3], TurnOrderAlly);

         m_Turns = m_PartyManager.m_CurrentParty.Count;
         PressTurnManager.instance.m_TurnKeeper = m_UiTabTurnKeeper;
         m_UiTabTurnKeeper.gameObject.SetActive(true);
         PressTurnManager.instance.StartTurn(m_Turns);
         m_UiTabTurnKeeper.UpdateTurnIcons(m_Turns);
         m_UiTabTurnKeeper.SetIconType(true);
         
         UiManager.instance.PushScreen(UiManager.UiScreens.CommandBoard);
         
         UiScreen temp = UiManager.instance.GetScreen(UiManager.UiScreens.CommandBoard);
         ((UiScreenCommandBoard) temp).m_CommandboardCreature = m_PartyManager.m_CurrentParty[0];

         aArena.gameObject.SetActive(true);

    }

    public void RemoveAllCreaturesInSkill()
    {
        for (int i = m_CreaturesInSkill.Count; i > 0; i--)
        {
            m_CreaturesInSkill.RemoveAt(i);
        }

    }

    public void AddCreaturesInActiveSkill(Creatures aCreature)
    {
        m_CreaturesInSkill.Add(aCreature);
    }

    public void ProcessAction(List<IEnumerator> aSkillActions, List<Creatures> aCreatures)
    {
        RemoveAllCreaturesInSkill();
        for (int i = 0; i < aCreatures.Count; i++)
        {
            AddCreaturesInActiveSkill(aCreatures[i]);
        }

        for (int i = 0; i < aSkillActions.Count; i++)
        {
            StartCoroutine(aSkillActions[i]);
        }
    }
    
    public void ProcessAction(IEnumerator aSkillActions, Creatures aCreature)
    {
        AddCreaturesInActiveSkill(aCreature);
        StartCoroutine(aSkillActions);
    }

    public void CharacterSkillFinished(Creatures aCreature, PressTurnManager.PressTurnReactions aPressTurnReaction )
    {
        m_PressTurnReactions.Add(aPressTurnReaction);
        for (int i = 0; i < m_CreaturesInSkill.Count; i++)
        {
            if (m_CreaturesInSkill[i] == aCreature)
            {
                m_CreaturesInSkill.RemoveAt(i);
                break;
            }
        }

        if (m_CreaturesInSkill.Count == 0)
        {    
            PressTurnManager.instance.ProcessTurn(m_PressTurnReactions);
            
            for (int i = m_PressTurnReactions.Count - 1; i > 0; i--)
            {
                m_PressTurnReactions.RemoveAt(i);
            }

            ActionEnd();
        }

    }


    public void ActionEnd()
    {
        if (TurnOrderEnemy.Count == 0)
        {
            EndCombat();
        }
        else
        {
            ProgressToNextCharacter();    
        }
        
    }




    public void UpdateCurrentTurnAmount(int aTurn,TurnStates aTurnStates )
    {

        if (aTurnStates == TurnStates.Add)
        {
            m_Turns += aTurn;
        }
        
        if (aTurnStates == TurnStates.Set)
        {
            m_Turns = aTurn;
        }

       
    }

    public void ProgressToNextCharacter()
    {
        
        if (m_Turns > 0)
        {
            StartCoroutine(NextAlleyTurn());
        }

        if (m_Turns == 0)
        {
            //swap sides
            StartCoroutine(EnemyTurn());
        }
    }


    public IEnumerator NextEnemyTurn()
    {

        ((Enemy) TurnOrderEnemy[m_Turns]).AiSetup();
        
        yield return new WaitForSeconds(2.5f);
        
    }
    
    public IEnumerator NextAlleyTurn()
    {

        yield return new WaitForSeconds(2.5f);
        
        UiManager.Instance.PopScreen();
        yield return new WaitForSeconds(0.4f);
        UiManager.instance.PushScreen(UiManager.UiScreens.CommandBoard);
         
        UiScreen temp = UiManager.instance.GetScreen(UiManager.UiScreens.CommandBoard);
        ((UiScreenCommandBoard) temp).m_CommandboardCreature = m_PartyManager.m_CurrentParty[0];
        
    }
    
    public void EndTurn()
    {
        StartCoroutine(EnemyTurn());

    }



    public void AddCreatureToCombat(Creatures aCreature,List<Creatures> aList)
    {
        if (aCreature == null)
        {
            return;
        }

        aList.Add(aCreature);

        //int TopElement = aList.Count - 1;

        //Model
//        aList[TopElement].ModelInGame = Instantiate<GameObject>(aList[TopElement].Model);
        //aList[TopElement].ModelInGame.transform.position = // m_Grid.GetNode(aPosition.x, aPosition.y).gameObject.transform.position + CreatureOffset;
     //   aList[TopElement].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);
        
        
        //Healthbar

     //   aList[TopElement].m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aList[TopElement].m_CreatureAi.transform);
     //   AddHealthbar(aList[TopElement]);
    }


    public void AddHealthbar(Creatures aCreature)
    {

        if (aCreature == null)
        {
            Debug.Log("Creature Was null");
            return;
        }

        aCreature.m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aCreature.m_CreatureAi.transform);
        aCreature.m_CreatureAi.m_Healthbar.Partymember = aCreature;
    }


    void Update()
    {


       switch (m_BattleStates)
       {
           case CombatStates.Spawn:
             
                   m_BattleStates = CombatStates.AllyTurn;


             
               break;

           case CombatStates.AllyTurn:
               

                break;

            case CombatStates.EnemyTurn:
                

                break;



            case CombatStates.EndOfCombat:

           //    if (Input.anyKey)
            //   {
              //    CombatEnd();
             //  }
               break;
       }

    }
    
    
    public void RemoveDeadFromList(Creatures aDeadCreature)
    {
        if (aDeadCreature.charactertype == Creatures.Charactertype.Ally)
        { 
            for (int i = TurnOrderAlly.Count - 1; i >= 0; i--)
            {
                if (TurnOrderAlly[i] == aDeadCreature)
                {    
                    TurnOrderAlly.RemoveAt(i);
                }
            }
        }
        
        if (aDeadCreature.charactertype == Creatures.Charactertype.Enemy)
        { 
            for (int i = TurnOrderEnemy.Count - 1; i >= 0; i--)
            {
                if (TurnOrderEnemy[i] == aDeadCreature)
                {    
                    TurnOrderEnemy.RemoveAt(i);
                }
            }
            
            if (TurnOrderEnemy.Count == 0)
            {
                EndCombat();
            }
        }



    }

    public IEnumerator EnemyTurn()
    {
        m_BattleStates = CombatStates.EnemyTurn;
        
        m_UiTabTurnKeeper.SetIconType(false);
        UpdateCurrentTurnAmount(TurnOrderEnemy.Count,TurnStates.Set);

        yield return new WaitForSeconds(2f);


    }

    public void EndCombat()
    {
        UiManager.instance.PopAllScreens();
        
        m_UiTabTurnKeeper.gameObject.SetActive(false);
        for (int i = TurnOrderEnemy.Count - 1; i >= 0; i--)
        {
            TurnOrderEnemy.RemoveAt(i);
        }

        EnemyManager.instance.ResetEnemyManager();
        AudioManager.instance.PlaySoundRepeating(AudioManager.AudioClips.Exploration,AudioManager.Soundtypes.Music);
        
        m_CombatArena.gameObject.SetActive(false);
        m_CurrentFloorManager.SwitchToExploration();
    }

    public void EnemyMovement()
    {

    }

    public IEnumerator AllyTurn()
    {
        m_BattleStates = CombatStates.AllyTurn;
        

        m_UiTabTurnKeeper.SetIconType(true);
        foreach (Creatures creature in TurnOrderAlly)
        {
            creature.EndTurn();
        }
        
        UpdateCurrentTurnAmount(TurnOrderAlly.Count,TurnStates.Set);
        
        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
    }
    

}