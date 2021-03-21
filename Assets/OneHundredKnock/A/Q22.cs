using System;
using System.Linq;
using System.Runtime.Serialization;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.UI;

namespace OneHundredKnock.A
{
    /// <summary>
    /// ヒストグラム操作
    /// ヒストグラムの平均値をm0=128, 標準偏差をs0=52になるように操作する
    /// これはヒストグラムのダイナミックレンジを変更するのでなく, ヒストグラムを平坦に変更する操作である
    /// </summary>
    public class Q22 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_dark", CvType.CV_8UC3);
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC3);
            var meanMat = new MatOfDouble();
            var stdDevMat = new MatOfDouble();
            var s0 = (byte) 52;
            var m0 = (byte) 128;
            Core.meanStdDev(src, meanMat, stdDevMat);
            var meansEachColor = meanMat.toArray();
            var stdDevsEachColor = stdDevMat.toArray();
            var totalMean = (int) meansEachColor.Average();
            var totalStdDev = (int) Math.Sqrt((stdDevsEachColor.Select(x => x * x).Sum() +
                                                meansEachColor.Select(x => x * x).Sum()) / 3d - totalMean * totalMean);

            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    var col = new byte[src.channels()];
                    src.get(x, y, col);
                    for (var c = 0; c < src.channels(); c++)
                    {
                        col[c] = (byte) (s0 / totalStdDev * (col[c] - totalMean) + m0);
                    }

                    dst.put(x, y, col);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}