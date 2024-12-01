using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prb.Wine.Keeper.Core
{
    public class WineCollection
    {
        private List<WineBox> WineBoxes = new List<WineBox>();


        public List<WineBox> wineBoxes
        {
            get { return WineBoxes; }
        }
        public void AddWineBox(WineBox box)
        {
                if (box == null)
                {
                    throw new Exception("er moet een winebox megegeven worden");
                }
                WineBoxes.Add(box);
        }
        public  void RemoveWineBox(WineBox box)
        {
            
            if (box == null)
            {
                throw new Exception("er moet een winebox megegeven worden");
            }
            if (WineBoxes.Contains(box))
            {
                WineBoxes.Remove(box);
            }
            
        }
        public List<WineBox> FilteringWineBoxes(List<WineBox> Allboxes,string filterString, DateTime? isDrinkableAt)
        {
           
            List<WineBox> FilterdwineBoxes = new List<WineBox>();  
            
            foreach (WineBox w in Allboxes)
            {

                if (w.ToString().Contains(filterString))
                {
                        if(isDrinkableAt.HasValue)
                        {
                            if (w.IsDrinkableAt(isDrinkableAt.Value))
                            {
                                FilterdwineBoxes.Add(w);
                            }
                    
                        }
                        else
                        {
                            FilterdwineBoxes.Add(w);

                        }
                }
                
            }
            return FilterdwineBoxes.ToList() ;
        }

      
    }
}
