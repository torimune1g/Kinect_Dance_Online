using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルトシーンのスコア表示
public class Result : MonoBehaviour
{
    //初音ミクのスコアテキスト
    public Text m_early;
    public Text m_perfect;
    public Text m_slow;
    public Text m_damage;
    public Text m_Score;

    //鏡音リンのスコアテキスト
    public Text r_early;
    public Text r_perfect;
    public Text r_slow;
    public Text r_damage;
    public Text r_Score;

    //勝者表示用テキスト
    public Text winner;

    // Start is called before the first frame update
    void Start()
    {
        //ScoreManageスクリプトからスコア要素を取得し、表示
        m_early.text = "Early: " + ScoreManage.Get_m_earlyForResult().ToString();
        m_perfect.text = "Perfect: " + ScoreManage.Get_m_perfectForResult().ToString();
        m_slow.text = "Slow: " + ScoreManage.Get_m_slowForResult().ToString();
        m_damage.text = "Damage: " + ScoreManage.Get_m_damageForResult().ToString();
        m_Score.text = "Score: " + ScoreManage.Get_m_ScoreForResult().ToString();
        r_early.text = "Early: " + ScoreManage.Get_r_earlyForResult().ToString();
        r_perfect.text = "Perfect: " + ScoreManage.Get_r_perfectForResult().ToString();
        r_slow.text = "Slow: " + ScoreManage.Get_r_slowForResult().ToString();
        r_damage.text = "Damage: " + ScoreManage.Get_r_damageForResult().ToString();
        r_Score.text = "Score: " + ScoreManage.Get_r_ScoreForResult().ToString();

        //勝者を表示
        if (ScoreManage.Get_m_ScoreForResult() > ScoreManage.Get_r_ScoreForResult())
        {
            winner.text = "Hatsune Miku";
        }
        else if (ScoreManage.Get_m_ScoreForResult() < ScoreManage.Get_r_ScoreForResult())
        {
            winner.text = "Kagamine Rin";
        }
        else
        {
            winner.text = "Hatsune Miku\nKagamine Rin";
        }
    }
}
