using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//観客アニメーションの切り替え
public class OtakukunAnimation : MonoBehaviour
{
    private Animator animator;
    public ChangeAnimationState changeAnimationState; //Timeline上で観客のアニメーションをステートを変化させるためのスクリプト
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (changeAnimationState.type)
        {
            case ChangeAnimationState.ANIMATION_TYPE.idle1:
              animator.SetBool("psyllium_start", false);
                break;
            case ChangeAnimationState.ANIMATION_TYPE.tebyoshi:
                animator.SetBool("psyllium_start", true);
                break;
            case ChangeAnimationState.ANIMATION_TYPE.idle2:
                animator.SetBool("psyllium_start", false);
                break;
        }
    }
}
