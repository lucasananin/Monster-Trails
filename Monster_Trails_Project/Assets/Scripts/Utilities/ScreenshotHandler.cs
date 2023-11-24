//using Sirenix.OdinInspector;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public class ScreenshotHandler : MonoBehaviour
    {
        //[Title("// General")]
        [SerializeField, Range(1, 9)] int _screenshotScale = 1;
        //[SerializeField, Range(0f, 1f)] float _timeScale = 1f;

        //[Title("// Transparent Screenshot")]
        [SerializeField] int _width = 1024;
        [SerializeField] int _height = 1024;

        private void Update()
        {
            //Time.timeScale = _timeScale;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeScreenshot();
            }
        }

        //[Button("Take Screenshot ()")]
        [Button]
        public void TakeScreenshot()
        {
            string _filename = string.Format("Assets/Screenshots/capture_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));
            VerifyDirectory();

            ScreenCapture.CaptureScreenshot(_filename, _screenshotScale);
            Debug.Log($"// {_filename} Taken!");
        }

        //[Button("Take Transparent ScreenShot ()")]
        public void TakeTransparentScreenshot()
        {
            string _filename = string.Format("Assets/Screenshots/capture_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));
            VerifyDirectory();

            TakeTransparentScreenshot(Camera.main, _width, _height, _filename);
            Debug.Log($"// {_filename} Taken!");
        }

        private void TakeTransparentScreenshot(Camera cam, int width, int height, string savePath)
        {
            // Depending on your render pipeline, this may not work.
            var bak_cam_targetTexture = cam.targetTexture;
            var bak_cam_clearFlags = cam.clearFlags;
            var bak_RenderTexture_active = RenderTexture.active;

            var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
            // Must use 24-bit depth buffer to be able to fill background.
            var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            var grab_area = new Rect(0, 0, width, height);

            RenderTexture.active = render_texture;
            cam.targetTexture = render_texture;
            cam.clearFlags = CameraClearFlags.SolidColor;

            // Simple: use a clear background
            cam.backgroundColor = Color.clear;
            cam.Render();
            tex_transparent.ReadPixels(grab_area, 0, 0);
            tex_transparent.Apply();

            // Encode the resulting output texture to a byte array then write to the file
            byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
            File.WriteAllBytes(savePath, pngShot);

            cam.clearFlags = bak_cam_clearFlags;
            cam.targetTexture = bak_cam_targetTexture;
            RenderTexture.active = bak_RenderTexture_active;
            RenderTexture.ReleaseTemporary(render_texture);
            Destroy(tex_transparent);
        }

        private void VerifyDirectory()
        {
            if (!Directory.Exists("Assets/Screenshots"))
            {
                Directory.CreateDirectory("Assets/Screenshots");
            }
        }
    }
}
