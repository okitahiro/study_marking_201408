using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jp.ktsystem.kadai201408.h_okita
{
    [TestClass]
    public class Lv1Test
    {
        private static readonly string PASS = System.IO.Directory.GetCurrentDirectory() + "\\Lv1\\Input\\";

        [TestMethod]
        public void N001()
        {
            AssertEquals("001.txt", 0);
        }

        [TestMethod]
        public void N002()
        {
            AssertEquals("002.txt", 1);
        }

        [TestMethod]
        public void N003()
        {
            AssertEquals("003.txt", 4);
        }

        [TestMethod]
        public void N004()
        {
            AssertEquals("004.txt", 40);
        }

        [TestMethod]
        public void N005()
        {
            AssertEquals("005.txt", 1567);
        }

        [TestMethod]
        public void E101()
        {
            AssertFail(null, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E102()
        {
            AssertFail("", ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E103()
        {
            string filePass = PASS + "dummy.txt";
            AssertFail(filePass, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E104()
        {
            string filePass = PASS + "104.txt";
            AssertFail(filePass, ErrorCode.INVALID_DATA_STRING);
        }

        /// <summary>
        /// 正常時実行
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="ans">解答</param>
        private void AssertEquals(string fileName, int ans)
        {
            string filePass = PASS + fileName;
            Assert.AreEqual(Kadai.CalcScoreSum(filePass), ans);
        }

        /// <summary>
        /// 異常時実行
        /// </summary>
        /// <param name="filePass">ファイルパス</param>
        /// <param name="errorCode">エラーコード</param>
        private void AssertFail(string filePass, ErrorCode errorCode)
        {
            try
            {
                Kadai.CalcScoreSum(filePass);
            }
            catch (KadaiException e)
            {
                Assert.AreEqual(e.ErrorCode, errorCode);
            }
        }
    }
}
