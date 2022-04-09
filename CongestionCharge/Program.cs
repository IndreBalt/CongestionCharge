using System;
using System.Globalization;

namespace CongestionCharge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime entryTime;
                TimeSpan entryHours;
                DateTime exitTime;
                TimeSpan exitHours;
                TimeSpan totalTime;
                TimeSpan chargedTimeAM = new TimeSpan(0, 0, 0, 0);
                TimeSpan chargedTimePM = new TimeSpan(0, 0, 0, 0);
                TimeSpan totalChargedTime = chargedTimeAM + chargedTimePM;
                TimeSpan fullChargedDayAM;
                TimeSpan fullChargedDayPM;
                TimeSpan time7;
                TimeSpan time12;
                TimeSpan time19;
                TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
                TimeSpan freeDays = new TimeSpan(0, 0, 0, 0);
                double chargeAM = 2;
                double chargePM = 2.5;
                double charge = 1;
                double priceAM = 0;
                double pricePM = 0;
                double totalPrice = 0;
                double dayPriceAM = 0;
                double dayPricePM = 0;

                Console.WriteLine("Enter entry date: ");
                Console.Write("Year: ");
                int enYear = int.Parse(Console.ReadLine());
                Console.Write("Month: ");
                int enMonth = int.Parse(Console.ReadLine());
                Console.Write("Day: ");
                int enDay = int.Parse(Console.ReadLine());
                Console.Write("Hour: ");
                int enHour = int.Parse(Console.ReadLine());
                Console.Write("Minutes: ");
                int enMinutes = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("Enter exit date: ");
                Console.Write("Year: ");
                int exYear = int.Parse(Console.ReadLine());
                Console.Write("Month: ");
                int exMonth = int.Parse(Console.ReadLine());
                Console.Write("Day: ");
                int exDay = int.Parse(Console.ReadLine());
                Console.Write("Hour: ");
                int exHour = int.Parse(Console.ReadLine());
                Console.Write("Minutes: ");
                int exMinutes = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Choose vehicle: " + "\n" + "a. car" + "\n" + "b. motorbike" + "\n" + "c. van");
                string vehicle = Console.ReadLine();

                if (vehicle == "a" || vehicle == "c")
                {
                    Console.WriteLine("You choose car or van");
                    entryTime = new DateTime(enYear, enMonth, enDay, enHour, enMinutes, 0);
                    exitTime = new DateTime(exYear, exMonth, exDay, exHour, exMinutes, 0);
                    DateTime midnightAfterEntry = new DateTime(entryTime.Year, entryTime.Month, entryTime.Day, 23, 59, 59);
                    DateTime midnightBeforeExit = new DateTime(exitTime.Year, exitTime.Month, exitTime.Day, 00, 0, 0);
                    totalTime = exitTime - entryTime;
                    DateTime entryTime2 = entryTime;

                    for (var i = 0; i < totalTime.Days; i++)
                    {
                        entryTime2 = new DateTime(entryTime2.Year, entryTime2.Month, entryTime2.Day + 1, entryTime2.Hour, entryTime2.Minute, entryTime2.Second);
                        if ((int)entryTime2.DayOfWeek == 6 || (int)entryTime2.DayOfWeek == 0)
                        {
                            freeDays = freeDays + oneDay;
                        }
                    }
                    //same day
                    if (totalTime.Days <= 0 && entryTime.Day == exitTime.Day)
                    {
                        PaymentCar(entryTime, exitTime);
                    }
                    //diffrent day, les then 24h
                    else if (totalTime.Days <= 0 && entryTime.Day != exitTime.Day)
                    {
                        PaymentCar(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAM = chargedTimeAM;
                        TimeSpan firstChargedTimePM = chargedTimePM;
                        double firstPriceAM = priceAM;
                        double firstPricePM = pricePM;
                        double firstTotalPrice = totalPrice;

                        PaymentCar(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAM = chargedTimeAM;
                        TimeSpan secondChargedTimePM = chargedTimePM;
                        double secondPriceAM = priceAM;
                        double secondPricePM = pricePM;
                        double secondTotalPrice = totalPrice;

                        chargedTimeAM = firstChargedTimeAM + secondChargedTimeAM;
                        chargedTimePM = firstChargedTimePM + secondChargedTimePM;
                        priceAM = firstPriceAM + secondPriceAM;
                        pricePM = firstPricePM + secondPricePM;
                        totalPrice = firstTotalPrice + secondTotalPrice;
                    }
                    else if (totalTime.Days > 0)
                    {
                        totalTime = totalTime - freeDays;
                        Console.WriteLine($"total paid time: {totalTime.ToString()}");
                        PaymentCar(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAM = chargedTimeAM;
                        TimeSpan firstChargedTimePM = chargedTimePM;
                        double firstPriceAM = priceAM;
                        double firstPricePM = pricePM;
                        double firstTotalPrice = totalPrice;

                        PaymentCar(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAM = chargedTimeAM;
                        TimeSpan secondChargedTimePM = chargedTimePM;
                        double secondPriceAM = priceAM;
                        double secondPricePM = pricePM;
                        double secondTotalPrice = totalPrice;

                        fullChargedDayAM = fullChargedDayAM * totalTime.Days;
                        fullChargedDayPM = fullChargedDayPM * totalTime.Days;
                        chargedTimeAM = firstChargedTimeAM + secondChargedTimeAM + fullChargedDayAM;
                        chargedTimePM = firstChargedTimePM + secondChargedTimePM + fullChargedDayPM;
                        dayPriceAM = dayPriceAM * totalTime.Days;
                        dayPricePM = dayPricePM * totalTime.Days;
                        priceAM = firstPriceAM + secondPriceAM + dayPriceAM;
                        pricePM = firstPricePM + secondPricePM + dayPricePM;
                        totalPrice = firstTotalPrice + secondTotalPrice + dayPricePM + dayPriceAM;
                    }

                    void PaymentCar(DateTime entryTime, DateTime exitTime)
                    {
                        dayPriceAM = 0;
                        dayPricePM = 0;
                        priceAM = 0;
                        pricePM = 0;
                        totalPrice = 0;
                        chargedTimeAM = new TimeSpan(0, 0, 0, 0);
                        chargedTimePM = new TimeSpan(0, 0, 0, 0);

                        int enDW = (int)entryTime.DayOfWeek;
                        int enD = entryTime.Day;
                        int enH = entryTime.Hour;
                        int enM = entryTime.Minute;
                        int enS = entryTime.Second;
                        entryHours = new TimeSpan(enD, enH, enM, enS);

                        int exDW = (int)exitTime.DayOfWeek;
                        int exD = exitTime.Day;
                        int exH = exitTime.Hour;
                        int exM = exitTime.Minute;
                        int exS = exitTime.Second;
                        exitHours = new TimeSpan(exD, exH, exM, exS);
                        time7 = new TimeSpan(entryTime.Day, 7, 0, 0);
                        time12 = new TimeSpan(entryTime.Day, 12, 0, 0);
                        time19 = new TimeSpan(entryTime.Day, 19, 0, 0);
                        fullChargedDayAM = time12 - time7;
                        fullChargedDayPM = time19 - time12;

                        //all days 19-7h and weekends
                        if (enH < 7 && enH >= 19 && exH < 7 && exH >= 19 || enDW == 6 && exDW == 0)
                        {
                            totalPrice = 0;
                        }
                        //0-12h 
                        else if (enH < 7 && enH >= 0 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAM = exitHours - time7;
                        }
                        //0-19h 
                        else if (enH < 7 && enH >= 0 && exH < 19 && exH >= 12)
                        {
                            chargedTimePM = exitHours - time7;
                            chargedTimeAM = fullChargedDayAM;
                        }
                        //0-24h 
                        else if (enH < 7 && enH >= 0 && exH < 24 && exH >= 19)
                        {
                            chargedTimeAM = fullChargedDayAM;
                            chargedTimePM = fullChargedDayPM;
                            priceAM = dayPriceAM;
                            pricePM = dayPricePM;
                        }
                        //7-12h
                        else if (enH < 12 && enH >= 7 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAM = exitHours - entryHours;
                        }
                        //7-19h 
                        else if (enH < 12 && enH >= 7 && exH < 19 && exH >= 12)
                        {
                            chargedTimeAM = time12 - entryHours;
                            chargedTimePM = exitHours - time12;
                        }
                        //7-24 
                        else if (enH < 12 && enH >= 7 && exH < 24 && exH >= 19)
                        {
                            chargedTimeAM = time12 - entryHours;
                            chargedTimePM = fullChargedDayPM;
                        }
                        //12-19h 
                        else if (enH < 19 && enH >= 12 && exH < 19 && exH >= 12)
                        {
                            chargedTimePM = exitHours - entryHours;
                        }
                        //12-24h 
                        else if (enH < 19 && enH >= 12 && exH < 24 && exH >= 19)
                        {
                            chargedTimePM = time19 - entryHours;
                        }
                        dayPriceAM = fullChargedDayAM.TotalMinutes * (chargeAM / 60);
                        dayPricePM = fullChargedDayPM.TotalMinutes * (chargePM / 60);
                        priceAM = Math.Round(chargedTimeAM.TotalMinutes * (chargeAM / 60), 1);
                        pricePM = Math.Round(chargedTimePM.TotalMinutes * (chargePM / 60), 1);
                        totalPrice = Math.Round(priceAM + pricePM, 1);
                    }
                    Console.WriteLine($"Charge for {chargedTimeAM.Hours}h {chargedTimeAM.Minutes}m (AM rate):£{String.Format("{0:0.00}", priceAM)}");
                    Console.WriteLine($"Charge for {chargedTimePM.Hours}h {chargedTimePM.Minutes}m (PM rate):£{String.Format("{0:0.00}", pricePM)}");
                    Console.WriteLine($"Total Charge: £{String.Format("{0:0.00}", totalPrice)}");
                }
                else if (vehicle == "b")

                {
                    Console.WriteLine("mototrbike");
                    entryTime = new DateTime(enYear, enMonth, enDay, enHour, enMinutes, 0);
                    exitTime = new DateTime(exYear, exMonth, exDay, exHour, exMinutes, 0);
                    DateTime midnightAfterEntry = new DateTime(entryTime.Year, entryTime.Month, entryTime.Day, 23, 59, 59);
                    DateTime midnightBeforeExit = new DateTime(exitTime.Year, exitTime.Month, exitTime.Day, 00, 0, 0);
                    totalTime = exitTime - entryTime;
                    DateTime entryTime2 = entryTime;

                    for (var i = 0; i < totalTime.Days; i++)
                    {
                        entryTime2 = new DateTime(entryTime2.Year, entryTime2.Month, entryTime2.Day + 1, entryTime2.Hour, entryTime2.Minute, entryTime2.Second);
                        Console.WriteLine(entryTime2.ToString());
                        Console.WriteLine(entryTime2.DayOfWeek);

                        if ((int)entryTime2.DayOfWeek == 6 || (int)entryTime2.DayOfWeek == 0)
                        {
                            freeDays = freeDays + oneDay;
                        }
                    }
                    //same day
                    if (totalTime.Days <= 0 && entryTime.Day == exitTime.Day)
                    {
                        PaymentMoto(entryTime, exitTime);
                    }
                    //diffrent day, les then 24h
                    else if (totalTime.Days <= 0 && entryTime.Day != exitTime.Day)
                    {
                        PaymentMoto(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAM = chargedTimeAM;
                        TimeSpan firstChargedTimePM = chargedTimePM;
                        double firstPriceAM = priceAM;
                        double firstPricePM = pricePM;
                        double firstTotalPrice = totalPrice;

                        PaymentMoto(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAM = chargedTimeAM;
                        TimeSpan secondChargedTimePM = chargedTimePM;
                        double secondPriceAM = priceAM;
                        double secondPricePM = pricePM;
                        double secondTotalPrice = totalPrice;

                        chargedTimeAM = firstChargedTimeAM + secondChargedTimeAM;
                        chargedTimePM = firstChargedTimePM + secondChargedTimePM;
                        priceAM = firstPriceAM + secondPriceAM;
                        pricePM = firstPricePM + secondPricePM;
                        totalPrice = firstTotalPrice + secondTotalPrice;
                    }
                    //more than 24h
                    else if (totalTime.Days > 0)
                    {
                        totalTime = totalTime - freeDays;
                        Console.WriteLine($"total paid time: {totalTime.ToString()}");
                        PaymentMoto(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAM = chargedTimeAM;
                        TimeSpan firstChargedTimePM = chargedTimePM;
                        double firstPriceAM = priceAM;
                        double firstPricePM = pricePM;
                        double firstTotalPrice = totalPrice;

                        PaymentMoto(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAM = chargedTimeAM;
                        TimeSpan secondChargedTimePM = chargedTimePM;
                        double secondPriceAM = priceAM;
                        double secondPricePM = pricePM;
                        double secondTotalPrice = totalPrice;

                        fullChargedDayAM = fullChargedDayAM * totalTime.Days;
                        fullChargedDayPM = fullChargedDayPM * totalTime.Days;
                        chargedTimeAM = firstChargedTimeAM + secondChargedTimeAM + fullChargedDayAM;
                        chargedTimePM = firstChargedTimePM + secondChargedTimePM + fullChargedDayPM;
                        dayPriceAM = dayPriceAM * totalTime.Days;
                        dayPricePM = dayPricePM * totalTime.Days;
                        priceAM = firstPriceAM + secondPriceAM + dayPriceAM;
                        pricePM = firstPricePM + secondPricePM + dayPricePM;
                        totalPrice = firstTotalPrice + secondTotalPrice + dayPricePM + dayPriceAM;
                    }

                    void PaymentMoto(DateTime entryTime, DateTime exitTime)
                    {
                        dayPriceAM = 0;
                        dayPricePM = 0;
                        priceAM = 0;
                        pricePM = 0;
                        totalPrice = 0;
                        chargedTimeAM = new TimeSpan(0, 0, 0, 0);
                        chargedTimePM = new TimeSpan(0, 0, 0, 0);

                        int enDW = (int)entryTime.DayOfWeek;
                        int enD = entryTime.Day;
                        int enH = entryTime.Hour;
                        int enM = entryTime.Minute;
                        int enS = entryTime.Second;
                        entryHours = new TimeSpan(enD, enH, enM, enS);

                        int exDW = (int)exitTime.DayOfWeek;
                        int exD = exitTime.Day;
                        int exH = exitTime.Hour;
                        int exM = exitTime.Minute;
                        int exS = exitTime.Second;
                        exitHours = new TimeSpan(exD, exH, exM, exS);
                        time7 = new TimeSpan(entryTime.Day, 7, 0, 0);
                        time19 = new TimeSpan(entryTime.Day, 19, 0, 0);
                        fullChargedDayAM = new TimeSpan(0, 0, 0, 0);
                        fullChargedDayPM = time19 - time7;

                        //all days 19-7h and weekends
                        if (enH < 7 && enH >= 19 && exH < 7 && exH >= 19 || enDW <= 6 && exDW == 0 && exDW <= 6)
                        {
                            totalPrice = 0;
                        }
                        //0-19h 
                        else if (enH < 7 && enH >= 0 && exH < 19 && exH >= 7)
                        {
                            chargedTimePM = exitHours - time7;
                        }
                        //0-24h 
                        else if (enH < 7 && enH >= 0 && exH < 24 && exH >= 19)
                        {
                            chargedTimePM = fullChargedDayPM;
                        }
                        //7-19h 
                        else if (enH < 19 && enH >= 7 && exH < 19 && exH >= 7)
                        {
                            chargedTimePM = exitHours - entryHours;
                        }
                        //7-24 
                        else if (enH < 19 && enH >= 7 && exH < 24 && exH >= 19)
                        {
                            chargedTimePM = time19 - entryHours;
                        }
                        dayPriceAM = fullChargedDayAM.TotalMinutes * (chargeAM / 60);
                        dayPricePM = fullChargedDayPM.TotalMinutes * (charge / 60);
                        priceAM = Math.Round(chargedTimeAM.TotalMinutes * (chargeAM / 60), 1);
                        pricePM = Math.Round(chargedTimePM.TotalMinutes * (charge / 60), 1);
                        totalPrice = Math.Round(priceAM + pricePM, 1);
                    }
                    Console.WriteLine($"Charge for {chargedTimeAM.Hours}h {chargedTimeAM.Minutes}m (AM rate):£{String.Format("{0:0.00}", priceAM)}");
                    Console.WriteLine($"Charge for {chargedTimePM.Hours}h {chargedTimePM.Minutes}m (PM rate):£{String.Format("{0:0.00}", pricePM)}");
                    Console.WriteLine($"Total Charge: £{String.Format("{0:0.00}", totalPrice)}");
                }
                else
                {
                    Console.WriteLine("wrong letter, please try again");
                }

            }

        }
    }
}
