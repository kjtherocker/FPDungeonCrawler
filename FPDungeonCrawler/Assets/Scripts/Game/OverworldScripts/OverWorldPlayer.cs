using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    
    public float m_RotationSpeed = 0.5f;

    Vector3 m_Velocity = Vector3.zero;

    public Vector2 MoveDirection;

    public LevelNode CurrentLevelNode;
    
    public Level.Directions[] CardinalDirections;
    public Level.Directions CurrentDirection;
    public int CurrentDirectionValue;

    public GridFormations m_GridFormation;
    private Dictionary<Level.Directions, Vector3> m_DirectionRotations;
    
    public void Initialize ()
    {
        CardinalDirections = new []
        {
            Level.Directions.Up, Level.Directions.Right, Level.Directions.Down, Level.Directions.Left
        };
        
        m_DirectionRotations = new Dictionary<Level.Directions, Vector3>();
        
        m_DirectionRotations.Add( Level.Directions.Down, new Vector3(0, 90, 0));
        m_DirectionRotations.Add( Level.Directions.Left,  new Vector3(0, 360, 0));
        m_DirectionRotations.Add( Level.Directions.Up, new Vector3(0, 270, 0));
        m_DirectionRotations.Add( Level.Directions.Right, new Vector3(0, 180, 0));
        
        

        
        CurrentDirectionValue = 0;

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        StartCoroutine(InterpolateRotationSmooth(transform, m_DirectionRotations[CurrentDirection],0.0f));
       
        
       InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    }
    
    public  IEnumerator InterpolateRotationSmooth(Transform aObject, Vector3 aTargetRotation, float aTimeUntilDone)
    {
        Vector3 CurrentPostion = new Vector3(aObject.localRotation.eulerAngles.x,aObject.localRotation.eulerAngles.y,aObject.localRotation.eulerAngles.z);
 
        float Frametime = Time.time;
        while(Time.time < Frametime + aTimeUntilDone && aObject != null)
        {
            float timePercentage = (Time.time - Frametime) / aTimeUntilDone;

            Quaternion targetQuaternion = Quaternion.Euler(0, aTargetRotation.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, timePercentage);
           //aObject.localEulerAngles = new Vector3 (0, Mathf.SmoothStep (CurrentPostion.y, aTargetRotation.y, timePercentage), 0);
            //obj.localEulerAngles = Vector3.Lerp(source,destinationRotation,(Time.time - startTime)/overTime);
            yield return null;
        }
 
        aObject.localEulerAngles = aTargetRotation;
    }
    
    public  IEnumerator InterpolateMovementSmooth(Transform aObject, Transform aTargetNode, float aTimeUntilDone)
    {
        Vector3 CurrentPostion = aObject.position;
        
        Vector3 FinalTargetPosition = new Vector3(aTargetNode.position.x, 
            aTargetNode.position.y + Constants.Constants.m_HeightOffTheGrid, aTargetNode.position.z);
 
        float Frametime = Time.time;
      // while(Time.time < Frametime + aTimeUntilDone && CurrentPostion != null)
      // {
      //     float timePercentage = (Time.time - Frametime) / aTimeUntilDone;
      //     
      //     //aObject.localEulerAngles = new Vector3 (0, Mathf.SmoothStep (CurrentPostion.y, aTargetRotation.y, timePercentage), 0);
      //     aObject.position = Vector3.Lerp(CurrentPostion,aTargetNode.position,(Time.time - Frametime)/timePercentage);
      //     yield return null;
      // }
 
        aObject.position =  FinalTargetPosition;
        yield return null;
    }

    
    public void PlayerMovement(Vector2 aDirection)
    {
        
        int direction = (int)aDirection.x * -1;
        
        CurrentDirectionValue += direction;

        CheckMinAndMax();

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        
        Vector3 NewRotation = m_DirectionRotations[CurrentDirection];

        StartCoroutine(InterpolateRotationSmooth(transform, NewRotation,0.5f));
        
       // transform.eulerAngles = m_Directions[CurrentDirection];

        if (aDirection.y > 0)
        {
            LevelNode TargetNode = m_GridFormation.GetNode(CurrentLevelNode.m_PositionInGrid, CurrentDirection);
            Debug.Log(TargetNode.m_PositionInGrid);
            
            if (TargetNode == null)
            {
                return;
            }

            StartCoroutine(InterpolateMovementSmooth(transform, TargetNode.transform, 0.5f));
        }



        
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

    IEnumerator MoveToDirection()
    {
     // float SpeedUpdate = Player_Speed * Time.deltaTime;
     // 
     // m_Velocity = (new Vector3(MoveDirection.x ,0  ,MoveDirection.y )* SpeedUpdate );
     // 
     // transform.position =  gameObject.transform.position + m_Velocity;
        yield return  new WaitForEndOfFrame();
    }


    // Update is called once per frame
	void FixedUpdate ()
    {



    }
    
}

