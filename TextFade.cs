using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//曲名表示
public class TextFade : MonoBehaviour
{
    private Text text;
    Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        sequence = DOTween.Sequence()
        .Append(
            DOTween.ToAlpha(
            () => text.color,
            color => text.color = color,
            1,
            2.0f
            )
        )
        .Append(
            DOTween.ToAlpha(
            () => text.color,
            color => text.color = color,
            1,
            1.0f
            )
        )
        .Append(
            DOTween.ToAlpha(
            () => text.color,
            color => text.color = color,
            0,
            2.0f
            )
        );
    }

    // Update is called once per frame
    void Update()
    {
        sequence.Play();
    }
}
