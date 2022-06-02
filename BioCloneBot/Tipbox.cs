using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    internal class Tipbox : Labware
    {
        private int totalTips;
        private int remainingTips;
        private double tipDistance;
        private double tipDiameter;
        private double[,] tips;

        public Tipbox()
        {
            /* place holder data
             * dimensions 
             * start_location
             * tip_distance
             * tip_diameter
             */
            dimensions = new double[3] { 127.76, 85.48, 15.50 };
            topLeftCorner = new double[2];
            startLocation = new double[2] { 14.38, 11.24 };
            row = 8;
            col = 12;
            maxVolume = 200.00;
            tips = new double[row, col];
            labwareType = "tipbox";

            totalTips = 96;
            remainingTips = 96;
            tipDistance = 9.00;
            tipDiameter = 5.50;

            reservoirSeparation = tipDistance;

            //initializes all tips to true meaning there are 96 new tips available
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tips[i, j] = 1.0;
                }
            }
            volumes = tips;
        }
    }
}