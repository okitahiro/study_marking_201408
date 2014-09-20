using System;

namespace jp.ktsystem.kadai201408.h_okita
{
    /// <summary>
    /// Kadai用例外クラス
    /// </summary>
    public class KadaiException: Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="errorCode">エラーコード</param>
        public KadaiException(ErrorCode errorCode)
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// エラーコード
        /// </summary>
        public ErrorCode ErrorCode
        { 
            get
            {
                return _errorCode;
            }
        }
        private ErrorCode _errorCode;
    }

    /// <summary>
    /// エラーコードの列挙型
    /// </summary>
    public enum ErrorCode { FILE_IO_ERRER, INVALID_DATA_STRING, OTHERS};
}
