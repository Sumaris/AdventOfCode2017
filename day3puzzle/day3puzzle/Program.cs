﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //368078 
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine(Pgm1(input));
            Console.WriteLine(Pgm2(input));

            Console.ReadKey();
        }
        private static int Pgm1(int input)
        {

            double inputd = Math.Sqrt((double)input);
            int floor = (int)inputd + 1;
            int goodSquare = floor * floor;
            int squareBelow = (floor - 2) * (floor - 2);
            int matrixStart = (goodSquare - squareBelow) / 2;
            int matrixLength = matrixStart / 2;

            int steps = 0;

            //is input closer to goodSquare or squareBelow
            if (input > matrixStart)
            {
                //matrixStart < input < goodSquare
                if (input > goodSquare - matrixLength)
                {
                    //goodSquare-matrixLength < input < goodSquare, bottom line
                    steps = matrixLength - (goodSquare - input);
                }
                else
                {
                    //matrixStart < input < goodSquare-matrixLength, left column
                    steps = matrixLength - ((goodSquare - matrixLength) - input);
                }
            }
            else
            {
                if (input > matrixLength)
                {
                    //matriLength < input < matrixStart, top line
                    steps = matrixLength - (matrixStart - input);
                }
                else
                {
                    //squareBelow < input < matrixLength, right column
                    steps = matrixLength - input;
                }
            }

            if (steps < matrixLength)
            {
                steps = matrixLength - steps;
            }
            return steps;
        }

        private static int Pgm2(int input)
        {
            int[,] n = new int[100, 100];
            int x=49;
            int y=49;
            n[x, y] = 1;

            int[] previousPos = { x, y };
            string dirString = "right";
            int[] direction = { 1, 0 }; //1,0 = right (initial). -1,0 = left. 0,1 = down. 0,-1 = up
            
            x += direction[0];
            y += direction[1];

            n[x, y] = 1;
            previousPos = new int[] { x, y };

            dirString = "up";
            direction = new int[] { 0, -1 };

            int a = 0; // == previous node value
            int b = 0; // == sum of all n-values in proximity
            do
            {
                //get next n and fill it out with a+b
                x += direction[0];
                y += direction[1];
                a = n[previousPos[0], previousPos[1]];
                
                b = n[x-1, y-1] + n[x, y-1] + n[x+1, y-1] + n[x+1, y] + n[x+1, y+1] + n[x, y+1] + n[x-1, y+1] + n[x-1, y] - n[previousPos[0], previousPos[1]];

                //Any connecting nodes? if not, go back and change direction
                if (b > 0)
                {
                    //Update pos, previous pos, add new number
                    n[x, y] = a + b;
                    previousPos = new int[] { x, y };
                }
                else
                {
                    x = previousPos[0];
                    y = previousPos[1];
                    //chose another direction and go back one square
                    switch (dirString)
                    {
                        case "right":
                            dirString = "up";
                            direction[0] = 0;
                            direction[1] = -1;
                            break;
                        case "down":
                            dirString = "right";
                            direction[0] = 1;
                            direction[1] = 0;
                            break;
                        case "left":
                            dirString = "down";
                            direction[0] = 0;
                            direction[1] = 1;
                            break;
                        case "up":
                            dirString = "left";
                            direction[0] = -1;
                            direction[1] = 0;
                            break;
                    }
                }
            } while (n[x, y] < input);
            return n[x, y];
        }
    }
}
