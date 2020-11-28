using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]

public class LevelNode : Cell
{

    public enum CardinalNodeDirections
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum CombatNodeTypes
    {
        Normal,
        Test,
        Wall,
        Empty
    }

    public enum WalkOntopTriggerTypes
    {
        None,
        RelicTower,
        DialoguePrompt,
        Items,
        Memoria
        
    }
    
    public enum DomainCombatNode
    {
        None,
        Domain
    }

    public int m_Heuristic;
    public Vector2Int m_PositionInGrid;

    public int m_MovementCost;
    public int m_NodeHeight;
    public float m_NodeHeightOffset;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    
    public bool m_IsWalkable;
    public bool m_IsCovered;
    public DomainCombatNode m_DomainCombatNode;
    public WalkOntopTriggerTypes m_WalkOnTopTriggerTypes;

    
    public NodeReplacement m_NodeReplacement;
    public Creatures m_CreatureOnGridPoint;

    List<LevelNode> neighbours = null;
    public Grid m_Grid;


    public CombatNodeTypes m_CombatsNodeType;

    
    public PropList.Props m_PropOnNode;

    public EnemyList.EnemyTypes m_EnemyOnNode;
    EnemyList.EnemyTypes m_EnemyOnNodeTemp;

    public PropList.NodeReplacements m_NodeReplacementOnNode;

    public List<CardinalNodeDirections> m_WalkableDirections;

    public List<GameObject> NodeWalls;
    
    public GridFormations NodesGridFormation;
    public Memoria m_MemoriaOnTop;

    // Use this for initialization



    public void Initialize(short aWalkableDirections)
    {
        m_MovementCost = 1;

        m_Grid = Grid.Instance;
        
        m_DomainCombatNode = LevelNode.DomainCombatNode.None;
        SetWalkableDirections(aWalkableDirections);
    }

    public void SetLevelNode(List<CardinalNodeDirections> aWalkableDirections)
    {
        for(int i = NodeWalls.Count; i < 0;i++)
        {
            NodeWalls[i].SetActive(true);
        }

        m_WalkableDirections = aWalkableDirections;
        
        foreach(CardinalNodeDirections node in m_WalkableDirections)
        {
            NodeWalls[(int)node].SetActive(false);
        }

    }


    public void DestroyEnemy()
    {
        if (m_CreatureOnGridPoint == null)
        {
            return;
        }

        DestroyImmediate(m_CreatureOnGridPoint.gameObject);
        m_CreatureOnGridPoint = null;
        m_IsCovered = false;
    }
    
    public void SpawnEnemy()
    {
        
        if (m_EnemyOnNodeTemp == m_EnemyOnNode)
        {
            return;
        }
        else
        {
            DestroyEnemy();
        }
        
        if (m_EnemyOnNode == EnemyList.EnemyTypes.None)
        {
            return;
        }

        if (LevelCreator.instance.m_EnemyList == null)
        {
            LevelCreator.instance.StartEditor();
        }

        m_EnemyOnNodeTemp = m_EnemyOnNode;
        
        Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        

        GameObject Enemy = PrefabUtility.
            InstantiatePrefab(LevelCreator.Instance.m_EnemyList.ReturnEnemyData(m_EnemyOnNode)) as GameObject;


        Creatures m_EnemysCreature = Enemy.GetComponent<Creatures>();
       
        m_CreatureOnGridPoint = m_EnemysCreature;
     //  NodesGridFormation.m_EnemysInGrid.Add(m_EnemysCreature);
     //  
     //  Enemy.transform.parent = NodesGridFormation.Enemy.transform;
        Enemy.transform.position = gameObject.transform.position + CreatureOffset;
        Enemy.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);

        EnemyAiController m_CreatureAi = (EnemyAiController)m_EnemysCreature.m_CreatureAi;

        m_CreatureAi.Node_ObjectIsOn = this;
        m_CreatureAi.Node_MovingTo = this;
        m_CreatureAi.m_Position = m_PositionInGrid;
        m_CreatureAi.m_Grid = m_Grid;
            
        
        m_IsCovered = true;

    }




   public void SetCreatureOnTopOfNode(Creatures aCreatures)
   {
       m_CreatureOnGridPoint = aCreatures;

       ActivateWalkOnTopTrigger();

   }


   public void ActivateWalkOnTopTrigger()
   {
       switch (m_WalkOnTopTriggerTypes)
       {
           case WalkOntopTriggerTypes.None:

               break;
           case WalkOntopTriggerTypes.RelicTower:
               break ;
           case WalkOntopTriggerTypes.Items:
               
               break ;
           case WalkOntopTriggerTypes.DialoguePrompt:
               
               break ;
           case  WalkOntopTriggerTypes.Memoria:
               
               UiManager.Instance.PushScreen(UiManager.Screen.Memoria);
               
               UiMemoria ScreenTemp =
                   UiManager.Instance.GetScreen(UiManager.Screen.Memoria) as UiMemoria;

               ScreenTemp.SetMemoriaScreen(m_CreatureOnGridPoint,m_MemoriaOnTop);
               
               break;
               
       }
   }

   public bool IsDirectionWalkable(CardinalNodeDirections aDirection)
   {
     for (int i = 0; i < m_WalkableDirections.Count; i++)
     {
         if (m_WalkableDirections[i] == aDirection)
         {
             return true;
         }
     }

       return false;
   }

   public void SetWalkableDirections(short aWalkabledirections)
   {
       if (m_WalkableDirections.Count != 0)
       {
           for (int i = m_WalkableDirections.Count; i > 0; i--)
           {
               m_WalkableDirections.RemoveAt(i);
           }
       }

       if (aWalkabledirections == (short) Level.LevelCreationDirections.Up)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
       }
                
       if (aWalkabledirections == (short) Level.LevelCreationDirections.Left)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
                
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.Down)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
       }
                
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.Right)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.AllSidesOpen)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.UpDown)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.UpLeft)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.Upright)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.DownLeft)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.DownRight)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.LeftRight)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.UpLeftRight)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.UpLeftDown)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.UpRightDown)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Up);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
       }
       
       if (aWalkabledirections ==  (short)Level.LevelCreationDirections.RightLeftDown)
       {
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Left); 
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Down);
           m_WalkableDirections.Add(LevelNode.CardinalNodeDirections.Right);
       }
       
       

       SetLevelNode(m_WalkableDirections);
   }


   protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)
    };
    
    public override List<LevelNode> GetNeighbours(List<LevelNode> cells)
    {
        if (neighbours == null)
        {
            neighbours = new List<LevelNode>(4);
            foreach (var direction in _directions)
            {
                var neighbour = cells.Find(c => c.m_PositionInGrid == m_PositionInGrid + direction);
                if (neighbour == null) continue;

                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

}



