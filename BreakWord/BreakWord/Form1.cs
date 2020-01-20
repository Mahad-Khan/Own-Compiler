using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
namespace BreakWord
{
    public partial class Form1 : Form
    {

        Node CurrentNode123;
        bool f;

        KeywordListClass keywordList;
        PunctListClass punctList;
        OperatorList operatorList;
        LinkList BreakWordList;
        LinkList BreakWordList_Clear;
        LinkList TokenList;
        LinkList TokenList2;
        Regex identifier;
        Regex int_constant;
        Regex float_constant;
        Regex char_constant;
        Regex string_constant;
        bool result_identifier;
        bool result_int;
        bool result_float;
        bool result_char;
        bool result_string;
        private string CLassPart;
        private int StringLenght;
        private int LineNo;
        string allText;
        bool flagforN = false;





        //public void BreakWordFunction(string inputText)
        //{

        //    string temp = null;
        //    string flag1 = null;
        //    string temp1 = null;
        //    char flagChar;

        //    int strLenght = inputText.Length;

        //    for (int i = 0; i < strLenght; i++)
        //    {
        //        flagChar = inputText[i];
        //        flag1 = Convert.ToString(inputText[i]);
        //        temp1 = flag1;
        //        if (i == strLenght - 1 && temp == null)                     //taky operator agy access na karain
        //        {                                                    //temp null q k temp ki value zaya na ho
        //            if (flag1 == "\n")
        //            {
        //                BreakWordList.AddBreakPartInList("Return");
        //                continue;
        //            }
        //            BreakWordList.AddBreakPartInList(flag1);
        //            continue;
        //        }


        //        if(flag1=="/" && inputText[i+1]=='/')          //single line comment
        //        {
        //            while(true)
        //            {
        //                i++;
        //                if(inputText[i]=='\n')
        //                {
        //                    BreakWordList.AddBreakPartInList("Return");
        //                    break;
        //                }
        //                if(i>= strLenght-1)
        //                {
        //                    break;
        //                }
        //            }
        //            continue;
        //        }

        //        if(flag1=="/" && inputText[i+1]=='*')         //multiLine Comment
        //        {
        //            i += 2;
        //            while (true)
        //            {

        //                if (i >= strLenght - 1)
        //                {
        //                    break;
        //                }
        //                if(inputText[i]=='\n')
        //                {
        //                    BreakWordList.AddBreakPartInList("Return");
        //                }
        //                if (inputText[i] == '*' && inputText[i+1]=='/')
        //                {
        //                    i++;
        //                    break;
        //                }
        //                i++;

        //            }
        //            continue;
        //        }

        //        if (flag1 == "'")             //char ki kahani
        //        {
        //            if (temp != null)
        //            {
        //                BreakWordList.AddBreakPartInList(temp);
        //                temp = null;
        //            }
        //            while (true)
        //            {

        //                temp = temp + inputText[i];
        //                i++;
        //                if (i >= strLenght)
        //                {
        //                    i--;
        //                    break;

        //                }

        //                string ch = Convert.ToString(inputText[i]);
        //                if (ch == "'")
        //                {
        //                    temp = temp + inputText[i];
        //                    BreakWordList.AddBreakPartInList(temp);
        //                    temp = null;
        //                    break;
        //                }
        //                if (inputText[i] == '\n')
        //                {
        //                    i--;
        //                    BreakWordList.AddBreakPartInList(temp);
        //                    temp = null;
        //                    break;
        //                }

        //            }
        //            continue;

        //        }

        //        if (flagChar == '"')                                        //String ki Kahani mri Zubani
        //        {
        //            if (temp != null)
        //            {
        //                BreakWordList.AddBreakPartInList(temp);
        //                temp = null;
        //            }
        //            while (true)
        //            {
        //                temp = temp + inputText[i];
        //                i++;
        //                if (i >= strLenght - 1)
        //                {
        //                    i--;
        //                    break;
        //                }

        //                if (inputText[i] == '\\')
        //                {
        //                    temp = temp + inputText[i];
        //                    i++;
        //                    if (i >= strLenght - 1)
        //                    {
        //                        i--;
        //                        break;

        //                    }
        //                    temp += inputText[i];
        //                    i++;
        //                }

