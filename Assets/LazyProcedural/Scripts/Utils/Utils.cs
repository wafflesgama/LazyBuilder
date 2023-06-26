using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazyProcedural
{

    public static class Utils
    {
        public static int GetRandomSeed()
        {
            return DateTime.UtcNow.Hour - DateTime.UtcNow.Millisecond + DateTime.UtcNow.Second;
        }
        public static string Capitalize(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        public static string SeparateCase(this string source)
        {
            string[] splittedWords = Regex.Split(source, @"(?<=[a-z])(?=[A-Z])");

            //string[] splittedWords = Regex.Split(source, @"(?<!^)(?=[A-Z])");
            string final = "";

            foreach (var splittedWord in splittedWords)
            {
                if (splittedWord.Length == 0) continue;
                final += splittedWord + " ";
            }
            //Remove last space
            final.Remove(final.Length - 1, 1);

            return final;
        }

        public static string AbsoluteFormat(this string source)
        {
            return source.Replace('/', '\\');
        }

        public static string RelativeFormat(this string source)
        {
            return source.Replace('\\', '/');
        }

        public static char Upper(this char source)
        {
            return char.ToUpper(source);
        }

        public static string ToFullString(this List<int> list)
        {
           return  string.Join(",", list.ToArray());
        }


        public static string[] GetFiles(string path, bool createDir = false)
        {
            if (!Directory.Exists(path))
            {
                if (createDir)
                    Directory.CreateDirectory(path);
                else
                    return new string[0];
            }

            DirectoryInfo tmpInfo = new DirectoryInfo(path);
            FileInfo[] files = tmpInfo.GetFiles();

            //Get all file (exluding meta files) names sorted by the modification date
            return files
                .Where(x => !x.Extension.Contains("meta"))
                .OrderBy(f => f.LastWriteTime)
                .Select(f => f.Name)
                .ToArray();
        }

        public static PopupField<string> CreateDropdownField(VisualElement parentElement, string name = null)
        {
            PopupField<string> field = new PopupField<string>();

            if (name != null)
                field.name = name;

            field.style.flexGrow = parentElement.style.flexGrow;
            field.style.flexWrap = parentElement.style.flexWrap;
            //field.style.visibility= parentElement.style.visibility;
            //field.style.opacity= parentElement.style.opacity;
            field.style.width = new StyleLength(Length.Percent(100));
            field.style.height = new StyleLength(Length.Percent(100));

            parentElement.Add(field);
            return field;

        }

        public static Material ConvertToDefault(this Material material)
        {
            Shader defaultShader;
            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
                defaultShader = AssetDatabase.GetBuiltinExtraResource<Shader>("Standard.shader");
            else
                defaultShader = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.defaultShader;

            material.shader = defaultShader;
            return material;

        }

        public static Texture2D FetchImage(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
            }
            return tex;
        }


        public static void TryInvoke(this Action action)
        {
            if (action != null)
                action();

        }
    }



}
