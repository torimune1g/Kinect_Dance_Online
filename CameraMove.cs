using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//カメラ移動とフェードアウト
public class CameraMove : MonoBehaviour
{
    public GameObject timeline; //Timelineを司るゲームオブジェクト
    private PlayableDirector playableDirector; 

    public GameObject changeAnimationStateObj; 
    public ChangeAnimationState changeAnimationState; //Timeline上で観客のアニメーションステートを変化させるためのスクリプト

    public Image img; //リザルトシーン切り替え時にフェードアウトをするイメージ、カメラの目の前に存在

    private float speed = 0.02f; //フェードアウトのスピード
    private float alfa;  
    private float red, green, blue; 

    // Start is called before the first frame update
    void Start()
    {
        timeline = GameObject.FindGameObjectWithTag("timeline");
        playableDirector = timeline.GetComponent<PlayableDirector>();

        changeAnimationStateObj = GameObject.FindGameObjectWithTag("changeAnimationState");
        changeAnimationState = changeAnimationStateObj.GetComponent<ChangeAnimationState>();
        red = img.GetComponent<Image>().color.r;
        green = img.GetComponent<Image>().color.g;
        blue = img.GetComponent<Image>().color.b;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        //タイムライン再生中はこのカメラを斜め上後方に800秒かけて移動
        if (playableDirector.state == PlayState.Playing) 
        {
            transform.DOLocalMove(new Vector3(0, 1.5f, 3), 800);
        }
        //観客のアニメーションがidle2に変化したらImageのalfa値を上げてフェードアウト
        if (changeAnimationState.type == ChangeAnimationState.ANIMATION_TYPE.idle2)
        {
            alfa += speed;
            img.color = new Color(red, green, blue, alfa);
        }
    }

    //リザルトシーンではImageを削除
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ResultScene")
        {
            Destroy(img);
        }
    }
}
