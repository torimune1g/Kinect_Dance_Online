using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1(秒)後に自身のパーティクルを削除
public class destroyEffect : MonoBehaviour
{
    private float timeOut = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOut += Time.deltaTime;
        if(timeOut > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
