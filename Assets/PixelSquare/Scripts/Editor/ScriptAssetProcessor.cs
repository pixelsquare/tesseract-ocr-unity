//------------------------------------------------------------------------------
// <copyright file="ScriptAssetProcessor.cs" company="PixelSquare">
//    Copyright © 2018 PixelSquare
// </copyright>
// <author>Anthony Ganzon</author>
// <date></date>
// <summary></summary>
//------------------------------------------------------------------------------

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace PixelSquare.Scripts.Editor
{
    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public class ScriptAssetProcessor : UnityEditor.AssetModificationProcessor
    {
        private const string kNamespaceTitle = "PixelSquare";
        private const int kNamespaceMaxDepth = 3;

        private static string[] s_ValidExtensionNames = new string[]
        {
            ".cs",
            ".js",
            ".boo",
            ".shader",
            ".compute"
        };

        private static string[] s_EnvironmentVariableNames = new string[]
        {
            "AUTHOR_NAME"
        };

        public static void OnWillCreateAsset(string path)
        {
            string filePath = path.Replace(".meta", string.Empty);
            string extension = System.IO.Path.GetExtension(filePath);

            if(!IsValidExtensionName(extension))
            {
                return;
            }

            int assetIdx = Application.dataPath.LastIndexOf("Assets");
            string fullFilePath = Application.dataPath.Substring(0, assetIdx) + filePath;
            string file = System.IO.File.ReadAllText(fullFilePath);

            file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);
            file = file.Replace("#COPYRIGHTYEAR#", System.DateTime.Now.ToString("yyyy"));
            file = file.Replace("#CREATIONDATE#", System.DateTime.Now.ToString("yyyy/MM/dd"));

            file = ReplaceNamespaceVariables(filePath, file, "#NAMESPACE#");

            foreach(string envVarName in s_EnvironmentVariableNames)
            {
                file = ReplaceEnvironmentVariables(file, envVarName);
            }

            System.IO.File.WriteAllText(fullFilePath, file, System.Text.Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static bool IsValidExtensionName(string extension)
        {
            foreach(string ext in s_ValidExtensionNames)
            {
                if(extension.Contains(ext))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ReplaceEnvironmentVariables(string targetStr, string envVariableName)
        {
            string envVar = System.Environment.GetEnvironmentVariable(envVariableName, System.EnvironmentVariableTarget.User);

            if(!string.IsNullOrEmpty(envVar))
            {
                targetStr = targetStr.Replace("%" + envVariableName + "%", envVar);
            }           

            return targetStr;
        }

        private static string ReplaceNamespaceVariables(string filePath, string targetStr, string keyword)
        {
            string directoryPath = System.IO.Path.GetDirectoryName(filePath);

            bool isEditorFile = directoryPath.Contains("Editor");

            if(filePath.StartsWith("Assets"))
            {
                List<string> splitPath = new List<string>(directoryPath.Split('/'));
                splitPath.RemoveAll(str => str == kNamespaceTitle);

                string namespaceName = kNamespaceTitle;
                int namespaceDepth = 0;

                for(int i = 1; i < splitPath.Count; i++)
                {
                    if(!string.IsNullOrEmpty(namespaceName))
                    {
                        namespaceName += ".";
                    }

                    namespaceName += splitPath[i];

                    if(++namespaceDepth >= kNamespaceMaxDepth)
                    {
                        break;
                    }
                }

                if(isEditorFile)
                {
                    namespaceName += ".Editor";
                }

                targetStr = targetStr.Replace(keyword, namespaceName);
            }
            else
            {
                targetStr = targetStr.Replace(keyword, kNamespaceTitle + ".Unknown");
            }

            return targetStr;
        }
	}

} // PixelSquare.Scripts.Editor

#endif