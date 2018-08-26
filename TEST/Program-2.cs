using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Application_CopyFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ����� ���������� � ��������� 'Copy File'! ��������� ���� ��������� �� ������ ��������� ����������� ��� ����������" +
                              " (���������� �������������� ��� ������������� �� ���������) � ��������� ��������.\n");
        repeatCommand:
            Console.WriteLine(" 1) ��������� � �������� ����������� ����������\n" +
                              " 2) ��������� � �������� ��������� ����������\n" +
                              " 3) ��������� ���������\n");
            Console.Write(" >> ������� ����� ����������� �������: "); Console.ForegroundColor = ConsoleColor.White;
            int comm;
            try
            {
                comm = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" " + exc.Message + "\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                goto repeatCommand;
            }
        
            switch (comm)
            {
                case 1:
                    {
                        Console.WriteLine();
                        ICopy();
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine();
                        DocCopy();
                        break;
                    }
                case 3:
                    {
                        return;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" ����������� �������. ��������� �������!\n"); Console.ForegroundColor = ConsoleColor.Cyan;
                        goto repeatCommand;
                    }
            }
        repeatExit:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n ������ ���������� ������ � ����������? [Y - ��] / [N - ���]: "); Console.ForegroundColor = ConsoleColor.White;
            string commExit = Console.ReadLine();
            if (commExit == "Y" || commExit == "y") { Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(); goto repeatCommand; }
            else if (commExit == "N" || commExit == "n") return;
            else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(" ����������� �������. ��������� �������!"); goto repeatExit; }
        }

        static void ICopy()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ������� ��� ������������/������������ ��������� (��� ����� .data �� �����.): "); Console.ForegroundColor = ConsoleColor.White;
            string DocName = Console.ReadLine();

            FileStream fin;
            try
            {
                fin = new FileStream(DocName + ".data", FileMode.OpenOrCreate);
                if (fin.Length != 0)
                {
                repeatInfoFile:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" �������� �������� � ���� ������ ����������. �� ������ ������������ ��? [Y - ��] / [N - ���]: "); Console.ForegroundColor = ConsoleColor.White;
                    string commInfoFile = Console.ReadLine();

                    if (commInfoFile == "Y" || commInfoFile == "y") { Console.ForegroundColor = ConsoleColor.Cyan; fin.SetLength(0); }
                    else if (commInfoFile == "N" || commInfoFile == "n") { fin.Position = fin.Length; }
                    else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(" ����������� �������. ��������� �������!"); goto repeatInfoFile; }
                }
            }
            catch (IOException exc)
            {
                Console.WriteLine(" ������: " + exc.Message);         // ���������� �������� ������ ��������� ���� �� ��������� ����� ConsoleErrorException,
                Console.WriteLine(" ������ ������� ��������������."); // ��� � ������ �� ��������� ����� ���������� try-catch, �.�. ���������� ������������
                return;                                               // ���������� ��� ������, ��������� � ��������� ��������������� ��������� ���������� fin
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ������� ����������� �����: "); Console.ForegroundColor = ConsoleColor.White;
            string TextCopy = Console.ReadLine();

            StreamWriter fin_out = new StreamWriter(fin);
            try
            {
                fin_out.Write(TextCopy);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" ������. ��������� ��������.");
            }
            catch (IOException exc)
            {
                ConsoleErrorException(exc);
            }
            finally { fin_out.Close(); }

        }

        static void ConsoleErrorException(IOException exc)
        {
            Console.WriteLine(" ������: " + exc.Message);
            Console.WriteLine(" ������ ������� ��������������.");
            return;
        }
        static void DocCopy()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ������� ��� ������������/������������ ��������� (��� ����� .data �� �����.): "); Console.ForegroundColor = ConsoleColor.White;
            string DocName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ������� ��� �����, �� �������� ����� ������������ ���������� (��� ����� .data �� �����.): "); Console.ForegroundColor = ConsoleColor.White;
            string FileName = Console.ReadLine(); 
            int i;

            try
            {
                FileStream fout = new FileStream(DocName + ".data", FileMode.OpenOrCreate);
                FileStream fin = new FileStream(FileName + ".data", FileMode.Open);

                char[] ch = new char[fin.Length];
                int j = 0;
                do
                {
                    i = fin.ReadByte();
                    if (i != -1) { fout.WriteByte((byte)i); ch[j] = (char)i; j++; }
                } while (i != -1);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ������. ���������� ���������: "); Console.ForegroundColor = ConsoleColor.White;
                for (int k = 0; k < fin.Length; k++) { Console.Write(ch[k]); }
            }
            catch (IOException exc)
            {
                Console.WriteLine(" ������: " + exc.Message);
                Console.WriteLine(" ������ ������� ��������������.");
                return;
            }
        }
    }
}