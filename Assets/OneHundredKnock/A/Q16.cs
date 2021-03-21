using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// Sobelフィルタ
    /// エッジ抽出フィルタの1種
    /// prewittフィルタの中心部分に重みを付けたフィルタ
    ///          | 1  2  1|          | 1  0 -1|
    /// 縦: K =  | 0  0  0|   横: K = | 2  0 -2|
    ///         |-1  -2 -1|          | 1  0 -1|
    /// </summary>
    public class Q16 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var k_v = new[]
            {
                1f, 2f, 1f,
                0f, 0f, 0f,
                -1f, -2f, -1f
            };
            var k_h = new[]
            {
                1f, 0f, -1f,
                2f, 0f, -2f,
                1f, 0f, -1f
            };
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.filter2D(dst, dst, -1, new MatOfFloat(k_v));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}