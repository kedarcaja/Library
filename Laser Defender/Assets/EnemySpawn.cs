using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject EnemyPrefab;
    public float width = 5f;
    public float height = 5f;
    private bool movingRight = true;
    public float speed = 5f;
    private float xmax;
    private float xmin;
	
	void Start () {
        float distancecamera = transform.position.z - Camera.main.transform.position.z;
       Vector3 LeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,distancecamera));
       Vector3 RightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distancecamera));
       xmax =  RightEdge.x;
       xmin = LeftBoundary.x;
           foreach (Transform child in transform) {
               GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
               enemy.transform.parent = child;
           }
	}
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
    }
	
	void Update () {
        if (movingRight)
        {
            transform.position += Vector3.right* speed * Time.deltaTime;
        }
        else { transform.position += Vector3.left * speed * Time.deltaTime; }

         float rightEdgeOfFormation = transform.position.x + (0.5f*width);
         float LeftEdgeOfFormation = transform.position.x + (0.5f * width);

         if (LeftEdgeOfFormation<xmin||rightEdgeOfFormation>xmax) {
             movingRight = !movingRight;
         }
    }
   
}
