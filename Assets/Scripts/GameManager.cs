using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Runtime.InteropServices;



public class GameManager : MonoBehaviour {

    // Use this for initialization
    private GameObject obj;
    private Image image;
   // private Image[] images;
    private Sprite[]  BackGround;
    //private Sprite[] dressIcon;
    private Sprite[] dresses;
    private Sprite[] hairs;
    private string Dress = "Dress-",Hair="Hair-";
    private Transform ladyTrans, manTrans;
    //private Image curtainTrans;
    private int sliderJ;
    public int Changes;//换衣服2，换背景1，换发型3的状态变量
    public int sex;//男女生切换,男0女1,初始为2

 //   private SimpleQRCode simpleQRCode;


    void Start () {
        
        Changes = 0;
        sex=2;
        BackGround = Resources.LoadAll<Sprite>("BackGround");

        obj = GameObject.Find("lady");
        ladyTrans = obj.transform;
        obj.SetActive(false);       
        obj = GameObject.Find("man");
        manTrans = obj.transform;
        obj.SetActive(false);


        //GameObject.Find("CrutainEffect").GetComponent<Mask>().showMaskGraphic();

        ButtonsFalse();

        Texture2D m_texture= Resources.Load<Texture2D>("BackGround/bg21");

        GameObject.Find("BackGround").GetComponentInChildren<SpriteRenderer>().sprite =
            Sprite.Create(m_texture, new Rect(0, 0, m_texture.width, m_texture.height), Vector2.one *0.5f,280); ;
        // Resources.Load <Sprite>("BackGround/bg16");

        ButtonsFalse();

        //开启拍照线程
        // Thread thread = new Thread(GetHead);
        //thread.Start();
        StartCoroutine(GetHead());
        //SceneManager.UnloadScene("Scene_Lady");
     
        //string sceneName = SceneManager.GetActiveScene().name;

        //SceneManager.



    }
	
