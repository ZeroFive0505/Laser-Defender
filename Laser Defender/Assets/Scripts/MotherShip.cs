using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour {

    public float health = 2000;
    public GameObject EnemyLaser;
    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    private Rigidbody2D rb3;
    public float laserSpeed;
    public float shotPerSeconds = 0.8f;
    private ScoreText scoretext;
    public int Score = 5000;
    public AudioClip shoot;
    public AudioClip explosion;
    private LevelManager levelManager;
    private ScoreText scoreText;

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        scoreText = GameObject.FindObjectOfType<ScoreText>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider);
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if (health <= 0)
            {
                scoreText.Score(Score);
                AudioSource.PlayClipAtPoint(explosion, transform.position, 7f);
                Destroy(gameObject);
                levelManager.NextLevel();
            }
            //Debug.Log("Hit by a laser");
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * shotPerSeconds;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        //Vector3 StartPosition = transform.position + new Vector3(0f, -1f, 0f);
        GameObject Laser = Instantiate(EnemyLaser, transform.position, Quaternion.identity);
        GameObject Laser2 = Instantiate(EnemyLaser, transform.position - new Vector3(-3f, 0, 0f), Quaternion.identity);
        GameObject Laser3 = Instantiate(EnemyLaser, transform.position - new Vector3(3f, 0, 0f), Quaternion.identity);
        AudioSource.PlayClipAtPoint(shoot, Laser.transform.position, 5f);
        rb1 = Laser.GetComponent<Rigidbody2D>();
        rb2 = Laser2.GetComponent<Rigidbody2D>();
        rb3 = Laser3.GetComponent<Rigidbody2D>();

        rb1.velocity = new Vector2(Random.Range(-2f, 2f), laserSpeed);
        rb2.velocity = new Vector2(Random.Range(-2f, 2f), laserSpeed);
        rb3.velocity = new Vector2(Random.Range(-2f, 2f), laserSpeed);
    }

}