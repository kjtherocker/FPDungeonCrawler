using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Floor : MonoBehaviour
{
    public enum LevelCreationDirections
    {
        Empty = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        AllSidesOpen = 5,
        UpDown = 6,
        UpLeft = 7,
        Upright = 8,
        LeftDown = 9,
        RightDown = 10,
        LeftRight = 11,
        UpLeftRight = 12,
        UpLeftDown = 13,
        UpRightDown = 14,
        DownLeftRight = 15,
    }


    public short GridDimensionX = 10;
    public short GridDimensionY = 10;
    public short[] FloorBlueprint;
    public bool[] FloorRevealed;

    public List<InitializeOverWorldEnemy> m_Enemy;
    
    delegate void EnemySpawners();

    private List<EnemySpawners> m_EnemySpawners;
    
    public float EnemySpawnRate;

    public Vector2Int m_DefaultSpawnPosition;

    // Start is called before the first frame update
    public virtual void Intialize()
    {
        GridDimensionX = 10;
        GridDimensionY = 10;

        FloorRevealed = new bool[GridDimensionX * GridDimensionY];

        for (int i = FloorRevealed.Length - 1; i >= 0; i--)
        {
            FloorRevealed[i] = true;
        }


        FloorBlueprint = new short[]
        {
            4, 15,  9,  0,  0,  0,  0,  0,  0,  0,
            2,  8, 13,  0,  0,  0,  0,  0,  0,  0,
            8,  9,  6,  0,  0,  0,  0,  0,  0,  0,
            0,  6,  6,  0,  0,  0,  0,  0,  0,  0,
            0,  8,  5, 15, 15,  9,  0,  0,  0,  0,
            0,  0,  8, 12, 12,  7,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,  0,  0
        };
        
        
        
        
      // FloorBlueprint = new short[]
      // {
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0
      // };

      
 //   FloorBlueprint = new short[]
 //   {
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //20
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //19
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //18
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //17
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //16
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //15
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //14
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //13
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //12
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //11
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //10
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //9
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //8
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //7
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //6
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //5
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //4
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //3
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //2
 //       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0  //1
 //       //  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20
 //   }; 

        

        SpawnCamera();

        SpawnGimmicks();
    }

    public void InitializeEnemys()
    {
        EnemysThatCanSpawn();
    }

    public int GetIndex(int aRow, int aColumn)
    {
        return aRow * GridDimensionX + aColumn;
    }

    public void SpawnCamera()
    {
        m_DefaultSpawnPosition = new Vector2Int(0,1);
    }
    
    public void SpawnGimmicks()
    {
        
    }

    public void EnemysThatCanSpawn()
    {
        m_Enemy = new List<InitializeOverWorldEnemy>();
        m_EnemySpawners = new List<EnemySpawners>();
       // m_EnemySpawners.Add(Enemy1);
        m_EnemySpawners.Add(Enemy2);
      // m_EnemySpawners.Add(Enemy3);
      // m_EnemySpawners.Add(Enemy4);
      // m_EnemySpawners.Add(Enemy5);
      // m_EnemySpawners.Add(Enemy6);

        foreach (EnemySpawners enemySpawners in m_EnemySpawners)
        {
            enemySpawners();
        }
    }


    public void Enemy1()
    {
        List<Vector2Int> m_PositionsEnemyCanMove = new List<Vector2Int>();
        
        m_PositionsEnemyCanMove.Add(new Vector2Int(9,3));
        m_PositionsEnemyCanMove.Add(new Vector2Int(8,3));
        m_PositionsEnemyCanMove.Add(new Vector2Int(7,3));
        m_PositionsEnemyCanMove.Add(new Vector2Int(6,3));
        m_PositionsEnemyCanMove.Add(new Vector2Int(5,3));
        
        m_Enemy.Add(new InitializeOverWorldEnemy(new Vector2Int( 9,3),m_PositionsEnemyCanMove, EnemySet1));
    }
    
    
    public void Enemy2()
    {
        List<Vector2Int> m_PositionsEnemyCanMove = new List<Vector2Int>();
        
        m_PositionsEnemyCanMove.Add(new Vector2Int(9,1));
        m_PositionsEnemyCanMove.Add(new Vector2Int(8,1));
        m_PositionsEnemyCanMove.Add(new Vector2Int(7,1));
        m_PositionsEnemyCanMove.Add(new Vector2Int(6,1));
        m_PositionsEnemyCanMove.Add(new Vector2Int(5,1));
        
        m_Enemy.Add(new InitializeOverWorldEnemy(new Vector2Int( 5,1),m_PositionsEnemyCanMove, EnemySet3));
    }



    public List<EnemyList.EnemyTypes> EnemySet1()
    {
        List<EnemyList.EnemyTypes> EnemysToCombat = new List<EnemyList.EnemyTypes>();
        
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight3);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight2);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight2);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight1);

        return EnemysToCombat;
    }

    public List<EnemyList.EnemyTypes> EnemySet2()
    {
        List<EnemyList.EnemyTypes> EnemysToCombat = new List<EnemyList.EnemyTypes>();
        
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight2);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight3);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight4);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight1);

        return EnemysToCombat;
    }

    public List<EnemyList.EnemyTypes> EnemySet3()
    {
        List<EnemyList.EnemyTypes> EnemysToCombat = new List<EnemyList.EnemyTypes>();
        
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight3);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight2);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight2);
        EnemysToCombat.Add(EnemyList.EnemyTypes.RedKnight3);

        return EnemysToCombat;
    }



}