	// Update is called once per frame
	void Update () {

        //if (sex!=2)
        //{
        //    obj = GameObject.Find("Begin_Animation");
        //    obj.SetActive(false);
        //}


		
	}
    void ButtonsFalse() {
        obj = GameObject.Find("Button1");
        image = obj.GetComponentInChildren<Image>();
        image.enabled = false;
        obj = GameObject.Find("Button2");
        image = obj.GetComponentInChildren<Image>();
        image.enabled = false; obj = GameObject.Find("Button3");
        image = obj.GetComponentInChildren<Image>();
        image.enabled = false; obj = GameObject.Find("Button4");
        image = obj.GetComponentInChildren<Image>();
        image.enabled = false; obj = GameObject.Find("Button5");
        image = obj.GetComponentInChildren<Image>();
        image.enabled = false;
        GameObject.Find("QRCode").GetComponentInChildren<RawImage>().enabled = false;
        


    }
    void BGButtonsTure()  {

        
       
        for (int i = 1; i <= 5; i++)
        {
            string str = i.ToString();
            string ButtonPath = "Button" + str;
            obj = GameObject.Find(ButtonPath);//找到Button组件
            image = obj.GetComponentInChildren<Image>();
            image.enabled = true;
            image.sprite = BackGround[i + sliderJ-1];//替换Button 图片
        }

    }
    void DressButtonTrue(string manLady)
    {
        
        for (int i = 1; i <= 5; i++)
        {
            string str = i.ToString();
            string ButtonPath = "Button" + str;
            str = (i + sliderJ).ToString();
            string dressesIconPath = "Dresses/"+Dress + manLady + str+"/"+ Dress + manLady + str+"-icon";//
            //dressesIconPath = "Dresses/Dress-Lady-1/Dress-Lady-1-icon";


            Sprite sprit= Resources.Load<Sprite>(dressesIconPath);
          //  dressIcon[i] = sprit;
            obj = GameObject.Find(ButtonPath);//找到Button组件
            image = obj.GetComponentInChildren<Image>();
            image.enabled = true;
            image.sprite = sprit;//替换Button 图片
        }

    }
    void HairButtonTrue(string manLady)
    {
        for (int i = 1; i <=5; i++)
        {
            string str = i.ToString();
            string ButtonPath = "Button" + str;
            str = (i + sliderJ).ToString();
            string hairIconPath = "Hairs/" + Hair + manLady  + "/" + Hair + manLady +"-" +str + "-icon";//
            //hairIconPath = "Hairs/Hair-Lady/Hair-Lady-1-icon";


            Sprite sprit = Resources.Load<Sprite>(hairIconPath);
            //  dressIcon[i] = sprit;
            obj = GameObject.Find(ButtonPath);//找到Button组件
            image = obj.GetComponentInChildren<Image>();
            image.enabled = true;
            image.sprite = sprit;//替换Button 图片
        }

    }
    public void OnClikeChangBackGround()  {

        Changes = 1;
        sliderJ = 0;
        BGButtonsTure();
       // OnClikeButton();


    }
    public void OnClikeChangDresses()
    {
        Changes = 2;
        sliderJ = 0;


        //simpleQRCode = new SimpleQRCode();
        //simpleQRCode.NormalCreate();
        //GameObject.Find("Panel").GetComponentInChildren<SimpleQRCode>().code = "test";
        GameObject.Find("Panel").GetComponentInChildren<SimpleQRCode>().NormalCreate();
        GameObject.Find("QRCode").GetComponentInChildren<RawImage>().enabled = true;




        if (sex == 0)
        {
            string manLady = "Man-";
            manTrans.gameObject.SetActive(true);
            DressButtonTrue(manLady);
        }

        else if (sex == 1)
        {
            string manLady = "Lady-";
            ladyTrans.gameObject.SetActive(true);
            DressButtonTrue(manLady);


        }

        else
            return;


    }
    public void OnClikeChangHairs()
    {
        Changes = 3;
        sliderJ = 0;
        ChangeFace();
        if (sex == 0)//男0女1,初始为2
        {
            //disable head
            manTrans.gameObject.SetActive(true);
            obj = GameObject.Find("head_m");
            // obj.SetActive(false);
            obj.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("man_hair").GetComponent<SpriteRenderer>().enabled = true;
            string manLady = "Man";
            HairButtonTrue(manLady);

        }
        else if (sex==1)
        {
            ladyTrans.gameObject.SetActive(true);          

           obj = GameObject.Find("head_l");
            obj.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("lady_hair").GetComponent<SpriteRenderer>().enabled = true;
            // obj.SetActive(false);
            string manLady = "Lady";
            HairButtonTrue(manLady);

        }
        else
            return;
    }
    public void OnClikeButton(int index)
    {

        if(Changes==1)
        {
            ChangeBackGround(index);
            if (sliderJ + 5 >=8)//?该处18不可靠
            {
                sliderJ = 0;
            }
            else
                sliderJ++;
            BGButtonsTure();
        }
        if (Changes == 2)
        {
            if (sliderJ + 5 >=8)//?该处8不可靠
            {
                sliderJ = 0;
            }
            else
                sliderJ++;
            if (sex == 0)
            {
                ChangeDress(index, "man_");
                DressButtonTrue("Man-");
            }

            else if (sex == 1)
            {
                ChangeDress(index, "lady_");
                DressButtonTrue("Lady-");

            }
                 
            //else
            //    return;
            
            

        }
        if (Changes==3)
        {
            if (sliderJ + 5 >=8)//?该处8不可靠
            {
                sliderJ = 0;
            }
            else
                sliderJ++;
            if (sex == 0)
            {
                ChangeHair(index, "man_");
                HairButtonTrue("Man");
            }

            else if (sex == 1)
            {
                ChangeHair(index, "lady_");
                HairButtonTrue("Lady");

            }

        }
    

    }
    
    
    private void ChangeBackGround(int index)
    {

        obj = GameObject.Find("BackGround");
        SpriteRenderer sprite1 = obj.GetComponentInChildren<SpriteRenderer>();
        string str = "Button" + index.ToString();
        sprite1.sprite = GameObject.Find(str).GetComponentInChildren<Image>().sprite;


    }
    private void ChangeHair(int index,string sex)//sex=man_;lady_
    {

        string str = sex + "hair"; //man_hair; lady_hair
        obj = GameObject.Find(str);
        SpriteRenderer sprite1 = obj.GetComponentInChildren<SpriteRenderer>();
        
        str = "Button" + index.ToString();
        Sprite sprite = GameObject.Find(str).GetComponentInChildren<Image>().sprite;
        str = sprite.name;//Hair-Lady-0-icon
        string hairName= str.Substring(0, str.Length - 5);//Hair-Lady-0
        hairName = hairName.Substring(hairName.Length - 1, 1);//0
        string hairPath1= str.Substring(0, str.Length - 7);//Hair-Lady
        string hairPath2 = "hairs/" + hairPath1+"/"+ hairPath1;//hairs/hair
        hairs= Resources.LoadAll<Sprite>(hairPath2);

        //Sprite hair = hairs[index];
        //sprite1.sprite = hair;
        //Debug.Log(hair.name);
        foreach (Sprite hair in hairs)
        {
            string hairName1 = hair.name;//hair.name=Hair-Lady_0
            string hairNameIndex = hairName1.Substring(hairName1.Length - 1, 1);//0
            if (hairNameIndex == hairName)
                sprite1.sprite = hair;
        }
        
    }
    private void ChangeDress(int index, string sex)
    {

        string str = "Button" + index.ToString();
        Sprite sprite = GameObject.Find(str).GetComponentInChildren<Image>().sprite;
        str = sprite.name;
        str = str.Substring(0, str.Length - 5);//Dress-Lady-1
        string dressesPath = "Dresses/" + str+"/"+ str+ "-multi";//Dresses/Dress-Lady-1/Dress-Lady-1-multi
        dresses = Resources.LoadAll<Sprite>(dressesPath);
        for (int i=0;i<5;i++)
        {
            str = sex + i.ToString();
            obj = GameObject.Find(str);
            SpriteRenderer sprite1 = obj.GetComponentInChildren<SpriteRenderer>();
            sprite1.sprite = dresses[i];//

        }        
       
    }
    private void ChangeFace()
    {
        Texture2D m_texture = Resources.Load<Texture2D>("face-2");
        Sprite sprite1 = Sprite.Create(m_texture, new Rect(0, 0, m_texture.width, m_texture.height), new Vector2(0.5f,0.5f), 300);
        if (sex == 1)
            GameObject.Find("face_l").GetComponent<SpriteRenderer>().sprite = sprite1;
        if (sex == 0)
            GameObject.Find("face_m").GetComponent<SpriteRenderer>().sprite = sprite1;
        Debug.Log("goood");


    }
    // private void GetHead()
    private IEnumerator GetHead()
    {
        //for(long i=0;i<100000000;i++)
        
       sex =1;

        yield return new WaitForSeconds(3);
        

        Texture2D m_texture = Resources.Load<Texture2D>("head-2");
        Sprite spriteHead=Sprite.Create(m_texture, new Rect(0, 0, m_texture.width, m_texture.height), Vector2.one * 0.5f, 300);
        if (sex==1)
        {
            ladyTrans.gameObject.SetActive(true);
            GameObject.Find("head_l").GetComponentInChildren<SpriteRenderer>().sprite = spriteHead;
            GameObject.Find("lady_hair").GetComponent<SpriteRenderer>().enabled = false;
           // curtainTrans.gameObject.SetActive(true);
           // StartCoroutine(Timer(2));
           // curtainTrans.gameObject.SetActive(false);
        }
        if (sex==0)
        {
            manTrans.gameObject.SetActive(true);
            GameObject.Find("head_m").GetComponentInChildren<SpriteRenderer>().sprite = spriteHead;
            GameObject.Find("man_hair").GetComponent<SpriteRenderer>().enabled = false;

        }
       


         GameObject.Find("Begin_Animation").SetActive(false);
        // SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneManager.GetActiveScene().name));
        //SceneManager.LoadScene("test");
        //SceneManager.UnloadSceneAsync("test");
        //Invoke("SceneManager.LoadScene("Scene_Begin")", 2);
    }
    private IEnumerator Timer (int i)
    {
        yield return new WaitForSeconds(i);

    }


}
