using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スコア記録
public class ScoreManage : Photon.MonoBehaviour
{
    public int m_early = 0; //ミクのタッチが早かった回数
    public int m_perfect = 0; //ミクのタッチが良かった回数
    public int m_slow = 0; //ミクのタッチが遅かった回数
    public int m_damage = 0; //ミクのミスタッチ回数(プレイしにくくなると判断し、途中で機能削除)
    public int m_Score = 0; //ミクのトータルスコア

    public int r_early = 0; //リンのタッチが早かった回数
    public int r_perfect = 0; //リンのタッチが良かった回数
    public int r_slow = 0; //リンのタッチが遅かった回数
    public int r_damage = 0; //リンのミスタッチ回数(機能削除)
    public int r_Score = 0; //リンのトータルスコア

    //上の変数を格納し、Photonのルームプロパティに書き込む用
    public int m_earlyForLink = 0;
    public int m_perfectForLink = 0;
    public int m_slowForLink = 0;
    public int m_damageForLink = 0;
    public int m_ScoreForLink = 0;

    public int r_earlyForLink = 0;
    public int r_perfectForLink = 0;
    public int r_slowForLink = 0;
    public int r_damageForLink = 0;
    public int r_ScoreForLink = 0;

    //Photonのルームプロパティに書き込まれた上の変数を格納する用
    static public int m_earlyForResult = 0;
    static public int m_perfectForResult = 0;
    static public int m_slowForResult = 0;
    static public int m_damageForResult = 0;
    static public int m_ScoreForResult = 0;

    static public int r_earlyForResult = 0;
    static public int r_perfectForResult = 0;
    static public int r_slowForResult = 0;
    static public int r_damageForResult = 0;
    static public int r_ScoreForResult = 0;

    public TextMesh m_text; //メインシーン中ミクのスコアテキスト
    public TextMesh r_text; //メインシーン中リンのスコアテキスト

    public manage1 manage1; //Photon接続関連のスクリプト
    private int clientNumber; //何番目のPhotonルーム入室者か

    public ChangeAnimationState changeAnimationState; //Timeline上で観客のアニメーションステートを変化させるためのスクリプト

    private bool isCalledOnce; //一回だけ呼ぶ用

    private PhotonView pv; //自身のPhotonViewコンポーネント

