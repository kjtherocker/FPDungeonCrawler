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
        UpDown = 5,
        UpLeft = 6,
        Upright = 7,
        
    }


    public short GridDimensionX = 10;
    public short GridDimensionY = 10;
    public short[] LevelBlueprint;



    // Start is called before the first frame update
    public void Intialize()
    {
        LevelBlueprint = new short[]
        {
            1,1, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 1, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
    }








}
