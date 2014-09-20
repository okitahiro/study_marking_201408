using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace jp.ktsystem.kadai201408.h_okita
{
    /// <summary>
    /// ユーティリティークラス
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 文字列がnullまたは空文字かどうかチェックする
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>nullまたは空文字：true、それ以外：false</returns>
        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// ファイルの存在チェック
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ファイルが存在：true、それ以外：false</returns>
        public static bool IsFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        ///// <summary>
        ///// 文字列が半角英字のみかどうか調べる
        ///// </summary>
        ///// <param name="str">文字列</param>
        ///// <returns>半角英字のみ：true、それ以外：false</returns>
        //public static bool IsHalfWidtAlphabet(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return false;
        //    }

        //    foreach (char c in str)
        //    {
        //        if (!IsHalfWidthAlphabet(c))
        //        {
        //            //半角英数字およびコンマが含まれているとき
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        /// <summary>
        /// 文字が半角英字のみかどうか調べる
        /// </summary>
        /// <param name="c">調べる文字</param>
        /// <returns>半角英字：true、それ以外：false</returns>
        public static bool IsHalfWidthAlphabet(char c)
        {
            return ('A' <= c && 'Z' >= c) || ('a' <= c && 'z' >= c);
        }

        /// <summary>
        /// コンマかどうかチェックする
        /// </summary>
        /// <param name="c">調べる文字</param>
        /// <returns>コンマ：true、それ以外：false</returns>
        public static bool IsComma(char c)
        {
            return (',' == c);
        }

    }
}
