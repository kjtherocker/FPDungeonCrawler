﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DomainState
{
    None,
    Emenating
}


public class AiController : MonoBehaviour
{
    public Grid m_Grid;

    public Vector2Int m_Position;
    public Vector2Int m_InitalPosition;

    public LevelNode m_PreviousNode;
    public DomainState m_Domainstate;

    public Pathfinder _Pathfinder;
    public LevelNode Node_MovingTo;
    public LevelNode Node_ObjectIsOn;
    public Animator m_CreaturesAnimator;
    public Creatures m_Creature;

    public Transform m_AiModel;

    private Dictionary<LevelNode, List<LevelNode>> cachedPaths = null;

    public HealthBar m_Healthbar;

    public HashSet<LevelNode> m_NodeInWalkableRange;
    public HashSet<LevelNode> m_NodeInDevourRange;
    public HashSet<LevelNode> m_NodeInDomainRange;


    public Vector3 CreatureOffset;

    public int m_Movement;
    public int m_Jump;

    public bool m_MovementHasStarted;
    public bool m_HasAttackedForThisTurn;
    public bool m_HasMovedForThisTurn;

    public delegate bool DelegateReturnNodeIndex(LevelNode node, Vector2Int Postion);
    public MovementType m_MovementType;

    // Use this for initialization
    public virtual void Initialize()
    {
        //m_Goal = new Vector2Int(9, 2);
        //m_Position = new Vector2Int(4, 4);
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Jump = 2;
        m_HasMovedForThisTurn = false;
        m_MovementHasStarted = false;
        m_InitalPosition = m_Position;

        m_Grid = Grid.Instance;
        
        
        Node_ObjectIsOn = m_Grid.GetNode(m_Position);
        Node_MovingTo = Node_ObjectIsOn;


        if (m_AiModel == null)
        {
            m_AiModel = transform.GetChild(0);
        }


        if (m_CreaturesAnimator == null)
        {
            m_CreaturesAnimator = GetComponentInChildren<Animator>();
        }
        

        
        _Pathfinder = new Pathfinder();

    }




    // Update is called once per frame
    public virtual void Update()
    {

        if (Node_ObjectIsOn != Node_MovingTo)
        {
            transform.position = Vector3.MoveTowards
            (transform.position, Node_MovingTo.gameObject.transform.position + CreatureOffset,
                8 * Time.deltaTime);
        }

        if (m_MovementHasStarted == true)
        {
            if (transform.position == Node_MovingTo.transform.position + CreatureOffset)
            {
                Node_ObjectIsOn = Node_MovingTo;
            }
        }



    }

    public void SetGoal(Vector2Int m_Goal)
    {
        m_Grid.SetHeuristicToZero();
        m_Grid.m_GridPathToGoal.Clear();
        m_Grid.GetNode(m_Goal.x, m_Goal.y).m_IsGoal = true;



    }

    public void FindAllPaths()
    {
        m_NodeInWalkableRange = GetAvailableDestinations(m_Grid.m_GridPathList, Node_ObjectIsOn, m_Movement);

    }

    public void DeselectAllPaths()
    {
        if (m_NodeInWalkableRange != null)
        {

            foreach (LevelNode node in m_NodeInWalkableRange)
            {
                node.m_Heuristic = 0;
            }
        }
    }

