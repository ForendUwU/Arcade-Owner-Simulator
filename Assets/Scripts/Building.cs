using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Building : MonoBehaviour
{

    public Building() { }

    public Renderer MainRenderer;
    public Vector2Int Size = new Vector2Int(1,1);

    public float nonStaticX;
    public float nonStaticZ;
    public float dir;

    public bool isTaken = false;
    NavMeshObstacle pro;

    public int cost;
    public float earn;

    public AudioClip sound;
    private static AudioSource soundPlay;

    private void Start()
    {
        pro = this.GetComponent<NavMeshObstacle>();
    }


    public void SetTranparent(bool available)
    {
        pro = this.GetComponent<NavMeshObstacle>();
        
        pro.enabled = false;
        if (available)
        {
            MainRenderer.material.color = Color.green;

        }
        else
        {
            MainRenderer.material.color = Color.red;
        }
        
    }

    public static bool vis;
    IEnumerator showTextCoroutine()
    {
        yield return new WaitForSeconds(2);

        vis = false;

    }
    void OnGUI()
    {
        if (vis)
        {
            GUI.skin.label.fontSize = 70;
            GUI.color = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300), "Not Enought Money");
            StartCoroutine(showTextCoroutine());
        }

    }
    public void SetNormal()
    {
        if (CoinsScript.coins - cost < 0)
        {
            vis = true;
            BuildingGrid.destroyBuild(this);
            
        }
        else
        {
            CoinsScript.coins = CoinsScript.coins - cost;
            CoinsScript.SaveCoins();
            UpgradeScript.Machines++;

            pro = this.GetComponent<NavMeshObstacle>();
            pro.enabled = true;
            MainRenderer.material.color = Color.white;
            isTaken = false;
            if (dir >= 360f)
                dir %= 360f;
            switch (dir)
            {
                case 0:
                    nonStaticX = transform.position.x;
                    nonStaticZ = transform.position.z - 0.5f;
                    break;
                case 90f:
                    nonStaticX = transform.position.x - 0.5f;
                    nonStaticZ = transform.position.z;
                    break;
                case 180f:
                    nonStaticX = transform.position.x;
                    nonStaticZ = transform.position.z + 0.5f;
                    break;
                case 270f:
                    nonStaticX = transform.position.x + 0.5f;
                    nonStaticZ = transform.position.z;
                    break;
            }
        }

    }

    public void earnMoney() 
    {
        soundPlay = Instantiate(gameObject.AddComponent<AudioSource>());
        soundPlay.clip = sound;
        soundPlay.playOnAwake = false;
        soundPlay.PlayOneShot(sound);
        DestroyObject(soundPlay, 1f);

        CoinsScript.coins += earn;
    }



    public void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0f, 1f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
