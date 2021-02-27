using System;
using System.Numerics;


/* Purpose of this design is to show how to use BigInteger to 
 * compute the exponential function
 * 
 * 
 * NOTE: Original purpose of this project was to extend BigInteger to support
 * NaN, PositiveInfinity or NegativeInfinity.  Reading through the BigInteger 
 * documentation, it is clear that BigInteger does *not* support NaN, 
 * PositiveInfinity or NegativeInfinity
 * And, only a float or double operation in C# results in these values
 * Hence, it does not make sense in the context of C# to add NaN and both infinity
 * support to BigInteger.
 * 
 * In this program, we will use the power series to compute e^x.
 * e^x = sum(n,0,infinity)[x^n/n!]
 * 
 * Another way is to use a Lookup Table (LUT), precomputed and stored in memory:
 * Input (x)  Output (e^x)
 *     0          1
 *     0.5       1.649
 *     ...       ...
 * Advantages of the LUT:
 * O(1):  constant time access (assuming we use arrays to store the data, need to know
 *   ahead of time how many values we are storing)
 * Disadvantage of the LUT:
 * We can only return precomputed data present in the table.  
 * For example, in the table above, we can't compute e^(0.25).
 * But, the fact that we have O(1) (constant time access) is so useful that
 * a practical digital signal processing algorithm called CORDIC uses precomputed
 * lookup tables for trigonometric functions.
 */

namespace BigIntegerExample {
    class Program {
        /* Notes : 
         * 1.  This approach is also called "Unit Testing".
         * 2.  Always remember: K.I.S.S = Keep it Super Simple!
         */

        // STEP 1:  Understand the problem : return sum(n,0,N)[x^n/n!]        
        private static double BigIntegerExponential(BigInteger x) {
            /* STEP 2: Devise a plan:  breakdown the problem into pieces. 
             * Essentially we have to compute a sum ("piece 1") of x^n ("piece 2")
             * over n! ("piece 3")
             * We only return N terms in the series
             */
            // Next is STEP 3: Carry out the plan and STEP 4: Check your answer
            BigInteger N = 99; // @TODO(Bharath): perhaps ask the user for # of terms? 
            double sum = 0.0;
            // @TODO(Bharath):  Loss of precision in factorial below!
            for(int n = 0;n < N;n++) {                
                sum += ((double)BigInteger.Pow(x, n)) / ((double)Program.factorial(n));
            }            
            return sum;
        }
        /* n! = 1 if n == 0,
         *    = n*[(n-1)*(n-2)...3.2.1]  if n > 0 (we don't care about negative integers)
         *    = n*[(n-1)!]
         * Example:
         * 0! = 1 (by definition)
         * 1! = 1*0! 
         *    = 1
         * 2! = 2*1!
         *    = 2
         * 3! = 3*2!
         *    = 3*2*1!
         *    = 3*2*1*0!
         *    = 3*2*1*1
         *    = 6
         * Notice:  factorial is a RECURSIVE function
         */
        private static BigInteger factorial(BigInteger n) {
            if (n == 0) {                
                return (new BigInteger(1.0)); // base case
            }
            return n * Program.factorial(n - 1);
        }

        static void Main(string[] args) {
            BigInteger x = new BigInteger(1); // uses BigInteger(double) constructor
            Console.WriteLine("Implement e^x using BigInteger");
            Console.WriteLine(Program.BigIntegerExponential(x));         
        }

       
    }
}
