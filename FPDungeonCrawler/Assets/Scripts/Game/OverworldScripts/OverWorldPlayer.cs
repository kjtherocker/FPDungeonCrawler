using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
 
    public Animator m_PlayerAnimatior;

    public float Player_Speed = 5;
    public float m_PlayerRotationSpeed;
    public bool m_IsInMenu;
    private float Player_Speed_Delta;

    
    private bool Player_Movment;
    float m_Gravity;
    float mass = 3.0f;
    Vector3 m_Velocity = Vector3.zero;

    public Vector2 MoveDirection;
    public Level.Directions[] CardinalDirections;

    private Vector3[] CardinalRotations;

    public Level.Directions CurrentDirection;
    public int CurrentDirectionValue;

    private GridFormations m_GridFormation;
    
    
    public void Initialize ()
    {

        m_PlayerAnimatior = GetComponentInChildren<Animator>();
        CardinalDirections = new []
        {
            Level.Directions.Up, Level.Directions.Right, Level.Directions.Down, Level.Directions.Left
        };

        CardinalRotations = new[]
        {
             new Vector3(0, 90, 0), 
             new Vector3(0, 180, 0), 
             new Vector3(0, 270, 0),
             new Vector3(0, 0, 0)
        };


        transform.eulerAngles = CardinalRotations[0];
        
        CurrentDirection = CardinalDirections[0];

        CurrentDirectionValue = 0;
        
       InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    }

    public void PlayerMovement(Vector2 aDirection)
    {
        
        MoveDirection = aDirection;
        
        CurrentDirectionValue += (int)aDirection.x;

        CheckMinAndMax();

        CurrentDirection = CardinalDirections[CurrentDirectionValue];


        if (aDirection.y > 0)
        {
            
        }


        // Vector3 NextRotation = new Vector3(MoveDirection.x, 0, MoveDirection.y);
        
        //float SpeedUpdate = m_PlayerRotationSpeed * Time.deltaTime;

      //  transform.rotation =  Quaternion.LookRotation( NextRotation,Vector3.up );

        
    }


    public void CheckMinAndMax()
    {
        if (CurrentDirectionValue > 3)
        {
            CurrentDirectionValue = 0;
        }
        
        if (CurrentDirectionValue < 0)
        {
            CurrentDirectionValue = 3;
        }
    }

    IEnumerator MoveToDirection()
    {
        float SpeedUpdate = Player_Speed * Time.deltaTime;
        
        m_Velocity = (new Vector3(MoveDirection.x ,0  ,MoveDirection.y )* SpeedUpdate );
        
        transform.position =  gameObject.transform.position + m_Velocity;
        yield return  new WaitForEndOfFrame();
    }


    // Update is called once per frame
	void FixedUpdate ()
    {



    }
    
}