        //                if (inputText[i] == '"')
        //                {
        //                    temp = temp + inputText[i];
        //                    BreakWordList.AddBreakPartInList(temp);
        //                    temp = null;
        //                    break;
        //                }








        //                if (inputText[i] == '\n')
        //                {
        //                    i--;
        //                    BreakWordList.AddBreakPartInList(temp);
        //                    temp = null;
        //                    break;
        //                }

        //            }
        //            continue;
        //        }

        //        if (flag1 == " " && temp == null)
        //        {
        //            continue;                           // Jab 1 se ziada Space Hun
        //        }

        //        if (CheckBreakCondi(flag1))
        //        {
        //            if ((flag1 == "-" && Convert.ToString(inputText[i + 1]) == ">" ||
        //                flag1 == "+" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "-" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "/" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "*" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "%" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "<" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == ">" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "=" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == "~" && Convert.ToString(inputText[i + 1]) == "=" ||
        //                flag1 == ":" && Convert.ToString(inputText[i + 1]) == ":") && temp == null)
        //            {
        //                i++;
        //                string fla = Convert.ToString(inputText[i]);
        //                flag1 = flag1 + fla;
        //                BreakWordList.AddBreakPartInList(flag1);
        //                continue;
        //            }


        //            if (flag1 == "\n" && temp == null)
        //            {
        //                BreakWordList.AddBreakPartInList("Return");
        //                continue;
        //            }

        //            if (temp == null)
        //            {
        //                BreakWordList.AddBreakPartInList(temp1);
        //                temp1 = null;
        //                continue;
        //            }

        //            BreakWordList.AddBreakPartInList(temp);
        //            temp = null;
        //            if (i != 0)
        //            { i--; }
        //            continue;
        //        }

        //        temp = temp + inputText[i];
        //    }
        //    if (temp != null)
        //        BreakWordList.AddBreakPartInList(temp);
        //}

        //public bool CheckBreakCondi(string s)
        //{

        //    if (s == "\n")
        //    {

        //        i--;
        //        return true;
        //    }

        //    if (s == " ")
        //    {
        //        return true;
        //    }

        //    if (s == "(" || s == ")" || s == "{" || s == "}" || s == "[" || s == "]" ||
        //        s == ";" || s == "," || s == "+" || s == "-" || s == "*" || s == "/" ||
        //        s == "%" || s == "<" || s == "<" || s == "=" || s == "-" ||
        //        s == "?" || s == ":" || s == "~" || s==".")
        //    {
        //        if (i != 0)
        //        {
        //            i--;
        //        }
        //        return true;
        //    }

        //    return false;
        //}
        public void OutNextLine()
        {

            while (CurrentNode123 != null)
            {
                if (CurrentNode123.GetClassPart() == "NEWLINE")
                {
                    if (f)
                    {
                        string cpart = CurrentNode123.GetClassPart();
                        string vpart = CurrentNode123.GetValuePart();
                        int LNo = CurrentNode123.GetLineNo();
                        TokenList2.CreateToken(cpart, vpart, LNo);
                        CurrentNode123 = CurrentNode123.GetNextValAdd();
                        f = false;
                    }
                    else { CurrentNode123 = CurrentNode123.GetNextValAdd(); }
                }
                else
                {
                    string cpart = CurrentNode123.GetClassPart();
                    string vpart = CurrentNode123.GetValuePart();
                    int LNo = CurrentNode123.GetLineNo();
                    TokenList2.CreateToken(cpart, vpart, LNo);
                    CurrentNode123 = CurrentNode123.GetNextValAdd();
                    f = true;
                }


            }

        }

        public void AddItemsInToken()
        {
            TokenList = new LinkList();
            Node current = BreakWordList_Clear.GetHead();
            string temp = null;
            bool boolValue = false;

            for (; current != null; current = current.NextValueAdd)
            {
                temp = current.ValuePart;
                string previous = temp;
                if (temp == "commentReturn")
                {
                    LineNo++;
                    continue;
                }
                if (temp == "Return")
                {
                    TokenList.CreateToken("NEWLINE", "newline", LineNo);
                    LineNo++;
                    continue;
                }

                boolValue = MatchBreakWord(temp);

                if (boolValue == true)
                {
                    TokenList.CreateToken(CLassPart, temp, LineNo);
                }
                else
                {
                    TokenList.CreateToken("InValid", temp, LineNo);
                }


            }
            TokenList.CreateToken("$", "$", LineNo);
        }



