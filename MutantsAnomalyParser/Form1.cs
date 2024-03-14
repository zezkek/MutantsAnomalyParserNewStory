using MutantsAnomalyParser.Models;
using Newtonsoft.Json;

namespace MutantsAnomalyParser
{
    public partial class Form1 : Form
    {
        static DZEModel dZEModel;
        static string outputAnomalies;
        static string outputMutants;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Файлы мапинга (*.dze)|*.dze";
            openFileDialog1.DefaultExt = "dze";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "Роуг лох";
            saveFileDialog1.Filter = "Файлы аномалий (*.anomaly)|*.anomaly|Файлы мутантов (*.spawn)|*.spawn";
            saveFileDialog1.DefaultExt = "spawn";
            saveFileDialog1.AddExtension = true;
            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                    return;
                Stream stream = openFileDialog1.OpenFile();
                using StreamReader reader = new(stream);
                string? str = reader.ReadToEnd();
                dZEModel = JsonConvert.DeserializeObject<DZEModel>(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            outputAnomalies = string.Empty;
            outputMutants = string.Empty;
            int anomalies = 0;
            int mutants = 0;
            for (int i = 0; i < dZEModel.EditorObjects.Count; i++)
            {
                List<string> parsedName = dZEModel.EditorObjects[i].DisplayName.Split('=').ToList();
                if (parsedName.Count == 5)
                {
                    outputAnomalies += string.Join("|", parsedName[0], dZEModel.EditorObjects[i].Position[0].ToString() + " " + dZEModel.EditorObjects[i].Position[1].ToString() + " " + dZEModel.EditorObjects[i].Position[2].ToString(), parsedName[1], parsedName[2], parsedName[3], dZEModel.EditorObjects[i].Type, parsedName[4] + "\n");
                    anomalies++;
                }
                else if (parsedName.Count == 6)
                {
                    outputMutants += string.Join("|", parsedName[0], dZEModel.EditorObjects[i].Type, dZEModel.EditorObjects[i].Position[0].ToString() + " " + dZEModel.EditorObjects[i].Position[1].ToString() + " " + dZEModel.EditorObjects[i].Position[2].ToString(), parsedName[1], parsedName[2], parsedName[3], parsedName[4], parsedName[5] + "\n");
                    mutants++;
                }
                else
                MessageBox.Show($"Строка {i+1} не обработана");
            }
            MessageBox.Show($"Кол-во обработанных аномалий - {anomalies}, мутантов - {mutants}");
            if (mutants + anomalies == 0)
                return;

            if(anomalies != 0)
            {
                saveFileDialog1.Filter = "Файлы аномалий (*.anomaly)|*.anomaly";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                File.WriteAllText(saveFileDialog1.FileName, outputAnomalies);
            }
            if(mutants!= 0)
            {
                saveFileDialog1.Filter = "Файлы мутантов (*.spawn)|*.spawn";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                File.WriteAllText(saveFileDialog1.FileName, outputMutants);
            }
            MessageBox.Show($"Файлы сохранёны");
        }
    }
}
