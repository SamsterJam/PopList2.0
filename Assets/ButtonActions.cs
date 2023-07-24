using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonActions : MonoBehaviour
{
    public GameObject BubbleTaskPrefab;
    public Slider sizeSlider;
    public TMP_InputField nameField;

    private GameObject designedBubble;
    private Bubble dgnBblScript;

    void Start(){
        designedBubble = Instantiate(BubbleTaskPrefab, new Vector3(0,3.5f,0), Quaternion.identity);
        dgnBblScript = designedBubble.GetComponent<Bubble>();

        dgnBblScript.bubbleColor = 0;
        dgnBblScript.bubbleSize = 1f;
        dgnBblScript.bubbleTargetPos = new Vector2(0,3.5f);
        dgnBblScript.bubbletTaskName = "";
        dgnBblScript.bubbleNumber = MasterList.list.Count;
        dgnBblScript.refreshProperties();

        sizeSlider.onValueChanged.AddListener(SliderChange);
        nameField.onValueChanged.AddListener(ChangeText);

        EventSystem.current.SetSelectedGameObject(nameField.gameObject);
    }

    public void ChangeColor0()
    {
        dgnBblScript.bubbleColor = 0;
        dgnBblScript.refreshProperties();
    }

    public void ChangeColor1()
    {
        dgnBblScript.bubbleColor = 1;
        dgnBblScript.refreshProperties();
    }

    public void ChangeColor2()
    {
        dgnBblScript.bubbleColor = 2;
        dgnBblScript.refreshProperties();
    }

    public void ChangeColor3()
    {
        dgnBblScript.bubbleColor = 3;
        dgnBblScript.refreshProperties();
    }

    public void ChangeColor4()
    {
        dgnBblScript.bubbleColor = 4;
        dgnBblScript.refreshProperties();
    }

    public void ChangeColor5()
    {
        dgnBblScript.bubbleColor = 5;
        dgnBblScript.refreshProperties();
    }

    void SliderChange(float size){
        dgnBblScript.bubbleSize = CustomSliderScale.ScaleSliderValue(size);
        dgnBblScript.refreshProperties();
    }

    void ChangeText(string name){
        dgnBblScript.bubbletTaskName = name;
        dgnBblScript.refreshProperties();
    }


    public void SubmitNewTask(){
        StartCoroutine(littlePause());
        Debug.Log("Adding new Task to MasterList...");
        MasterList.list.Add(new Task(dgnBblScript.bubbletTaskName, dgnBblScript.bubbleColor, dgnBblScript.bubbleSize, MasterList.GLOBAL_NEWBUBBLEPOS));
        Debug.Log("Added new Task!");
        MasterList.SaveMasterList();
        SceneManager.LoadScene("ListScene");
    }

    IEnumerator littlePause(){
        yield return new WaitForSeconds(1);
    }

    public void GoBack(){
        SceneManager.LoadScene("ListScene");
    }

    public void Update(){
        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu)){
            SceneManager.LoadScene("ListScene");
        }
    }
    
}
