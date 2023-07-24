using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreateBounds : MonoBehaviour
{
    void Awake () 
    {
      AddCollider();
    }

    void Start() {
      Application.targetFrameRate = 120;
      Input.gyro.enabled = true;
    }

    void AddCollider () 
    {
      if (Camera.main==null) {Debug.LogError("Camera.main not found, failed to create edge colliders"); return;}

      var cam = Camera.main;
      if (!cam.orthographic) {Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return;}

      var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, -cam.pixelHeight*10, cam.nearClipPlane));
      var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
      var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
      var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, -cam.pixelHeight*10, cam.nearClipPlane));

      // add or use existing EdgeCollider2D
      var edge = GetComponent<EdgeCollider2D>()==null?gameObject.AddComponent<EdgeCollider2D>():GetComponent<EdgeCollider2D>();

      var edgePoints = new [] {bottomLeft,topLeft,topRight,bottomRight};
      edge.points = edgePoints;
    }

    private void Update() {
      if(Input.GetKeyUp(KeyCode.R)){
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
      }
    }
}
