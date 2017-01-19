using System;
using log4net;

class Program
{
    static void Main(string[] args)
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        for (int i = 0; i < 100; i++)
        {
            logger.Info("Hello World!");
        }
    }
}
