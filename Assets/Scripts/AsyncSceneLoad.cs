using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AsyncSceneLoad : MonoBehaviour
{
    public TextMeshProUGUI LoadText;
    public float progress = 0.0f;

    void Start()
    {
        StartCoroutine(SceneAsyncLoad("Main Menu"));
    }

    IEnumerator SceneAsyncLoad(string scene)
    {
        AsyncOperation syncOp = SceneManager.LoadSceneAsync(scene);
        syncOp.allowSceneActivation = false;

        // 10 SECONDS TIMER
        int duration = 10;
        while (duration > 0)
        {
            progress = (10f - duration) / 10f;
            LoadText.text = $"Loading: {(int)(progress * 100)} %";
            
            yield return new WaitForSeconds(1);
            duration--;
        }
        
        progress = 1f;
        LoadText.text = "Loading: 100 %";

        syncOp.allowSceneActivation = true;
    }
}
