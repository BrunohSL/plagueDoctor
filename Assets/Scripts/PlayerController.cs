using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private BedController bedController;
    private CauldronController cauldronController;
    private GameObject bed = null;

    private bool canPickUpItem = false;
    private bool canHealPatient = false;
    private bool potionReady = false;
    private string herbType = "";
    private int life = 3;

    public Rigidbody2D player;
    public Animator animator;
    public Text lifeText;
    public Text lifeShadowText;

    public float actionTimer = 4f;
    public bool doingAction = false;
    public bool isHoldingRedHerb = false;
    public bool isHoldingGreenHerb = false;
    public bool healDone = false;
    public bool caldroonAction = false;
    public bool mortarAction = false;
    public bool holdingPotion = false;
    public bool holdingMedicine = false;

    void Start() {
        GameObject theCauldron = GameObject.Find("Cauldron");
        cauldronController = theCauldron.GetComponent<CauldronController>();

        GameObject theBed = GameObject.Find("bed_controller");
        bedController = theBed.GetComponent<BedController>();

        player = GetComponent<Rigidbody2D>();

        lifeText.text = "Life: " + life;
        lifeShadowText.text = "Life: " + life;
    }

    void Update() {
        potionReady = cauldronController.potionReady;

        if (life <= 0) {
            SceneManager.LoadScene("GameOver");
        }

        lifeText.text = "Life: " + life;
        lifeShadowText.text = "Life: " + life;

        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("holdingPotion"+holdingPotion);
            Debug.Log("isHoldingGreenHerb"+isHoldingGreenHerb);
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetBool("Red", false);
        }

        if (doingAction) {
            CalculateCountdown();
        }

        if (Input.GetButtonDown("Fire1")) {
            InputFire1Function();
        }

        bool green = getGreen();
        bool red = getRed();

        animator.SetBool("Red", red);
        animator.SetBool("Green", green);

        if (!green && !red) {
            animator.SetTrigger("idle");
            animator.ResetTrigger("Herb");
            animator.ResetTrigger("Cure");
        }

        if (isHoldingRedHerb || isHoldingGreenHerb) {
            animator.SetTrigger("Herb");
            return;
        }

        if (holdingMedicine || holdingPotion) {
            animator.SetTrigger("Cure");
            return;
        }
    }

    public void loseLife() {
        this.life -= 1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!isHoldingRedHerb || !isHoldingGreenHerb) {
            if (other.tag == "green_herb_collider" || other.tag == "red_herb_collider") {
                herbType = other.tag;
                canPickUpItem = true;
            }
        }

        if (other.tag == "patient_collider") {
            bed = other.transform.parent.gameObject;

            if (holdingPotion && bed.GetComponent<Bed>().getColor() == "green") {
                canHealPatient = true;
            }

            if (holdingMedicine && bed.GetComponent<Bed>().getColor() == "red") {
                canHealPatient = true;
            }
        }

        if (other.tag == "cauldron_collider") {
            caldroonAction = true;
        }

        if (other.tag == "mortar_collider") {
            mortarAction = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "green_herb_collider" || other.tag == "red_herb_collider") {
            canPickUpItem = false;
        }

        if (other.tag == "patient_collider") {
            canHealPatient = false;
            bed = null;
        }

        if (other.tag == "cauldron_collider") {
            caldroonAction = false;
        }

        if (other.tag == "mortar_collider") {
            mortarAction = false;
        }
    }

    void InputFire1Function() {
        if (canHealPatient && (holdingPotion || holdingMedicine)) {
            bed.GetComponent<Bed>().setHealed(true);

            holdingPotion = false;
            holdingMedicine = false;
            canHealPatient = false;
            return;
        }

        if (isHoldingRedHerb || isHoldingGreenHerb) {
            isHoldingRedHerb = false;
            isHoldingGreenHerb = false;
            animator.SetBool("Green", false);
            animator.SetBool("Red", false);
            return;
        }

        if (canPickUpItem) {
            GrabHerbs();
        }
    }

    void CalculateCountdown() {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0) {
            doingAction = false;
            actionTimer = 5f;
        }
    }

    void GrabHerbs() {
        if(herbType == "green_herb_collider"){
            isHoldingGreenHerb = true;
        }

        if(herbType == "red_herb_collider"){
            isHoldingRedHerb = true;
        }

        canPickUpItem = false;
    }

    bool getGreen(){
        if(isHoldingGreenHerb || holdingPotion){
            return true;
        }
        return false;
    }
    bool getRed(){
        if(isHoldingRedHerb || holdingMedicine){
            return true;
        }
        return false;
    }
}
