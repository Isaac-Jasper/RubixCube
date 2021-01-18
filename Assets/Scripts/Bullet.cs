using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;

    public int damage;
    public float speed = 100;
    public float lifeSpan;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Life());
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Damage(damage, this.name);
            Destroy(gameObject);
        }
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeSpan * Time.deltaTime);
        Destroy(gameObject);
    }
}
