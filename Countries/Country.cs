using System.Collections.Generic;
using System.Drawing;

namespace Countries
{
    class Country
    {
        public int Colour;
        public List<Plot> Plots;

        public Country(int colour)
        {
            this.Colour = colour;
            this.Plots = new List<Plot>();
        }

        public void ClaimPlot(Plot plot)
        {
            if (!OwnsPlot(plot))
                Plots.Add(plot);
        }

        public bool OwnsPlot(Plot plot)
        {
            bool result = Plots.Contains(plot);
            return result;
        }

        public void Annex(Country victim)
        {
            foreach (Plot plot in victim.Plots)
            {
                Plots.Add(plot);
            }
        }

        override
        public string ToString() {
            return $"Colour: {Colour}, #Plots: {Plots.Count}";
        }
    }
}
