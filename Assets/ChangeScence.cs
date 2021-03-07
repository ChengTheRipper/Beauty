using System.Collections;
using UnityEngine;


public class ChangeScence : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(NextScence());


    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator NextScence()

    {
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("Scence_Lady");
    }
}
