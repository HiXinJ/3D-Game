using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        // Debug.Log(translationX+" "+translationZ);
        {
            if (translationX != 0 || translationZ != 0)
            {
                this.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                this.GetComponent<Animator>().SetBool("run", false);
            }
            if(Mathf.Abs(translationX)>0.1||Mathf.Abs(translationZ)>0.1)
            { 
                Vector3 target = this.transform.position;
                target.x+=translationX*10;
                target.z+=translationZ*10;
                this.transform.LookAt(target);

                this.transform.position+=Vector3.forward*translationZ * speed * Time.deltaTime;
                this.transform.position+=Vector3.right*translationX * speed * Time.deltaTime;
            }
            //防止碰撞带来的移动
            if (this.transform.localEulerAngles.x != 0 || this.transform.localEulerAngles.z != 0)
            {
                this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
            }
            if (this.transform.position.y != 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }     
        }
    }
}
