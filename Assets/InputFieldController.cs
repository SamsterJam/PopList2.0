using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour
{
    public TMP_InputField inputField;
    private bool firstLetterCapitalized = false;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(CapitalizeFirstLetter);
    }

    private void Start(){
        inputField.text = "";
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }

    private void CapitalizeFirstLetter(string value)
    {
        if (string.IsNullOrEmpty(value) || firstLetterCapitalized) return;

        // Capitalize the first letter and update the text
        char firstLetter = char.ToUpper(value[0]);
        inputField.text = firstLetter + value.Substring(1);
        firstLetterCapitalized = true;
    }
}