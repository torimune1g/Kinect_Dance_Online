using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//透明度を上げながら1(秒)後にタッチ判定テキストを削除
public class TextDestroy : MonoBehaviour
{
    private float timeOut = 1;
    private TextMesh tm;

    Color alpha = new Color(0, 0, 0, 0.005f);

    // Start is called before the first frame update
    void Start()
    {
        tm = this.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tm.color.a >= 0)
        { 
            tm.color -= alpha;
        }
        timeOut -= Time.deltaTime;
        if (timeOut < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
