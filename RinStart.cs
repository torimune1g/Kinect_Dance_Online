using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鏡音リン側で開始の合図を送信
public class RinStart : MonoBehaviour
{
    public bool isHit;

    //右手を上げてコライダに当たったらisHitをtrueに
    //Photonサーバーに接続するスクリプトmanage1がこのisHitの値を読み取る
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "rinCol")
        {
            isHit = true;
        }
    }
}
