using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronController : MonoBehaviour {
    private PlayerController playerController;

    private bool doingAction = false;

    public Animator animator;

    public float actionTimer = 1.5f;
    public bool potionReady = false;

    void Start() {
        GameObject thePlayer = GameObject.Find("player");
        playerController = thePlayer.GetComponent<PlayerController>();
    }

    void Update() {
        if (doingAction) {
            CalculateCountdown();
        }
        if(playerController.caldroonAction){


            if (Input.GetButtonDown("Fire1") && playerController.isHoldingGreenHerb)
            {
                playerController.isHoldingGreenHerb = false;
                doingAction = true;
                animator.SetTrigger("cooking");
                CalculateCountdown();
            }

            if (Input.GetButtonDown("Fire1") && !playerController.isHoldingGreenHerb && potionReady) {
                animator.SetTrigger("empty");
                playerController.holdingPotion = true;
                potionReady = false;
            }
        }
    }

    void CalculateCountdown() {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0) {
            doingAction = false;
            actionTimer = 1.5f;
            potionReady = true;
            animator.SetTrigger("done");
            Debug.Log("Done");
        }
    }
}
