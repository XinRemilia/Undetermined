using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D thisRig;
    private void Start()
    {
        thisRig.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Map"))
        {
            Destroy(gameObject);
        }
    }
}




