using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Notesを生成する場所を鏡音リンに追従させる
public class ObjPosRin : MonoBehaviour
{
    public GameObject director;
    private manage1 m1; //Photonサーバーに接続するスクリプト

    private bool isCalledOnce; //一回だけ呼ぶ用
    public GameObject rin; //初音ミクを格納
    private Vector3 rinPos; //初音ミクの座標

    // Start is called before the first frame update
    void Start()
    {
        m1 = director.GetComponent<manage1>();
    }

    // Update is called once per frame
    void Update()
    {
        //二番目にPhotonのルームに入った場合、鏡音リンをrinに格納
        if (m1.totalMembers == 2)
        {
            if (isCalledOnce == false)
            {
                rin = GameObject.FindGameObjectWithTag("rin");
                isCalledOnce = true;
            }
        }
        //鏡音リンの座標を取得し、自身の座標に代入
        rinPos = rin.transform.position;
        this.transform.position = rinPos;
    }
}
