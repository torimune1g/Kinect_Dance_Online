using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Notesのランダム生成
public class ObjInstance : MonoBehaviour
{
    private int objTotal = 9; //登録したNotesの合計+1
    private int objNum = 0; //乱数格納
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Notes生成関数、Timelineで任意に呼び出し
    public void MakeObj()
    {
        //1~8までの乱数を格納
        objNum = Random.Range(1, objTotal);

        //Notesのランダム生成
        switch (objNum)
        {
            case 1:
                GameObject cone = PhotonNetwork.Instantiate("cone", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 2:
                GameObject cube = PhotonNetwork.Instantiate("cube", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 3:
                GameObject sankakukei_v = PhotonNetwork.Instantiate("sankakukei_v", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 4:
                GameObject seihoukei = PhotonNetwork.Instantiate("seihoukei", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 5:
                GameObject seihoukei_v = PhotonNetwork.Instantiate("seihoukei_v", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 6:
                GameObject seishimentai_v = PhotonNetwork.Instantiate("seishimentai_v", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 7:
                GameObject sphere = PhotonNetwork.Instantiate("sphere", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            case 8:
                GameObject cube_v = PhotonNetwork.Instantiate("cube_v", this.gameObject.transform.position, Quaternion.identity, 0);
                break;
            //case 9:
            //    GameObject batten = PhotonNetwork.Instantiate("batten", this.gameObject.transform.position, Quaternion.identity, 0);
            //    break;
        }
    }
}
