using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other) {
     // Using the tag method.
     if (other.tag == "Player") {
         Destroy(this.gameObject);
     }
    }
}
