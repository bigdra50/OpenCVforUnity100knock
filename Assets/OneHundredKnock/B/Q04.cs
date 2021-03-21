using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// 大津の2値化
    /// 2値化における分離の閾値を自動決定するアルゴリズム. クラス内分散とクラス間分散の比から計算される
    /// </summary>
    public class Q04 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = BinarizeByOtsu(src);
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }

        private float[,] ToGrayScale(Mat srcMat)
        {
            var rgba = new byte[4];
            var grs = new float[srcMat.rows(), srcMat.cols()];
            for (var x = 0; x < srcMat.width(); x++)
            {
                for (var y = 0; y < srcMat.height(); y++)
                {
                    srcMat.get(x, y, rgba);
                    grs[x, y] = .2126f * rgba[0] + .7152f * rgba[1] + .0722f * rgba[2];
                }
            }

            return grs;
        }

        private int GetThreshByOtsu(float[,] grs, int pNum)
        {
            var (thresh, max) = (1, 0f);
            for (var t = 1; t < 255; t++)
            {
                var m0 = 0f; // 各クラス内の画素の輝度の平均
                var m1 = 0f;
                var p0 = 0; // 各クラスに含まれる画素数
                var p1 = 0;
                foreach (var gr in grs)
                {
                    if (gr < t)
                    {
                        // class 0
                        p0++;
                        m0 += gr;
                    }
                    else
                    {
                        // class 1
                        p1++;
                        m1 += gr;
                    }
                }

                m0 /= p0;
                m1 /= p1;
                var r0 = p0 / (float) pNum;
                var r1 = p1 / (float) pNum;
                var sbsb = r0 * r1 * (m0 - m1) * (m0 - m1); // クラス間分散

                if (sbsb < max) continue;
                thresh = t;
                max = sbsb;
            }

            return thresh;
        }

        private Mat BinarizeByOtsu(Mat srcMat)
        {
            var grs = ToGrayScale(srcMat);
            var pNum = srcMat.rows() * srcMat.cols();
            var thresh = GetThreshByOtsu(grs, pNum);

            var dstMat = new Mat(srcMat.rows(), srcMat.cols(), CvType.CV_8UC4);
            var rgba = new byte[4];
            for (var x = 0; x < srcMat.width(); x++)
            {
                for (var y = 0; y < srcMat.height(); y++)
                {
                    var col = (byte) (grs[x, y] < thresh ? 0 : 255);
                    rgba[0] = col;
                    rgba[1] = col;
                    rgba[2] = col;
                    dstMat.put(x, y, rgba);
                }
            }

            return dstMat;
        }
    }
}