using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionHandler : MonoBehaviour
{
    // private float movedAmount = 0f;
    // private Vector2 velocity = new Vector2(0,0);
    // private Vector2 scrollAnchorPoint;
    // private Vector2 originalCameraPos;

    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
    //         scrollAnchorPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //         originalCameraPos = transform.position;
    //         velocity = new Vector2(0,0);
    //         movedAmount = 0f;
    //     }

    //     if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){

    //     }
    // }

    private Vector3 touchStart;
    private float moved;
    private float holdCounter;
    private bool movingBubble;
    private Bubble bubbleTarget;

    void Update(){

        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu)){
            MasterList.SaveMasterList();
            Application.Quit();
        }


        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            moved=0f;
            holdCounter = 0f;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary){
            holdCounter += Time.deltaTime;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            moved+=Time.deltaTime;
            
            if(holdCounter > 0.01){

                if(!movingBubble){
                    //Check for Bubble
                    Vector3 pos = Camera.main.ScreenToWorldPoint (Input.touches[0].position);
                    RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                    if(hit != false && hit.collider != null){
                        bubbleTarget = hit.transform.gameObject.GetComponent<Bubble>();
                        bubbleTarget.Grabbed();
                        movingBubble = true;
                    }else{
                        holdCounter = 0f;
                    }
                }
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){

            // Save if bubble moved
            if(movingBubble){
                MasterList.list[bubbleTarget.bubbleNumber].target = bubbleTarget.bubbleTargetPos;
                MasterList.SaveMasterList();
                bubbleTarget.Released();
            }

            movingBubble = false;
            if(moved < 0.001f){
                rayCastBubble();
            }
        }

        if(movingBubble){
            bubbleTarget.bubbleTargetPos = new Vector2(Camera.main.ScreenToWorldPoint (Input.touches[0].position).x, Camera.main.ScreenToWorldPoint (Input.touches[0].position).y);
        }

        // if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
        //     Vector3 direction3D = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //     Camera.main.transform.position += new Vector3(0, direction3D.y, 0);
        //     inertia = Mathf.Clamp(inertiaPoint.y-Input.GetTouch(0).position.y, -2f, 2f);
        //     inertiaPoint = Input.GetTouch(0).position;
        //     moved+=Time.deltaTime;
        // }

        // if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
        //     if(moved < 0.01){
        //         SceneManager.LoadScene("CreateTask");
        //     }
        // }
        
        // if(!(Input.touchCount > 0)){
        //     Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+(inertia)*Time.deltaTime, transform.position.z);
        // }

    }
    

    // Bubble Ray Cast
    public static void rayCastBubble(){
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if(hit != false && hit.collider != null){
            Bubble temp = hit.transform.gameObject.GetComponent<Bubble>();
            temp.popTask();
        }else{
            MasterList.GLOBAL_NEWBUBBLEPOS = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            SceneManager.LoadScene("CreateTask");
        }
    }


    // void FixedUpdate() {

    //     CamDrag = Mathf.Abs(inertia/10);

    //     if(inertia >= CamDrag){
    //         inertia -= CamDrag;
    //     }else if(inertia <= -CamDrag){
    //         inertia += CamDrag;
    //     }else{
    //         inertia = 0f;
    //     }
        
    // }
}
