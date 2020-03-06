using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//生成されたNotesの挙動とタッチ判定
public class objMovement : MonoBehaviour
{
    //このNotesのサイズ
    private float scaleX;
    private float scaleY;
    private float scaleZ;

    private bool isMax; //このNotesのサイズが最大になったかどうか

    private float speed1 = 5; //このNotesが小さいサイズだった場合に拡大するスピード
    private float speed2 = 16; //このNotesが小さいサイズだった場合に縮小するスピード

    private float speed3 = 0.08f; //このNotesが大きいサイズだった場合に拡大するスピード
    private float speed4 = 0.2f; //このNotesが大きいサイズだった場合に縮小するスピード

    public Material[] mt; //マテリアル群、黄・青・赤がある
    private Renderer rd;

    public GameObject scoreManage;
    private ScoreManage sm; //スコア記録用スクリプト

    //タッチ判定のテキスト
    public GameObject earlyText;
    public GameObject perfectText;
    public GameObject slowText;

    //二人の正面のNotes生成場所
    public GameObject m_front;
    public GameObject r_front;

    public GameObject front;

    private PhotonView m_photonView;

    public enum judge //Notesのタッチ判定
    {
        early,    
        perfect,  
        slow 
    }
    public judge type;

    private Vector3 kurukuru;

    //public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        //Notesはモデリングしたものとそうでないものがあるため、デフォルトサイズが二種類ある
        //サイズ初期化
        if (this.gameObject.tag == "dekai")
        {
            gameObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(2, 2, 2);
        }

        //マテリアル初期化
        rd = this.gameObject.GetComponent<Renderer>();
        rd.material = mt[0];

        //スコア記録スクリプトを取得
        scoreManage = GameObject.FindGameObjectWithTag("scoremanage");
        sm = scoreManage.GetComponent<ScoreManage>();

        //二人の正面のNotes生成場所を取得
        m_front = GameObject.FindGameObjectWithTag("m_front");
        r_front = GameObject.FindGameObjectWithTag("r_front");

        //Instantiateされた場所からそれぞれの正面のNotes生成場所までの距離を取得
        float mDis = Vector3.Distance(this.transform.position, m_front.transform.position);
        float rDis = Vector3.Distance(this.transform.position, r_front.transform.position);

        //距離が短い方のキャラクターをこのNotesの帰属先とする
        if (mDis > rDis)
        {
            front = r_front;
        }
        else
        {
            front = m_front;
        }

        //ランダム回転の初期設定
        kurukuru = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));

        m_photonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //ランダム回転
        transform.Rotate(kurukuru * Time.deltaTime);

        //それぞれのキャラクターの正面に向かってこのNotesが移動
        transform.DOLocalMove(front.transform.position, 20f);

        //自身のサイズを取得
        scaleX = gameObject.transform.localScale.x;
        scaleY = gameObject.transform.localScale.y;
        scaleZ = gameObject.transform.localScale.z;

        //自身が大きいサイズのNotesの場合
        if (this.gameObject.tag == "dekai")
        {
            //サイズが0.15f以上になっていない場合
            if (isMax == false)
            {
                if (scaleX < 0.15f)
                {
                    //オブジェクトを拡大
                    gameObject.transform.localScale = new Vector3(scaleX + Time.deltaTime * speed3, scaleY + Time.deltaTime * speed3, scaleZ + Time.deltaTime * speed3);

                    //サイズが0.12fより小さい場合、earlyと判定し黄色に
                    type = judge.early;
                    rd.material = mt[0];
                    
                    //サイズが0.12fより大きく0.15fより小さい場合、perfectと判定し青色に
                    if (scaleX > 0.12f && scaleX < 0.15f)
                    {
                        type = judge.perfect;
                        rd.material = mt[1];
                    }
                }
                else
                {
                    //サイズが0.15f以上になっている場合、isMaxをtrueに
                    isMax = true;
                }
            }

            //サイズが一度0.15fより大きくなった場合
            if (isMax == true)
            {
                //オブジェクトを縮小
                gameObject.transform.localScale = new Vector3(scaleX - Time.deltaTime * speed4, scaleY - Time.deltaTime * speed4, scaleZ - Time.deltaTime * speed4);

                //slowと判定し、赤色に
                type = judge.slow;
                rd.material = mt[2];
            }
        }
        //自身が小さいサイズのNotesの場合
        else
        {
            if (isMax == false)
            {
                if (scaleX < 10f)
                {
                    type = judge.early;
                    rd.material = mt[0];
                    gameObject.transform.localScale = new Vector3(scaleX + Time.deltaTime * speed1, scaleY + Time.deltaTime * speed1, scaleZ + Time.deltaTime * speed1);
                    if (scaleX > 7f && scaleX < 10f)
                    {
                        type = judge.perfect;
                        rd.material = mt[1];
                    }
                }
                else
                {
                    isMax = true;
                }
            }

            if (isMax == true)
            {
                type = judge.slow;
                rd.material = mt[2];
                gameObject.transform.localScale = new Vector3(scaleX - Time.deltaTime * speed2, scaleY - Time.deltaTime * speed2, scaleZ - Time.deltaTime * speed2);
            }
        }
        
        //自身のサイズが0以下になった場合、削除
        if (gameObject.transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //タッチ判定
    void OnTriggerEnter(Collider col)
    {
        //自身側で生成したNotesのみ判定
        if (m_photonView.isMine == true)
        {
            //初音ミクの四肢に衝突した場合
            if (col.gameObject.tag == "mikuCol")
            {
                //パーティクル生成
                GameObject hoge = PhotonNetwork.Instantiate("Heart 1", this.gameObject.transform.position, Quaternion.identity, 0);

                //それぞれの場合でスコア更新、判定テキストを生成
                switch (type)
                {
                    case judge.early:
                        sm.m_early++;
                        Instantiate(earlyText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                    case judge.perfect:
                        sm.m_perfect++;
                        Instantiate(perfectText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                    case judge.slow:
                        sm.m_slow++;
                        Instantiate(slowText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                }

                //衝突後このNotesを削除
                Destroy(this.gameObject);
            }
            //鏡音リンの四肢に衝突した場合
            if (col.gameObject.tag == "rinCol")
            {
                GameObject hoge = PhotonNetwork.Instantiate("Heart 1", this.gameObject.transform.position, Quaternion.identity, 0);
                switch (type)
                {
                    case judge.early:
                        sm.r_early++;
                        Instantiate(earlyText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                    case judge.perfect:
                        sm.r_perfect++;
                        Instantiate(perfectText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                    case judge.slow:
                        sm.r_slow++;
                        Instantiate(slowText, this.transform.position, Quaternion.Euler(0, 180, 0));
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
