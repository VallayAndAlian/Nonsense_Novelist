using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSaveImage : MonoBehaviour
{
    /// <summary>
    /// ���������Ƭ������
    /// </summary>
    /// <param name="photographyCamera">���</param>
    /// <param name="width">ͼ����</param>
    /// <param name="height">ͼ��߶�</param>
    /// <param name="path">����·��</param>
    /// <param name="imageName">����ͼƬ����</param>
    public void CreateCameraCaptureAndSaveLocal(Camera photographyCamera, int width, int height, string path, string imageName)
    {
        // ����֮ǰ�� RenderTexture �� Texture2D
        if (photographyCamera.targetTexture != null)
        {
            RenderTexture.ReleaseTemporary(photographyCamera.targetTexture);
            photographyCamera.targetTexture = null;
            RenderTexture.active = null;
        }

        // ���� RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        photographyCamera.targetTexture = rt;
        GL.Clear(true, true, Color.clear); // �����ɫ����Ȼ�����
        photographyCamera.Render();
        RenderTexture.active = rt;

        // ���� Texture2D ����ȡͼ������
        Texture2D image = new Texture2D(width, height, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        // ��Ҫ���� targetTexture ����Ϊ null���Ա����������Ⱦ������Ļ
        photographyCamera.targetTexture = null;
        RenderTexture.active = null;

        // ��鱣��·���Ƿ�Ϊ�ջ���Ч
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Invalid save path.");
            return;
        }

        // ����ļ��в����ڣ��򴴽��ļ���
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }

        // ����ͼ�񵽱����ļ���

        byte[] bytes = image.EncodeToJPG();
        if (bytes != null)
        {
            string savePath = System.IO.Path.Combine(path, imageName + ".jpg");

            try
            {
                System.IO.File.WriteAllBytes(savePath, bytes);
                Debug.Log("Image saved successfully: " + savePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error saving image: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to encode image to JPG.");
        }


       



    }


}
