using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// 大津の2値化
    /// 2値化における分離の閾値を自動決定するアルゴリズム. クラス内分散とクラス間分散の比から計算される
    /// </summary>
    public class Q04 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            Imgproc.threshold(src, dst, 0, 255, Imgproc.THRESH_OTSU);
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}