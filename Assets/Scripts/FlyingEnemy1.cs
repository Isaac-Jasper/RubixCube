using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy1 : MonoBehaviour
{
    public ParticleSystem Death;
    public ParticleSystem ChargeShot;
    public Rigidbody rb;
    private GameObject player;
    public GameObject shooter;
    public GameObject Bullet;

    private int health = 3;
    public int attackDelay = 1000;
    public float moveSpeed = 10;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float bobtime = 200f;
    public float rotationSpeed = 100f;
    private float spawnHeight;
    public Vector3 velocity;

    private bool jumping;
    private bool agro;
    private bool shooting = false;

    void Start()
    {
        StartCoroutine(bob());
        player = GameObject.FindGameObjectWithTag("Player");
        spawnHeight = transform.position.y;
    }


    IEnumerator bob()
    {
        float startingY;
        while (true)
        {
            startingY = transform.position.y;

            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            
            jumping = true;
            yield return null;
            yield return new WaitUntil(() => transform.position.y <= startingY);
            velocity.y = -2f;
        }
    }

    void Update()
    {
        int rand = Random.Range(0, attackDelay);
        if (jumping)
        {
            velocity.y += gravity * Time.deltaTime;
            rb.velocity = new Vector3(rb.velocity.x, velocity.y, rb.velocity.z);
        }

        if (agro)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(player.transform.position - transform.position), //look at player
                rotationSpeed * Time.deltaTime);

            if(!shooting)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, spawnHeight, player.transform.position.z),  moveSpeed * Time.deltaTime);

            if (rand == 0 && !shooting)
                StartCoroutine(shoot());
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            agro = true;
        }
    }

    public void Shot(int damage)
    {
        if (!agro)
        {
            StartCoroutine(shoot());
            agro = true;
        }

        health -= damage;

        if (health == 0)
        {
            Instantiate(Death, transform.position, Death.transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator shoot()
    {
        shooting = true;

        ChargeShot.Play();
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 5; i++)
        {
            transform.position += transform.forward * Time.deltaTime * 10;
            Instantiate(Bullet, shooter.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        shooting = false;
    }
}
