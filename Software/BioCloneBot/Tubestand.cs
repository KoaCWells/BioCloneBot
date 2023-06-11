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
            //length, width, height of labware
            dimensions = new double[3] {125.50, 85.20, 85.00};
            topLeftCorner = new double[2];
            //x and y distance from the top left corner of labware position to center of reservoir
            startLocation = new double[2] { 17.80, 11.80 };
            row = 4;
            col = 6;
            maxVolume = 1500.00; //1.5 mL or 1500 uL
            volumes = new double[row][];
            for (int i = 0; i < row; i++)
            {
                volumes[i] = new double[col];
            }
            labwareType = "tubestand";
            
            tubeCount = row*col;
            tubeDistance = 19.75;
            tubeDiameter = 7.30;

            reservoirSeparation = tubeDistance;
            //initializes all tube volumes to 0.0 uL
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    volumes[i][j] = 0.0;
                }
            }
        }
    }
}
