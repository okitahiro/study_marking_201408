using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jp.ktsystem.kadai201408.h_okita
{
    [TestClass]
    public class Lv2Test
    {
        private static readonly string INPUT_PASS = System.IO.Directory.GetCurrentDirectory() + "\\Lv2\\Input\\";
        private static readonly string OUTPUT_PASS = System.IO.Directory.GetCurrentDirectory() + "\\Lv2\\Output\\";
        private static readonly string EXPECT_PASS = System.IO.Directory.GetCurrentDirectory() + "\\Lv2\\Expect\\";


        [TestMethod]
        public void N001()
        {
            AssertEquals("001.txt");
        }

        [TestMethod]
        public void N002()
        {
            AssertEquals("002.txt");
        }

        [TestMethod]
        public void N003()
        {
            AssertEquals("003.txt");
        }
        [TestMethod]
        public void N004()
        {
            AssertEquals("004.txt");
        }
        [TestMethod]
        public void N005()
        {
            AssertEquals("005.txt");
        }
        [TestMethod]
        public void N006()
        {
            AssertEquals("006.txt");
        }

        [TestMethod]
        public void E101()
        {
            string outputFilePass = OUTPUT_PASS + "101.text";
            AssertFail(null, outputFilePass, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E102()
        {
            string outputFilePass = OUTPUT_PASS + "101.text";
            AssertFail("", outputFilePass, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E103()
        {
            string inputFilePass = INPUT_PASS + "101.text";
            AssertFail(inputFilePass, null, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E104()
        {
            string inputFilePass = INPUT_PASS + "101.text";
            AssertFail(inputFilePass, "", ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E105()
        {
            string inputFilePass = INPUT_PASS + "dummy.txt";
            string outputFilePass = OUTPUT_PASS + "dummy.txt";
            AssertFail(inputFilePass, outputFilePass, ErrorCode.FILE_IO_ERRER);
        }

        [TestMethod]
        public void E106()
        {
            string inputFilePass = INPUT_PASS + "106.txt";
            string outputFilePass = OUTPUT_PASS + "106.txt";
            AssertFail(inputFilePass, outputFilePass, ErrorCode.INVALID_DATA_STRING);
        }

        /// <summary>
        /// 正常時実行
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="ans">解答</param>
        private void AssertEquals(string fileName)
        {
            string inputFilePass = INPUT_PASS + fileName;
            string outputFilePass = OUTPUT_PASS + fileName;
            string expectFilePass = EXPECT_PASS + fileName;

            Kadai.PrintMaxScore(inputFilePass, outputFilePass);

            FileStream fs1 = new FileStream(expectFilePass, FileMode.Open);
            FileStream fs2 = new FileStream(outputFilePass, FileMode.Open);
            int byte1;
            int byte2;

            try
            {
                if (fs1.Length == fs2.Length)
                {
                    byte1 = fs1.ReadByte();
                    byte2 = fs2.ReadByte();
                    if (byte1 != byte2)
                    {
                        Assert.Fail();
                    }

                    while (byte1 != -1 || byte2 != -1)
                    {
                        byte1 = fs1.ReadByte();
                        byte2 = fs2.ReadByte();
                        if(byte1 != byte2)
                        {
                            Assert.Fail();
                        }
                    }
                }
            }
            finally
            {
                fs1.Close();
                fs2.Close();
            }
        }

        /// <summary>
        /// 異常時実行
        /// </summary>
        /// <param name="filePass">ファイルパス</param>
        /// <param name="errorCode">エラーコード</param>
        private void AssertFail(string inputFilePass, string outputFilePass, ErrorCode errorCode)
        {
            try
            {
                Kadai.PrintMaxScore(inputFilePass, outputFilePass);
            }
            catch (KadaiException e)
            {
                Assert.AreEqual(e.ErrorCode, errorCode);
            }
        }
    }

}
