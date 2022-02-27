using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool buildable;
    public GameObject model;
    public Building building;

    private Color baseColor;

    public void Start()
    {
        if (!buildable)
        {
            Color uneditableColor = new Color(GetComponent<Renderer>().material.color.r - 0.1f, GetComponent<Renderer>().material.color.g - 0.1f, GetComponent<Renderer>().material.color.b - 0.1f);
            GetComponent<Renderer>().material.color = uneditableColor;
            baseColor = uneditableColor;
        }
    }

    public void OnMouseEnter()
    {
        baseColor = GetComponent<Renderer>().material.color;
        if (buildable)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = baseColor;
    }

    public void OnMouseDown()
    {
        model.transform.parent.GetComponent<TileManager>().Build(this, model.transform.parent.GetComponent<TileManager>().availableBuildings[0]);
    }

}
