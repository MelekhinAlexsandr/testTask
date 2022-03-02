namespace test_file
{
    public partial class Form1 : Form
    {
        public string path;
        public string fileName;
        public List<String> file_text = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                fileName = openFileDialog.FileName;
            path = Path.GetDirectoryName(fileName);
            using (StreamReader sr = new StreamReader(fileName))
            {
                richTextBox1.Text = sr.ReadToEnd();
            }
            fileName = Path.GetFileNameWithoutExtension(fileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(path + @"\" + fileName + ".txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    file_text.Add(line);
                }
            }
            fileName = path + @"\" + fileName + "_new.txt";
            using (StreamWriter fs = File.CreateText(fileName))
            {
                fs.Write(richTextBox1.Text);
            }
 
            //только изменнная строка
            var result = richTextBox1.Lines.Except(file_text);
            var exept = file_text.Except(richTextBox1.Lines);

            int k = 0;
            int j = 0;
            int n = 0;

            for (int i = 0; i <= file_text.Count - 1; i++)
            {
                if (exept.Contains(file_text[i]))
                {
                    if (richTextBox1.Lines[k]!=result.ElementAt(n))
                    {
                        richTextBox2.SelectionColor = Color.Red;
                        richTextBox2.AppendText(file_text[i] + "\n");
                        j++;
                        if (richTextBox1.Lines[j] == result.ElementAt(n))
                        {
                            richTextBox2.SelectionColor = Color.Yellow;
                            richTextBox2.AppendText(result.ElementAt(n) + "\n");
                            n++;
                        }
                        else if(richTextBox1.Lines[j-1] == result.ElementAt(n))
                        {
                            richTextBox2.SelectionColor = Color.Yellow;
                            richTextBox2.AppendText(result.ElementAt(n) + "\n");
                            n++;
                        }
                    }
                    k++;
                    j = j+k;
                }
                else
                {
                    richTextBox2.SelectionColor = Color.Green;
                    richTextBox2.AppendText(file_text[i]+"\n");
                }
            }
        }
    }
}