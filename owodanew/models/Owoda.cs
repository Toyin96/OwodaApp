using System;
using System.Collections.Generic;
using System.Text;

namespace owodanew.models
{
    class Owoda
    {

        private decimal Balance { get; set; }
        private decimal BossMoney { get; set; }
        private int Counter { get; set; }
        public int TicketCounter { get; set; }
        private readonly Random RandomNumber = new Random();

        private int PromptCustomer()
        {
            string message = PromptClient();
            Console.WriteLine(message);
            int ticketType = int.Parse(Console.ReadLine());

            return ticketType;
        }

        private string PromptClient()
        {
            string extra = "\nWelcome to the Owoda terminal.\n";
            string message = "\nKindly indicate the ticket you would like to purchase by entering the number that apply\n" +
                "1. Daily ticket - #100 Naira/per ticket\n2. Monthly ticket - #1500 Naira/per month\n3. Close terminal";
            if (this.Counter < 1)
            {
                this.Counter++;
                return extra + message;
            }

            return message;
        }

        private decimal GetPrice(int userChoice)
        {
            var sales = 0m;

            if (userChoice == 1)
            {
                sales += 100;
                this.TicketCounter++;
                PrintTransaction(userChoice, sales);
            }
            else if (userChoice == 2)
            {
                sales += 1500;
                this.TicketCounter++;
                PrintTransaction(userChoice, sales);
            }

            return sales;
        }

        private void PrintTransaction(int userChoice, decimal price)
        {
            Console.WriteLine($"\n=========================================================\n" +
                $"Transaction processed successfully...Kindly find details below:\n" +
                $"++++ Ticket type - Daily ticket\n" +
              $"++++ Price - #{price} naira\n++++ Receipt number - {GenerateTicketNumber(userChoice)}\n" +
            $"++++ Time purchased - {DateTime.Now}\n=========================================================");
        }

        private string GenerateTicketNumber(int type)
        {
            string ticketSerialNumber = "DAY";

            if (type == 2)
            {
                ticketSerialNumber = "MTN";
            }

            for (int counter = 0; counter < 5; counter++)
            {
                ticketSerialNumber += this.RandomNumber.Next(0, 9);
            }

            return ticketSerialNumber;
        }

        private decimal CalculateBossShare(decimal sales)
        {
            if (sales == 0m)
            {
                return this.BossMoney;
            }

            this.Balance += sales;

            this.BossMoney = 0.65m * this.Balance;
            return this.BossMoney;
        }

        private bool ProcessController()
        {
            Console.WriteLine("\nDo you want to make another transaction?\nEnter a for 'true' or b for 'false'");
            char status = char.Parse(Console.ReadLine());
            if (status == 'a')
            {
                return true;
            }

            return false;
        }

        private void DisplayTransaction(decimal bossShare, decimal agberoMoney)
        {
            Console.WriteLine($"You've made a total sales of #{this.Balance} Naira today\nNumber of tickets sold is {this.TicketCounter}\nThe " +
               $"NURTW chairman estimated earnings is #{bossShare} Naira\nYour estimated earnings is #{agberoMoney} Naira for the day");
        }

        public void Transact()
        {
            bool signal = true;
            while (signal)
            {
                decimal sales = GetPrice(PromptCustomer());
                decimal bossMoney = CalculateBossShare(sales);
                decimal agberoShare = this.Balance - bossMoney;

                if (!ProcessController())
                {
                    DisplayTransaction(bossMoney, agberoShare);
                    break;
                }
            }
        }
    }
}