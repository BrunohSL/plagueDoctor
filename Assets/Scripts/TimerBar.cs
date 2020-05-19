using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public Image timerBar;
    public GameObject timeBarCanvas;

    public float maxTime;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        // timerBar.GetComponent<Image>();
        timeLeft = maxTime;
        // timeBarCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(maxTime);
        Debug.Log(timerBar.fillAmount);
        Debug.Log(timeLeft);

        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / 5f;
        }
    }
}