        public void break_words(string source_code)

        {

            //   ArrayList pre_tokens = new ArrayList();
            //ArrayList pre_tokens_clear = new ArrayList();
            //ArrayList tokens = new ArrayList();
            Regex int_constant = new Regex("^[0-9]+$");

            string temp = "";
            string temp1 = "";
            string temp2 = "";

            for (int i = 0; i < source_code.Length; i++)
            {

            start_pretoken:


                if (i == source_code.Length)
                {
                    break;
                }

                if (i == (source_code.Length - 1))
                {
                    goto single_check;
                }


                if (source_code[i] == '.' && source_code[i + 1] == '1' ||
                    source_code[i] == '.' && source_code[i + 1] == '2' ||
                    source_code[i] == '.' && source_code[i + 1] == '3' ||
                    source_code[i] == '.' && source_code[i + 1] == '4' ||
                    source_code[i] == '.' && source_code[i + 1] == '5' ||
                    source_code[i] == '.' && source_code[i + 1] == '6' ||
                    source_code[i] == '.' && source_code[i + 1] == '7' ||
                    source_code[i] == '.' && source_code[i + 1] == '8' ||
                    source_code[i] == '.' && source_code[i + 1] == '9' ||
                    source_code[i] == '.' && source_code[i + 1] == '0')
                {
                    goto float_check;
                }

            single_check:
                if (source_code[i] != ' ' && source_code[i] != '\n' && source_code[i] != ',' 
                  && source_code[i] != ':' && source_code[i] != '{' && source_code[i] != '}' && source_code[i] != '('
                  && source_code[i] != ')' && source_code[i] != '[' && source_code[i] != ']' && source_code[i] != '+'
                  && source_code[i] != '-' && source_code[i] != '/' && source_code[i] != '*' && source_code[i] != '%'
                  && source_code[i] != '='  && source_code[i] != '<' && source_code[i] != '>' && source_code[i] != '~'
                  && source_code[i] != '"' && source_code[i] != '\'' && source_code[i] != '.' && source_code[i] != '|')
                {
                    temp = temp + source_code[i];

                    if (i == (source_code.Length - 1))
                    {
                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                    }
                    i++;
                    goto start_pretoken;
                }

                BreakWordList.AddBreakPartInList(temp);
                //pre_tokens.Add(temp);
                temp = "";

                if (source_code[i] == '"')
                {
                str_start:
                    temp = temp + source_code[i];

                    if (i == (source_code.Length - 1))
                    {
                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        temp = "";
                        break;
                    }

                    i++;

                    if (source_code[i] == '\n')
                    {

                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        temp = "";
                        goto start_pretoken;
                    }

                    if (source_code[i] == '\\')
                    {
                        temp = temp + source_code[i];
                        i++;
                        goto str_start;
                    }

                    if (source_code[i] == '"')
                    {
                        temp = temp + source_code[i];
                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        temp = "";
                        i++;
                        goto start_pretoken;
                    }
                    else
                    {
                        goto str_start;
                    }
                }
                if (source_code[i] == '\'')
                {
                char_start:
                    temp = temp + source_code[i];

                    if (i == (source_code.Length - 1))
                    {
                        BreakWordList.AddBreakPartInList(temp);
                        // pre_tokens.Add(temp);
                        temp = "";
                        break;
                    }

                    i++;

                    if (source_code[i] == '\n')
                    {
                        BreakWordList.AddBreakPartInList(temp);
                        // pre_tokens.Add(temp);
                        temp = "";
                        goto start_pretoken;
                    }

                    if (source_code[i] == '\\')
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            temp = temp + source_code[i];
                            i++;
                            if (i == source_code.Length)
                            {
                                break;
                            }
                        }
                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        temp = "";
                        goto start_pretoken;
                    }
                    else
                    {
                        for (int p = 0; p < 2; p++)
                        {
                            temp = temp + source_code[i];
                            i++;
                            if (i == source_code.Length)
                            {
                                break;
                            }
                        }
                    }
                    //  pre_tokens.Add(temp);
                    BreakWordList.AddBreakPartInList(temp);
                    temp = "";
                    goto start_pretoken;
                }

