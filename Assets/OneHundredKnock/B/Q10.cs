using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// メディアンフィルター
    /// 注目画素の3x3の領域内の、メディアン値(中央値)を出力するフィルタ
    /// </summary>
    public class Q10 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_noise");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var color = new byte[4];
            const int grid = 3;
            var srcRgb = new byte[src.width(), src.height(), 4];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, color);
                    for (var i = 0; i < 3; i++)
                    {
                        srcRgb[x, y, i] = color[i];
                    }
                }
            }

            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    var medRgb = new List<List<byte>>
                    {
                        new List<byte>(),
                        new List<byte>(),
                        new List<byte>(),
                    };
                    for (var dx = -grid / 2; dx <= grid / 2; dx++)
                    {
                        for (var dy = -grid / 2; dy <= grid / 2; dy++)
                        {
                            if (x + dx < 0 || x + dx >= src.width() || y + dy < 0 || y + dy >= src.height())
                                continue;
                            for (var i = 0; i < medRgb.Count; i++)
                            {
                                medRgb[i].Add(srcRgb[x + dx, y + dy, i]);
                            }
                        }
                    }

                    for (var i = 0; i < medRgb.Count; i++)
                    {
                        medRgb[i].Sort();
                        color[i] = medRgb[i][medRgb[i].Count / 2];
                    }

                    dst.put(x, y, color);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}