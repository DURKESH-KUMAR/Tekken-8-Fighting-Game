using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectStage : MonoBehaviour{
    public void StageSelect(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}