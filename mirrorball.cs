using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ミラーボール回転
public class mirrorball : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //y軸回転
        transform.Rotate(new Vector3(0, 2, 0) * Time.deltaTime, Space.World);
    }
}
