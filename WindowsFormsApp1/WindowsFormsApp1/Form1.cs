using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double Lagrang(double[] x, double x0, double[] y, int k)
        {
          
                double[] l = new double[5];
            double basicsPol = 1;
            double lagrangePol = 0;
           
            for (int i = 0; i < k; i++)
            {
                basicsPol = 1;
                for (int j = 0; j < k; j++)
                {
                    if (i != j)
                    {
                        basicsPol *= (x0 - x[j]) / (x[i] - x[j]);
                    }
                }
                lagrangePol += basicsPol * y[i];
            }
            return lagrangePol;
        }
        int Factor(int i)
        {
            int Fact = 1;
            if (i == 0 || i == 1)
            {
                Fact = 1;
            }
            else
            {
                for (int j = 2; j <= i; j++)
                {
                    Fact *= j;
                }
            }
            return Fact;
        }

        double Newton(double[] x, double x0, double[] y, int k)
        {

            double[,] matrix = new double[k, k];
            double basicsPol = 1;
            double NewtonPol = 0;
            double[] residualI = new double[k];
            fTest = 0;
            double error = 0;
            for (int i = 0; i < k; i++)
            {
                matrix[i, 0] = y[i];
            }
            for (int i = 0; i < k; i++)
            {
                residualI[i] = xTest - x[i];
            }
            for (int j = 1; j < k; j++)
            {
                for (int i = 0; i < k - j; i++)
                {
                    matrix[i, j] = matrix[i + 1, j - 1] - matrix[i, j - 1];

                }

            }
            for (int j = 0; j < k; j++)
            {

                for (int i = 0; i < k; i++)
                {
                    dataGridView2.Rows[i].Cells[j + 1].Value = matrix[i, j];
                }
            }

            for (int i = 0; i < k; i++)
            {
                basicsPol = 1;
                for (int j = 0; j < i; j++)
                {
                    basicsPol *= (x0 - x[j]);
                }

                NewtonPol += basicsPol * matrix[0, i] / (Factor(i) * Math.Pow(h, i));
            }
            for (int i = 0; i < k; i++)
            {
                basicsPol = 1;
                for (int j = 0; j < i; j++)
                {
                    basicsPol *= (xTest - x[j]);
                }

                fTest += basicsPol * matrix[0, i] / (Factor(i) * Math.Pow(h, i));
            }
            label11.Text = Convert.ToString(Math.Round(fTest, 5));
            double t = 1;
            for (int i = 0; i < k; i++)
            {
                t *= residualI[i];
            }

            error = Math.Round(Math.Abs(matrix[0, k - 1] / Math.Pow(h, k) / Factor(k) * t), 12);

            label12.Text = Convert.ToString(error);
            if (error < 0.002)
            {
                label14.Text = "Accuracy  achieved";

            }
            else
            { label14.Text = "Accuracy not  achieved"; }
            return NewtonPol;
        }

       
        int CountRows(double a, double b, double h)
        {
            int c = 0;
            
                while (a <= b)
                {
                a = a + h;
                c++;
                }
            
            return c;
        }
        public void FillBasic()
        {
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[2].Points.Clear();
            dataGridView1.Refresh();
            a = Convert.ToDouble(textBox1.Text);
            double c = a;
            b = Convert.ToDouble(textBox2.Text);
            h = Convert.ToDouble(textBox3.Text);
            xTest = Convert.ToDouble(textBox5.Text);
            n = CountRows(a, b, h);
            x = new double[n];
            y = new double[n];
           dataGridView1.RowCount = CountRows(a, b, h);
            dataGridView1.ColumnCount = 3;
            dataGridView2.RowCount = CountRows(a, b, h);
            dataGridView2.ColumnCount = n + 3;
            for (int i = 0; i < n; i++)
            {

                x[i] = c;
                y[i] = Math.Round(Math.Pow(3, (4 * Math.Pow(x[i], 4) + 5)), 4);
                if (c <= b)
                {
                    c += h;
                }
                chart1.Series[3].Points.AddXY(x[i], y[i]);

            }
            for (int i = 0; i < n; i++)
            {

                dataGridView1.Rows[i].Cells[1].Value = y[i];
                dataGridView1.Rows[i].Cells[0].Value = x[i];
                dataGridView2.Rows[i].Cells[0].Value = x[i];
                dataGridView2.Rows[i].Cells[1].Value = y[i];
            }


        }
       
        double a ;
        double b ;
        double h ;
        int n;
        double[] x;
        double[] y;
        double xTest, fTest;

        private void button1_Click(object sender, EventArgs e)
        {
            
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[2].Points.Clear();
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value = Lagrang(x, x[i], y,n);
                chart1.Series[0].Points.AddXY(x[i], Lagrang(x, x[i], y,n));
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[2].Points.Clear();
            for (int i = 0; i < n; i++)
            {
                dataGridView2.Rows[i].Cells[n+1].Value = Newton(x, x[i], y,n);
                chart1.Series[1].Points.AddXY(x[i], Newton(x, x[i], y, n));

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillBasic();
        }





       




    }
}
