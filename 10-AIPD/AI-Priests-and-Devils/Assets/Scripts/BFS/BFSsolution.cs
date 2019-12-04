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

        // this.beginJNode = new Node(bNode);
        // this.endJNode = new Node(eNode);
        // this.currentJNode = null;

        while(exploreList.Count!=0){

//             this.currentJNode = exploreList.poll();
//             if(currentJNode.equals(eNode)){
//                 this.getPath();
//                 break;
//             }
//             Node[] nextNodes = new Node[]{
//                     new Node(this.currentJNode), new Node(this.currentJNode),
//                     new Node(this.currentJNode), new Node(this.currentJNode)
//             };
//             for (int i = 0; i < 4; i++) {
//                 if (nextNodes[i].move(i) && !visitedList.contains(nextNodes[i])) {
// //                    Node tempJNode = new Node(nextNodes[i]);
//                     visitedList.add(nextNodes[i]);
//                     exploreList.add(nextNodes[i]);
//                 }
//             }

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

            //0 0, 0 1, 0 2, 1 0, 1 1, 1 2, 2 0, 2 1, 2 2
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
