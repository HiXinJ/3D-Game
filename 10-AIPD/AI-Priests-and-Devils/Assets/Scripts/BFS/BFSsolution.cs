using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSsolution : ScriptableObject
{
    public List<Node> BFSearch(Node bNode, Node eNode) {

        List<Node> solutionPath = null;
        Queue<Node> exploreList = new Queue<Node>();
        List<Node> visitedList = new List<Node>();
        exploreList.Enqueue(bNode);
        visitedList.Add(bNode);

        Node beginNode = new Node(bNode);
        Node endNode = new Node(eNode);
        Node currentNode = null;

        while(exploreList.Count!=0){
            currentNode = exploreList.Dequeue();
            if(currentNode.Equals(endNode)){
                solutionPath = new List<Node>(); 
                Node pathNode = currentNode;
                while(pathNode != null){
                    // Debug.Log(pathNode);
                    solutionPath.Add(pathNode);
                    pathNode = pathNode.parent;
                }
                Debug.Log("Find");
                break;
            }
            for(int i = 0; i <= 2; i++)
            {
                for(int j = 0; j <= 2; j++)
                {
                    if(i == 0 && j == 0 || i+j > 2)
                    {
                        continue;
                    }
                    Node nextNode = new Node(currentNode);
                    if(nextNode.action(i,j) && !visitedList.Contains(nextNode)){
                        // Debug.Log(nextNode);
                        Node tempNode = new Node(nextNode);
                        visitedList.Add(tempNode);
                        exploreList.Enqueue(tempNode);
                    }
                }
            }
        }
        return solutionPath;
    }
}
