using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
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
            var rgba = new byte[4];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, rgba);
                    var gray = .2126f * rgba[0] + .7152f * rgba[1] + .0722f * rgba[2];
                    var col = (byte)(gray < 128 ? 0 : 255);
                    rgba[0] = col;
                    rgba[1] = col;
                    rgba[2] = col;
                    dst.put(x, y, rgba);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}