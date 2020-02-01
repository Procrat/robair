using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float health;

    private SpriteRenderer sprRenderer;

    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        health = 1;
    }

    private void Update()
    {
        sprRenderer.material.SetFloat("_Percent", health);
    }

    public void subtractHealth(float sub)
    {
        health -= sub;
    }
}
