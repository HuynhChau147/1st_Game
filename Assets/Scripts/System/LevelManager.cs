using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image LoadingProcess;
    private float _target;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScence(int SceneIndex)
    {
        _target = 0;
        LoadingProcess.fillAmount = 0;
        
        var scene = SceneManager.LoadSceneAsync(SceneIndex);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do{
            await Task.Delay(1000);
            _target = scene.progress;
            Debug.Log(_target);
        }while (scene.progress < 0.9f);
        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    private void Update() {
        LoadingProcess.fillAmount = Mathf.MoveTowards(LoadingProcess.fillAmount,_target + 0.1f, 3 * Time.deltaTime);
    }
}
