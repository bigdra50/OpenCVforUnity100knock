using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// グレースケール化
    /// 画像の輝度表現方法の一種で下式で計算される
    /// Y = 0.2126R + 0.7151G + 0.0722B
    /// </summary>
    public class Q02 : MonoBehaviour
    {
        void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}