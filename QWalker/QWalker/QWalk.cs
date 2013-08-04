using System;
using System.Numerics;
using QWalker;

namespace QWalk
{
    public class qWalker
    {

        public static bool isNormalised(Complex rightInitial, Complex leftInitial)
        {
            double leftProb = Complex.Abs(leftInitial) * Complex.Abs(leftInitial);
            double rightProb = Complex.Abs(rightInitial) * Complex.Abs(rightInitial);
            // take accuracy of +- 0.01
            double probability = leftProb + rightProb;
            return 0.99 < probability && probability < 1.01;
        }

        public static WalkResults runWalk(Complex rightInitial, Complex leftInitial)
        {
            var number = new Complex(1, 0);
            // hadamard coin operator entry
            var a = Math.Sqrt(0.5);
            // array to store amplitudes at each coin state of each position
            var positions = new Complex[101, 2];
            // initial condition
            //Complex init = new Complex(0, 1);
            positions[50, 0] = leftInitial;
            positions[50, 1] = rightInitial;
            for (int t = 0; t < 50; t++)
            {
                // array to store amplitudes as they're updated
                var newPositions = new Complex[101, 2];
                for (int i = 1; i < 100; i++)
                {
                    // shift amplitude from 'moving left' coin state
                    newPositions[i, 0] = a * positions[i + 1, 0] + a * positions[i + 1, 1];
                    // shift amplitude from 'moving right' coin state
                    newPositions[i, 1] = a * positions[i - 1, 0] - a * positions[i - 1, 1];
                }
                positions = newPositions;
            }
            // array to store probability at each position 
            var probabilities = new double[101][];
            for (int i = 0; i < 100; i++)
            {
                probabilities[i] = new double[2];
                probabilities[i][0] = i - 50;
                // probability at each position is the modulus squared of each coin state at that position
                probabilities[i][1] = Complex.Abs(positions[i, 0]) * Complex.Abs(positions[i, 0]) + Complex.Abs(positions[i, 1]) * Complex.Abs(positions[i, 1]);
            }


            return new WalkResults { DateRun = DateTime.Now, Results = probabilities, Left = leftInitial, Right = rightInitial };
        }

    }
}
