using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
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
}
