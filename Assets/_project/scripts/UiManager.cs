using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public void OnButtonLoadSceneClicked(string name)
    {
        SceneManager.LoadScene(name);
    }   
}