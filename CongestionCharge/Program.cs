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
                Console.WriteLine("Choose vehicle: " + "\n" + "a. car" + "\n" + "b. motorbike" + "\n" + "c. van");
                string vehicle = Console.ReadLine();
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
                TimeSpan fullChargedDay;
                TimeSpan time7;
                TimeSpan time12;
                TimeSpan time19;
                double chargeAM = 2;
                double chargePM = 2.5;
                double charge = 1;
                double priceAM = 0;
                double pricePM = 0;
                double price = 0;
                double totalPrice = 0;
                double dayPriceAM = 0;
                double dayPricePM = 0;
                double dayPrice = 0;

                if (vehicle == "a" || vehicle == "c")
                {
                    Console.WriteLine("car or van");
                    entryTime = new DateTime(2008,04,22,15,32,0);
                                       
                    exitTime = new DateTime(2008,04,23,12,42,0);

                    DateTime midnightAfterEntry = new DateTime(2008, 04, 22, 23, 59, 59);
                    DateTime midnightBeforeExit = new DateTime(2008, 04, 23, 00, 0, 0);


                    if (entryTime.Day == exitTime.Day)
                    {
                        time7 = new TimeSpan(entryTime.Day, 7, 0, 0);
                        time12 = new TimeSpan(entryTime.Day, 12, 0, 0);
                        time19 = new TimeSpan(entryTime.Day, 19, 0, 0);
                    }
                    else
                    {
                        time7 = new TimeSpan(exitTime.Day, 7, 0, 0);
                        time12 = new TimeSpan(exitTime.Day, 12, 0, 0);
                        time19 = new TimeSpan(exitTime.Day, 19, 0, 0);
                    }
                    fullChargedDayAM = time12 - time7;                    
                    fullChargedDayPM = time19 - time12;                    
                    totalTime = exitTime - entryTime;
                 
                   
                   
                    

                    //same day
                    if (totalTime.Days <= 0 && entryTime.Day == exitTime.Day)
                    {
                        Payment(entryTime, exitTime);
                    }
                    //diffrent day, les then 24h
                    else if (totalTime.Days <= 0 && entryTime.Day != exitTime.Day)
                    {                 
                        
                        Payment(entryTime, midnightAfterEntry);
                        Console.WriteLine(dayPricePM);
                        Payment(midnightBeforeExit, exitTime);
                        Console.WriteLine(dayPricePM);



                    }
                    else if (totalTime.Days > 0)
                    {
                     
                    }


                    void Payment(DateTime entryTime, DateTime exitTime )
                    {
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

                        dayPriceAM = 0;
                        dayPricePM = 0;
                        priceAM = 0;
                        pricePM = 0;
                        totalPrice = 0;



                        //all days 19-7h and weekends
                        if (enH < 7 && enH >= 19 && exH < 7 && exH >= 19 || enDW <= 6 && exDW >= 7)
                        {
                            totalPrice = 0;
                        }
                        //0-12h same day
                        else if (enH < 7 && enH >= 0 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAM = exitHours - time7;
                        }
                        //0-19h same day
                        else if (enH < 7 && enH >= 0 && exH < 19 && exH >= 12)
                        {
                            chargedTimePM = exitHours - time7;
                            chargedTimeAM = fullChargedDayAM;
                        }
                        //0-24h same day
                        else if (enH < 7 && enH >= 0 && exH < 0 && exH >= 19)
                        {
                            chargedTimeAM = fullChargedDayAM;
                            chargedTimePM = fullChargedDayPM;
                            priceAM = dayPriceAM;
                            pricePM = dayPricePM;
                        }
                        //7-12h same day
                        else if (enH < 12 && enH >= 7 && exH < 12 && exH >= 7)
                        {
                            chargedTimeAM = exitHours - entryHours;
                        }
                        //7-19h same day
                        else if (enH < 12 && enH >= 7 && exH < 19 && exH >= 12)
                        {
                            chargedTimeAM = time12 - entryHours;
                            chargedTimePM = exitHours - time12;
                        }
                        //7-24 same day
                        else if (enH < 12 && enH >= 7 && exH < 0 && exH >= 19)
                        {
                            chargedTimeAM = time12 - entryHours;
                            chargedTimePM = fullChargedDayPM;
                        }
                        //12-19h same day
                        else if (enH < 19 && enH >= 12 && exH < 19 && exH >= 12)
                        {
                            chargedTimePM = exitHours - entryHours;
                        }
                        //12-24h same day
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
                    

                    //dayPriceAM = fullChargedDayAM.TotalMinutes * (chargeAM / 60);
                    //dayPricePM = fullChargedDayPM.TotalMinutes * (chargePM / 60);
                    //priceAM = Math.Round(chargedTimeAM.TotalMinutes * (chargeAM / 60),1);
                    //pricePM = Math.Round(chargedTimePM.TotalMinutes * (chargePM / 60),1);
                    //totalPrice = Math.Round(priceAM + pricePM, 1);
                    Console.WriteLine($"Charge for {chargedTimeAM.Hours}h {chargedTimeAM.Minutes}m (AM rate):£{String.Format("{0:0.00}", priceAM)}");
                    Console.WriteLine($"Charge for {chargedTimePM.Hours}h {chargedTimePM.Minutes}m (PM rate):£{String.Format("{0:0.00}", pricePM)}");
                    Console.WriteLine($"Total Charge: £{String.Format("{0:0.00}", totalPrice)}");


                }
                else if (vehicle == "b")
                {
                    Console.WriteLine("motorbike");
                    entryTime = new DateTime(2008, 04, 24, 10, 32, 0);
                    int enDW = (int)entryTime.DayOfWeek;
                    int enD = entryTime.Day;
                    int enH = entryTime.Hour;
                    int enM = entryTime.Minute;
                    int enS = entryTime.Second;
                    entryHours = new TimeSpan(enD, enH, enM, enS);
                    exitTime = new DateTime(2008, 04, 24, 11, 42, 0);
                    int exDW = (int)exitTime.DayOfWeek;
                    int exD = exitTime.Day;
                    int exH = exitTime.Hour;
                    int exM = exitTime.Minute;
                    int exS = exitTime.Second;
                    exitHours = new TimeSpan(exD, exH, exM, exS);

                    if (enD == exD)
                    {
                        time7 = new TimeSpan(enD, 7, 0, 0);
                        time19 = new TimeSpan(enD, 19, 0, 0);
                    }
                    else
                    {
                        time7 = new TimeSpan(exD, 7, 0, 0);
                        time19 = new TimeSpan(exD, 19, 0, 0);
                    }
                  
                    fullChargedDayPM = time19 - time7;                    
                    totalTime = exitTime - entryTime;                    

                    //all days 19-7h and weekends
                    if (enH <= 7 && enH > 19 && exH <= 7 && exH > 19 && enD == exD || enDW <= 6 && exDW >= 7)
                    {
                        totalPrice = 0;
                    }                    
                    //0-19h same day
                    else if (enH <= 7 && enH > 0 && exH <= 19 && exH > 7 && enD == exD)
                    {
                        chargedTimePM = exitHours - time7;
                    }
                    //0-24h same day
                    else if (enH <= 7 && enH > 0 && exH <= 24 && exH > 19 && enD == exD)
                    {                       
                        chargedTimePM = fullChargedDayPM;
                        pricePM = dayPricePM;
                    }
                    //7-19h same day
                    else if (enH <= 19 && enH > 7 && exH <= 19 && exH > 7 && enD == exD)
                    {
                        chargedTimePM = exitHours - entryHours;
                    }

                    dayPricePM = fullChargedDayPM.TotalMinutes * (charge / 60);
                    pricePM = Math.Round(chargedTimePM.TotalMinutes * (charge / 60),2);
                    totalPrice = pricePM;

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
