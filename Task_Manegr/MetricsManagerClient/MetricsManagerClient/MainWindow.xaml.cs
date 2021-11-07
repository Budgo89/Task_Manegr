using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerClient.Agents.Interfaces;
using MetricsManagerClient.Agents.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetricsManagerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAgentsRepository _agenrsRepository;
        private IMetricsRepository _metricsRepository;

        public MainWindow()
        {
            InitializeComponent();
            _agenrsRepository = new AgentsRepository();
            _metricsRepository = new MetricsRepository();

        }

        private void ListAgent_Click(object sender, RoutedEventArgs e)
        {
            var agent = _agenrsRepository.ReceivingAgentById();
            string ListAgentText = "";
            for (int i = 0; i < agent.agent.Count; i++)
            {
                ListAgentText = $"{ListAgentText} ID Агента: {agent.agent[i].AgentId.ToString()} \nUrl Агента: {agent.agent[i].AgentUrl.ToString()} \nEnabled: {agent.agent[i].Enabled.ToString()} \n\n";
            }
            ListAgent.Text = ListAgentText;
        }

        private void Enable_Click(object sender, RoutedEventArgs e)
        {            
            if (IsNumber(IdClientText.Text))
            {
                _agenrsRepository.EnableAgent(IdClientText.Text);
            }
            else IdClientText.Text = "Введите корректный ID";
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumber(IdClientText.Text))
            {
                _agenrsRepository.DisableAgent(IdClientText.Text);
            }
            else IdClientText.Text = "Введите корректный ID";
        }

        private bool IsNumber(string Number)
        {
            bool isNumber = false;
            foreach (char c in Number)
            {
                if (!char.IsDigit(c)) { isNumber = false; break; }
                else
                    isNumber = true;
            }
            return isNumber;
        }
        private bool CheckIsNumberIsDate()
        {
            if (IsNumber(IdAgentaMetric.Text) == false || TextIsDate(fromTime.Text) == false || TextIsDate(toTime.Text) == false)
            {
                if (IsNumber(IdAgentaMetric.Text) == false)
                {
                    IdAgentaMetric.Text = "Введите корректный ID";
                }
                if (TextIsDate(fromTime.Text) == false)
                {
                    fromTime.Text = "yyyy-MM-ddTHH:mm:ssZ";
                }
                if (TextIsDate(toTime.Text) == false)
                {
                    toTime.Text = "yyyy-MM-ddTHH:mm:ssZ";
                }
                return false;
            }
            else return true;
        }
        private bool CheckIsDate()
        {
            if (TextIsDate(fromTime.Text) == false || TextIsDate(toTime.Text) == false)
            {
                if (TextIsDate(fromTime.Text) == false)
                {
                    fromTime.Text = "yyyy-MM-ddTHH:mm:ssZ";
                }
                if (TextIsDate(toTime.Text) == false)
                {
                    toTime.Text = "yyyy-MM-ddTHH:mm:ssZ";
                }
                return false;
            }
            else return true;
        }
        private void CpuMetrics_Click(object sender, RoutedEventArgs e)
        {

            if(CheckIsNumberIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.CpuMetricLoading(IdAgentaMetric.Text, fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }            
            }
        }

        static bool TextIsDate(string text)
        {
            var dateFormat = "yyyy-MM-ddTHH:mm:ssZ";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

        

        private void CpuMetricsAll_Click(object sender, RoutedEventArgs e)
        {

            if(CheckIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.CpuMetricLoadingAll(fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void DotNetMetrics_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsNumberIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.DotNetMetricLoading(IdAgentaMetric.Text, fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void DotNetMetricsAll_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.DotNetMetricLoadingAll(fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }
   

        private void HddMetrics_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsNumberIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.HddMetricLoading(IdAgentaMetric.Text, fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void HddMetricsAll_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.HddMetricLoadingAll(fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void NetworkMetrics_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsNumberIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.NetworkMetricLoading(IdAgentaMetric.Text, fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void NetworkMetricsAll_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.NetworkMetricLoadingAll(fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void RamMetrics_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsNumberIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.RamkMetricLoading(IdAgentaMetric.Text, fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void RamMetricsAll_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsDate())
            {
                CpuChart.ColumnSeriesValues[0].Values.Clear();
                var cpuMetrics = _metricsRepository.RamMetricLoadingAll(fromTime.Text, toTime.Text);
                var listMetrics = new List<double>();
                foreach (var item in cpuMetrics.Metrics)
                {
                    listMetrics.Add(item.Value);
                }
                for (int i = 0; i < listMetrics.Count; i++)
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add(listMetrics[i]);
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumber(IdClientText.Text))
            {
                _agenrsRepository.RegisterAgent(IdClientText.Text, Url.Text);
            }
            else IdClientText.Text = "Введите корректный ID";
        }
    }
}
