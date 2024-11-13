using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset };                              // a

    [Header("Inscribed")]
    public eType boundsType = eType.center;                                   // a
    public float radius = 1f;
    public bool keepOnScreen = true;


    [Header("Dynamic")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;                             // b
        camWidth = camHeight * Camera.main.aspect;                            // c
    }

    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset     // b
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        isOnScreen = true;                                                    // d

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            isOnScreen = false;                                               // e
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;                                               // e
        }

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            isOnScreen = false;                                               // e
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            isOnScreen = false;                                               // e
        }

        if (keepOnScreen && !isOnScreen)
        {                                  // f
            transform.position = pos;                                         // g
            isOnScreen = true;
        }
    }
}
