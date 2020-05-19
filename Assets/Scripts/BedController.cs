using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedController : MonoBehaviour {
    private GameObject playerObj;
    private PlayerController playerController;

    private float nextPatientTime = 8f;
    private float timeLeftForNextPatient;

    public Text deadPatientText;
    public Text deadPatientShadowText;

    void Start() {
        playerObj = GameObject.Find("player");
        playerController = playerObj.GetComponent<PlayerController>();
        timeLeftForNextPatient = nextPatientTime;

        deadPatientText.enabled = false;
        deadPatientShadowText.enabled = false;
    }

    void Update() {
        bedsValidation();

        if (timeLeftForNextPatient > 0) {
            timeLeftForNextPatient -= Time.deltaTime;
        }

        if (timeLeftForNextPatient <= 0) {
            int occupiedBeds = 0;
            foreach (GameObject bed in GameObject.FindGameObjectsWithTag("bed")) {
                if (bed.GetComponent<Bed>().getIsAvailable()) {
                    bed.GetComponent<Animator>().SetBool("Healed", false);
                    int color = Random.Range(0, 2);
                    Debug.Log(color);
                    if (color == 0) {
                        bed.GetComponent<Animator>().SetBool("bed_red", true);
                        bed.GetComponent<Bed>().setColor("red");
                    } else {
                        bed.GetComponent<Animator>().SetBool("bed_green", true);
                        bed.GetComponent<Bed>().setColor("green");
                    }
                    bed.GetComponent<Bed>().setIsAvailable(false);
                    break;
                } else {
                    occupiedBeds++;
                }

            }

            if (occupiedBeds == 3) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                deadPatientText.enabled = true;
                deadPatientShadowText.enabled = true;
                deadPatientText.GetComponent<TextFade>().fadeText = true;
                deadPatientShadowText.GetComponent<TextFade>().fadeText = true;
                player.GetComponent<PlayerController>().loseLife();
            }

            timeLeftForNextPatient = nextPatientTime;
        }
    }

    private void bedsValidation() {
        foreach (GameObject bed in GameObject.FindGameObjectsWithTag("bed")) {
            if (bed.GetComponent<Bed>().getHealed()) {
                bed.GetComponent<Animator>().SetTrigger("Heal");
                bed.GetComponent<Animator>().SetBool("bed_red", false);
                bed.GetComponent<Animator>().SetBool("bed_green", false);
                bed.GetComponent<Bed>().setIsAvailable(true);
                bed.GetComponent<Bed>().setHealed(false);
            }
        }
    }
}
