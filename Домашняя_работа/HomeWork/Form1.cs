using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SolverFoundation.Services;

namespace HomeWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] names = {"Алюминий","Медь","Олово","Цинк","Свинец" };
            int[] prod1 = {10,20,15,30,20};
            int[] prod2 = {70,50,35,40,45};
            int[] volume = {570,420,300,600,400};
            dataGridView1.Rows.Add(); dataGridView1.Rows.Add();
            dataGridView1.Rows.Add(); dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            for (int i = 0; i <= 4; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = names[i];
                dataGridView1.Rows[i].Cells[1].Value = prod1[i];
                dataGridView1.Rows[i].Cells[2].Value = prod2[i];
                dataGridView1.Rows[i].Cells[3].Value = volume[i];
            }
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] names = { "Алюминий", "Медь", "Олово", "Цинк", "Свинец" };
            int[] prod1 = { 10, 20, 15, 30, 20 };
            int[] prod2 = { 70, 50, 35, 40, 45 };
            int[] volume = { 570, 420, 300, 600, 400 };
            dataGridView1.Rows.Add(); dataGridView1.Rows.Add();
            dataGridView1.Rows.Add(); dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            for (int i = 0; i <= 4; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = names[i];
                dataGridView1.Rows[i].Cells[1].Value = prod1[i];
                dataGridView1.Rows[i].Cells[2].Value = prod2[i];
                dataGridView1.Rows[i].Cells[3].Value = volume[i];
            }
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();
            Decision x01 = new Decision(Domain.IntegerNonnegative, "product1");
            Decision x02 = new Decision(Domain.IntegerNonnegative, "product2");
            model.AddDecisions(x01, x02);
            model.AddConstraints("production",
               0.4 * x01 - 0.6 * x02 >= 0,
               x01 * prod1[0] + x02 * prod2[0] <= volume[0],
                x01 * prod1[1] + x02 * prod2[1] <= volume[1],
                x01 * prod1[2] + x02 * prod2[2] <= volume[2],
                x01 * prod1[3] + x02 * prod2[3] <= volume[3],
                x01 * prod1[4] + x02 * prod2[4] <= volume[4]
            );
            {
                model.AddGoal("cost", GoalKind.Maximize, 30 * x01 + 80 * x02);
                Solution solution = context.Solve(new SimplexDirective());
                Report report = solution.GetReport();
                x1.Text = x01.ToDouble().ToString();
                x2.Text = x02.ToDouble().ToString();
                cost.Text = model.Goals.First().ToDouble().ToString();
                res.Text += report;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
