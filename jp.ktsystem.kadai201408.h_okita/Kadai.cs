using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jp.ktsystem.kadai201408.h_okita
{
    /// <summary>
    /// kadaiクラス
    /// </summary>
    public class Kadai
    {
        /// <summary>文字エンコード </summary>
        private static readonly Encoding ENCODING = Encoding.UTF8;

        /// <summary>
        /// 勉強会課題（201408）問1
        /// </summary>
        /// <param name="anInputPath">入力ファイルパス</param>
        /// <returns>データの点数</returns>
        public static long CalcScoreSum(string anInputPath)
        {
            //ファイルパスがnullおよび空文字、ファイル存在しない場合
            if (Utility.IsNullOrEmpty(anInputPath) || !Utility.IsFileExists(anInputPath))
            {
                throw new KadaiException(ErrorCode.FILE_IO_ERRER);
            }

            //一行読み込み
            string aLine = ReadLine(anInputPath);

            //ファイルが空だった場合
            if(Utility.IsNullOrEmpty(aLine))
            {
                return 0;
            }

            //データの点数計算
            return GetTotalScore(aLine);
        }

        /// <summary>
        /// ファイルから一行読み込み、読み込んだ文字列を返す
        /// </summary>
        /// <param name="anInputPath">ファイルパス</param>
        /// <returns>一行分の文字列</returns>
        private static string ReadLine(string anInputPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(anInputPath, ENCODING))
                {
                    return sr.ReadLine();
                }
            }
            catch
            {
                throw new KadaiException(ErrorCode.FILE_IO_ERRER);
            }
        }

        /// <summary>
        /// 文字列からデータの点数の積の合計を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>データの点数</returns>
        private static long GetTotalScore(string str)
        {
            int n = 1;  //データの出現位置
            int data = 0;    //データの点数
            long totalScore = 0; //データの点数の積の合計
            foreach (char c in str)
            {
                if (Utility.IsComma(c))
                {
                    totalScore += data * n;

                    n++;
                    data = 0;
                    continue;
                }
                //データの内部が半角英字以外の場合
                if (!Utility.IsHalfWidthAlphabet(c))
                {
                    throw new KadaiException(ErrorCode.INVALID_DATA_STRING);
                }
                //大文字変換
                char upperC = char.ToUpper(c);

                data += GetAlphabetScore(upperC);
            }
            totalScore += data * n;

            return totalScore;
        }

        /// <summary>
        /// 勉強会課題（201408）問2
        /// </summary>
        /// <param name="anInputPath">入力ファイルパス</param>
        /// <param name="anOutputPath">出力ファイルパス</param>
        public static void PrintMaxScore(string anInputPath, string anOutputPath)
        {
            //入力ファイルパスがnullおよび空文字、ファイル存在しない場合
            if (Utility.IsNullOrEmpty(anInputPath) || !Utility.IsFileExists(anInputPath))
            {
                throw new KadaiException(ErrorCode.FILE_IO_ERRER);
            }

            //出力ファイルパスがnullおよび空文字の場合
            if (Utility.IsNullOrEmpty(anOutputPath))
            {
                throw new KadaiException(ErrorCode.FILE_IO_ERRER);
            }

            //一行読み込み
            string aLine = ReadLine(anInputPath);

            //ファイルが空だった場合
            if (Utility.IsNullOrEmpty(aLine))
            {
                aLine =string.Empty;
            }

            //最大点数リストの取得
            List<MaxDataModel> maxList = GetMaxData(aLine);

            //最大点数をファイルへ書き込む
            OutputData(maxList, anOutputPath);
        }

        /// <summary>
        /// 文字列からデータの最大点数のリストを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>最大点数のリスト</returns>
        private static List<MaxDataModel> GetMaxData(string str)
        {
            int n = 1;  //データの出現位置
            StringBuilder sb = new StringBuilder();  //データの内容
            int data = 0;    //データの点数
            int maxData = 0;    //最大点数
            List<MaxDataModel> maxList = new List<MaxDataModel>();
            foreach (char c in str)
            {
                if (Utility.IsComma(c))
                {
                    if (maxData < data)
                    {
                        //最大値の更新、およびリストのクリア
                        maxData = data;
                        maxList.Clear();
                    }
                    //リストに追加
                    if (maxData == data)
                    {
                        maxList.Add(new MaxDataModel(n, sb.ToString(), maxData));
                    }

                    n++;
                    sb.Clear();
                    data = 0;
                    continue;
                }
                //データの内部が半角英字以外の場合
                if (!Utility.IsHalfWidthAlphabet(c))
                {
                    throw new KadaiException(ErrorCode.INVALID_DATA_STRING);
                }

                sb.Append(c);
                //大文字変換
                char upperC = char.ToUpper(c);
                data += GetAlphabetScore(upperC);
            }
            if (maxData < data)
            {
                //最大値の更新、およびリストのクリア
                maxData = data;
                maxList.Clear();
            }
            //リストに追加
            if (maxData == data)
            {
                maxList.Add(new MaxDataModel(n, sb.ToString(), maxData));
            }

            return maxList;
        }

        /// <summary>
        /// 最大点数をファイルに出力する
        /// </summary>
        /// <param name="maxList">最大点数情報のリスト</param>
        /// <param name="anOutputPath">出力ファイルパス</param>
        private static void OutputData(List<MaxDataModel> maxList, string anOutputPath)
        {
            using (StreamWriter sr = new StreamWriter(anOutputPath, false, ENCODING))
            {
                foreach (MaxDataModel model in maxList)
                {
                    sr.WriteLine("[{0}]:[{1}]:[{2}]", model.N, model.DataStr, model.DataScore);
                }
            }
        }


        /// <summary>
        /// 英字の点数を取得する
        /// </summary>
        /// <param name="c">英字</param>
        /// <returns>点数</returns>
        private static int GetAlphabetScore(char c)
        {
            return (int)c - (int)'A' + 1;
        }

       
        /// <summary>
        /// 問2・最大値出力用データ情報モデル
        /// </summary>
        private class MaxDataModel
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="n">データの出現位置</param>
            /// <param name="dataStr">データの内容</param>
            /// <param name="data">データの点数</param>
            public MaxDataModel(int n, string dataStr, int dataScore)
            {
                _n = n;
                _dataStr = dataStr;
                _dataScore = dataScore;
            }

            /// <summary>
            /// データの出現位置
            /// </summary>
            public string N
            {
                get
                {
                    return _n.ToString();
                }
            }
            private int _n;

            /// <summary>
            /// データの内容
            /// </summary>
            public string DataStr
            {
                get
                {
                    return _dataStr;
                }
            }
            private string _dataStr;

            /// <summary>
            /// データの点数
            /// </summary>
            public string DataScore
            {
                get
                {
                    return _dataScore.ToString();
                }
            }
            private int _dataScore;
        }
    }
}
