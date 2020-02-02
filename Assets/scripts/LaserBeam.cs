using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private float laserDamage = 0.15f;
    void Awake()
    {
        StartCoroutine(LaserLife());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Using the tag method.
        if (other.tag == "Player")
        {
            other.SendMessage("takeDamage", laserDamage);
            Destroy(this.gameObject);
        }
    }

    IEnumerator LaserLife()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
