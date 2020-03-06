using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//パーティクルを回転させない
public class ColorParticle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
