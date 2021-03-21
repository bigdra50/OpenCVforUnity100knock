using System;
using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// ヒストグラム正規化
    /// 画像を人の目に見やすくするために, ヒストグラムを正規化する
    /// </summary>
    public class Q21 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_dark", CvType.CV_8UC3);
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC3);
            var a = (byte) 0;
            var b = (byte) 255;
            var c = byte.MaxValue;
            var d = byte.MinValue;
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    var col = new byte[src.channels()];
                    src.get(x, y, col);
                    for (var ch = 0; ch < dst.channels() ; ch++)
                    {
                        c = Math.Min(c, col[ch]);
                        d = Math.Max(d, col[ch]);
                    }
                }
            }

            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    var col = new byte[src.channels()];
                    src.get(x, y, col);
                    for (var ch = 0; ch < dst.channels(); ch++)
                    {
                        if (col[ch] < c) col[ch] = a;
                        else if (c <= col[ch] && col[ch] < d)
                            col[ch] = (byte) (((b - a) / (d - c)) * (col[ch] - c) + a);
                        else col[ch] = b;
                    }

                    dst.put(x, y, col);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}