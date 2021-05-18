using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behavior : MonoBehaviour {

    public float health = 350;
    public GameObject EnemyLaser;
    private Rigidbody2D rb;
    public float laserSpeed;
    public float shotPerSeconds = 0.7f;
    private ScoreText scoretext;
    public int Score = 350;
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
                levelManager.Check();
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
        AudioSource.PlayClipAtPoint(shoot, Laser.transform.position, 5f);
        rb = Laser.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, laserSpeed);
    }

}
