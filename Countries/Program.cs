using System;
using System.Collections.Generic;

namespace Countries
{
    class Program
    {
        // private readonly int[][] matrix = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 1, 3 }, new int[] { 2, 2, 3 }, new int[] { 1, 1, 1 } };
        // private readonly int[][] matrix = new int[][] { new int[] { 1, 2, 3, 4, 1 }, new int[] { 1, 1, 3, 4, 1 }, new int[] { 2, 2, 3, 4, 1 }, new int[] { 1, 1, 1, 4, 1 } };
        private readonly int[,] matrix = new int[,] { { 1, 1, 1, 1, 2, 1 }, { 2, 2, 2, 1, 1, 1 }, { 2, 2, 3, 3, 3, 3 }, { 4, 3, 4, 4, 4, 4 } };

        private void CheckNeighbour(List<Country> countries, Country myCountry, Plot plot)
        {
            if (!myCountry.OwnsPlot(plot))
            {
                Country otherCountry = BelongsToACountry(countries, plot);
                if (otherCountry == null)
                {
                    myCountry.ClaimPlot(plot);
                }
                else
                {
                    countries.Remove(otherCountry);
                    myCountry.Annex(otherCountry);
                }
            }
        }

        private void CheckNeighbours(List<Country> countries, Country myCountry, Plot currentPlot, Map map)
        {
            Plot neighbouringPlot;
            if ((neighbouringPlot = map.Get(currentPlot.X - 1, currentPlot.Y))?.Colour == myCountry.Colour)
            {
                CheckNeighbour(countries, myCountry, neighbouringPlot);
            }
            if ((neighbouringPlot = map.Get(currentPlot.X, currentPlot.Y - 1))?.Colour == myCountry.Colour)
            {
                CheckNeighbour(countries, myCountry, neighbouringPlot);
            }
            if ((neighbouringPlot = map.Get(currentPlot.X + 1, currentPlot.Y))?.Colour == myCountry.Colour)
            {
                CheckNeighbour(countries, myCountry, neighbouringPlot);
            }
            if ((neighbouringPlot = map.Get(currentPlot.X, currentPlot.Y + 1))?.Colour == myCountry.Colour)
            {
                CheckNeighbour(countries, myCountry, neighbouringPlot);
            }
        }

        private Country BelongsToACountry(List<Country> countries, Plot plot)
        {
            Country result = null;

            if (countries.Count > 0)
            {
                List<Country> tmp = countries.FindAll(c => c.Colour == plot.Colour);

                foreach (Country country in tmp)
                {
                    if (country.OwnsPlot(plot))
                    {
                        result = country;
                        break;
                    }
                }
            }

            return result;
        }

        public List<Country> GetCountries(Map map)
        {
            List<Country> countries = new List<Country>();

            foreach (Plot plot in map)
            {
                Country myCountry = BelongsToACountry(countries, plot);
                if (myCountry == null)
                {
                    myCountry = new Country(plot.Colour);
                    myCountry.ClaimPlot(plot);
                    countries.Add(myCountry);
                }

                CheckNeighbours(countries, myCountry, plot, map);
            }

            return countries;
        }

        public void Start(string[] args)
        {
            Map map = new Map(matrix);            
            Console.WriteLine(map);
            List<Country> result = GetCountries(map);
            Console.WriteLine($"#Countries: {result.Count}");
        }

        public static void Main(string[] args)
        {
            new Program().Start(args);
            Console.ReadKey();
        }
    }
}
