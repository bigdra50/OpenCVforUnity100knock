using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// ガウシアンフィルタ
    /// 画像の平滑化を行うフィルタの一種であり， ノイズ除去にも使われる.
    /// ガウシアンフィルタは注目がその周辺画素を, ガウス分布による重み付けで平滑化する.
    ///
    /// </summary>
    public class Q09 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_noise");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var kernel = new double[,]
            {
                {1d / 16d, 2d / 16d, 1d / 16d,},
                {2d / 16d, 4d / 16d, 2d / 16d,},
                {1d / 16d, 2d / 16d, 1d / 16d,},
            };

            var col = new byte[4];
            var srcRgb = new byte[src.width(), src.height(), 3];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, col);
                    for (var i = 0; i < srcRgb.GetLength(2); i++)
                    {
                        srcRgb[x, y, i] = col[i];
                    }
                }
            }

            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    var additive = new byte[4];
                    for (var dx = -kernel.GetLength(0) / 2; dx <= kernel.GetLength(0) / 2; dx++)
                    {
                        for (var dy = -kernel.GetLength(0) / 2; dy <= kernel.GetLength(0) / 2; dy++)
                        {
                            if (x + dx < 0 || x + dx >= src.width() || y + dy < 0 || y + dy >= src.height())
                                continue;
                            for (var i = 0; i < additive.Length - 1; i++)
                            {
                                var tmp1 = srcRgb[x + dx, y + dy, i];
                                var tmp2 = kernel[dx + kernel.GetLength(0) / 2, dy + kernel.GetLength(0) / 2];
                                additive[i] += (byte) (tmp1 * tmp2);
                            }
                        }
                    }

                    dst.put(x, y, additive);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}