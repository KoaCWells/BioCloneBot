using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    internal class Tubestand : Labware
    {
        private int tubeCount;
        private double tubeDistance;
        private double tubeDiameter;

        public Tubestand()
        {
            dimensions = new double[3] {125.50, 85.20, 62.60};
            topLeftCorner = new double[2];
            startLocation = new double[2] { 14.30, 11.00 };
            row = 4;
            col = 6;
            maxVolume = 1500.00; //1.5 mL or 1500 uL
            volumes = new double[row, col];
            labwareType = "tubestand";
            
            tubeCount = row*col;
            tubeDistance = 19.67;
            tubeDiameter = 7.30;

            reservoirSeparation = tubeDistance;
            //initializes all tube volumes to 0.0 uL
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    volumes[i, j] = 0.0;
                }
            }
        }
    }
}
