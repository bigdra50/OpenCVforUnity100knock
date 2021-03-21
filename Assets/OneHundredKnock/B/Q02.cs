using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// グレースケール化
    /// 画像の輝度表現方法の一種で下式で計算される
    /// Y = 0.2126R + 0.7151G + 0.0722B
    /// </summary>
    public class Q02 : MonoBehaviour
    {
        void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);

            var rgba = new byte[4];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, rgba);
                    var gr = .2126f * rgba[0] + .7152f * rgba[1] + .0722f * rgba[2];
                    rgba[0] = (byte)gr;
                    rgba[1] = (byte)gr;
                    rgba[2] = (byte)gr;
                    dst.put(x, y, rgba);
                }
            }
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}