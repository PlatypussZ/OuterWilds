using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuadGenerator : MonoBehaviour
{
    private List<GameObject> quads = new List<GameObject>();
    public void recieveData( List<WindowData> data )
    {
        for ( int i = 0; i < data.Count; i++ )
        {
            WindowData rect = data[i];

            int ID = i;

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            //WindowManager.instance.SendDataToUIdebugger(rect, ID);

            Debug.Log("valid window with ID: " + ID + " at: " + rect.Top + "," + rect.Left + ", size: " + width + "x" + height);

            Vector2 origin = new Vector2(rect.Left, rect.Top);
            Vector2 dimensions = new Vector2(width, height);
            InstantiateQuad(origin, dimensions);
        }
    }

    private void InstantiateQuad( Vector2 origin, Vector2 Dimensions )
    {
        GameObject quad = new GameObject();
        quads.Add(quad);
        quad.transform.position = origin;
        quad.transform.localScale = Dimensions;
    }
}
