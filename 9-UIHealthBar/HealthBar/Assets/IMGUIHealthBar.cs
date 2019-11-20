using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUIHealthBar : MonoBehaviour
{
    public float size=50f;

    void Start()
    {
    }
    void Update()
    {
    }
    void OnGUI()
    {
        Vector3 pos= Camera.main.WorldToScreenPoint(this.transform.position);
        if(size <=100)
            size += Time.deltaTime*3;
        
        GUI.HorizontalScrollbar(new Rect(pos.x-50,pos.y,100,20),  0,size,0f,100f);
    }
}
