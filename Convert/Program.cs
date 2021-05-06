using System;

namespace ZigZag
{
    class Program
    {
        static string ZigZag(string s, int numRows) {
            int sLength = s.Length;
            
            if(numRows == 1){//when there is only one row the string stays the same
                return s;
            }
            
            //when the number of rows is greater than or equal to the string length, the string would have one letter in each row, but the order would still stay the same
            if(numRows >= sLength){
                return s;
            }
            
            char[] charS = new char[sLength]; //will hold the the string in the new order
            int dist = (numRows-1)*2; //how far apart the letters of the first row are on the original string
    //a pattern was found of what order the indexes of the original string were used in the new string
    //the original indexes of the first row were used as the middle to caluculate the indexes of succeeding rows
    // seq[] would hold the indexes of the original string that the new string would read from
    // the following if-else statement created the seq array for the second row        
            int[] seq; 
            if(sLength%dist == 0){//if the last value of the first row was the final value of the original string
                //Console.WriteLine("FIT");
                seq = new int[2*(sLength/dist + 1)];
                for(int j=0; j<sLength/dist + 1; j++){
                    seq[j*2] = j*dist - 1;
                    seq[j*2 + 1] = j*dist + 1;
                    //Console.WriteLine(seq[j*2]);
                    //Console.WriteLine(seq[j*2 + 1]);
                }
            }
            else{
                //Console.WriteLine("NONFIT");
                seq = new int[2*(sLength/dist+2)];
                for(int j=0; j<sLength/dist+2; j++){
                    seq[j*2] = j*dist - 1;
                    seq[j*2 + 1] = j*dist + 1;
                    //Console.WriteLine(seq[j*2]);
                    //Console.WriteLine(seq[j*2 + 1]);
                }
            }
            
            //i will be used to iterate through the new string
            //k will be used to iterate throught the seq array
            int i=0, k=0;
            while(i<sLength){
                //Console.WriteLine("i: " + i);
                //Console.WriteLine("k: " + k);
                //for first row
                if(i < sLength/dist){
                    charS[i] = s[i*dist];
                    i++;
                }
                //if the last value of the first row is same as the final value of the original string
                else if(i == sLength/dist && sLength%dist != 0){
                    charS[i] = s[i*dist];
                    i++;
                }
                //if the index is higher it and even then it is brough into range
                else if(seq[k] > (sLength - 1) && k%2 ==0){
                    seq[k] = seq[k] - 1;
                    k++;
                }
                //if the value held in seq was not in the range of the string lengths
                else if(seq[k] < 0 || seq[k] > (sLength - 1)){
                    k++;
                }
                //by the last row the new string, the seq array would have overlapping indexes
                //the following else statement ensured the same value was not put in twice
                else if(k < seq.Length - 1 && seq[k] == seq[k + 1]){
                    charS[i] = s[seq[k]];
                    i++;
                    k+=2;
                }
                //if the seq index was even, the program would use the index of the original string stored in it and then decrease it by 1
                else if(k%2 ==0){
                    charS[i] = s[seq[k]];
                    seq[k] = seq[k] - 1;
                    i++;
                    k++;
                }
                //if the seq index was odd, the program would use the index of the original string stored in it and then increase it by 1
                else{
                    charS[i] = s[seq[k]];
                    seq[k] = seq[k] + 1;
                    i++;
                    k++;
                }
                //ensures the seq array is looped through
                if(k > seq.Length-1){
                    k=0;
                }
            }
            string newS = new string(charS);//converts character array to string
            return newS;
        }
        static void printZigZag(string newS, int numRows){
            int sLength = newS.Length;
            int gap0 =0, gap1 = (numRows-1)*2, gapCount, index =0;
            bool gap = false; //false is gap0, true is gap1
            for(int row=0; row<numRows; row++){
                gapCount = gap0/2;
                gap = false;
                for(int column=0; column<sLength; column++){
                    if(gapCount == 0){
                        Console.Write(newS[index]);
                        //Console.Write(newS[0]);
                        index++;
                        gap = !gap;
                        gapCount = gap ? gap1 : gap0;
                        if(gapCount == 0){
                            gap = !gap;
                            gapCount = gap ? gap1 : gap0;
                        }
                        //Console.Write("\t");
                        column--;
                    }
                    else if(gapCount > 0){
                        Console.Write("\t");
                        gapCount--;
                    }
                }
                gap0+=2;
                gap1-=2;
                Console.Write("\n");
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string that you would like to convert to zigzag form:");
            string s = Console.ReadLine();
            Console.WriteLine("Enter the number of rows in the zigzag:");
            string rows = Console.ReadLine();
            int numRows = Convert.ToInt32(rows);
            string newS = ZigZag(s,numRows);
            Console.WriteLine("The new String is: \"" + newS + "\"");
            printZigZag(newS, numRows);
        }
    }
}