    public HashSet<LevelNode> GetAvailableDestinations(List<LevelNode> cells, LevelNode NodeHeuristicIsBasedOff,
        int Range)
    {
        cachedPaths = new Dictionary<LevelNode, List<LevelNode>>();

        var paths = cachePaths(cells, NodeHeuristicIsBasedOff, m_MovementType.CheckIfNodeIsClearAndReturnNodeIndex);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => c.m_MovementCost);
            key.m_Heuristic = pathCost;
            if (pathCost <= Range)
            {
                cachedPaths.Add(key, path);
            }
        }

        return new HashSet<LevelNode>(cachedPaths.Keys);
    }



    public HashSet<LevelNode> GetNodesInRange(List<LevelNode> aCells, LevelNode aNodeHeuristicIsBasedOff, int aRange,DelegateReturnNodeIndex delegateReturnNodeIndex)
    {
        cachedPaths = new Dictionary<LevelNode, List<LevelNode>>();

        var paths = cachePaths(aCells, aNodeHeuristicIsBasedOff,
            delegateReturnNodeIndex);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => 1);
            key.m_Heuristic = pathCost;
            if (pathCost <= aRange)
            {
                cachedPaths.Add(key, path);
            }
        }

        return new HashSet<LevelNode>(cachedPaths.Keys);
    }


    public virtual void SetGoalPosition(Vector2Int m_Goal)
    {

        SetGoal(m_Goal);

        m_NodeInWalkableRange =
            GetAvailableDestinations(m_Grid.m_GridPathList, m_Grid.GetNode(m_Goal.x, m_Goal.y), 100);


        foreach (LevelNode node in m_NodeInWalkableRange)
        {
            node.m_IsWalkable = true;
        }



        List<LevelNode> TempList = m_Grid.GetTheLowestH(Node_ObjectIsOn.m_PositionInGrid, m_Movement);


        StartCoroutine(GetToGoal(TempList));

    }
    


    public void DomainClash(int aDomainRemoved)
    {
        StartCoroutine(DomainClashResult(aDomainRemoved));
    }

    public IEnumerator DomainClashResult(int aDomainRemoved)
    {

        int i = 0;
        foreach (LevelNode node in m_NodeInDomainRange.Reverse())
        {
            if (i == aDomainRemoved)
            {
                break;
            }

            i++;
            
          //  GameManager.Instance.mMCombatCameraController.m_CameraPositionInGrid = node.m_PositionInGrid;
          m_NodeInDomainRange.Remove(node);


            if (i < 10)
            {
                yield return new WaitForSeconds(1.0f/ i);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        
    
    }

    public void SetDomain(int aDomainRange)
    {
        m_NodeInDomainRange =
            GetNodesInRange(m_Grid.m_GridPathList, m_Grid.GetNode(m_Position.x, m_Position.y), aDomainRange,
                m_Creature.m_Domain.CheckIfNodeIsClearAndReturnNodeIndex );
    }

    public void ActivateDomain()
    {

        foreach (LevelNode node in m_NodeInDomainRange)
        {
            node.m_DomainCombatNode = LevelNode.DomainCombatNode.Domain;

            
            if (node.m_CreatureOnGridPoint != null)
            {
                node.m_CreatureOnGridPoint.DomainAffectingCreature = m_Creature.m_Domain.DomainName;
            }
        }

        m_Creature.m_Domain.AdditionalDomainEffects();
    }

    public virtual IEnumerator GetToGoal(List<LevelNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
       // GameManager.Instance.mMCombatCameraController.m_cameraState = CombatCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_IsCovered = false;
        for (int i = 0; i < aListOfNodes.Count;)
        {

                if (Node_MovingTo == Node_ObjectIsOn)
                {

                    Node_MovingTo = aListOfNodes[i];
                    Vector3 relativePos = aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                    m_Position = Node_MovingTo.m_PositionInGrid;

                  //  GameManager.Instance.mMCombatCameraController.m_CameraPositionInGrid = m_Position;

                    m_AiModel.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                    CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
                    i++;
                    yield return new WaitUntil(() => Node_MovingTo == Node_ObjectIsOn);
                }
            

        }

        
        //Camera no longer following the player;
      // GameManager.Instance.mMCombatCameraController.m_cameraState = CombatCameraController.CameraState.Normal;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
       m_HasMovedForThisTurn = true;

       m_MovementHasStarted = false;
        //Changing the position from where the Creature was before
      

        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;
        m_PreviousNode = Node_ObjectIsOn;
        
        //Setting the node you are on to the new one
        Node_ObjectIsOn = Grid.instance.GetNode(m_Position);
        Node_ObjectIsOn.SetCreatureOnTopOfNode(m_Creature);
        Node_ObjectIsOn.m_IsCovered = true;
        
     //   m_PreviousNode.DomainOnNode.UndoDomainEffect(ref  Node_ObjectIsOn.m_CreatureOnGridPoint);
        
        
        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }

        
    }

    public virtual Dictionary<LevelNode, List<LevelNode>> cachePaths(List<LevelNode> cells, LevelNode aNodeHeuristicIsBasedOn,DelegateReturnNodeIndex delegateReturnNodeIndex )
    {
        var edges = GetGraphEdges(cells,delegateReturnNodeIndex);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn,m_Movement);
        return paths;
    }

    protected virtual Dictionary<LevelNode, Dictionary<LevelNode, int>> GetGraphEdges(List<LevelNode> NodeList,DelegateReturnNodeIndex delegateReturnNodeIndex)
    {
        Dictionary<LevelNode, Dictionary<LevelNode, int>> ret = new Dictionary<LevelNode, Dictionary<LevelNode, int>>();

        foreach (LevelNode Node in NodeList)
        {
            if (delegateReturnNodeIndex(Node,m_Position) == true|| Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<LevelNode, int>();
                foreach (LevelNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (delegateReturnNodeIndex(neighbour,m_Position) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }

    public virtual void ReturnToInitalPosition()
    {
        if (m_MovementHasStarted == false)
        {
            Grid.instance.GetNode(m_Position).m_CreatureOnGridPoint = null;
            Grid.instance.GetNode(m_Position).m_CombatsNodeType = LevelNode.CombatNodeTypes.Normal;

            m_Position = m_InitalPosition;

            CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Grid.instance.GetNode(m_Position).m_NodeHeightOffset, 0);

            Grid.instance.GetNode(m_Position).m_CreatureOnGridPoint = m_Creature;
            gameObject.transform.position = Grid.instance.GetNode(m_Position).transform.position + CreatureOffset;

            m_HasMovedForThisTurn = false;
        }

    }
}
