using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaAmericana
{
    public partial class Form1 : Form
    {
        private string file = "";



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpPeriodo.CustomFormat = "MMyyyy";
            cboProceso.SelectedIndex = 0;
        }

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            string proceso = "AM";
            string RNC = "101009918";
            DateTime fechaActual = DateTime.Now;
            DateTime periodo = dtpPeriodo.Value;

            file = $"{proceso}_{RNC}_{periodo}.txt";

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            HttpClient client = new HttpClient();

            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            client.BaseAddress = new Uri("https://localhost:44341/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var header = new HeaderDTO() { Proceso = proceso, RNC = Int32.Parse(RNC), PeriodoAutodeterminacion = DateTime.Now, FechaTransmision = periodo };
            var response = await client.PostAsJsonAsync("Headers", header);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Header>(data);
            var idHeader = result.IdHeader;

            SqlConnection conn = Conexion.getConnection();
            SqlDataReader reader;

            string query = "SELECT Tipo_Identificacion, Identificacion, Salario, Moneda FROM Nomina";

            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var detail = new Detail() { TipoIdentificacion = reader.GetValue(0).ToString(), Identificacion = reader.GetValue(1).ToString(), Salario = decimal.Parse(reader.GetValue(2).ToString()), Moneda = reader.GetValue(3).ToString(), IdHeader = idHeader };
                var responseDetail = await client.PostAsJsonAsync("Detail", detail);
                Console.WriteLine(responseDetail);
            }

            reader.Close();
            cmd.Dispose();
            conn.Close();

            DialogResult dResult = MessageBox.Show($"Se ha enviado correctamente la data");

            try
            {



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer los datos de la base de datos.");
            }
        }
        private void writeFileLine(string pLine)
        {
            using (StreamWriter w = File.AppendText(file))
            {
                w.WriteLine(pLine);
            }
        }
    }
}
