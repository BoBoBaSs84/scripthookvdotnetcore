//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

namespace GTA
{
    public sealed class VehicleWindowCollection
    {
        #region Fields

        readonly Vehicle _owner;
        readonly Dictionary<VehicleWindowIndex, VehicleWindow> _vehicleWindows = new();

        #endregion

        internal VehicleWindowCollection(Vehicle owner)
        {
            _owner = owner;
        }

        public VehicleWindow this[VehicleWindowIndex index]
        {
            get
            {
                if (!_vehicleWindows.TryGetValue(index, out VehicleWindow vehicleWindow))
                {
                    vehicleWindow = new VehicleWindow(_owner, index);
                    _vehicleWindows.Add(index, vehicleWindow);
                }

                return vehicleWindow;
            }
        }

        public bool AllWindowsIntact => Call<bool>(Hash.ARE_ALL_VEHICLE_WINDOWS_INTACT, _owner.Handle);

        public void RollDownAllWindows()
        {
            Call(Hash.ROLL_DOWN_WINDOWS, _owner.Handle);
        }
    }
}