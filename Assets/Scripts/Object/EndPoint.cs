using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip ClearStageSound;
    [SerializeField] private Animator anim;
    [SerializeField] private int SceneIndex;
    private string ScreenName;
    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Player"))
        {
            this.GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Appear");
            audioSrc.Stop();
            audioSrc.PlayOneShot(ClearStageSound);
            StartCoroutine(ChangeScene());
        }
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.LoadScence(SceneIndex);
    }
}
