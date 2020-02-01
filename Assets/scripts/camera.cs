using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    // Update is called once per frame
    void Update()
    {
        var lerp = Vector2.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
    }
}
