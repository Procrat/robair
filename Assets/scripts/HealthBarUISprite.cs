using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUISprite : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float m_health;

    private SpriteRenderer sprRenderer;

    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();

        m_health = 1;
    }

    private void Update()
    {
        sprRenderer.material.SetFloat("_Percent", m_health);
    }
}
