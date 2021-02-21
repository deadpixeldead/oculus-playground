using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("OnTriggerEnter " + col.gameObject.name);
    //    Debug.Log("OnTriggerEnter " + col.gameObject.tag);

    //    //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
    //    if (col.gameObject.tag == "Enemy")
    //    {
    //        //Destroy(col.gameObject);
    //        //add an explosion or something
    //        //destroy the projectile that just caused the trigger collision
    //        //Destroy(gameObject);
    //    }
    //}
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter " + collision.gameObject.name);
        Debug.Log("OnCollisionEnter " + collision.gameObject.tag);

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (collision.gameObject.tag == "Enemy")
        {
            //Destroy(col.gameObject);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
    }
}
