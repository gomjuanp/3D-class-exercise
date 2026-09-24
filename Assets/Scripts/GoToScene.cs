using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{
    public string scene;

    public void OnClick(){
        SceneManager.LoadSceneAsync(scene);
    }
}