                if (source_code[i] == '/' && source_code[i + 1] == '*')
                {
                    temp = "";
                    temp = Convert.ToString(source_code[i]) + Convert.ToString(source_code[i + 1]);
                    i++;
                    i++;
                double_line_comment:
                    if (source_code[i] == '\n')
                    {
                        temp = "";
                        temp = Convert.ToString(Keys.Enter);
                        temp = "comment" + temp;
                        BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        temp = "";
                        i++;
                        goto double_line_comment;
                    }
                    if (source_code[i] == '*' && source_code[i + 1] == '/')
                    {

                        temp = temp + Convert.ToString(source_code[i]) + Convert.ToString(source_code[i + 1]);
                        // pre_tokens.Add(temp);
                        // BreakWordList.AddBreakPartInList(temp);
                        temp = "";
                        i++;
                        i++;
                        if (i == source_code.Length)
                        {
                            break;
                        }
                        goto start_pretoken;

                    }
                    else
                    {
                        temp = temp + source_code[i];
                        i++;
                        goto double_line_comment;
                    }
                }





                if (source_code[i] == '/' && source_code[i + 1] == '/')  //single line comment
                {

                    temp = "";
                    temp = Convert.ToString(source_code[i]) + Convert.ToString(source_code[i + 1]);
                    i++;
                    i++;
                single_line_comment:
                    if (source_code[i] == '\n')
                    {
                        // BreakWordList.AddBreakPartInList(temp);
                        //pre_tokens.Add(temp);
                        flagforN = true;
                        temp = "";
                        goto start_pretoken;


                    }
                    else
                    {
                        temp = temp + source_code[i];
                        i++;
                        goto single_line_comment;
                    }

                }













                if (source_code[i] == '=' && source_code[i + 1] == '=' ||    //addition
                    source_code[i] == '+' && source_code[i + 1] == '=' ||
                  source_code[i] == '-' && source_code[i + 1] == '>' ||
                    source_code[i] == '-' && source_code[i + 1] == '=' ||
                   source_code[i] == '/' && source_code[i + 1] == '=' ||
                   source_code[i] == '*' && source_code[i + 1] == '=' ||
                   source_code[i] == '~' && source_code[i + 1] == '=' || 
                   source_code[i] == '%' && source_code[i + 1] == '=' ||
                   source_code[i] == '<' && source_code[i + 1] == '=' ||
                   source_code[i] == '>' && source_code[i + 1] == '=' ||
                   source_code[i] == ':' && source_code[i + 1] == ':')
                {
                    temp = Convert.ToString(source_code[i]) + Convert.ToString(source_code[i + 1]);
                    //pre_tokens.Add(temp);

                    BreakWordList.AddBreakPartInList(temp);
                    temp = "";
                    i++;
                    i++;
                    goto start_pretoken;

                }












                if (source_code[i] == ',' || source_code[i] == ':' || source_code[i] == '{' || source_code[i] == '~'
                    || source_code[i] == '}' || source_code[i] == '(' || source_code[i] == ')' || source_code[i] == '['
                    || source_code[i] == ']' || source_code[i] == '+' || source_code[i] == '-'
                    || source_code[i] == '/' || source_code[i] == '*' || source_code[i] == '%' || source_code[i] == '<'
                    || source_code[i] == '>' || source_code[i] == '=' || source_code[i] == '.' || source_code[i] == '|')
                {

                    temp = Convert.ToString(source_code[i]);
                    BreakWordList.AddBreakPartInList(temp);
                    //pre_tokens.Add(temp);
                    temp = "";
                    i++;
                    goto start_pretoken;
                }

