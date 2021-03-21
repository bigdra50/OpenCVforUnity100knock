using System;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// ガウシアンフィルタ
    /// </summary>
    public class Q09 : MonoBehaviour
    {
        private Mat _src;
        private Mat _dst;
        [SerializeField] private int _ksize = 3;
        [SerializeField] private double _sigma = 1.3d;
        private Renderer _renderer;

        private void Start()
        {
            _src = Util.LoadTexture("imori_noise");
            _dst = new Mat();
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            var kernel = Imgproc.getGaussianKernel(_ksize, _sigma);
            Imgproc.filter2D(_src, _dst, -1, kernel);
            _renderer.material.mainTexture = Util.MatToTexture2D(_dst);
        }
    }
}