using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//테스트용 건들지 않아도됨
public class SceneLoadTest_ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadingSceneController.LoadScene("Test2");
        }
    }
}
