using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Spine.Unity;

public class jieAnim : MonoBehaviour {

	public Camera m_Camera;
    private string filePath="E:\\arknightsAnim\\";
    private bool m_IsEnableAlpha = true;
    private CameraClearFlags m_CameraClearFlags;
    private int i = 0;
    private int j = 0;
    public GameObject anim;
    private SkeletonAnimation aa;

    private void Start()
    {
        aa = anim.GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        j++;
        if (j == 20)
        {
            anim.SetActive(true);
            aa.state.Complete += delegate
            {
                gameObject.SetActive(false);
            };
        }
        if (j <= 20) return;
        i++;
        TakeShot();
    }

    private void TakeShot()
    {
        m_CameraClearFlags = m_Camera.clearFlags;
        if (m_IsEnableAlpha)
        {
            m_Camera.clearFlags = CameraClearFlags.Depth;
        }

        int resolutionX = (int) Handles.GetMainGameViewSize().x;
        int resolutionY = (int) Handles.GetMainGameViewSize().y;
        RenderTexture rt = new RenderTexture(resolutionX, resolutionY, 24);
        m_Camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resolutionX, resolutionY, TextureFormat.ARGB32, false);
        m_Camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resolutionX, resolutionY), 0, 0);
        m_Camera.targetTexture = null;
        RenderTexture.active = null;
        m_Camera.clearFlags = m_CameraClearFlags;
        //Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string fileName = filePath + i.ToString() + ".png";
        File.WriteAllBytes(fileName, bytes);
        Debug.Log("截图成功");
    }
}
