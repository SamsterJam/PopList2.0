using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrolling : MonoBehaviour
{  
    
    public float maxHeight;
    public static GameObject activeList;

    private float CamDrag;
    private Vector2 scrollOriginalPos;
    private Vector2 scrollAnchorPoint;
    private float velocityY = 0f;
    private float moved = 0f;
    private float anchorSpotY;
    private float currentToutchPos;


    // Update is called once per frame
    void Update(){

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            scrollAnchorPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            scrollOriginalPos = transform.position;
            velocityY = 0f;
            moved = 0;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Vector2 currentToutchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if(Mathf.Abs(currentToutchPos.x-scrollAnchorPoint.x) > 5f){

            }


            Debug.DrawLine(currentToutchPos,scrollAnchorPoint);
            //transform.position += new Vector3(transform.position.x,anchorSpotY-currentToutchPos.y,0);

            if(Mathf.Abs(currentToutchPos.y-scrollAnchorPoint.y) > Mathf.Abs(currentToutchPos.x-scrollAnchorPoint.x)){
                transform.position += new Vector3(transform.position.x,scrollAnchorPoint.y-currentToutchPos.y,0);
            }else{
                transform.position += new Vector3(scrollAnchorPoint.x-currentToutchPos.x,transform.position.y,0);
            }


            if(transform.position.y > maxHeight){
                anchorSpotY = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y;
            }
            moved += 1;
            
        }



        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            anchorSpotY = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y;
            velocityY = 0f;
            moved = 0;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Vector2 currentToutchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position += new Vector3(transform.position.x,anchorSpotY-currentToutchPos.y,0);
            if(transform.position.y > maxHeight){
                anchorSpotY = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y;
            }
            moved += 1;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            Vector2 currentToutchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            velocityY = (anchorSpotY-currentToutchPos.y)*2;
            velocityY = Mathf.Clamp(velocityY, -2f, 2f);
            if(moved <= 1 && moved >= -1){
                //LoadBubbles.rayCastBubble();
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y+(velocityY*Time.deltaTime*60), transform.position.z);
        
        if(transform.position.y > maxHeight){
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        

    }

    private void FixedUpdate() {

        CamDrag = Mathf.Abs(velocityY/10);

        if(velocityY >= CamDrag){
            velocityY -= CamDrag;
        }else if(velocityY <= -CamDrag){
            velocityY += CamDrag;
        }else{
            velocityY = 0f;
        }

                //activeList = ray
        
    }

    
}
