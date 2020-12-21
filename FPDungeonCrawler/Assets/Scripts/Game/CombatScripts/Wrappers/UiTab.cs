using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTab : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Highlight;

    public Color m_Highlight;
    public Color m_NotHighlight;
    public virtual void Initialize()
    {
        
    }
    
    public void TabHoveredOver(bool isHoverovered)
    {
        Highlight.color = isHoverovered ? m_Highlight : m_NotHighlight;
    }
}
