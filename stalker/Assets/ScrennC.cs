using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum emScreenEffectType
{
    None,
    FaderToClear,
    FaderToBlack,
}
public class ScrennC : MonoBehaviour
{
    public Button m_Button;
    public Text m_Text;
    public emScreenEffectType effectType;
    public Image Screen;
    private float m_ScreenImagValue;
    public float m_ScreenSpeed;
    private void Awake()
    {
        effectType = emScreenEffectType.None;
        Screen = transform.Find("screenFader").GetComponent<Image>();
        StartCoroutine(setFaderToClear());
        m_Button.gameObject.SetActive(false);
    }
    
    

    //延迟两秒后执行
    IEnumerator setFaderToClear()
    {
        yield return new WaitForSeconds(0.8f);
        effectType = emScreenEffectType.FaderToClear;
    }

    // Update is called once per frame
    void Update()
    {
        if (effectType == emScreenEffectType.FaderToClear)
        {
            FaderToClear();
        }
        else if (effectType == emScreenEffectType.FaderToBlack)
        {
            FaderToBlack();
        }
        ShowResult();
    }

    public void ShowResult()
    {

        //成功失败的条件
        //成功:玩家取得钥匙前往撤离点
        //失败:玩家被敌人逮住了
        if (PlayerC.instance.WinKey)
        {
            FaderToBlack();
            m_Text.text = "挑战成功";
        }
        if (PlayerC.instance.PlayDie)
        {
            FaderToBlack();
            m_Text.text = "挑战失败";
          

        }

    }

    public void Click()
    {
        SceneManager.LoadScene(0);
    }

    private void FaderToClear()
    {
        m_ScreenImagValue = Mathf.Lerp(Screen.color.a, 0f, (Time.deltaTime * m_ScreenSpeed));
        Screen.color = new Color(Screen.color.r, Screen.color.g, Screen.color.b, m_ScreenImagValue);
        if (Screen.color.a <= 0.001f)
        {
            effectType = emScreenEffectType.None;
            Debug.Log("全黑到透明的任务完成");
        }
    }

    private void FaderToBlack()
    {
        m_Button.gameObject.SetActive(true);
        m_ScreenImagValue = Mathf.Lerp(Screen.color.a, 1f, (Time.deltaTime * m_ScreenSpeed));
        Screen.color = new Color(Screen.color.r, Screen.color.g, Screen.color.b, m_ScreenImagValue);
        if (Screen.color.a >= 0.999f)
        {
          
            Debug.Log("透明到全黑的任务完成");
            effectType = emScreenEffectType.None;
          

        }
    }
}
