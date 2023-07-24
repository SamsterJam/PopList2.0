using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SimpleBounds : MonoBehaviour
{
    void Awake () 
    {
      AddCollider();
    }

    void Start() {
      Application.targetFrameRate = 120;
      Input.gyro.enabled = true;
    }

    // Create bounds around camera
    void AddCollider() {
      if (Camera.main==null) {Debug.LogError("Camera.main not found, failed to create edge colliders"); return;}

      var cam = Camera.main;
      if (!cam.orthographic) {Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return;}

      var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
      var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
      var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
      var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

      // add or use existing EdgeCollider2D
      var edge = GetComponent<EdgeCollider2D>()==null?gameObject.AddComponent<EdgeCollider2D>():GetComponent<EdgeCollider2D>();

      var edgePoints = new [] {bottomLeft,topLeft,topRight,bottomRight,bottomLeft};
      edge.points = edgePoints;
    }


    // Save on Exit
    void OnApplicationQuit(){
        MasterList.SaveMasterList();
    }





    // #### DEBUG MENU OPTION #### //

    private void Update() {
      if(Input.GetKeyUp(KeyCode.R)){
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
      }

      // Force Save to Disk
      if(Input.GetKey(KeyCode.W)){
          Debug.Log("Writing MasterList to disk...");
          MasterList.SaveMasterList();
          Debug.Log("MasterList Saved!");
      }

      // Force Load from Disk
      if(Input.GetKey(KeyCode.S)){
          Debug.Log("Loading from disk...");
          MasterList.LoadMasterList();
          Debug.Log("MasterList Loaded!");
      }

      // Add new Bubble
      if(Input.GetKey(KeyCode.C)){
          Debug.Log("Switching to NewBubble Scene...");
          SceneManager.LoadScene("CreateTask");
      }
    }
}
