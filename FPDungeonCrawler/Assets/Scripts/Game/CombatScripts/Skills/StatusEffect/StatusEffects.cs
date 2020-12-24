using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : Skills
{
    protected int Length;

    protected Dictionary<Creatures,int> ActivatedCreature;
    // Start is called before the first frame update
    public virtual void RevertStatusEffect(Creatures aCreature)
    {
      //  ActivatedCreature[aCreature].;

    }

    public bool CheckIfStatusEffectIsActive()
    {
        if (Length <= 0)
        {
            return false;
        }

        return true;
    }

    public void EndOfTurn()
    {
        Length--;

        if (Length == 0)
        {
         //   RevertStatusEffect();
        }
    }
    
}
