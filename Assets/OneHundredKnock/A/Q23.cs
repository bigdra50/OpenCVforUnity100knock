using System;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    public class Q23 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_dark");
            var dst = new Mat();
            //Imgproc.equalizeHist(src, src);
            //GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(src);

        }
    }
}