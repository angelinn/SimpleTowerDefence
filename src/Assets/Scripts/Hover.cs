﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    public void Activate(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Deactivate()
    {
        GameManager.Instance.ClickedButton = null;
        spriteRenderer.sprite = null;
    }
}