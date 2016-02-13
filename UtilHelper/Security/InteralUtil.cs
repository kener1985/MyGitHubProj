namespace UtilHelper.Security
{
    internal class InteralUtil
    {
        public static System.Text.Encoding ISOEncoding
        {
            get
            {
                //ISO-8859-1
                return System.Text.Encoding.GetEncoding(28591);
            }
        }
        public static System.Text.Encoding UTF8Encoding
        {
            get
            {
                //UTF-8
                return System.Text.Encoding.UTF8;
            }
        }
    }
}
