using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// 平滑化フィルタ
    /// フィルタ内の画素の平均値を出力するフィルタ
    /// </summary>
    public class Q11 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat();
            var size = 3;
            Imgproc.blur(src, dst, new Size(size, size));
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}