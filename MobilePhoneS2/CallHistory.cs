using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePhoneS2
{
    public class CallHistory
    {
        private Stack<string> HistoryList;
        public CallHistory()
        {
            this.HistoryList = new Stack<string>();
        }
        public void NewCall(string call)
        {
            HistoryList.Push(call);
        }
        public void ShowHistory()
        {
            if (HistoryList.Count == 0)
            {
                Console.WriteLine("Call history is empty");
                Console.Write("Type any key to back to menu:");
                Console.ReadKey();
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine("Last calls:");
                foreach (var c in HistoryList)
                {
                    Console.WriteLine(c);
                }
            }
        }
        public void LastCall()
        {
            
            if(HistoryList.Count > 0)
                Console.WriteLine(HistoryList.Peek());
            
            else
                Console.WriteLine("Call history is empty, unable to call.");
            Console.Write("Type any key to back to menu:");
            Console.ReadKey();
            Console.WriteLine();
        }   
    }
}
