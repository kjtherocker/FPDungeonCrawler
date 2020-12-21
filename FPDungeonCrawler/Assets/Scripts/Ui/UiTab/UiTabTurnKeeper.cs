using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTabTurnKeeper : UiTabs
{
    public List<RawImage> m_Images;


    public void UpdateTurnIcons(int aTurns)
    {
        for (int i = 0; i < m_Images.Count; i++)
        {
            m_Images[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < aTurns; i++)
        {
            m_Images[i].gameObject.SetActive(true);
        }

    }
}