            float_check:
                if (source_code[i] == '.' && source_code[i + 1] == '1' ||
                    source_code[i] == '.' && source_code[i + 1] == '2' ||
                    source_code[i] == '.' && source_code[i + 1] == '3' ||
                    source_code[i] == '.' && source_code[i + 1] == '4' ||
                    source_code[i] == '.' && source_code[i + 1] == '5' ||
                    source_code[i] == '.' && source_code[i + 1] == '6' ||
                    source_code[i] == '.' && source_code[i + 1] == '7' ||
                    source_code[i] == '.' && source_code[i + 1] == '8' ||
                    source_code[i] == '.' && source_code[i + 1] == '9' ||
                    source_code[i] == '.' && source_code[i + 1] == '0')
                {

                    if (temp == "")
                    {
                        goto dot_creat;
                    }

                    temp = "";

                temp1_creat:
                    i--;

                    if (i == -1)
                    {
                        goto dot_enter;

                    }

                    else if (source_code[i] != '=' && source_code[i] != ' ' && source_code[i] != '\n' && source_code[i] != '.')

                    {
                        temp1 = source_code[i] + temp1;
                        goto temp1_creat;

                    }




                dot_enter:
                    i++;
                dot_creat:

                    if (source_code[i] != '.')
                    {
                        i++;
                        goto dot_creat;
                    }


                    temp = Convert.ToString(source_code[i]);


                temp2_creat:

                    i++;
                    if (i == source_code.Length)
                    {
                        goto regex_passing;

                    }

                    if (source_code[i] != '=' && source_code[i] != ' ' && source_code[i] != '\n' && source_code[i] != '.')
                    {
                        temp2 = temp2 + source_code[i];
                        goto temp2_creat;
                    }



                regex_passing:
                    bool result_temp1 = int_constant.IsMatch(Convert.ToString(temp1));
                    bool result_temp2 = int_constant.IsMatch(Convert.ToString(temp2));

                    if (result_temp1 == true && result_temp2 == true)
                    {
                        //  pre_tokens.Add(temp1 + temp + temp2);
                        BreakWordList.AddBreakPartInList(temp1 + temp + temp2);
                        temp = "";
                        temp1 = "";
                        temp2 = "";
                    }

                    if (result_temp1 == false && result_temp2 == true)
                    {

                        // pre_tokens.Add(temp1);
                        //pre_tokens.Add(temp + temp2);
                        BreakWordList.AddBreakPartInList(temp1);
                        BreakWordList.AddBreakPartInList(temp + temp2);
                        temp = "";
                        temp1 = "";
                        temp2 = "";
                    }

                    if (result_temp1 == false && result_temp2 == false)
                    {
                        //   pre_tokens.Add(temp1);
                        // pre_tokens.Add(temp);
                        //pre_tokens.Add(temp2);
                        BreakWordList.AddBreakPartInList(temp1);
                        BreakWordList.AddBreakPartInList(temp);
                        BreakWordList.AddBreakPartInList(temp2);
                        temp = "";
                        temp1 = "";
                        temp2 = "";
                    }

                    if (result_temp1 == true && result_temp2 == false)
                    {
                        //pre_tokens.Add(temp1);
                        //pre_tokens.Add(temp);
                        //pre_tokens.Add(temp2);
                        BreakWordList.AddBreakPartInList(temp1);
                        BreakWordList.AddBreakPartInList(temp + temp2);

                        temp = "";
                        temp1 = "";
                        temp2 = "";
                    }

                    goto start_pretoken;
                }


                if (source_code[i] == '\n')
                {
                    if (flagforN == true)
                    {
                        temp = "";
                        temp = Convert.ToString(Keys.Enter);
                        temp = "comment" + temp;
                        BreakWordList.AddBreakPartInList(temp);
                        flagforN = false;
                        //pre_tokens.Add(temp);
                        temp = "";
                        i++;
                        goto start_pretoken;

                    }
                    temp = "";
                    temp = Convert.ToString(Keys.Enter);
                    BreakWordList.AddBreakPartInList(temp);
                    //pre_tokens.Add(temp);
                    temp = "";
                    i++;
                    goto start_pretoken;

                }


            }





            //richTextBox2.Text = "";

