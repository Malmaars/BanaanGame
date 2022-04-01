using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //initiate cutscene and end game
        if(other.gameObject.tag == "Player")
        {
            //initiate cutscene
            SceneManager.LoadScene(1);
        }
    }
}
