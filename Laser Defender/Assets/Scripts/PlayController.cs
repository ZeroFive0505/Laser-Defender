using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

    public float Velocity;
    private Rigidbody2D rb;
    public GameObject laserPrefab;
    public float LaserSpeed;
    public float xmin;
    public float xmax;
    public float firingRate;
    public float health = 200;
    public AudioClip Shoot;
    private LevelManager levelManager;
  
    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x;
        xmax = rightmost.x;
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }



    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-Velocity * Time.deltaTime, 0, 0);
            transform.position +=  Vector3.left * Velocity * Time.deltaTime;
        }

        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(Velocity *Time.deltaTime, 0, 0);
            transform.position += Vector3.right * Velocity * Time.deltaTime;
        }
        //mathf.clamp
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Fire()
    {
        //Vector3 offset = new Vector3(0, 1f, 0);
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(Shoot, laser.transform.position, 5f);
        rb = laser.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, LaserSpeed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider);
        EnemyLaser laser = collider.gameObject.GetComponent<EnemyLaser>();
        EnemyLaser2 laser2 = collider.gameObject.GetComponent<EnemyLaser2>();
        MotherShipLaser M_laser = collider.gameObject.GetComponent<MotherShipLaser>();

        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                levelManager.LoadLevel("Lose");
            }
            //Debug.Log("Player Hit by a laser");
        }
        else if(laser2)
        {
            health -= laser2.GetDamage();
            laser2.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                levelManager.LoadLevel("Lose");
            }
        }
        else if(M_laser)
        {
            health -= M_laser.GetDamage();
            M_laser.Hit();
            if(health<=0)
            {
                Destroy(gameObject);
                levelManager.LoadLevel("Lose");
            }
        }
    }
}
