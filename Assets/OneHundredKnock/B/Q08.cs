using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// Maxプーリング
    /// 平均値でなく最大値でプーリングする
    /// </summary>
    public class Q08 : MonoBehaviour
    {
        [SerializeField] private int _grid = 16;
        void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);

            for (var x = 0; x < src.width(); x += _grid)
            {
                for (var y = 0; y < src.height(); y += _grid)
                {
                    var col = new byte[4];
                    var maxCol = new byte[4];
                    
                    for (var dx = 0; dx < _grid; dx++)
                    {
                        for (var dy = 0; dy < _grid; dy++)
                        {
                            src.get(x+dx, y+dy, col);
                            for (var i = 0; i < col.Length; i++)
                            {
                                maxCol[i] = col[i] > maxCol[i] ? col[i] : maxCol[i];
                            }
                        }
                    }

                    for (var dx = 0; dx < _grid; dx++)
                    {
                        for (var dy = 0; dy < _grid; dy++)
                        {
                            dst.put(x+dx, y+dy, maxCol);
                        }
                    }
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}