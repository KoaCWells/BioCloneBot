 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    internal class Wellplate : Labware
    {
        private int wellCount;
        private double wellDistance;
        private double wellDiameter;

        public Wellplate()
        {
            dimensions = new double[3] { 127.76, 85.48, 15.50 }; //length, width, height
            startLocation = new double[2] { 14.38, 11.24 }; //x and y distance from the top left corner of the plate
            row = 8;
            col = 12;
            maxVolume = 200.00; //200.00 uL
            volumes = new double[this.row, this.col];
            labwareType = "wellplate";

            wellCount = 96;
            wellDistance = 9.00;
            wellDiameter = 5.50;

            //initializes all well volumes to 0.0 uL
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
