using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
//using Unity.Plastic.Newtonsoft.Json;
//using Newtonsoft.Json;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    [SerializeField] public static Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;


    void Start()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
        
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        //PlayerControls.canWalk = false;
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        flyingBuilding = Instantiate(buildingPrefab);

    }


    
    private void Update()
    {
        bool block = false;
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.mouseScrollDelta.y < 0)
            {
                flyingBuilding.transform.Rotate(0, 90f, 0, Space.Self);
                flyingBuilding.dir += 90f;
                //flyingBuilding.Size = new Vector2Int(1, 2); IMPORTANT!!!!!
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                flyingBuilding.transform.Rotate(0, -90f, 0, Space.Self);
                flyingBuilding.dir += 270f;
            }
                

            if (groundPlane.Raycast(ray, out float position))
            {
                block = true;
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;
                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;
                if ((x == 7 && y <= 3) || (x == 8 && y <= 3) || (x == 9 && y <= 3) || (x == 4 && y <= 1) || (x == 5 && y <= 1) || (x == 6 && y <= 1))  available = false;
                if (available && IsPlaceTaken(x, y)) available = false;
                if (available && IsAround(x, y, flyingBuilding.transform.rotation.y)) available = false;

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTranparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                    block = false;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Destroy(flyingBuilding.gameObject);
                    block = false;
                }
            }
        }
        if (Input.GetKey("r") && !block)
        {
            bool oneTime = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Machine" && oneTime)
                {
                    Building checkCost = hit.collider.gameObject.GetComponent<Building>();
                    Destroy(hit.collider.gameObject);
                    CoinsScript.coins += (float)(checkCost.cost * 0.5);
                    LoadDataCost -= checkCost.cost;
                    PlayerPrefs.SetFloat("LoadData", LoadDataCost);
                    UpgradeScript.Machines -= 1;
                    oneTime = false;
                }
            }
        }
    }



    public static void destroyBuild(Building flyu)
    {
        Destroy(flyu.gameObject);
    }

    private bool IsAround(int placeX, int placeY, float rotation) 
    {
        if (placeX == GridSize.x - 1 && placeY == GridSize.y - 1)
        {
            if (grid[placeX - 1, placeY] || grid[placeX, placeY - 1]) return true;
        }
        else if (placeX == GridSize.x - 1 && placeY == 0)
        {
            if (grid[placeX - 1, placeY] || grid[placeX, placeY + 1]) return true;
        }
        else if (placeX == 0 && placeY == GridSize.y - 1)
        {
            if (grid[placeX + 1, placeY] || grid[placeX, placeY - 1]) return true;
        }
        else if (placeX == 0 && placeY == 0)
        {
            if (grid[placeX + 1, placeY] || grid[placeX, placeY + 1]) return true;
        }
        else if (placeX == GridSize.x - 1)
        {
            if (grid[placeX - 1, placeY] || grid[placeX, placeY + 1] || grid[placeX, placeY - 1]) return true;
        }
        else if (placeX == 0)
        {
            if (grid[placeX + 1, placeY] || grid[placeX, placeY + 1] || grid[placeX, placeY - 1]) return true;
        }
        else if (placeY == 0)
        {
            if (grid[placeX + 1, placeY] || grid[placeX, placeY + 1] || grid[placeX - 1, placeY]) return true;

        }
        else if (placeY == GridSize.y - 1)
        {
            if (grid[placeX - 1, placeY] || grid[placeX + 1, placeY] || grid[placeX, placeY - 1]) return true;
        }
        else
        {
            if (grid[placeX - 1, placeY] || grid[placeX + 1, placeY] || grid[placeX, placeY - 1] || grid[placeX, placeY + 1]) return true; /*IMPORTANT TO BACKUP!!!!!!*/
        }
        return false;
    }

    private bool IsPlaceTaken(int placeX, int placeY) 
    {

        if (grid[placeX, placeY] != null) return true;
        for (int i = 0; i < flyingBuilding.Size.x; i++)
        {
            for (int j = 0; j < flyingBuilding.Size.y; j++)
            {
                if (grid[placeX + i, placeY + j] != null) return true;
            }
        }
        return false;
    }

    int LoadDataCost = 0;
    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        //grid[placeX, placeY] = flyingBuilding;
        for (int i = 0; i < flyingBuilding.Size.x; i++)
        {
            for (int j = 0; j < flyingBuilding.Size.y; j++)
            {
                grid[placeX + i, placeY + j] = flyingBuilding;
            }
        }
        flyingBuilding.SetNormal();
        LoadDataCost += flyingBuilding.cost;
        Debug.Log(LoadDataCost);
        PlayerPrefs.SetFloat("LoadData", LoadDataCost);
        flyingBuilding = null;


        //PlayerControls.canWalk = true;
    }

}
