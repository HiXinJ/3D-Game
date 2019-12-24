using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
 
public class EventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject Garen;


    void Start()
    {
        VirtualButtonBehaviour VRbehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();
        if(VRbehaviours)
            VRbehaviours.RegisterEventHandler(this);

        Garen = GameObject.Find("Garen");
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {        
        switch (vb.VirtualButtonName)
        {
            case "Up":
                Garen.transform.position += new Vector3(0, 0.01f, 0);
                break;
            case "Down":
                Garen.transform.position -= new Vector3(0, 0.01f, 0);
                break;
            case "Left":
                Garen.transform.position += new Vector3(0.01f, 0, 0);
                break;
            case "Right":
                Garen.transform.position -= new Vector3(0.01f, 0, 0);
                break;    
        }
    }
}