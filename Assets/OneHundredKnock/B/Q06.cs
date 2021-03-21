using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// 画像の値を256^3から4^3, すなわちR,G,B in {32, 96, 160, 224}の各4値に減色する.(量子化操作)
    /// </summary>
    public class Q06 : MonoBehaviour
    {
        void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var color = new byte[4];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, color);
                    for (var i = 0; i < color.Length; i++)
                    {
                        switch (color[i] / 64)
                        {
                            case 0:
                                color[i] = 32;
                                break;
                            case 1:
                                color[i] = 96;
                                break;
                            case 2:
                                color[i] = 160;
                                break;
                            case 3:
                                color[i] = 224;
                                break;
                        }

                        dst.put(x, y, color);
                    }
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);


        }
    }
}