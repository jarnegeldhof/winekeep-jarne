using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prb.Wine.Keeper.Core
{
    public class WineSeeder
    {
         public static List<WineBox> GetWineBoxes()
        {
            List<WineBox> winebox = new List<WineBox>()
            {
                new WineBox()
                {
                    Description ="La Bestia Governo",
                    MinStorageMonths = 0,
                    MaxStorageMonths = 24,
                    PurchaseDate = new DateTime(2022, 01, 01),
                    NumberOfBottles = 12
                },
                new WineBox()
                {
                    Description = "Château Roseraie Saint-Émilion",
                    MinStorageMonths = 12,
                    MaxStorageMonths = 36,
                    PurchaseDate = new DateTime(2022, 03,01),
                    NumberOfBottles = 6                    
                },
                new WineBox()
                {
                    Description = "Beaujolais Nouveau",
                    MinStorageMonths = 0,
                    MaxStorageMonths = 3,
                    PurchaseDate = new DateTime(2022, 02,01),
                    NumberOfBottles = 3
                },
                new WineBox()
                {
                    Description = "Pouilly-Fuisé",
                    MinStorageMonths = 24,
                    MaxStorageMonths = 60,
                    PurchaseDate = new DateTime(2022, 03,01),
                    NumberOfBottles = 1
                }
            };
            return winebox;

        }

    }
}
