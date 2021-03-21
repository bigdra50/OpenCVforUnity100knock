using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// Laplacianフィルタ
    /// エッジ抽出フィルタの1種
    /// 輝度の2次微分
    ///      | 0  1  0|
    /// K =  | 1 -4  1| 
    ///      | 0  1  0|
    /// </summary>
    public class Q17 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var k = new[]
            {
                0f, 1f, 0f,
                1f, -4f, 1f,
                0f, 1f, 0f
            };
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.filter2D(dst, dst, -1, new MatOfFloat(k));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}