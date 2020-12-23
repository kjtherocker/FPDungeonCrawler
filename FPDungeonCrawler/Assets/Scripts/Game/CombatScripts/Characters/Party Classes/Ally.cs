using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Creatures
{
    public override void Death()
    {
        base.Death();
        
    //    Destroy(ModelInGame.gameObject);
    }
}
