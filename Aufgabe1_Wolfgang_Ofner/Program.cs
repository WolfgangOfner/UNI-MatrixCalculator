// ----------------------------------------------------------------------- 
// <copyright file="Program.cs" company="FHWN"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This program calculates with matrices.</summary> 
// <author>Wolfgang Ofner</author> 
// -----------------------------------------------------------------------
namespace Default
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Header of the class Program
    /// </summary>
    /// <param></param>
    public class Program
    {
        /// <summary>
        /// Main of the program
        /// </summary>
        /// <param name="args">command line parameter</param>
        public static void Main(string[] args)
        {
            string[] check_name = new string[10];
            string[,,] matrix = new string[11, 500, 500];                                      // initialse Matrix 
            int counter = 0;                                                                    // counter for break
            int name_position = 1;                                                              // shows the position of the name (always 1. dimension)
            int counter_name;
            bool no_error;                                                                      // bool for return from methods
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please Enter your Matrix: ");                                // Input from user
                Console.ResetColor();
                string input = Console.ReadLine();
                string[] split_name = input.Split(new char[] { '=' });                          // check if name was already input
                check_name[counter] = split_name[0];

                bool empty_space_bool;                                                          // bool to check if there is an empty space in a string
                string empty_space_string = " ";
                do
                {                                                                               // deleting spaces for storage
                    check_name[counter] = check_name[counter].Replace(" ", string.Empty);       // deletes spaces in the input befor checking the name
                    empty_space_bool = check_name[counter].Contains(empty_space_string);        // checks if the string contains spaces                           
                }
                while (empty_space_bool == true);

                if (counter > 0)
                {
                    counter_name = counter;
                    do
                    {
                        bool empty_name = string.IsNullOrWhiteSpace(split_name[0]);                     // checks if the string is empty
                        if (empty_name == true)
                        {
                            break;
                        }

                        bool name_input_check = check_name[counter_name - 1].Equals(check_name[counter]);     // checks if the name is already stored
                        if (name_input_check == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Your name is already in the database. Please select an other name");
                            Console.ResetColor();
                            Console.WriteLine();
                            break;
                        }

                        counter_name--;
                    }
                    while (counter_name > 0);
                }

                bool add = input.Contains('+');
                bool sub = input.Contains('-');
                bool mul = input.Contains('*');
                bool det = input.Contains("det");
                if ((add || sub || mul || det) == true)
                {                                                                               // if an operator sign is used in the name
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name can't contain operater signs (\" + \", \" - \", \" * \", \" det \" )");
                    Console.ResetColor();
                    Console.WriteLine();
                    continue;
                }

                no_error = Check(input);                                                        // checks if the input is correct
                if (no_error == false)
                {
                    continue;
                }

                bool empty = string.IsNullOrWhiteSpace(input);                                  // checks if the string is empty
                if (empty == true)
                {
                    break;                                                                      // if empty stop input
                }

                string[] split = input.Split(new char[] { '=', '[', ';', ']' });                // splits matrix into the name and the rows

                do
                {
                    empty_space_bool = split[0].Contains(empty_space_string);                   // checks if the string contains a space
                    split[0] = split[0].Replace(" ", string.Empty);                             // deletes the spaces                
                }
                while (empty_space_bool == true);

                string name = split[0];                                                         // Contains name of matrix

                int row = 1;                                                                    // Variable for counting rows
                int col = 1;                                                                    // Variable for counting columns

                for (int i = 2; i < (split.Length - 1); i++)
                {
                    string[] split1 = split[i].Split(new char[] { ',', ',', });                 // split[i] (2) contains the first number
                    for (col = 0; col < split1.Length; col++)
                    {
                        matrix[name_position, row, col + 1] = split1[col];                      // fils the single numbers in the second and third dimension of the array, col +1 -> to start at x,1,1                                    
                    }

                    matrix[name_position, 0, 1] = Convert.ToString(row);                        // stores the dimension of the rows next to the name
                    matrix[name_position, 0, 2] = Convert.ToString(col);                        // stores the dimension of the columns next to the name and rows
                    row++;
                }

                matrix[name_position, 0, 0] = name;                                             // name of the Matrix, always X,0,0 (starts at 1)
                counter++;
                name_position++;
            }
            while (counter < 10);                                                               // max 10 times

            bool breakout;                                                                      // bool for stopping
            string input_to_calculate;                                                          // input from user
            string[] calculate;                                                                 // array for the input
            string contains_det = "det";
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                Console.WriteLine("Please enter your operation: ");
                Console.ResetColor();
                input_to_calculate = Console.ReadLine();
                Console.WriteLine();

                calculate = input_to_calculate.Split(new char[] { '+', '-', '*' });              // splits the first matrix, the operator and the second matrix
                breakout = string.IsNullOrWhiteSpace(input_to_calculate);
                bool determinant = input_to_calculate.Contains(contains_det);
                if (calculate.Length == 1 && breakout == false && determinant == false)
                {                                                                                // if one element was insert (but not empty)                                  
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your input is invalid. You may have forgotten one of your matrix. A correct input is for example A + B or det(A).");
                    Console.ResetColor();
                }

                if (calculate.Length == 2 && determinant == false)
                {
                    bool matrix_1 = string.IsNullOrWhiteSpace(calculate[0]);
                    if (matrix_1 == true)
                    {                                                                            // if matrix_1 == true -> matrix_1 == empty
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your input is invalid. You may have forgotten one of your matrix. A correct input is for example A + B or det(A).");
                        Console.ResetColor();
                        Console.WriteLine();
                    }

                    bool matrix_2 = string.IsNullOrWhiteSpace(calculate[1]);
                    if (matrix_2 == true)
                    {                                                                            // if matrix_2 == true -> matrix_2 == empty
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your input is invalid. You may have forgotten one of your matrix. A correct input is for example A + B or det(A).");
                        Console.ResetColor();
                        Console.WriteLine();
                    }

                    if (matrix_1 == false && matrix_2 == false)
                    {                                                                               // Matrix calculation start
                        int[,] calculate_matrix_1 = new int[0, 0];                            // initialise two arrays for filtering the two needed matrices
                        int[,] calculate_matrix_2 = new int[0, 0];
                        string calculate_1 = calculate[0];                                          // contains the name of the first matrix
                        string calculate_2 = calculate[1];                                          // contains the name of the second matrix

                        bool unknown_matrix = Initialise_matrix(ref calculate_matrix_1, calculate_1, matrix, counter);
                        if (unknown_matrix == true)
                        {                                                                           // if the bool return from the methdod Initialise_matrix = true -> matrix wasn't found in the database
                            continue;                                                               // ask again for input
                        }

                        unknown_matrix = Initialise_matrix(ref calculate_matrix_2, calculate_2, matrix, counter);
                        if (unknown_matrix == true)
                        {                                                                           // if the bool return from the methdod Initialise_matrix = true -> matrix wasn't found in the database
                            continue;                                                               // ask again for input
                        }

                        bool add = input_to_calculate.Contains('+');
                        bool sub = input_to_calculate.Contains('-');
                        bool mul = input_to_calculate.Contains('*');
                        int[,] result = new int[0, 0];

                        if (add == true && sub == false && mul == false)
                        {                                                                           // checks if the user used only a plus sign
                            no_error = Matrix_add(calculate_matrix_1, calculate_matrix_2, ref result);
                            if (no_error == false)
                            {
                                continue;
                            }

                            Print_matrix(result);
                        }

                        if (sub == true && add == false && mul == false)
                        {                                                                           // checks if the user used only a minus sign
                            no_error = Matrix_sub(calculate_matrix_1, calculate_matrix_2, ref result);
                            if (no_error == false)
                            {
                                continue;
                            }

                            Print_matrix(result);
                        }

                        if (mul == true && add == false && sub == false)
                        {                                                                           // checks if the user used only a muliplication sign
                            no_error = Matrix_mul(calculate_matrix_1, calculate_matrix_2, ref result);
                            if (no_error == false)
                            {
                                continue;
                            }

                            Print_matrix(result);
                        }
                    }
                }

                if (calculate.Length == 3)
                {                                                                                   // calculate.Length == 3 if more than one operator was insert        
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Multiple operator selected. Use only one operater");
                    Console.ResetColor();
                }

                if (determinant == true)
                {
                    string contains_det_bracket = "det(";
                    bool det_bracket = input_to_calculate.Contains(contains_det_bracket);
                    if (det_bracket == true)
                    {
                        int[,] calculate_matrix_1 = new int[0, 0];
                        calculate = input_to_calculate.Split(new char[] { '(', ')' });              // split the input det und brackets from the matrix name
                        if (calculate.Length <= 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong using of brackets. A correct input looks like det(A)");
                            Console.ResetColor();
                            Console.WriteLine();
                            continue;
                        }

                        bool empty = string.IsNullOrWhiteSpace(calculate[2]);
                        if (empty == false)
                        {                                                                           // if emtpy --> closing bracket is missing
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(") is missing");
                            Console.ResetColor();
                            Console.WriteLine();
                            continue;
                        }

                        string calculate_1 = calculate[1];                                          // contains the name of the needed matrix
                        bool unknown_matrix = Initialise_matrix(ref calculate_matrix_1, calculate_1, matrix, counter);
                        if (unknown_matrix == true)
                        {                                                                           // if the bool return from the methdod Initialise_matrix = true -> matrix wasn't found in the database
                            continue;                                                               // jump to the request for input
                        }

                        int det_result = 0;
                        no_error = Matrix_det(calculate_matrix_1, ref det_result);
                        if (no_error == false)
                        {
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;                              // print of the result of the determinant
                        Console.WriteLine("The result of your operation is: ");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(det_result);
                        Console.ResetColor();
                    }
                    else
                    {                                                                               // if input was wrong (not det( )
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong using of brackets. A correct input looks like det(A)");
                        Console.ResetColor();
                        Console.WriteLine();
                        continue;                                                                   // jump to the request for input   
                    }
                }
            }
            while (breakout == false);
        }

        /// <summary>
        /// Method to write the needed matrix from the big array into a small one
        /// </summary>
        /// <param name="calculate_matrix">matrix which is needed for calculation</param>
        /// <param name="calculate">Contains the name of the needed matrix</param>
        /// <param name="matrix">big array with all matrices</param>
        /// <param name="counter">Counter to know how many elements are in the big array</param>
        /// <returns>check if the needed matrix was found or not</returns>
        private static bool Initialise_matrix(ref int[,] calculate_matrix, string calculate, string[,,] matrix, int counter)
        {
            int position_name = 1;
            do
            {
                bool empty_space_bool;                                                                  // bool to check if there is an empty space in a string
                string empty_space_string = " ";
                do
                {
                    calculate = calculate.Replace(" ", string.Empty);                                    // deletes spaces in the input befor checking the name
                    empty_space_bool = calculate.Contains(empty_space_string);                           // checks if the string contains spaces                           
                }
                while (empty_space_bool == true);

                bool matrix_1 = calculate.Equals(matrix[position_name, 0, 0]);                          // searches for the name
                if (matrix_1 == true)
                {                                                                                       // when name was found
                    int row = Convert.ToInt32(matrix[position_name, 0, 1]);                             // number of rows was stored next to the name 
                    int col = Convert.ToInt32(matrix[position_name, 0, 2]);                             // number of columns was stored next to the columns

                    calculate_matrix = new int[row, col];
                    for (int i = 1; i <= calculate_matrix.GetLength(0); i++)
                    {
                        for (int j = 1; j <= calculate_matrix.GetLength(1); j++)
                        {                                                                               // writes the numbers of the needed matrix from the big matrix in a small matrix with fitting dimension   
                            calculate_matrix[i - 1, j - 1] = Convert.ToInt32(matrix[position_name, i, j]);
                        }
                    }

                    break;
                }

                if (position_name == counter)
                {                                                                                       // After checking all fields --> matrix is not in the database
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("At least one of your matrices can't be found in the database");
                    Console.ResetColor();
                    Console.WriteLine();
                    return true;                                                                        // return bool for breaking if matrix is not in the database
                }

                position_name++;
            }
            while (position_name <= counter);
            return false;                                                                               // bool = false -> programm will go on
        }

        /// <summary>
        /// Method to check the input for "[", "]" and "="
        /// </summary>
        /// <param name="input">insert string</param>
        /// /// <returns>check if the shape of the insert matrix was correct or not</returns>
        private static bool Check(string input)
        {
            string equal = "=";
            string bracket_open = "[";
            string bracket_close = "]";
            bool empty;
            bool a = input.Contains(equal);                                                        // checks if the equal sign is in the input
            bool b = input.Contains(bracket_open);                                                 // checks if a open square bracket is in the input
            bool c = input.Contains(bracket_close);                                                // checks if a close square bracket is in the input

            empty = string.IsNullOrWhiteSpace(input);
            if (empty == true)
            {                                                                                       // if empty == true , break and jump to the matrix calculation
                return true;
            }

            if (a == false)
            {                                                                                       // if equal sign is missing
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("= is missing");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            if (b == false)
            {                                                                                       // if open bracket is missing                                                                                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ is missing");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            if (c == false)
            {                                                                                       // if close bracket  is missing                                                                                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("] is missing");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            string[] split = input.Split(new char[] { '=', ';' });                                  // split name and rows
            empty = string.IsNullOrWhiteSpace(split[0]);                                            // checks the field of the array
            if (empty == true)
            {                                                                                       // if its empty --> name is missing
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name is missing");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            bool empty_space_bool;                                                                  // bool to check if there is an empty space in a string
            bool empty_space_test;                                                                  // bool to check if the open bracket is on the right place
            string empty_space_string = " ";
            do
            {
                empty_space_bool = split[1].Contains(empty_space_string);                           // checks if the open bracket is in the second string of the array
                split[1] = split[1].Replace(" ", string.Empty);                                     // deletes the spaces (       [...)
                empty_space_test = split[1].Substring(0, 1).Equals("[");                            // checks if the open bracket is on the first position
            }
            while (empty_space_bool == true);

            if (empty_space_test == false)
            {                                                                                        // if empty_space_test == false --> open bracket is not the first sign   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("False using of parantheses");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            split = input.Split(new char[] { '=', '[', ',', ';' });                               // splits the original input
            do
            {
                int length = split.Length - 1;                                                    // -1 for the highest index                      
                empty_space_bool = split[length].Contains(empty_space_string);                    // checks if the closed bracket is in the last string of the array  
                split[length] = split[length].Replace(" ", string.Empty);                         // deletes the spaces (...]    )
                int bracket_position = split[length].Length - 1;                                  // start of the closed bracket char  
                if (bracket_position >= 0)
                {                                                                                 // if bracket_position >= 0 --> there is a closed bracket
                    empty_space_test = split[length].Substring(bracket_position, 1).Equals("]");
                }
                else
                {                                                                                  // if bracket_position <0 --> the closed bracket is missing
                    empty_space_test = false;
                }
            }
            while (empty_space_bool == true);

            if (empty_space_test == false)
            {                                                                                       // if order of square bracket is wrong
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("False using of parantheses");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            split = input.Split(new char[] { '=', '[', ',', ';', ']' });                            // splits the original input    
            for (int i = 2; i < split.Length - 1; i++)
            {
                int number_check_int;
                bool number_check_bool = int.TryParse(split[i], out number_check_int);              // try to convert every single number into int
                if (number_check_bool == false)
                {                                                                                   // wrong sign was insert
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your elements of the matrix don't contain only integer numbers");
                    Console.ResetColor();
                    Console.WriteLine();
                    return false;
                }
            }

            split = input.Split(new char[] { '=', '[', ';', ']' });                                 // splits the original input
            for (int i = 3; i < split.Length - 1; i++)
            {                                                                                       // checks if the dimension of every row fits with the first row
                string[] array_1_length = split[2].Split(new char[] { ',' });
                string[] array_2_length = split[i].Split(new char[] { ',' });
                if (array_1_length.Length != array_2_length.Length)
                {                                                                                   // if not --> dimension of insert matrix wrong
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dimension of insert Matrix don't fit");
                    Console.ResetColor();
                    Console.WriteLine();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// method for adding two integer arrays(matrices)
        /// </summary>
        /// <param name="matrix1">first matrix</param>
        /// <param name="matrix2">second matrix</param>
        /// <param name="matrix_add_result">result of the adding</param>
        /// <returns>result of adding</returns>
        private static bool Matrix_add(int[,] matrix1, int[,] matrix2, ref int[,] matrix_add_result)
        {
            int row = matrix1.GetLength(0);                                                 // check the number of the rows from matrix 1
            int col = matrix1.GetLength(1);                                                 // check the number of the columns from matrix 2
            matrix_add_result = new int[row, col];                                       // initialise the result matrix with the dimension from the adding 

            if ((matrix1.GetLength(0) == matrix2.GetLength(1)) && (matrix1.GetLength(1) == matrix2.GetLength(1)))
            {                                                                               // adding only starts if dimension fit
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {                                                                           // row counter
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {                                                                       // col counter
                        matrix_add_result[i, j] = matrix1[i, j] + matrix2[i, j];            // adding the same row/col to the result matrix
                    }
                }
            }
            else
            {                                                                               // if dimension don't fit, give an error message
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The dimension of your matrices don't fit");
                Console.ResetColor();
                return false;
            }

            return true;
        }

        /// <summary>
        /// method for subtraction of two integer arrays(matrices)
        /// </summary>
        /// <param name="matrix1">first matrix</param>
        /// <param name="matrix2">second matrix</param>
        /// <param name="matrix_sub_result">result of the subtraction</param>
        /// <returns>result of subtraction</returns>
        private static bool Matrix_sub(int[,] matrix1, int[,] matrix2, ref int[,] matrix_sub_result)
        {
            int row = matrix1.GetLength(0);                                                 // check the number of the rows from matrix 1
            int col = matrix1.GetLength(1);                                                 // check the number of the columns from matrix 2
            matrix_sub_result = new int[row, col];                                       // initialise the result matrix with the dimension from the subtraction

            if ((matrix1.GetLength(0) == matrix2.GetLength(1)) && (matrix1.GetLength(1) == matrix2.GetLength(1)))
            {                                                                               // subtraction only starts if dimension fit
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {                                                                           // row counter
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {                                                                       // col counter
                        matrix_sub_result[i, j] = matrix1[i, j] - matrix2[i, j];            // subtraction of the same row/col to the result matrix
                    }
                }
            }
            else
            {                                                                               // if dimension don't fit, give an error message
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The dimension of your matrices don't fit");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            return true;
        }

        /// <summary>
        /// method for multiplication of two integer arrays(matrices)
        /// </summary>
        /// <param name="matrix1">first matrix</param>
        /// <param name="matrix2">second matrix</param>
        ///  <param name="matrix_mul_result">result of the multiplication</param>
        /// <returns>result of multiplication</returns>
        private static bool Matrix_mul(int[,] matrix1, int[,] matrix2, ref int[,] matrix_mul_result)
        {
            int row = matrix1.GetLength(0);                                                 // check the number of the rows from matrix 1
            int col = matrix2.GetLength(1);                                                 // check the number of the columns from matrix 2
            matrix_mul_result = new int[row, col];                                       // initialise the result matrix with the dimension from the multiplication     

            if (matrix1.GetLength(1) == matrix2.GetLength(0))
            {                                                                               // multiplication only starts if dimension fit
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {                                                                           // row counter
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {                                                                       // col counter
                        for (int jj = 0; jj < matrix1.GetLength(1); jj++)
                        {                                                                   // col counter matrix 1
                            matrix_mul_result[i, j] = (matrix1[i, jj] * matrix2[jj, j]) + matrix_mul_result[i, j];
                        }
                    }
                }
            }
            else
            {                                                                               // if dimension don't fit, give an error message
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The dimension of your matrices don't fit");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method for calculating the determinant of a matrix
        /// </summary>
        /// <param name="matrix">matrix to calculate the determinant</param>
        ///  <param name="determinant">result of determinant calculation</param>
        /// <returns>Value of the determinant</returns>
        private static bool Matrix_det(int[,] matrix, ref int determinant)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            int main_diagonal = 0;                                                                              // Variable for main diagonal
            determinant = 1;                                                                                    // Value for the determinant, initialise with one for first multiplication
            if (row == col)
            {
                int[,] eye_matrix = Identity_matrix(row, col);                                                  // create a new identity matrix
                for (int j = 0; j < matrix.GetLength(1); j++)
                {                                                                                               // colum counter
                    for (int i = j + 1; i < matrix.GetLength(0); i++)
                    {                                                                                           // row counter
                        eye_matrix[i, j] = matrix[i, j] * (-1) / matrix[main_diagonal, main_diagonal];          // multiplicate every number under the main diagonal with -1 and divide through the main diagonal in this column
                    }

                    bool no_error = Matrix_mul(eye_matrix, matrix, ref matrix);                                 // new matrix = changed identity matrix * original matrix (matrix before change and adding to the identity matrix   

                    eye_matrix = Identity_matrix(row, col);                                                     // create a fresh identity matrix
                    main_diagonal++;
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {                                                                                               // multiplicate every number in the main diagonal
                    determinant = matrix[i, i] * determinant;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("At least one of your matrices can't be found in the database");
                Console.ResetColor();
                Console.WriteLine();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method for creating an identity matrix
        /// </summary>
        /// <param name="row">rows of identity matrix</param>
        /// <param name="col">columns of identity matrix</param>
        /// <returns>identity matrix</returns>
        private static int[,] Identity_matrix(int row, int col)
        {
            int[,] eye_matrix = new int[row, col];                          // initialise with needed dimension
            for (int i = 0; i < row; i++)
            {                                                               // row counter
                for (int j = 0; j < col; j++)
                {                                                           // col counter
                    if (j == i)
                    {                                                       // main diagonal = 1
                        eye_matrix[i, j] = 1;
                    }
                    else
                    {                                                       // not main diagonal = 0
                        eye_matrix[i, j] = 0;
                    }
                }
            }

            return eye_matrix;
        }

        /// <summary>
        /// Method for printing the array(matrix) like a matrix
        /// </summary>
        /// <param name="matrix_add_result">result matrix from adding</param>
        private static void Print_matrix(int[,] matrix_add_result)
        {                                                                                   // row counter
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("The result of your operation is: ");
            for (int i = 0; i < matrix_add_result.GetLength(0); i++)
            {
                Console.WriteLine();                                                        // jump into the next line for every new row
                for (int j = 0; j < matrix_add_result.GetLength(1); j++)
                {                                                                           // col counter
                    string space = Convert.ToString(matrix_add_result[i, j]);               // write the chars from matrix into string space
                    int length = space.Length;                                              // count the number of chars
                    length = 12 - length;                                                   // -12 to calculate with numbers from -999.9 bilion to 999.9 bilion (if higher numbers are needed, increase 8 to a higher number)
                    space = " ";                                                            // initialise space with a space 
                    for (int space_counter = 0; space_counter < length; space_counter++)
                    {
                        space = space + " ";                                                // add every run a space 
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}{1} ", matrix_add_result[i, j], space);               // write the result + spaces
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
        }
    }
}