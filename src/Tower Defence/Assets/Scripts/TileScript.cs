﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; set; }
    public bool IsWalkable { get; set; }

    private Color32 redColor = new Color32(255, 118, 118, 255);
    private Color32 greenColor = new Color32(96, 255, 92, 255);

    private SpriteRenderer spriteRenderer;
    public Tower Tower { get; set; }

    public bool IsEmpty { get; set; }

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(bool isWalkable, Point point, Vector3 worldPoint)
    {
        IsWalkable = isWalkable;
        IsEmpty = true;
        GridPosition = point;
        transform.position = worldPoint;

        FloorManager.Instance.TileScripts.Add(point, this);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Clicked");

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GameManager.Instance.ClickedButton != null)
            {
                if (IsEmpty && !IsWalkable)
                {
                    ColorTile(greenColor);

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower();
                    }
                }
                else
                {
                    ColorTile(redColor);
                    if (Input.GetMouseButtonDown(0))
                        SoundManager.Instance.PlayAudio("TileRed");
                }
            }
            else if (Input.GetMouseButtonDown(0) && transform.childCount > 0)
            {
                Tower tower = transform.GetChild(0).GetComponent<Tower>();
                tower.Toggle();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        GameObject towerObject = Instantiate(GameManager.Instance.ClickedButton.Button, transform.position, Quaternion.identity);
        //tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -1);
        towerObject.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        towerObject.transform.SetParent(transform);
        Tower = towerObject.GetComponent<Tower>();
        Tower.Price = GameManager.Instance.ClickedButton.Price;

        IsEmpty = false;
        //GetComponent<BoxCollider2D>().enabled = false;

        GameManager.Instance.BuyTower();
    }

    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
