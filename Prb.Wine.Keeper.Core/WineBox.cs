using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prb.Wine.Keeper.Core
{
    public  class WineBox
    {
        private string description;
        private int minStorageMonths;
        private int maxStorageMonths;
        private DateTime purchaseDate;
        private int numberOfBottles;
        private DateTime drinkableFrom { get;}
        private DateTime drinkableUntil { get;}

        public string Description
        {
            get { return description; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("een discription mag niet leeg zijn");
                }
                description = value; 
            }

        }
        public int MinStorageMonths
        {
            get { return minStorageMonths; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                minStorageMonths = value;
            }
        }
        public int MaxStorageMonths
        {
            get { return maxStorageMonths; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                maxStorageMonths = value;
            }
        }
        public int NumberOfBottles
        {
            get { return numberOfBottles; }
            set
            {
                if(value < 0)
                {
                    value = 0;
                }
                numberOfBottles = value;
            }
        }
        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }

        public WineBox(string description, int minStorageMonths, int maxStorageMonth, DateTime purchaseDate, int numberOfBottles)
        {
           Description = description;
           MinStorageMonths = minStorageMonths;
           MaxStorageMonths = maxStorageMonth;
           PurchaseDate = purchaseDate;
           NumberOfBottles = numberOfBottles;
           drinkableFrom = PurchaseDate.AddMonths(minStorageMonths);
           drinkableUntil = PurchaseDate.AddMonths(maxStorageMonth);    
        }

        public WineBox()
        {
        }

        public bool DrinkBottle()
        {
            if (NumberOfBottles >= 1)
            {
                numberOfBottles--;
            }
            if (NumberOfBottles > 0)
            {
                 return true;
            }
            return false;
        }
        public bool IsDrinkableAt(DateTime drinkableAt)
        {
            if (drinkableAt > drinkableFrom && drinkableAt < drinkableUntil)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{description} ({numberOfBottles})";
        }


    }
}
