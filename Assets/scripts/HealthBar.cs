using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float health;
    // [SerializeField][Range(0, 1)] private float width;

    private SpriteRenderer sprRenderer;
    private float height;

    private void Start()
    {
        // width = this.GetComponent<SpriteRenderer>().bounds.size.x;
        sprRenderer = GetComponent<SpriteRenderer>();
        height = sprRenderer.sprite.rect.height;

        health = 1;
    }

    private void Update()
    {
        sprRenderer.material.SetFloat("_Percent", health);
        sprRenderer.material.SetFloat("y_pos", 0.1f);
        sprRenderer.material.SetFloat("_height", height);
        Debug.Log(sprRenderer.material.GetFloat("_height"));

    }

    public void subtractHealth(float sub)
    {
        health -= sub;
    }
}
