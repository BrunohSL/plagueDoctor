using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {
    private bool isAvailable = true;
    private bool healed = false;
    private string color;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public string getColor() {
        return this.color;
    }

    public void setColor(string color) {
        this.color = color;
    }

    public bool getIsAvailable() {
        return this.isAvailable;
    }

    public void setIsAvailable(bool isAvailable) {
        this.isAvailable = isAvailable;
    }

    public bool getHealed() {
        return this.healed;
    }

    public void setHealed(bool healed) {
        this.healed = healed;
    }
}
