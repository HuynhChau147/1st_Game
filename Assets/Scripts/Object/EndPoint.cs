using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private bool isWin;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip ClearStageSound;
    [SerializeField] private Animator anim;
    [SerializeField] private int SceneIndex;
    private string ScreenName;
    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex < 2 )
        {
            this.GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Appear");
            audioSrc.Stop();
            audioSrc.PlayOneShot(ClearStageSound);
            StartCoroutine(ChangeScene());
        }

        if(colTri.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            this.GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Appear");
            audioSrc.Stop();
            audioSrc.PlayOneShot(ClearStageSound);
            isWin = true;
        }
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.LoadScence(SceneIndex);
    }

    public bool getWinStatus()
    {
        return isWin;
    }

    public void setWinStatus(bool Status)
    {
        this.isWin = Status;
    }
}
