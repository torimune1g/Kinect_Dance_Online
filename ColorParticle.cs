using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//パーティクルを回転させない
public class ColorParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
