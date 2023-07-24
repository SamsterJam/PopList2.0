using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private SpriteRenderer mySpriteRenderer;
    private TextMeshPro myTextMeshPro;
    private int randCount = 0;

    public Vector2 bubbleTargetPos;
    public string bubbletTaskName;
    public float bubbleSize;
    public int bubbleColor;
    public int bubbleNumber;

    private Vector2 floatTargPos;
    private bool isGrabbed = false;

    private void Start(){
        randCount = Random.Range(0,20);
        floatTargPos = bubbleTargetPos;

        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTextMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    private void FixedUpdate() {

        // Random Floating
        if(!isGrabbed){
            if(randCount > 20){
                floatTargPos = new Vector2(bubbleTargetPos.x+Random.Range(-0.02f,0.02f), bubbleTargetPos.y+Random.Range(-0.02f,0.02f));
                randCount = 0;
            }else{
                randCount++;
            }
        } else {
            floatTargPos = bubbleTargetPos; // Update the target position to the bubbleTargetPos when grabbed
        }

        // Add force towards target
        Vector2 tempTarget = new Vector2(floatTargPos.x,floatTargPos.y);
        myRigidBody.AddForce(((Vector3)tempTarget-transform.position)*Time.fixedDeltaTime*(isGrabbed? 10000 : 4000));

        Debug.DrawLine(transform.position,tempTarget,Color.green);

        myRigidBody.AddForce(new Vector2((Input.gyro.gravity.x * (transform.localScale.x*1000))*0.01f, 0), 0);
    }

    // Call this method when the bubble is grabbed
    public void Grabbed() {
        isGrabbed = true;
        floatTargPos = transform.position; // Set the target position to the current position when grabbed
    }

    // Call this method when the bubble is released
    public void Released() {
        isGrabbed = false;
}
    


    public void refreshProperties(){
        GetComponent<SpriteRenderer>().color = ColorPalette.Colors[bubbleColor];
        transform.localScale = new Vector2(bubbleSize,bubbleSize);
        transform.GetChild(0).GetComponent<TextMeshPro>().text = bubbletTaskName;
    }


    public void popTask(){
        MasterList.list[bubbleNumber].compleated = true;
        AndroidManager.HapticFeedback();
        MasterList.SaveMasterList();
        Destroy(this.gameObject);
    }
}
