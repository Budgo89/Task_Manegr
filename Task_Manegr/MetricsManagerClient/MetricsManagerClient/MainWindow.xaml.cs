using MetricsManagerClient.Agents.Interfaces;
using MetricsManagerClient.Agents.Repository;
using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();
            _agenrsRepository = new AgentsRepository();
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
            foreach (char c in IdClientText.Text)
            {
                if (!char.IsDigit(c)) { isNumber = false; break; }
                else
                    isNumber = true;
            }
            return isNumber;
        }


    }
}
