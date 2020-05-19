using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour {
    private PlayerController playerController;

    private bool doingAction = false;
    private bool enableAction = false;

    public Animator animator;

    public float actionTimer = 2f;
    public bool medicineReady = false;

    void Start() {
        GameObject thePlayer = GameObject.Find("player");
        playerController = thePlayer.GetComponent<PlayerController>();
    }

    void Update() {
        if (doingAction) {
            CalculateCountdown();
        }

        if (playerController.mortarAction) {
            if (Input.GetButtonDown("Fire1") && playerController.isHoldingRedHerb) {
                playerController.isHoldingRedHerb = false;
                doingAction = true;
                animator.SetTrigger("mixingm");
                enableAction = false;
                CalculateCountdown();
            }

            if (Input.GetButtonDown("Fire1") && !playerController.isHoldingRedHerb && medicineReady) {
                animator.SetTrigger("idlem");
                playerController.holdingMedicine = true;
                medicineReady = false;
            }
        }
    }

    void CalculateCountdown() {
        actionTimer -= Time.deltaTime;

        if (actionTimer < 0) {
            doingAction = false;
            actionTimer = 5f;
            medicineReady = true;
            animator.SetTrigger("donem");
        }
    }
}
