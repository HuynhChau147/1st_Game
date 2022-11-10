using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect;
    private int currentPosition;

    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSelectionSound;
    [SerializeField] private AudioClip selectedSound;
    [SerializeField] private AudioSource audioSrc;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update() {
        // Input key to change selection
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
        }

        // Input to interact with selection
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            audioSrc.PlayOneShot(selectedSound);
            Interact();
        }
    }

    private void ChangeSelection(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
        {
            audioSrc.PlayOneShot(changeSelectionSound);
        }

        if(currentPosition <0)
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length -1)
        {
            currentPosition = 0;
        }

        // Change Y pos of Arrow to each select postion
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y,0);
    }

    private void Interact()
    {

        // Access the button component on each option and call it's function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
