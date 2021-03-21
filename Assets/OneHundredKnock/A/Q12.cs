using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// モーションフィルタ
    /// 対角方向の平均値を取るフィルタ
    ///     |1/3 0  0|
    /// k = |0  1/3 0|
    ///     |0  0 1/3|
    /// </summary>
    public class Q12 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat();
            var kernel = new MatOfFloat(
                1f / 3f, 0f, 0f,
                0f, 1f / 3f, 0f,
                0f, 0f, 1f / 3f);
            Imgproc.filter2D(src, dst, -1, kernel);
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}