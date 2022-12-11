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
            //length, width, height of labware
            dimensions = new double[3] { 127.76, 86.48, 47.5 };
            //x and y location for the top left corner of the labware slot
            topLeftCorner = new double[2];
            //x and y distance from the top left corner of labware position to center of reservoir
            startLocation = new double[2] { 15.38, 15.0 }; //x and y distance from the top left corner of the plate
            row = 8;
            col = 12;
            maxVolume = 200.00; //200.00 uL
            volumes = new double[row][];
            for (int i = 0; i < row; i++)
            {
                volumes[i] = new double[col];
            }
            labwareType = "wellplate";

            wellCount = 96;
            wellDistance = 9.00;
            wellDiameter = 5.50;

            reservoirSeparation = wellDistance;

            //initializes all well volumes to 0.0 uL
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
