using SmsService2.TwilioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SmsService2
{
    public partial class SmsService : ServiceBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private const int IntervalInMinutes = 2;
        private Timer _timer = new Timer(IntervalInMinutes * 60000);
        private TwilioMessage _twilioMessage = new TwilioMessage();

        public SmsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += DoWork;
            _timer.Start();
            Logger.Info("Service started...");
        }

        private void DoWork(object sender, ElapsedEventArgs e)
        {
            try 
            {
                SendMessage();
            }
            catch(Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void SendMessage()
        {
            _twilioMessage.SendMessage();
            Logger.Info("Message sent...");
        }

        protected override void OnStop()
        {
            _timer.Stop();
            Logger.Info("Service stopped...");
        }
    }
}