    //0.5(秒)間隔でPhotonルームプロパテ時に書き込む
    private float timeOut1 = 0.5f;
    private float timeOut2 = 0.5f;
    private float timeOut3 = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
        manage1 = GameObject.FindGameObjectWithTag("director").GetComponent<manage1>();
        changeAnimationState = GameObject.FindGameObjectWithTag("changeAnimationState").GetComponent<ChangeAnimationState>();
    }

    // Update is called once per frame
    void Update()
    {
        //何番目のPhotonルーム入室者か記録
        clientNumber = manage1.totalMembers;
        
        //二人ログインしてtebyoshiアニメーションが開始されたら、メインシーン中の二人のスコアを表示
        if (changeAnimationState.type == ChangeAnimationState.ANIMATION_TYPE.tebyoshi)
        {
            if (isCalledOnce == false)
            {
                m_text = GameObject.FindGameObjectWithTag("m_Text").GetComponent<TextMesh>();
                r_text = GameObject.FindGameObjectWithTag("r_Text").GetComponent<TextMesh>();
                isCalledOnce = true;
            }
        }

        //スコア計算
        m_Score = m_early * 50 + m_perfect * 100 + m_slow * 25 - m_damage * 30;
        r_Score = r_early * 50 + r_perfect * 100 + r_slow * 25 - r_damage * 30;

        //0.5(秒)に一回スコア要素をPhotonルームプロパティから読み取る
        timeOut1 -= Time.deltaTime;
        if (timeOut1 < 0)
        {
            m_ScoreForLink = (int)PhotonNetwork.room.CustomProperties["m_ScoreForLink"];
            m_earlyForLink = (int)PhotonNetwork.room.CustomProperties["m_earlyForLink"];
            m_perfectForLink = (int)PhotonNetwork.room.CustomProperties["m_perfectForLink"];
            m_slowForLink = (int)PhotonNetwork.room.CustomProperties["m_slowForLink"];
            m_damageForLink = (int)PhotonNetwork.room.CustomProperties["m_damageForLink"];

            r_ScoreForLink = (int)PhotonNetwork.room.CustomProperties["r_ScoreForLink"];
            r_earlyForLink = (int)PhotonNetwork.room.CustomProperties["r_earlyForLink"];
            r_perfectForLink = (int)PhotonNetwork.room.CustomProperties["r_perfectForLink"];
            r_slowForLink = (int)PhotonNetwork.room.CustomProperties["r_slowForLink"];
            r_damageForLink = (int)PhotonNetwork.room.CustomProperties["r_damageForLink"];

            timeOut1 = 0.5f;
        }

        //static変数に格納してスコアシーンでも使えるようにする
        m_earlyForResult = m_earlyForLink;
        m_perfectForResult = m_perfectForLink;
        m_slowForResult = m_slowForLink;
        m_damageForResult = m_damageForLink;
        m_ScoreForResult = m_ScoreForLink;

        r_earlyForResult = r_earlyForLink;
        r_perfectForResult = r_perfectForLink;
        r_slowForResult = r_slowForLink;
        r_damageForResult = r_damageForLink;
        r_ScoreForResult = r_ScoreForLink;

        //0.5(秒)に一回初音ミクのスコア要素をPhotonルームプロパティに上書き
        if (clientNumber == 1)
        {
            timeOut2 -= Time.deltaTime;
            if (timeOut2 < 0)
            {
                manage1.properties["m_ScoreForLink"] = m_Score;
                manage1.properties["m_earlyForLink"] = m_early;
                manage1.properties["m_perfectForLink"] = m_perfect;
                manage1.properties["m_slowForLink"] = m_slow;
                manage1.properties["m_damageForLink"] = m_damage;
                PhotonNetwork.room.SetCustomProperties(manage1.properties);

                //メインシーン中の二人のスコアを更新
                m_text.text = "Score : " + m_Score.ToString();
                r_text.text = "Score : " + r_ScoreForLink.ToString();

                timeOut2 = 0.5f;
            }
        }
        //0.5(秒)に一回鏡音リンのスコア要素をPhotonルームプロパティに上書き
        else if (clientNumber == 2)
        {
            timeOut3 -= Time.deltaTime;

            if (timeOut3 < 0)
            {
                manage1.properties["r_ScoreForLink"] = r_Score;
                manage1.properties["r_earlyForLink"] = r_early;
                manage1.properties["r_perfectForLink"] = r_perfect;
                manage1.properties["r_slowForLink"] = r_slow;
                manage1.properties["r_damageForLink"] = r_damage;
                PhotonNetwork.room.SetCustomProperties(manage1.properties);

                //メインシーン中の二人のスコアを更新
                m_text.text = "Score : " + m_ScoreForLink.ToString();
                r_text.text = "Score : " + r_Score.ToString();

                timeOut3 = 0.5f;
            }
        }
    }

    //スコアシーンで呼ぶ用の関数を用意
    static public int Get_m_earlyForResult()
    {
        return m_earlyForResult;
    }
    static public int Get_m_perfectForResult()
    {
        return m_perfectForResult;
    }
    static public int Get_m_slowForResult()
    {
        return m_slowForResult;
    }
    static public int Get_m_damageForResult()
    {
        return m_damageForResult;
    }
    static public int Get_m_ScoreForResult()
    {
        return m_ScoreForResult;
    }
    static public int Get_r_earlyForResult()
    {
        return r_earlyForResult;
    }
    static public int Get_r_perfectForResult()
    {
        return r_perfectForResult;
    }
    static public int Get_r_slowForResult()
    {
        return r_slowForResult;
    }
    static public int Get_r_damageForResult()
    {
        return r_damageForResult;
    }
    static public int Get_r_ScoreForResult()
    {
        return r_ScoreForResult;
    }
}
