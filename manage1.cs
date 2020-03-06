using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//Photonサーバ接続とゲーム開始
public class manage1 : MonoBehaviour
{
    public int totalMembers; //何番目にPhotonのルームに入ったか

    private GameObject mikuClone; //一人目がログインしたときに生成される初音ミク
    private GameObject rinClone; //二人目がログインしたときに生成される鏡音リン
    private GameObject camMiku; //初音ミクの前に生成されるカメラ
    private GameObject camRin; //鏡音リンの前に生成されるカメラ
    public ObjInstance[] mikuObjInstance; //初音ミクの周りでNotesを生成するスクリプト
    public ObjInstance[] rinObjInstance; //鏡音リンの周りでNotesを生成するスクリプト

    public GameObject miku; //ログイン人数確認用にシーン中の初音ミクを格納
    public GameObject rin; //ログイン人数確認用にシーン中の鏡音リンを格納

    public MikuStart mikuStart; //初音ミクの右手がコライダに当たったか判定するスクリプト
    public RinStart rinStart; //鏡音リンの右手がコライダに当たったか判定するスクリプト

    public PlayableDirector tl;

    private double startTime; //一人目がPhotonのルームに入室した時間
    private double elapsedTime; //startTimeからの経過時間
    private float timeOut = 1; //1秒おきに二人ログインしているか確認
    private bool isStart; //二人の右手がコライダに当たったか
    private bool isCalledOnce; //一回だけ呼ぶ用
    private double StartTimeInUnity = 1000000; 



    public ExitGames.Client.Photon.Hashtable properties; //ルームのプロパティ

    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        PhotonNetwork.ConnectUsingSettings("v0");
    }

    void OnJoinedLobby()
    {
        //ランダムにルーム入室
        PhotonNetwork.JoinRandomRoom();
    }

    void OnJoinedRoom()
    {
        Debug.Log("ルームへ入室しました");
        
        //何番目の入室者か記録
        totalMembers = PhotonNetwork.countOfPlayers;

        //最初に入室した時間をStartTimeという名前でルームのプロパティに設定
        properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("StartTime", PhotonNetwork.time);
        PhotonNetwork.room.SetCustomProperties(properties);

        //登録したPhoton時間をstartTimeに格納
        startTime = (double)PhotonNetwork.room.CustomProperties["StartTime"];

        //一人目の入室時
        if (totalMembers == 1)
        {
            //挙動を同期する初音ミクを生成
            mikuClone = PhotonNetwork.Instantiate("miku", new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, 0), 0);

            //初音ミクの正面にカメラを生成
            camMiku = (GameObject)Resources.Load("OVRCameraRigForMiku");
            GameObject cam = Instantiate(camMiku, new Vector3(-1, 1, 1.3f), Quaternion.Euler(10, 180, 0));

            //鏡音リンの周りでNotesが生成される場所をすべて削除
            for (int i = 0; i < rinObjInstance.Length; i++)
            {
                Destroy(rinObjInstance[i]);
            }

            //初音ミクのスコア要素になる変数をルームのプロパティに追加
            properties.Add("m_ScoreForLink", 0);
            properties.Add("m_earlyForLink", 0);
            properties.Add("m_perfectForLink", 0);
            properties.Add("m_slowForLink", 0);
            properties.Add("m_damageForLink", 0);
            PhotonNetwork.room.SetCustomProperties(properties);
        }

        //二人目の入室時
        if (totalMembers == 2)
        {
            //挙動を同期する鏡音リンを生成
            rinClone = PhotonNetwork.Instantiate("rin", new Vector3(1, 0, 0), Quaternion.Euler(0, 0, 0), 0);

            //鏡音リンの正面にカメラを生成
            camRin = (GameObject)Resources.Load("OVRCameraRigForRin");
            GameObject cam = Instantiate(camRin, new Vector3(1, 1, 1.3f), Quaternion.Euler(10, 180, 0));

            //初音ミクの周りでNotesが生成される場所をすべて削除
            for (int i = 0; i < mikuObjInstance.Length; i++)
            {
                Destroy(mikuObjInstance[i]);
            }

            //鏡音リンのスコア要素になる変数をルームのプロパティに追加
            properties.Add("r_ScoreForLink", 0);
            properties.Add("r_earlyForLink", 0);
            properties.Add("r_perfectForLink", 0);
            properties.Add("r_slowForLink", 0);
            properties.Add("r_damageForLink", 0);
            PhotonNetwork.room.SetCustomProperties(properties);
        }
    }

    //OnJoinRoom()が失敗した時、新たにルームを作成
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //startTimeからの経過時間を計算
        double elapsedTime = PhotonNetwork.time - startTime;

        //1秒に1回二人ログインしているか確認
        timeOut -= Time.time;
        if (timeOut < 0)
        {
            miku = GameObject.FindGameObjectWithTag("miku");
            rin = GameObject.FindGameObjectWithTag("rin");
            timeOut = 1;
        }

        //二人ログインし、かつ右手がコライダに当たったら、0.5f秒後にタイムライン開始
        if (!isCalledOnce)
        {
            if ((miku.tag == "miku" && rin.tag == "rin") && (mikuStart.isHit == true && rinStart.isHit == true))
            {
                isStart = true;
                StartTimeInUnity = elapsedTime + 0.5f;
                isCalledOnce = true;
            }
        }
        if (isStart == true && StartTimeInUnity < elapsedTime)
        {
            tl.Play();
        }
    }
}
