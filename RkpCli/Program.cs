using System;
using RKPModels;

namespace RkpCli
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.Error.WriteLine("Wrong number of Arguments");
                Console.Error.WriteLine("Usage: RkpCli <sql_host> <depmatrix_csv_file> <article_num>");
                return 1;
            }
            else
            {
                try
                {
                    var sql = new MySQLArticleClient(args[0]);
                    var mat = MaterialMatrix.ReadFromFile(args[1]);
                    // todo get productkey
                    string[] productkey = sql.GetProductkey(args[2]);
                    if(productkey == null)
                    {
                        Console.Error.WriteLine("Article not found");
                        return 2;
                    }
                    Console.WriteLine((AvailabilityCheck.IsAvailable(productkey, mat) ? 1 : 0));
                    return 0;
                }
                catch (Exception exc)
                {
                    Console.Error.WriteLine(exc);
                    return 3;
                }                
            }
        }
    }
}
