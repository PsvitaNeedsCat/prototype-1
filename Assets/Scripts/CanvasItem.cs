using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasItem : MonoBehaviour
{
    public enum CanvasItemTag
    {
        LAPSCOUNTER,
        STROKES,
        CHARGEBAR,
        COUNTDOWN
    }

    public CanvasItemTag itemTag;
}
