using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// 微分フィルタ
    /// 輝度の急激な変化が起こっている部分のエッジを取り出すフィルタ
    /// 隣り合う画素同士の差を取る
    ///          |0 -1  0|           | 0  0  0|
    /// 縦: K =  |0  1  0|   横: K = |-1  1  0|
    ///         |0  0  0|           | 0  0  0|
    /// </summary>
    public class Q14 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var k_v = new[]
            {
                0f, -1f, 0f,
                0f, 1f, 0f,
                0f, 0f, 0f
            };
            var k_h = new[]
            {
                0f, 0f, 0f,
                -1f, 1f, 0f,
                0f, 0f, 0f
            };
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.filter2D(dst, dst, -1, new MatOfFloat(k_v));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}