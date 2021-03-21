using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// LoG(Laplacian of Gaussian)フィルタ
    /// ガウシアンフィルタで平滑化した後にラプラシアンフィルタで輪郭を取り出すフィルタ
    /// Laplacianは2次微分をとるのでノイズが強調されるのを防ぐために, 予めGaussianでノイズを抑える
    /// 
    ///     |0  0  1  0  0|
    ///     |0  1  2  1  0|
    /// K = |1  2 -16 2  1|
    ///     |0  1  2  1  0|
    ///     |0  0  1  0  0|
    /// </summary>
    public class Q19 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_noise");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var k = new[]
            {
                0f, 0f, 1f, 0f, 0f,
                0f, 1f, 2f, 1f, 0f,
                1f, 2f, -16f, 2f, 1f,
                0f, 1f, 2f, 1f, 0f,
                0f, 0f, 1f, 0f, 0f,
            };
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2GRAY);
            //Imgproc.GaussianBlur(gray, gauss, new Size(5,5), 3d);
            //Imgproc.Laplacian(gauss, dst, -1, 5);
            Imgproc.filter2D(dst, dst, -1, new MatOfFloat(k));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}