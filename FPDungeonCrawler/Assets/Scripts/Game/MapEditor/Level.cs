using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public enum LevelnodeType
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
    public short[] LevelBlueprint;



    // Start is called before the first frame update
    public void Intialize()
    {
        LevelBlueprint = new short[]
        {
            4, 15, 9, 0, 0, 0, 0, 0, 0, 0,
            0, 8, 13, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 6, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 6, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
    }


    public int GetIndex(int aRow, int aColumn)
    {
        return aColumn * GridDimensionX + aRow;
    }

    public void SpawnCamera()
    {
        
    }
    
    public void SpawnGimmicks()
    {
        
    }




}
