using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingOnScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Draw a label text in specified location in 3D, used for Debuging.
    /// </summary>
    /// <param name="loc"> 3D location of the label text. </param>
    /// <param name="text"> String with text to display. </param>
    public void DrawLabel(Vector3 loc, string text)
    {
        Vector2 guiPosition = Camera.main.WorldToScreenPoint(loc);
        guiPosition.y = Screen.height - guiPosition.y;
        GUI.Label(new Rect(guiPosition, new Vector2(30, 20)), text);
    }
}
