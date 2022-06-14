using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    public Material LedMaterial;
    public Light Light;
    private Color aColor;

    private void Start()
    {
        aColor = new Color(255, 192, 203);
    }

    // Update is called once per frame
    void Update()
    {

        aColor = new Vector4(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), 1);
        LedMaterial.color = Color.Lerp(Color.red, Color.green, Mathf.Abs(Mathf.Sin(Time.time / 5)));
        Light.color = Color.Lerp(Color.red, Color.yellow, Mathf.Abs(Mathf.Sin(Time.time / 5)));

        
    }
}
