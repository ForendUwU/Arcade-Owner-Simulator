using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject[] NPCs;

    BuildingGrid bg;
    public GameObject builgrid;
    Building[,] mas;
    float rand;

    void Start()
    {
        //bg = builgrid.GetComponent<BuildingGrid>();
        //bg = FindObjectOfType<BuildingGrid>();
        //mas = bg.grid;
        mas = BuildingGrid.grid;
        rand = Random.Range(4, 6)-Mathf.Min((float)(0.1f * UpgradeScript.Machines), 4.0f);
    }

    private float secundomer;
    void FixedUpdate()
    {
        
        if (secundomer < rand) secundomer += Time.fixedDeltaTime;
        if (secundomer >= rand)
        {
            spawn();
            secundomer = 0;
            rand = Random.Range(4, 6) - Mathf.Min((float)(0.1f * UpgradeScript.Machines), 4.0f);
        }
    }

    void spawn()
    {
        foreach (Building item in mas)
        {
            if (item != null && !item.isTaken)
            {
                
                Instantiate(NPCs[Random.Range(0, NPCs.Length)]);
                return;
            }
        }
        


    }
}
