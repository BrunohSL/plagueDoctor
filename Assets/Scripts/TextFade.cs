using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {
    public bool fadeText = false;
    void Update() {
        if (fadeText) {
            StartCoroutine(FadeTextToZeroAlpha(2f, GetComponent<Text>()));
            fadeText = false;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
