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
        private double[][] tips;

        public Tipbox()
        {
            //length, width, and height of labware
            dimensions = new double[3] { 127.76, 80.48, 46.00 };
            topLeftCorner = new double[2];
            //x and y distance from the top left corner of labware position to center of reservoir
            startLocation = new double[2] { 15.80, 14.50 };
            row = 8;
            col = 12;
            maxVolume = 200.00;
            tips = new double[row][];
            for (int i = 0; i < row; i++)
            {
                tips[i] = new double[col];
            }
            labwareType = "tipbox";

            totalTips = 96;
            remainingTips = 96;
            tipDistance = 8.90;
            tipDiameter = 7.00;

            reservoirSeparation = tipDistance;

            //initializes all tips to true meaning there are 96 new tips available
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tips[i][j] = 1.0;
                }
            }
            volumes = tips;
        }
    }
}