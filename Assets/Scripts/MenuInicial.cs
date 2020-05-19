using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour {
    public string levelToLoad;

    void Update() {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
