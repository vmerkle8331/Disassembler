using System;
using System.Collections.Generic;


namespace Disassembler___Merkle
{
    class Program
    {
        static Dictionary<string, string> opcodeMap = new Dictionary<string, string>
        {
            {"00", "halt"}, {"10", "nop"}, {"20", "rrmovq"}, {"30", "irmovq"}, {"40", "rmmovq"}, {"50", "mrmovq"}, {"60", "addq"}, {"61", "subq"},
            {"62", "andq"}, {"63", "xorq"}, {"70", "jmp"}, {"71", "jle"}, {"72", "jl"}, {"73", "je"}, {"74", "jne"}, {"75", "jge"}, {"76", "jg"}, 
            {"80", "call"}, {"90", "ret"}, {"A0", "pushq"}, {"B0", "popq"}, {"C0", "iaddq"}, {"C1", "isubq"}, {"C2", "iandq"}, {"C3", "ixorq"}
        };

        static void Main()
        {
            Console.WriteLine("Input Hex:");
            string hexCode = Console.ReadLine();
            string assemblyCode = Disassemble(hexCode);
            Console.WriteLine("Disassembled Code:");
            Console.WriteLine(assemblyCode);
        }

        static string Disassemble(string hexCode)
        {
            int index = 0;
            string assemblyCode = "";

            try
            {
                while (index < hexCode.Length)
                {
                    // at least 2 characters left in the string
                    if (index + 2 <= hexCode.Length)
                    {
                        string opcode = hexCode.Substring(index, 2);
                        index += 2;

                        if (opcodeMap.ContainsKey(opcode))
                        {
                            string mnemonic = opcodeMap[opcode];
                            string operands = "";

                            if (mnemonic == "nop" || mnemonic == "halt" || mnemonic == "ret")
                            {
                                assemblyCode += mnemonic + "\n";
                            }
                            else
                            {
                                //at least 4 characters left in the string
                                if (index + 4 <= hexCode.Length)
                                {
                                    string destReg = hexCode.Substring(index, 2);
                                    string srcReg = hexCode.Substring(index + 2, 2);
                                    index += 4;

                                    operands = "%eax, %ebx";  // Replace with appropriate register names
                                    assemblyCode += $"{mnemonic} {destReg}, {srcReg}\n";
                                }
                                else
                                {
                                    //if there's not enough characters left
                                    assemblyCode += "Not Enough Characters For Instruction\n";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //invalid opcode
                            assemblyCode += "Invalid Opcode\n";
                            break;
                        }
                    }
                    else
                    {
                        //if there's not enough characters left
                        assemblyCode += "Invalid Instruction (Not Enough Characters)\n";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //other exceptions
                assemblyCode += "Error: " + ex.Message + "\n";
            }

            return assemblyCode;
        }
    }
}
