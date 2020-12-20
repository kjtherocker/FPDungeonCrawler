using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class PlayerMovementController : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    
    public FloorNode currentFloorNode;
    
    public FloorNode.CardinalNodeDirections[] CardinalDirections;
    public FloorNode.CardinalNodeDirections CurrentDirection;
    public int CurrentDirectionValue;

    public Vector2Int CurrentPosition;

    public FloorManager m_CurrentFloorManager;
    private Dictionary<FloorNode.CardinalNodeDirections, Vector3> m_DirectionRotations;

    public UiMap m_Map;  
    public void Initialize ()
    {
        CardinalDirections = new []
        {
            FloorNode.CardinalNodeDirections.Up, FloorNode.CardinalNodeDirections.Left, 
            FloorNode.CardinalNodeDirections.Down,FloorNode.CardinalNodeDirections.Right
        };
        
        m_DirectionRotations = new Dictionary<FloorNode.CardinalNodeDirections, Vector3>();
        
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Down, new Vector3(0, 90, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Left, new Vector3(0, 180, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Up, new Vector3(0, 270, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Right,  new Vector3(0, 360, 0));        
        CurrentDirectionValue = 0;

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        StartCoroutine(InterpolateRotationSmooth(transform, m_DirectionRotations[CurrentDirection],0.0f));
        
        InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    }

    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            m_CurrentFloorManager.SwitchToCombat();
        }
    }

    public  IEnumerator InterpolateRotationSmooth(Transform aObject, Vector3 aTargetRotation, float aTimeUntilDone)
    {
        Vector3 CurrentPostion = new Vector3(aObject.localRotation.eulerAngles.x,aObject.localRotation.eulerAngles.y,aObject.localRotation.eulerAngles.z);
        float elapsedTime = 0.0f;
        
        Quaternion targetQuaternion = Quaternion.Euler(0, aTargetRotation.y, 0);
        for(var t = 0f; t < 1; t += Time.deltaTime/aTimeUntilDone)
        {
      
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, t * 15);
            
            yield return new WaitForFixedUpdate();
        }

        aObject.localEulerAngles = aTargetRotation;
        yield return 0;
    }
    public  IEnumerator DirectMovement(Transform MainObject, Vector3 targetPosition, float TimeUntilDone)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < TimeUntilDone) 
        {
            
            yield return new WaitForFixedUpdate();
            elapsedTime += Time.deltaTime;
            MainObject.position = Vector3.Lerp(MainObject.position, targetPosition, elapsedTime /TimeUntilDone );
        }
        
        FloorNode TargetNode = m_CurrentFloorManager.GetNode(currentFloorNode.m_PositionInGrid, CurrentDirection);
        MainObject.position = targetPosition;
        currentFloorNode = TargetNode;
        TargetNode.ActivateWalkOnTopTrigger();
        yield return 0;
    }

    
    public void PlayerMovement(Vector2 aDirection)
    {
        RotatePlayer((int)aDirection.x);
        if (aDirection.y > 0)
        {
            if (currentFloorNode.IsDirectionWalkable(CurrentDirection))
            {
                MoveForward();
            }
        }
    }

    public void RotatePlayer(int aRotateDirection)
    {
        //Inverting Direction
        int direction = aRotateDirection * -1;
        
        CurrentDirectionValue += direction;

        CheckMinAndMax();

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        
        Vector3 NewRotation = m_DirectionRotations[CurrentDirection];

        StartCoroutine(InterpolateRotationSmooth(transform, NewRotation,0.3f));
    }

    public void MoveForward()
    {
        FloorNode TargetNode = m_CurrentFloorManager.GetNode(currentFloorNode.m_PositionInGrid, CurrentDirection);
        if (TargetNode == null)
        {
            Debug.Log("Cant Find Node " + currentFloorNode.m_PositionInGrid);
            return;
        }

                
        Vector3 NewNodePosition = new Vector3(TargetNode.transform.position.x,TargetNode.transform.position.y + Constants.Constants.m_HeightOffTheGrid,
            TargetNode.transform.position.z);
                
                
        StartCoroutine(DirectMovement(transform, NewNodePosition, 0.2f));

        int index = m_CurrentFloorManager.m_FloorCore.GetIndex(TargetNode.m_PositionInGrid.x,
            TargetNode.m_PositionInGrid.y);
        m_Map.SetPlayerNode(index);
        CurrentPosition = TargetNode.m_PositionInGrid;

    }


    public void SetPlayerMapPosition(FloorNode aFloorNode)
    {
        int index = m_CurrentFloorManager.m_FloorCore.GetIndex(aFloorNode.m_PositionInGrid.x,
            aFloorNode.m_PositionInGrid.y);
        m_Map.SetPlayerNode(index);
    }



    public void CheckMinAndMax()
    {
        if (CurrentDirectionValue > CardinalDirections.Length -1)
        {
            CurrentDirectionValue = 0;
        }
        
        if (CurrentDirectionValue < 0)
        {
            CurrentDirectionValue = CardinalDirections.Length - 1;
        }
    }
    

    
}

