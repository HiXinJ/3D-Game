using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{

    // number of priests at right bank
    public int priest;

    // number of devils at right bank
    public int devil;

    // true if boat is at right bank
    public bool boat;

    public int priestSum;
    public int devilSum;
    public Node parent;

    public Node(int p, int d, bool b, int pSum, int dSum){
        this.priest = p;
        this.devil = d;
        this.boat = b;
        this.priestSum = pSum;
        this.devilSum = dSum;
        this.parent=null;
    }
    public Node(Node n)
    {
        this.priest = n.priest;
        this.devil = n.devil;
        this.boat = n.boat;
        this.priestSum = n.priestSum;
        this.devilSum = n.devilSum;
        this.parent = n.parent;
    }

    public bool action(int p, int d){
        // boat is at right bank
        // Debug.Log(this+"  action: "+p+d);
        Node old = new Node(this);
        if(boat == true)
        {
            int rp = this.priest - p;
            int rd = this.devil - d;

            int lp = this.priestSum - rp;
            int ld = this.devilSum - rd;

            if(!(rp >=0 && rd >=0 && lp >=0 && ld >=0) || (rp!=0 && rp < rd) || (lp!=0&&lp < ld))
                return false;

            this.parent = new Node(this);
            this.priest = rp;
            this.devil = rd;
            this.boat = false;
        }
        // boat is at left bank
        else
        {
            int rp = this.priest + p;
            int rd = this.devil + d;

            int lp = this.priestSum - rp;
            int ld = this.devilSum - rd;

            if(!(rp >=0 && rd >=0 && lp >=0 && ld >=0) || (rp!=0&&rp < rd) || (lp!=0&&lp < ld))
                return false;

            this.parent = new Node(this);
            this.priest += p;
            this.devil += d;
            this.boat =true;
        }
        this.parent = old;
        return true;

    }

    public override string ToString()
    {
        return "right P "+this.priest +"  D "+this.devil +" "+this.boat;
    }
    // override object.Equals
    public override bool Equals(object obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
       
        Node n = (Node)(obj);
        if(n.priest ==this.priest && n.devil == this.devil && n.boat == this.boat)
            return true;
        else
            return false;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        int t = 0;
        if(this.boat)
            t = 1; 
        return this.priest*100+this.devil*10 + t;
    }
}