using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// Embossフィルタ
    /// 輪郭部分を浮き出しにするフィルタ
    ///      |-2 -1  0|
    /// K =  |-1  1  1| 
    ///      | 0  1  2|
    /// </summary>
    public class Q18 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var k = new[]
            {
                -2f, -1f, 0f,
                -1f, 1f, 1f,
                0f, 1f, 2f
            };
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.filter2D(dst, dst, -1, new MatOfFloat(k));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}