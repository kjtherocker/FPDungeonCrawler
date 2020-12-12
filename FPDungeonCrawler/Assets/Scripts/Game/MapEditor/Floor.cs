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
        DownLeft = 9,
        DownRight = 10,
        LeftRight = 11,
        UpLeftRight = 12,
        UpLeftDown = 13,
        UpRightDown = 14,
        RightLeftDown = 15,
       // UpRightDown = 16,
       
    }


    public short GridDimensionX = 10;
    public short GridDimensionY = 10;
    public short[] FloorBlueprint;
    public bool[] FloorRevealed;

    public Vector2Int m_DefaultSpawnPosition;

    // Start is called before the first frame update
    public void Intialize()
    {
        FloorRevealed = new bool[GridDimensionX * GridDimensionY];

        for (int i = FloorRevealed.Length - 1; i >= 0; i--)
        {
            FloorRevealed[i] = false;
        }


        FloorBlueprint = new short[]
        {
            4, 15, 9, 0, 0, 0, 0, 0, 0, 0,
            2, 8, 13, 0, 0, 0, 0, 0, 0, 0,
            8, 9, 6, 0, 0, 0, 0, 0, 0, 0,
            0, 6, 6, 0, 0, 0, 0, 0, 0, 0,
            0, 8, 5, 15, 15, 9, 0, 0, 0, 0,
            0, 0, 8, 12, 12, 7, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        
        

        SpawnCamera();

        SpawnGimmicks();
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




}
