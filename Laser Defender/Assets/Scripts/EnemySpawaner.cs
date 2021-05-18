using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawaner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f; 
    public float speed;
    public float padding = 5f;
    private bool movingRight = true;
    private float xmin;
    private float xmax;
    private GameObject Laser;
    public float spawndelay = 0.5f;
	// Use this for initialization
	void Start () {

        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x;
        xmax = rightmost.x;

        SpawnEnemy();

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }

    // Update is called once per frame
    void Update()
    {

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        //float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        //transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        float rightEdgeofFormation = transform.position.x + (0.5f * width);     
        float leftEdgeofFormation = transform.position.x - (0.5f * width);
        /*if( leftEdgeofFormation < xmin || rightEdgeofFormation > xmax)
        {
            movingRight = !movingRight;
        }*/

        if(leftEdgeofFormation<xmin)
        {
            movingRight = true;
        }
        else if(rightEdgeofFormation>xmax)
        {
            movingRight = false;
        }

        if(AllMembersDead())
        {
            SpawnUntilFull();

        }

        /*if (transform.position.x == xmax)
        {
            movingRight = false;
        }
        else if (transform.position.x == xmin)
        {
            movingRight = true;
        }*/
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositonGameObject in transform)
        {
            if (childPositonGameObject.childCount > 0)
                return false;
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach(Transform child in transform)
        {
            if (child.childCount == 0)
                return child.transform;
        }

        return null;
    }

    void SpawnEnemy()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity);
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawndelay);
        }
    }
}
