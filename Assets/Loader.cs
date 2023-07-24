using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Loader : MonoBehaviour
{
    public GameObject BubbleTaskPrefab;
    private string saveFile;

    void Awake(){

        // Load in MasterList
        MasterList.LoadMasterList();
        Debug.Log(Application.persistentDataPath);
        
    }

    void Start(){

        // Spawn In Bubbles from MasterList
        SpawnBubblesFromList();

    }


    // #### Spawning In Bubble #### //

     public void SpawnBubblesFromList(){

        for (int i=0; i < MasterList.list.Count; i++){
            Task cb = MasterList.list[i];

            if(!cb.compleated){
                GameObject spawnBubble = Instantiate(BubbleTaskPrefab, new Vector2(cb.target.x+Random.Range(-0.5f,0.5f), cb.target.y+Random.Range(-0.5f,0.5f)), Quaternion.identity);

                spawnBubble.GetComponent<SpriteRenderer>().color = ColorPalette.Colors[cb.color];
                spawnBubble.transform.localScale = new Vector2(cb.size,cb.size);
                spawnBubble.transform.GetChild(0).GetComponent<TextMeshPro>().text = cb.name;

                Bubble bubbleScript = spawnBubble.GetComponent<Bubble>();
                bubbleScript.bubbleTargetPos = new Vector2(cb.target.x, cb.target.y);
                bubbleScript.bubbleColor = cb.color;
                bubbleScript.bubbleSize = cb.size;
                bubbleScript.bubbletTaskName = cb.name;
                bubbleScript.bubbleNumber = i;
            }
        }
    }
}