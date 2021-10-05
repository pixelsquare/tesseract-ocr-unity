//------------------------------------------------------------------------------
// <copyright file="CoroutineRunner.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/03</date>
// <summary>Utility that runs a coroutine on a non-monobehavior script</summary>
//------------------------------------------------------------------------------

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR.Utility
{
    /// <summary>
    /// Utility that runs a coroutine on a non-monobehavior script
    /// </summary>
    /// <remarks></remarks>
    public class CoroutineRunner : MonoBehaviour
    {
        /// <summary>
        /// Runs a coroutine
        /// </summary>
        /// <param name="coroutine">Coroutine</param>
        /// <returns>Coroutine</returns>
        public static Coroutine RunCoroutine(IEnumerator coroutine)
        {
            GameObject go = new GameObject("runner");
            go.hideFlags = HideFlags.HideInHierarchy;
            CoroutineRunner runner = go.AddComponent<CoroutineRunner>();
            return runner.StartCoroutine(runner.UpdateRunner(coroutine));
        }

        /// <summary>
        /// Handles the update of the coroutine
        /// And destroys it after its done
        /// </summary>
        /// <param name="coroutine">Coroutine</param>
        /// <returns>IEnumerator</returns>
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