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
        private ObservableCollection<Msg> _mensajes;

        public bool CanSend { get => _canSend; }
        public bool CanConnect { get => _canConnect; }
        public DelegateCommand? BtnConectar => _btnConectar;
        public DelegateCommand? BtnDesconectar => _btnDesconectar;
        public DelegateCommand? BtnEnviar => _btnEnviar;
        public ObservableCollection<Msg> Mensajes => _mensajes;

        public MainPageVM()
        {
            _canSend = false;
            _canConnect = true;
            _connection = new HubConnectionBuilder()
                .WithUrl($"http://localhost:5177/chatHub")
                .Build();

            initBtns();
            _mensajes = new();

            _connection.On<Msg>("ReceiveMessage", addMensaje);

            Task.Run(() =>
            {
                Dispatcher.Dispatch(async () => await _connection.StartAsync());
            });
        }

        private void addMensaje(Msg m)
        {

        }

        async Task Connect()
        {
            await _connection.StartAsync();
        }

        async Task Disconnect()
        {
            await _connection.StopAsync();
        }

        #region Botones

        private void initBtns()
        {
            _btnConectar = new DelegateCommand(_btnConectarCmd);
            _btnDesconectar = new DelegateCommand(_btnDesconectarCmd);
        }

        private void _btnConectarCmd()
        {
            _canSend = true;
            _canConnect = false;
            NotifyPropertyChanged(nameof(CanSend));
            NotifyPropertyChanged(nameof(CanConnect));
        }

        private void _btnDesconectarCmd()
        {
            _canSend = false;
            _canConnect = true;
            NotifyPropertyChanged(nameof(CanSend));
            NotifyPropertyChanged(nameof(CanConnect));
        }

        #endregion
    }
}
