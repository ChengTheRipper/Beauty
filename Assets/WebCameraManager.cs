using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using FaceDetect;

public class WebCameraManager : MonoBehaviour
{
    //相机设备名称
    public string DeviceName;
    //相机画幅大小
    public Vector2 CameraSize;
    //相机帧数
    public float CameraFPS;
    //接收返回的图片数据 
    WebCamTexture _webCamera;
    public GameObject Plane;

    //tcp_client
    TCP_Client tc_;


    //标志位
    public static bool face_detected = false;


    /// 初始化摄像头
    ///  
    public IEnumerator InitCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("camera good");
            WebCamDevice[] devices = WebCamTexture.devices;
            DeviceName = devices[0].name;
            // _webCamera = new WebCamTexture(DeviceName, (int)CameraSize.x, (int)CameraSize.y, (int)CameraFPS);
            _webCamera = new WebCamTexture(DeviceName, 640, 480, 10);
            Debug.Log(CameraSize.x);
            Plane.GetComponent<MeshRenderer>().material.mainTexture = _webCamera;
            Plane.transform.localScale = new Vector3((float)0.4, (float)1, (float)0.3);
            _webCamera.Play();



            //StartCoroutine(NextScence());
        }
    }
    private void NextScence()

    {
        SceneManager.LoadScene("Scence_Lady");
        _webCamera.Stop();
        return;
    }

    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine(InitCamera());
        //tc_ = new TCP_Client();
        //tc_.InitServer();


    }

    //拍照函数
    private void TakePicture(string photo_path)
    {
        //yield /*return*/ new WaitForEndOfFrame();
        _webCamera.Pause();

        Texture2D texture = new Texture2D(_webCamera.width, _webCamera.height);
        texture.SetPixels(_webCamera.GetPixels(0, 0, _webCamera.width, _webCamera.height));
        texture.Apply();

        byte[] bt = texture.EncodeToPNG();

        System.IO.File.WriteAllBytes(photo_path, bt);
        Debug.Log("照片路径  " + photo_path);
        _webCamera.Play();

    }


    // Update is called once per frame
    float timer = 2.0f;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 2.0f;

            if (!face_detected)
            {
                //const string PhotoPath = "E:/UnityProjects/Beauty/Beauty/pic.png";
                //TakePicture(PhotoPath);
                //tc_.SendMes(PhotoPath);
                //tc_.ReceivedMessage();
            }
            else
            {
                NextScence();
            }
        }

    }


}
