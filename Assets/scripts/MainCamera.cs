using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Vector2 lowerLeftCorner;

    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        var lerp = Vector2.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        Vector2 halfScreenDiagonal = camera.ViewportToWorldPoint (new Vector2 (0.5f, 0.5f))
                                     - camera.ViewportToWorldPoint(new Vector2(0, 0));
        var clamped = Vector2.Max(lerp, lowerLeftCorner + halfScreenDiagonal);
        transform.position = new Vector3(clamped.x, clamped.y, transform.position.z);
    }
}
