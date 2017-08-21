using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject           EnemyPrefab;
    public float                width =                   5f;
    public float                height =                  5f;
    private bool                movingRight =             true;
    public float                speed =                   5f;
    private float               xmax;
    private float               xmin;
    public float                spawnDelay =              0.5f;
    
	
	void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightEdge.x;
        xmin = leftBoundary.x;
        SpawnUntilFull();
          
	}
    void spawnEnemies(){
        foreach (Transform child in transform){
            GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
    void SpawnUntilFull(){
        Transform freePosition = NextFreePosition();
        if (freePosition) {
            GameObject enemy = Instantiate(EnemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()){
        Invoke("SpawnUntilFull", spawnDelay);
        }
    }
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
    }

    void Update(){
        if (movingRight){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }else {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // Check if the formation is going outside the playspace...
        float rightEdgeOfFormation =    transform.position.x + (0.5f * width);
        float leftEdgeOfFormation =     transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin){
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax){
            movingRight = false;
        }

        if (AllMembersDead()){
            Debug.Log("Empty");
            SpawnUntilFull();
        }
    }
    Transform NextFreePosition(){ 
          foreach (Transform childPositionGameObject in transform) {
                if (childPositionGameObject.childCount == 0) { return childPositionGameObject; }
          } return null; 
        }
    
     bool AllMembersDead(){
            foreach (Transform childPositionGameObject in transform) {
                if (childPositionGameObject.childCount > 0) {return false;}
            } return true;   
        }  
}
     