using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private short GridDimensionX = 10;
    private short GridDimensionY = 10;
    private short[] LevelBlueprint;

    
    // Start is called before the first frame update
    void Start()
    {
        LevelBlueprint = new short[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        SetLevelNodes(LevelBlueprint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelNodes(short[] aLevelBlueprint)
    {
        for (int i = 0; i < aLevelBlueprint.Length; i++)
        {
            if (aLevelBlueprint[i] == (short)LevelnodeType.Empty)
            {
               
            }
        }
    }

}
