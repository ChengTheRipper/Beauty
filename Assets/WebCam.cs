using System.Collections;
using UnityEngine;

public class WebCam : MonoBehaviour
{

    public string deviceName;
    WebCamTexture tex;
    // Use this for initialization
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            tex = new WebCamTexture(deviceName, 400, 300, 12);
            //renderer.material.mainTexture = tex;
            tex.Play();



        }// Use this for initialization
         // void Start () {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
