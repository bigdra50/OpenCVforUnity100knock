using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// 2値化
    /// 画像を黒と白の2値で表現する方法.
    /// ここではグレースケールにおいて閾値を128に設定して2値化する
    /// </summary>
    public class Q03 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.threshold(dst, dst, 128, 255, Imgproc.THRESH_BINARY);
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}