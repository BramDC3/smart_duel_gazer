using Code.Core.Storage.DuelRoom;
using Zenject;

namespace Code.Core.DataManager.DuelRoom
{
    public interface IDuelRoomDataManager
    {
        SmartDuelServer.Interface.Entities.EventData.RoomEvent.DuelRoom GetDuelRoom();
        void SaveDuelRoom(SmartDuelServer.Interface.Entities.EventData.RoomEvent.DuelRoom room);
    }

    public class DuelRoomDataManager : IDuelRoomDataManager
    {
        private readonly IDuelRoomStorageProvider _duelRoomStorageProvider;

        [Inject]
        public DuelRoomDataManager(
            IDuelRoomStorageProvider duelRoomStorageProvider)
        {
            _duelRoomStorageProvider = duelRoomStorageProvider;
        }

        public SmartDuelServer.Interface.Entities.EventData.RoomEvent.DuelRoom GetDuelRoom()
        {
            return _duelRoomStorageProvider.GetDuelRoom();
        }

        public void SaveDuelRoom(SmartDuelServer.Interface.Entities.EventData.RoomEvent.DuelRoom room)
        {
            _duelRoomStorageProvider.SaveDuelRoom(room);
        }
    }
}