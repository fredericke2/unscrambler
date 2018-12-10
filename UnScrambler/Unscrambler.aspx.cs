using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace UnScrambler
{
    public partial class Unscrambler : System.Web.UI.Page
    {
        string[] logFile;
        List<string> logList;
        List<string> dictionary;
        List<string> wordsfound;
        List<string> wordssearched;
        string[] logFileresorted;
        List<words> logListresortedt;
        protected void Page_Load(object sender, EventArgs e)
        {
            logFile = File.ReadAllLines(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "sortedlist.txt");
            logList = new List<string>(logFile);
            logFileresorted = File.ReadAllLines(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "resortedlist.txt");
            logListresortedt = new List<words>();

            wordssearched = new List<string>();
            int i = 0;
            foreach (string j in logFileresorted)
            {
                logListresortedt.Add(new words(j, i));
                i++;
            }
        }

        protected void btn_unscramble_Click(object sender, EventArgs e)
        {
            int N = myTextletters.Text.Length;
            string input = myTextletters.Text;
            int[] nopts = new int[N + 2];
            int[,] option = new int[N + 2, N + 2];
            int i, candidate;
            int move = 0;
            int start = 0;
            string expectedoutput = "";
            wordsfound = new List<string>();
            nopts[start] = 1;
            for (i=0;i< Convert.ToInt32(wordlen.Text); i++)
            {
                string val = ((TextBox)rpt_container.Items[i].FindControl("letter")).Text;
                if (val == "")
                {
                    expectedoutput += "_";
                }
                else
                {
                    expectedoutput += ((TextBox)rpt_container.Items[i].FindControl("letter")).Text;
                }
            }
            dictionary = logList.Where(l => l.Length == expectedoutput.Length).ToList();
            logListresortedt = logListresortedt.Where(l => l.word.Length == expectedoutput.Length).ToList();
            while (nopts[start] > 0)
            {
                if (expectedoutput.Length == myTextletters.Text.Length)
                {
                    string word2 = "";
                    word2 = myTextletters.Text;
                    if (word2.Length == expectedoutput.Length)
                    {
                        
                        string wordsorted = String.Concat(word2.OrderBy(c => char.ToLower(c)));
                        if (!wordssearched.Any(w => w == wordsorted))
                        {
                            searchAllItem(wordsorted, expectedoutput);
                            wordssearched.Add(wordsorted);
                        }
                    }

                    nopts[start] = 0;
                    break;
                }
                if (nopts[move] > 0)
                {
                    nopts[++move] = 0;
                    string word = "";
                    for (i=1; i<move;i++)
                    {
                        word += input[option[i, nopts[i]] - 1];
                    }
                    if (word != "")
                    {
                        if (word.Length == expectedoutput.Length)
                        {
                            string wordsorted = String.Concat(word.OrderBy(c => char.ToLower(c)));
                            if (!wordssearched.Any(w => w == wordsorted)) {
                                searchAllItem(wordsorted, expectedoutput);
                                wordssearched.Add(wordsorted);
                            }
                        }
                    }

                    for (candidate = N; candidate >= 1; candidate--)
                    {
                        for (i = move - 1; i >= 1; i--)
                        {
                            if (option[i,nopts[i]] >= candidate) break;
                        }
                        if (!(i >= 1))
                            option[move,++nopts[move]] = candidate;
                    }

                }
                else
                {
                    nopts[--move]--;
                }
            }
            string[] rpt1 = new string[(wordsfound.Count / 3) + (wordsfound.Count % 3 > 0? 1: 0)];
            string[] rpt2 = new string[(wordsfound.Count / 3) + (wordsfound.Count % 3 > 1 ? 1 : 0)];
            string[] rpt3 = new string[(wordsfound.Count / 3)];
            int counter1, counter2, counter3, counter;
            counter1 = counter2 = counter3 = counter = 0;

            rpt_found_col1.DataSource = rpt1;
            rpt_found_col1.DataBind();
            rpt_found_col2.DataSource = rpt2;
            rpt_found_col2.DataBind();
            rpt_found_col3.DataSource = rpt3;
            rpt_found_col3.DataBind();
            foreach (string item in wordsfound)
            {
                if (counter%3 == 0)
                {
                    ((Label)rpt_found_col1.Items[counter1].FindControl("lbl_found")).Text = item;
                    counter1++;
                }
                else if (counter % 3 == 1)
                {
                    ((Label)rpt_found_col2.Items[counter2].FindControl("lbl_found")).Text = item;
                    counter2++;
                }
                else if (counter % 3 == 2)
                {
                    ((Label)rpt_found_col3.Items[counter3].FindControl("lbl_found")).Text = item;
                    counter3++;
                }
                counter++;
            }
        }

        public void searchAllItem(string word, string expectedoutput)
        {
            expectedoutput = expectedoutput.Replace("_", ".");
            List<words> newList = logListresortedt.FindAll(l => l.word.ToLower() == word.ToLower());
            foreach(words item in newList) {
                Match m = Regex.Match(logFile[item.index], expectedoutput, RegexOptions.IgnoreCase);
                if (m.Success) { 
                    if (!wordsfound.Any(w => w.ToLower() == logFile[item.index].ToLower()))
                        wordsfound.Add(logFile[item.index]);
                }
            }

        }

        protected void wordlen_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(wordlen.Text, out value))
            {
                if (value > myTextletters.Text.Length)
                {
                    invalidlengthvalidation.Visible = true;
                    rpt_container.DataSource = null;
                    rpt_container.DataBind();
                }
                else
                {
                    invalidlengthvalidation.Visible = false;
                    string[] child = new string[value];
                    rpt_container.DataSource = child;
                    rpt_container.DataBind();
                }
            }
            else
            {
                invalidlengthvalidation.Visible = true;
                rpt_container.DataSource = null;
                rpt_container.DataBind();
            }
        }
    }
}