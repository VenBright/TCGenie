using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseUp()
    {
        SceneManager.LoadScene("FullSreen", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
