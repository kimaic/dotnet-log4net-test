using System;
using log4net;

class Program
{
    static void Main(string[] args)
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        logger.Info("Hello World!");
    }
}
