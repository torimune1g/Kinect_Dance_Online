using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Notesを生成する場所を初音ミクに追従させる
public class ObjPosMiku : MonoBehaviour
{
    public GameObject director;
    private manage1 m1; //Photonサーバーに接続するスクリプト

    private bool isCalledOnce; //一回だけ呼ぶ用
    public GameObject miku; //初音ミクを格納
    private Vector3 mikuPos; //初音ミクの座標

    // Start is called before the first frame update
    void Start()
    {
        m1 = director.GetComponent<manage1>();
    }

    // Update is called once per frame
    void Update()
    {
        //最初にPhotonのルームに入った場合、初音ミクをmikuに格納
        if (m1.totalMembers == 1)
        {
            if (isCalledOnce == false)
           {
                miku = GameObject.FindGameObjectWithTag("miku");
                isCalledOnce = true;
           }
        }
        //初音ミクの座標を取得し、自身の座標に代入
        mikuPos = miku.transform.position;
        this.transform.position = mikuPos;
    }
}
