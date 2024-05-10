using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Blood : MonoBehaviour
{
    private Slider bloodSlider;
    private Text bloodText;
    // Start is called before the first frame update
    public float orange=0.9f;//��ɳ�ɫ����������ֵ
    public float red=0.8f;//��ɺ�ɫ����������ֵ
    public float SpeedCut = 0.2f;//�ٶȱ����ľ�����ֵ

    public playerWalk playerSpeed;
    private int flag = 1;//�ж��ٶ��Ƿ������
    void Start()
    {
        bloodSlider = GetComponent<Slider>();
        GameObject bloodObject = GameObject.FindGameObjectWithTag("blood");
        if (bloodObject != null)
        {
            bloodText = bloodObject.GetComponent<Text>();
            if (bloodText == null)
            {
                Debug.LogError("Player object does not have an Text component");
            }
        }
        else
        {
            Debug.LogError("Player object not found in the scene");
        }
        
        bloodText.text = PlayerPrefs.GetInt("BloodY", 0).ToString();//��PlayerPrefs���ؽ�Ǯ��ֵ�����û����Ĭ��Ϊ0
    }

    // Update is called once per frame
    void Update()
    {
        int i= int.Parse(bloodText.text);
        bloodSlider.value = i * 0.01f;

        if (bloodSlider.value < orange&&bloodSlider.value>=red)
        {
            bloodSlider.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("orangee"); 
        }
        else if (bloodSlider.value < red&&bloodSlider.value>0)
        {
            bloodSlider.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("redd");
        }
        else if (bloodSlider.value ==0)
        {
            bloodSlider.fillRect.GetComponent<Image>().color = new Color(1,0,0,0);
            //������Ϊ0�и�������֮��xxxx���͵�
            SceneManager.LoadScene("firstLevel");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
            bloodText.text = 100.ToString();
        }
        if (bloodSlider.value < SpeedCut&&flag==1)
        {
            playerSpeed.moveSpeed = playerSpeed.moveSpeed *0.5f;
            flag = 2;
        }
    }
    //Ѫ�����ٵĴ��룬��������
    public void decreseBlood(int amount)
    {
        int blood;
        blood = int.Parse(bloodText.text);
        blood -= amount;
        PlayerPrefs.SetInt("BloodY", blood); // ����PlayerPrefs�е�ֵ
        bloodText.text = blood.ToString(); // ����UI��ʾ
    }
}