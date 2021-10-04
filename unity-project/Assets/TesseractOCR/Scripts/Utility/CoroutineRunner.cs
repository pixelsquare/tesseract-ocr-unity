//------------------------------------------------------------------------------
// <copyright file="CoroutineRunner.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/03</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR.Utility
{
    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public class CoroutineRunner : MonoBehaviour
    {
        public static Coroutine RunCoroutine(IEnumerator coroutine)
        {
            GameObject go = new GameObject("runner");
            go.hideFlags = HideFlags.HideInHierarchy;
            CoroutineRunner runner = go.AddComponent<CoroutineRunner>();
            return runner.StartCoroutine(runner.UpdateRunner(coroutine));
        }

        private IEnumerator UpdateRunner(IEnumerator coroutine)
        {
            while(coroutine.MoveNext())
            {
                yield return coroutine.Current;
            }

            Destroy(gameObject);
        }
    }

} // namespace PixelSquare