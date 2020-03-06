using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//観客のアニメーションを変化
public class ChangeAnimationState : MonoBehaviour
{
    //観客のアニメーションステート
    public enum ANIMATION_TYPE
    {
        idle1, 
        tebyoshi, 
        idle2, 
    }
    public ANIMATION_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        type = ANIMATION_TYPE.idle1;
    }

    //ステート変化させる用の関数
    //Timeline上で任意に呼び出す
    public void ChangeToIdle1()
    {
        type = ANIMATION_TYPE.idle1;
    }
    public void ChangeToTebyoshi()
    {
        type = ANIMATION_TYPE.tebyoshi;
    }
    public void ChangeToIdle2()
    {
        type = ANIMATION_TYPE.idle2;
    }
}
