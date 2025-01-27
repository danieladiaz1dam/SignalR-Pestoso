using Microsoft.AspNetCore.SignalR.Client;
using MAUI.Models.Utils;
using System.Collections.ObjectModel;
using ENT;

namespace MAUI.Models
{
    public class MainPageVM : VMBase
    {
        private HubConnection _connection;
        private bool _canSend;
        private bool _canConnect;
        private DelegateCommand? _btnConectar, _btnDesconectar, _btnEnviar;
        private ObservableCollection<Msg> _mensajes = new();

        public bool CanSend { get => _canSend; }
        public bool CanConnect { get => _canConnect; }
        public DelegateCommand? BtnConectar => _btnConectar;
        public DelegateCommand? BtnDesconectar => _btnDesconectar;
        public DelegateCommand? BtnEnviar => _btnEnviar;
        public ObservableCollection<Msg> Mensajes
        {
            get { return _mensajes; }
        }

        public string entryUsuario { get; set; } = "";
        public string entryGrupo { get; set; } = "";
        public string entryMensaje { get; set; } = "";

        public MainPageVM()
        {
            _canSend = false;
            _canConnect = true;
            _connection = new HubConnectionBuilder()
                .WithUrl($"http://192.168.1.4:8001/chatHub")
                .Build();

            initBtns();
            _connection.On<Msg>("ReceiveMessage", addMensaje);
        }

        private void addMensaje(Msg m)
        {
            MainThread.BeginInvokeOnMainThread(() => _mensajes.Add(m));
        }

        #region Botones

        private void initBtns()
        {
            _btnConectar = new DelegateCommand(_btnConectarCmd);
            _btnDesconectar = new DelegateCommand(_btnDesconectarCmd);
            _btnEnviar = new DelegateCommand(_btnEnviarCmd);
        }

        private async void _btnEnviarCmd()
        {
            Msg m = new Msg(entryUsuario, entryMensaje, entryGrupo);
            await _connection.InvokeAsync("SendMessage", m);
            addMensaje(m);
        }
        private async void _btnConectarCmd()
        {
            await _connection.StartAsync();

            _canSend = true;
            _canConnect = false;
            NotifyPropertyChanged(nameof(CanSend));
            NotifyPropertyChanged(nameof(CanConnect));
        }

        private async void _btnDesconectarCmd()
        {
            await _connection.StopAsync();

            _canSend = false;
            _canConnect = true;
            NotifyPropertyChanged(nameof(CanSend));
            NotifyPropertyChanged(nameof(CanConnect));
        }

        #endregion
    }
}
