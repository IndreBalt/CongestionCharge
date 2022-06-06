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
                TimeSpan chargedTimeAm = new TimeSpan(0, 0, 0, 0);
                TimeSpan chargedTimePm = new TimeSpan(0, 0, 0, 0);
                TimeSpan totalChargedTime = chargedTimeAm + chargedTimePm;
                TimeSpan fullChargedDayAm;
                TimeSpan fullChargedDayPm;
                TimeSpan time7;
                TimeSpan time12;
                TimeSpan time19;
                TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
                TimeSpan freeDays = new TimeSpan(0, 0, 0, 0);
                double chargeAm = 2;
                double chargePm = 2.5;
                double charge = 1;
                double priceAm = 0;
                double pricePm = 0;
                double totalPrice = 0;
                double dayPriceAm = 0;
                double dayPricePm = 0;

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
                    if(vehicle == "a")
                    {
                        Console.WriteLine("You choose car");
                    }
                    else
                    {
                        Console.WriteLine("You choose van");
                    }                    
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
                        TimeSpan firstChargedTimeAm = chargedTimeAm;
                        TimeSpan firstChargedTimePm = chargedTimePm;
                        double firstPriceAm = priceAm;
                        double firstPricePm = pricePm;
                        double firstTotalPrice = totalPrice;

                        PaymentCar(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAm = chargedTimeAm;
                        TimeSpan secondChargedTimePm = chargedTimePm;
                        double secondPriceAm = priceAm;
                        double secondPricePm = pricePm;
                        double secondTotalPrice = totalPrice;

                        chargedTimeAm = firstChargedTimeAm + secondChargedTimeAm;
                        chargedTimePm = firstChargedTimePm + secondChargedTimePm;
                        priceAm = firstPriceAm + secondPriceAm;
                        pricePm = firstPricePm + secondPricePm;
                        totalPrice = firstTotalPrice + secondTotalPrice;
                    }
                    else if (totalTime.Days > 0)
                    {
                        totalTime = totalTime - freeDays;
                        PaymentCar(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAm = chargedTimeAm;
                        TimeSpan firstChargedTimePm = chargedTimePm;
                        double firstPriceAm = priceAm;
                        double firstPricePm = pricePm;
                        double firstTotalPrice = totalPrice;

                        PaymentCar(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAm = chargedTimeAm;
                        TimeSpan secondChargedTimePm = chargedTimePm;
                        double secondPriceAm = priceAm;
                        double secondPricePm = pricePm;
                        double secondTotalPrice = totalPrice;

                        fullChargedDayAm *= totalTime.Days;
                        fullChargedDayPm *= totalTime.Days;
                        chargedTimeAm = firstChargedTimeAm + secondChargedTimeAm + fullChargedDayAm;
                        chargedTimePm = firstChargedTimePm + secondChargedTimePm + fullChargedDayPm;
                        dayPriceAm *= totalTime.Days;
                        dayPricePm *= dayPricePm * totalTime.Days;
                        priceAm = firstPriceAm + secondPriceAm + dayPriceAm;
                        pricePm = firstPricePm + secondPricePm + dayPricePm;
                        totalPrice = firstTotalPrice + secondTotalPrice + dayPricePm + dayPriceAm;
                    }

                    void PaymentCar(DateTime entryTime, DateTime exitTime)
                    {
                        dayPriceAm = 0;
                        dayPricePm = 0;
                        priceAm = 0;
                        pricePm = 0;
                        totalPrice = 0;
                        chargedTimeAm = new TimeSpan(0, 0, 0, 0);
                        chargedTimePm = new TimeSpan(0, 0, 0, 0);

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
                        fullChargedDayAm = time12 - time7;
                        fullChargedDayPm = time19 - time12;

                        //all days 19-7h and weekends
                        if (enH < 7 && enH >= 19 && exH < 7 && exH >= 19 || enDW == 6 && exDW == 0)
                        {
                            totalPrice = 0;
                        }
                        //0-12h 
                        else if (enH < 7 && enH >= 0 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAm = exitHours - time7;
                        }
                        //0-19h 
                        else if (enH < 7 && enH >= 0 && exH < 19 && exH >= 12)
                        {
                            chargedTimePm = exitHours - time7;
                            chargedTimeAm = fullChargedDayAm;
                        }
                        //0-24h 
                        else if (enH < 7 && enH >= 0 && exH < 24 && exH >= 19)
                        {
                            chargedTimeAm = fullChargedDayAm;
                            chargedTimePm = fullChargedDayPm;
                            priceAm = dayPriceAm;
                            pricePm = dayPricePm;
                        }
                        //7-12h
                        else if (enH < 12 && enH >= 7 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAm = exitHours - entryHours;
                        }
                        //7-19h 
                        else if (enH < 12 && enH >= 7 && exH < 19 && exH >= 12)
                        {
                            chargedTimeAm = time12 - entryHours;
                            chargedTimePm = exitHours - time12;
                        }
                        //7-24 
                        else if (enH < 12 && enH >= 7 && exH < 24 && exH >= 19)
                        {
                            chargedTimeAm = time12 - entryHours;
                            chargedTimePm = fullChargedDayPm;
                        }
                        //12-19h 
                        else if (enH < 19 && enH >= 12 && exH < 19 && exH >= 12)
                        {
                            chargedTimePm = exitHours - entryHours;
                        }
                        //12-24h 
                        else if (enH < 19 && enH >= 12 && exH < 24 && exH >= 19)
                        {
                            chargedTimePm = time19 - entryHours;
                        }
                        dayPriceAm = fullChargedDayAm.TotalMinutes * (chargeAm / 60);
                        dayPricePm = fullChargedDayPm.TotalMinutes * (chargePm / 60);
                        priceAm = Math.Round(chargedTimeAm.TotalMinutes * (chargeAm / 60), 1);
                        pricePm = Math.Round(chargedTimePm.TotalMinutes * (chargePm / 60), 1, MidpointRounding.ToZero);
                        totalPrice = Math.Round(priceAm + pricePm, 1);
                    }
                    Console.WriteLine($"Charge for {chargedTimeAm.Hours}h {chargedTimeAm.Minutes}m (AM rate):£{String.Format("{0:0.00}", priceAm)}");
                    Console.WriteLine($"Charge for {chargedTimePm.Hours}h {chargedTimePm.Minutes}m (PM rate):£{String.Format("{0:0.00}", pricePm)}");
                    Console.WriteLine($"Total Charge: £{String.Format("{0:0.00}", totalPrice)}");
                    Console.WriteLine();
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
                        TimeSpan firstChargedTimeAm = chargedTimeAm;
                        TimeSpan firstChargedTimePm = chargedTimePm;
                        double firstPriceAm = priceAm;
                        double firstPricePm = pricePm;
                        double firstTotalPrice = totalPrice;

                        PaymentMoto(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAm = chargedTimeAm;
                        TimeSpan secondChargedTimePm = chargedTimePm;
                        double secondPriceAm = priceAm;
                        double secondPricePm = pricePm;
                        double secondTotalPrice = totalPrice;

                        chargedTimeAm = firstChargedTimeAm + secondChargedTimeAm;
                        chargedTimePm = firstChargedTimePm + secondChargedTimePm;
                        priceAm = firstPriceAm + secondPriceAm;
                        pricePm = firstPricePm + secondPricePm;
                        totalPrice = firstTotalPrice + secondTotalPrice;
                    }
                    //more than 24h
                    else if (totalTime.Days > 0)
                    {
                        totalTime = totalTime - freeDays;                        
                        PaymentMoto(entryTime, midnightAfterEntry);
                        TimeSpan firstChargedTimeAm = chargedTimeAm;
                        TimeSpan firstChargedTimePm = chargedTimePm;
                        double firstPriceAm = priceAm;
                        double firstPricePm = pricePm;
                        double firstTotalPrice = totalPrice;

                        PaymentMoto(midnightBeforeExit, exitTime);
                        TimeSpan secondChargedTimeAm = chargedTimeAm;
                        TimeSpan secondChargedTimePm = chargedTimePm;
                        double secondPriceAm = priceAm;
                        double secondPricePm = pricePm;
                        double secondTotalPrice = totalPrice;

                        fullChargedDayAm = fullChargedDayAm * totalTime.Days;
                        fullChargedDayPm = fullChargedDayPm * totalTime.Days;
                        chargedTimeAm = firstChargedTimeAm + secondChargedTimeAm + fullChargedDayAm;
                        chargedTimePm = firstChargedTimePm + secondChargedTimePm + fullChargedDayPm;
                        dayPriceAm = dayPriceAm * totalTime.Days;
                        dayPricePm = dayPricePm * totalTime.Days;
                        priceAm = firstPriceAm + secondPriceAm + dayPriceAm;
                        pricePm = firstPricePm + secondPricePm + dayPricePm;
                        totalPrice = firstTotalPrice + secondTotalPrice + dayPricePm + dayPriceAm;
                    }

                    void PaymentMoto(DateTime entryTime, DateTime exitTime)
                    {
                        dayPriceAm = 0;
                        dayPricePm = 0;
                        priceAm = 0;
                        pricePm = 0;
                        totalPrice = 0;
                        chargedTimeAm = new TimeSpan(0, 0, 0, 0);
                        chargedTimePm = new TimeSpan(0, 0, 0, 0);

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
                        fullChargedDayAm = new TimeSpan(0, 0, 0, 0);
                        fullChargedDayPm = time19 - time7;

                        //all days 19-7h and weekends
                        if (enH < 7 && enH >= 19 && exH < 7 && exH >= 19 || enDW <= 6 && exDW == 0 && exDW <= 6)
                        {
                            totalPrice = 0;
                        }
                        //0-19h 
                        else if (enH < 7 && enH >= 0 && exH < 19 && exH >= 7)
                        {
                            chargedTimePm = exitHours - time7;
                        }
                        //0-24h 
                        else if (enH < 7 && enH >= 0 && exH < 24 && exH >= 19)
                        {
                            chargedTimePm = fullChargedDayPm;
                        }
                        //7-19h 
                        else if (enH < 19 && enH >= 7 && exH < 19 && exH >= 7)
                        {
                            chargedTimePm = exitHours - entryHours;
                        }
                        //7-24 
                        else if (enH < 19 && enH >= 7 && exH < 24 && exH >= 19)
                        {
                            chargedTimePm = time19 - entryHours;
                        }
                        dayPriceAm = fullChargedDayAm.TotalMinutes * (chargeAm / 60);
                        dayPricePm = fullChargedDayPm.TotalMinutes * (charge / 60);
                        priceAm = Math.Round(chargedTimeAm.TotalMinutes * (chargeAm / 60), 1);
                        pricePm = Math.Round(chargedTimePm.TotalMinutes * (charge / 60), 1, MidpointRounding.ToZero);
                        totalPrice = Math.Round(priceAm + pricePm, 1);
                    }
                    Console.WriteLine($"Charge for {chargedTimeAm.Hours}h {chargedTimeAm.Minutes}m (AM rate):£{String.Format("{0:0.00}", priceAm)}");
                    Console.WriteLine($"Charge for {chargedTimePm.Hours}h {chargedTimePm.Minutes}m (PM rate):£{String.Format("{0:0.00}", pricePm)}");
                    Console.WriteLine($"Total Charge: £{String.Format("{0:0.00}", totalPrice)}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("wrong letter, please try again");
                }
                Console.WriteLine("Do you vant to repeat? (Y/N)");
                string ansver = Console.ReadLine().ToLower();
                if (ansver == "n")
                {
                    break;
                }
                else
                {
                    continue;
                }

            }

        }
    }
}
