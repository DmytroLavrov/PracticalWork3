using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;

namespace FirstTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string FormatDate = "dd/MM/yyyy";
            int count = 0;
            Action<DateTime, double> showTotalAmount = (date, amount) => Console.WriteLine($"{date.ToString(FormatDate)}: {amount}");
            Console.OutputEncoding = System.Text.Encoding.Default;
            string path = @"/Users/dmytrolavrov/Projects/PracticalWork3/FirstTask/FirstTask/Information.csv";
            string overwritingPath = @"/Users/dmytrolavrov/Projects/PracticalWork3/FirstTask/FirstTask/FirstTask";
            Func<string, DateTime> dateTransaction = transactionInfo => DateTime.ParseExact(transactionInfo.Split(',')[0], FormatDate, null);
            Func<string, double> amountTransaction = transactionInfo => double.Parse(transactionInfo.Split(',')[1]);
            var transactionsByDate = File.ReadAllLines(path).Skip(1).GroupBy(dateTransaction).Select(s => new { Date = s.Key, TotalMoney = s.Sum(amountTransaction) });
            StreamWriter editor = null;
            foreach (var transaction in transactionsByDate)
            {
                if (editor == null)
                {
                    Console.OutputEncoding = System.Text.Encoding.Default;
                    editor?.Dispose();
                    count++;
                    string filename = $"{overwritingPath}{$"transaction{count}"}.csv";
                    editor = new StreamWriter(filename);
                    editor.WriteLine("Дата,Сума");
                }
                editor.WriteLine($"{transaction.Date.ToString(FormatDate)},{transaction.TotalMoney}");
                showTotalAmount(transaction.Date, transaction.TotalMoney);
            }
            editor?.Dispose();
            Console.ReadKey();
        }
    }
}