            Node current = BreakWordList.GetHead();
            while (current != null)
            {

                if (current.ValuePart != " " && current.ValuePart != "\n" && current.ValuePart != " " && current.ValuePart != "")
                {
                    BreakWordList_Clear.AddBreakPartInList(current.ValuePart);
                }
                current = current.NextValueAdd;

            }



        }





        private bool MatchBreakWord(string breakTempWord)
        {

            TrueAndClassPartReturn isKeyword = keywordList.l.IsMatch(breakTempWord);

            if (isKeyword.isAvail == true)
            {
                CLassPart = isKeyword.cp;

                return true;
            }

            TrueAndClassPartReturn isPunct = punctList.PunctList.IsMatch(breakTempWord);

            if (isPunct.isAvail == true)
            {
                CLassPart = isPunct.cp;

                return true;
            }

            TrueAndClassPartReturn isOperator = operatorList.OpList.IsMatch(breakTempWord);

            if (isOperator.isAvail == true)
            {
                CLassPart = isOperator.cp;

                return true;
            }

            result_identifier = identifier.IsMatch(breakTempWord);
            result_int = int_constant.IsMatch(breakTempWord);
            result_float = float_constant.IsMatch(breakTempWord);
            result_char = char_constant.IsMatch(breakTempWord);
            result_string = string_constant.IsMatch(breakTempWord);

            if (result_identifier == true)
            {
                CLassPart = "ID";
                return true;
            }
            if (result_int == true)
            {
                CLassPart = "INT_CONST";
                return true;
            }
            if (result_float == true)
            {
                CLassPart = "FLOAT_CONST";
                return true;
            }
            if (result_char == true)
            {
                CLassPart = "CHAR_CONST";
                return true;
            }
            if (result_string == true)
            {
                CLassPart = "STRING_CONST";
                return true;
            }

            else
                return false;
        }


        public Form1()
        {
            InitializeComponent();


            
            keywordList = new KeywordListClass();
            punctList = new PunctListClass();
            operatorList = new OperatorList();
            identifier = new Regex("^[_a-zA-Z]*[a-zA-Z]{1,}[a-zA-Z0-9]*$");
            int_constant = new Regex("^[0-9]+$");
            char_constant = new Regex(@"^'[a-zA-Z0-9]'$|^'[+-]'$|^'\\[\\nrt]'$");
            float_constant = new Regex("^[0-9]*[.][0-9]+$");
            string_constant = new Regex("^\"([a-zA-Z0-9]|(\\\\\\\\)|[!@#$%^&*()-=+{}|;:<>,.?/']|\\[|\\]|_|\\\\[nrtfab0]|\\\\\"|\\\\')*\"$");
            result_identifier = false;
            result_int = false;
            result_float = false;
            result_char = false;
            result_string = false;
            CLassPart = null;
       

            //char_constant = new Regex(@"^'[a-zA-Z0-9]'$|^'\\[\\nrtfab0]'$|^'[+-=_();<>,[]{}*/!@#]'$");
            // string_constant = new Regex("^\"[^\"]*(\\\\\")*[^\"]*$");




        }

        private void button1_Click(object sender, EventArgs e)
        {

            f = false;
            richTextBox6.Text = null;
           TokenList2 = new LinkList();
            LineNo = 1;
            BreakWordList = new LinkList();
            BreakWordList_Clear = new LinkList();
           // richTextBox2.Text = null;
           // richTextBox3.Text = null;
            allText = richTextBox1.Text;
            StringLenght = allText.Length;
            //BreakWordFunction(allText);
            break_words(allText);

           // richTextBox2.Text = BreakWordList_Clear.DisplayList();

            AddItemsInToken();
           // richTextBox3.Text = TokenList.DisplayTokens();
            //SyntaxAnalyzer SA = new SyntaxAnalyzer(TokenList);


            CurrentNode123 = TokenList.GetHead();

            OutNextLine();

            richTextBox5.Text = TokenList2.DisplayTokens();

            SyntaxAnalyzer SA = new SyntaxAnalyzer(TokenList2);
            
            Do pa = new Do();
            
            
            pa = SA.ABS();

            if (pa.cppp == "Valid Syntax")
            {
                richTextBox4.Text = pa.cppp;
            }
            else
            {
                richTextBox4.Text = pa.cppp + "\n" + "Syntax Error on Line"+pa.cpline;
            }

            int i = 1;
            while(i<100)
            {
                richTextBox6.Text += i+"\n";
                i++;
            }
        

            //StreamWriter streamWriter = new StreamWriter(@"C:\Users\Mahad Khan\source\repos\BreakWord\BreakWord\TokensFile.txt");
            //streamWriter.WriteLine(richTextBox3.Text);
            //streamWriter.Close();

            BreakWordList = null;
            TokenList = null;
            TokenList2 = null;
            richTextBox2.Text = SA.TypeDef.Dis();
            richTextBox3.Text = SA.FuncTable.DisplayTokens();
            richTextBox7.Text = SA.RetTE();
            


                

        }

  

            
        

      
    }

}